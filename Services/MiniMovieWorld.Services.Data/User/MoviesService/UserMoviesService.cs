namespace MiniMovieWorld.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;

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

        public ICollection<MovieViewModel> SearchMovieByInput(string input)
        {
            var movie = this.moviesRepository
                .All()
                .Where(x => x.Title.Contains(input.Trim()))
                .Select(x => new MovieViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Duration = x.Duration,
                    UserRates = x.Ratings.Any() ? x.Ratings.Average(y => y.Rate) : 0,
                    Year = x.Year,
                    Actors = x.Actors.Select(y => new ActorsViewModel
                    {
                        Id = y.ActorId,
                        FirstName = y.Actor.FirstName,
                        MiddleName = y.Actor.MiddleName,
                        LastName = y.Actor.LastName,
                    }).ToList(),
                    Producers = x.Producers.Select(y => new ProducersViewModel
                    {
                        Id = y.ProducerId,
                        FirstName = y.Producer.FirstName,
                        MiddleName = y.Producer.MiddleName,
                        LastName = y.Producer.LastName,
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
                })
                .ToList();

            return movie;
        }

        public MovieViewModel GetMovie(int id)
        {
            var movie = this.moviesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MovieViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Duration = x.Duration,
                    Image = x.Image,
                    UserRates = x.Ratings.Any() ? x.Ratings.Average(y => y.Rate) : 0,
                    Year = x.Year,
                    Actors = x.Actors.Select(y => new ActorsViewModel
                    {
                        Id = y.Actor.Id,
                        FirstName = y.Actor.FirstName,
                        MiddleName = y.Actor.MiddleName,
                        LastName = y.Actor.LastName,
                    }).ToList(),
                    Producers = x.Producers.Select(y => new ProducersViewModel
                    {
                        Id = y.Producer.Id,
                        FirstName = y.Producer.FirstName,
                        MiddleName = y.Producer.MiddleName,
                        LastName = y.Producer.LastName,
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

        public ICollection<MovieViewModel> GetAllMovies(int page, int itemsPerPage)
        {
            var movies = this.moviesRepository
            .All()
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .Select(x => new MovieViewModel
            {
                Id = x.Id,
                Image = x.Image,
                Title = x.Title,
                Synopsis = x.Synopsis,
                Duration = x.Duration,
                UserRates = x.Ratings.Any() ? x.Ratings.Average(y => y.Rate) : 0,
                Year = x.Year,
                Actors = x.Actors.Select(y => new ActorsViewModel
                {
                    Id = y.ActorId,
                    FirstName = y.Actor.FirstName,
                    MiddleName = y.Actor.MiddleName,
                    LastName = y.Actor.LastName,
                }).ToList(),
                Producers = x.Producers.Select(y => new ProducersViewModel
                {
                    Id = y.ProducerId,
                    FirstName = y.Producer.FirstName,
                    MiddleName = y.Producer.MiddleName,
                    LastName = y.Producer.LastName,
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
            ;
            return movies;
        }

        public int GetMoviesCount()
        {
            return this.moviesRepository.All().Count();
        }
    }
}
