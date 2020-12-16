namespace MiniMovieWorld.Services.Data.User.UsersService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<UserMovie> userMoviesRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IDeletableEntityRepository<UserActor> userActorsRepository;

        public UsersService(
            IDeletableEntityRepository<UserMovie> userMoviesRepository,
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Actor> actorsRepository,
            IDeletableEntityRepository<UserActor> userActorsRepository)
        {
            this.userMoviesRepository = userMoviesRepository;
            this.moviesRepository = moviesRepository;
            this.actorsRepository = actorsRepository;
            this.userActorsRepository = userActorsRepository;
        }

        public async Task AddActorToUserFavourites(int actorId, ApplicationUser user)
        {
            if (this.userActorsRepository.All().Any(x => x.Actor.Id == actorId && x.User.Id == user.Id))
            {
                return;
            }

            var actor = this.GetActor(actorId);

            var userActor = new UserActor
            {
                User = user,
                Actor = actor,
            };

            await this.userActorsRepository.AddAsync(userActor);
            await this.userActorsRepository.SaveChangesAsync();
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

        public ICollection<MovieViewModel> GetUserMovieCollection(string userId)
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

        public ICollection<SingleActorViewModel> GetUserFavouriteActors(string userId)
        {
            var actors = this.userActorsRepository
                .All()
                .Where(x => x.User.Id == userId)
                .Select(x => new SingleActorViewModel
                {
                    Image = x.Actor.Image,
                    Id = x.Actor.Id,
                    FirstName = x.Actor.FirstName,
                    LastName = x.Actor.LastName,
                    Age = x.Actor.Age,
                })
                .ToList();

            return actors;
        }

        private Movie GetMovie(int movieId)
        {
            var movie = this.moviesRepository
                .All()
                .Where(x => x.Id == movieId)
                .FirstOrDefault();

            return movie;
        }

        private Actor GetActor(int actorId)
        {
            var actor = this.actorsRepository
                .All()
                .Where(x => x.Id == actorId)
                .FirstOrDefault();

            return actor;
        }
    }
}
