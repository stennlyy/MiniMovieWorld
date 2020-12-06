namespace MiniMovieWorld.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UserMoviesService : IUserMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public UserMoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public MovieViewModel GetMovie(int id)
        {
            var movie = this.moviesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MovieViewModel
                {
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Duration = x.Duration,
                    Image = x.Image,
                    Actors = x.Actors.Select(y => new ActorsViewModel
                    {
                        Id = y.Actor.Id,
                        FirstName = y.Actor.FirstName,
                        MiddleName = y.Actor.MiddleName,
                        LastName = y.Actor.LastName,
                    }).ToList(),
                    Writers = x.Writers.Select(y => new WritersViewModel
                    {
                        Id = y.Writer.Id,
                        FirstName = y.Writer.FirstName,
                        MiddleName = y.Writer.MiddleName,
                        LastName = y.Writer.LastName,
                    }).ToList(),
                    Directors = x.Directors.Select(y => new DirectorsViewModel
                    {
                        Id = y.Director.Id,
                        FirstName = y.Director.FirstName,
                        MiddleName = y.Director.MiddleName,
                        LastName = y.Director.LastName,
                    }).ToList(),
                    Categories = x.Categories.Select(y => new CategoriesViewModel
                    {
                        CategoryName = y.Category.CategoryName,
                    }).ToList(),
                })
                .FirstOrDefault();

            return movie;
        }

        public IEnumerable<MovieViewModel> GetAllMovies()
        {
            var movies = this.moviesRepository
            .All()
            .Select(x => new MovieViewModel
            {
                Id = x.Id,
                Image = x.Image,
                Title = x.Title,
                Synopsis = x.Synopsis,
                Duration = x.Duration,
                Actors = x.Actors.Select(y => new ActorsViewModel
                {
                    Id = y.ActorId,
                    FirstName = y.Actor.FirstName,
                    MiddleName = y.Actor.MiddleName,
                    LastName = y.Actor.LastName,
                }).ToList(),
                Writers = x.Writers.Select(y => new WritersViewModel
                {
                    Id = y.WriterId,
                    FirstName = y.Writer.FirstName,
                    MiddleName = y.Writer.MiddleName,
                    LastName = y.Writer.LastName,
                }).ToList(),
                Directors = x.Directors.Select(y => new DirectorsViewModel
                {
                    Id = y.DirectorId,
                    FirstName = y.Director.FirstName,
                    MiddleName = y.Director.MiddleName,
                    LastName = y.Director.LastName,
                }).ToList(),
                Categories = x.Categories.Select(y => new CategoriesViewModel
                {
                    CategoryName = y.Category.CategoryName,
                }).ToList(),
            }).ToList();

            return movies;
        }
    }
}
