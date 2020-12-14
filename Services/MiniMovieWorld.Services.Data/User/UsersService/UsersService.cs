namespace MiniMovieWorld.Services.Data.User.UsersService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<UserMovie> userMoviesRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<UserMovie> userMoviesRepository,
            IDeletableEntityRepository<Movie> moviesRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userMoviesRepository = userMoviesRepository;
            this.moviesRepository = moviesRepository;
            this.userManager = userManager;
        }

        public async Task AddMovieToUserCollectionAsync(int movieId, ApplicationUser user)
        {
            if (this.userMoviesRepository.All().Any(x => x.Movie.Id == movieId && x.User.Id == user.Id))
            {
                return;
            }

            var movie = this.GetMovie(movieId);

            var userMovie = new UserMovie
            {
                User = user,
                Movie = movie,
            };

            await this.userMoviesRepository.AddAsync(userMovie);
            await this.userMoviesRepository.SaveChangesAsync();
        }

        public async Task RemoveMovieFromCollection(int movieId, string userId)
        {
            var userMovie = this.userMoviesRepository.All().FirstOrDefault(x => x.Movie.Id == movieId && x.User.Id == userId);

            if (userMovie == null)
            {
                return;
            }

            this.userMoviesRepository.HardDelete(userMovie);
            await this.userMoviesRepository.SaveChangesAsync();
        }

        public IEnumerable<MovieViewModel> GetUserMovieCollection(string userId)
        {
            var movies = this.userMoviesRepository
                .All()
                .Where(x => x.User.Id == userId)
                .Select(x => new MovieViewModel
                {
                    Id = x.Movie.Id,
                    Image = x.Movie.Image,
                    Title = x.Movie.Title,
                    Duration = x.Movie.Duration,
                    Categories = x.Movie.Categories.Select(y => new CategoriesViewModel
                    {
                        CategoryName = y.Category.CategoryName,
                    }).ToList(),
                })
                .ToList();

            return movies;
        }

        private Movie GetMovie(int movieId)
        {
            var movie = this.moviesRepository
                .All()
                .Where(x => x.Id == movieId)
                .FirstOrDefault();

            return movie;
        }
    }
}
