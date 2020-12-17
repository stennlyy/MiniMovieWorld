namespace MiniMovieWorld.Services.Data.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class ViewComponentsService : IViewComponentsService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public ViewComponentsService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.actorsRepository = actorsRepository;
        }

        public ICollection<MovieViewModel> GetRecentlyAddedMovies()
        {
            var movies = this.moviesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(6)
                .Select(x => new MovieViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    Title = x.Title,
                    Synopsis = x.Synopsis,
                    Duration = x.Duration,
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

            return movies;
        }

        public ICollection<SingleActorViewModel> GetMostRatedActors()
        {
            var actors = this.actorsRepository
                .All()
                .OrderByDescending(x => x.ActorRates.Average(y => y.Rate))
                .ThenBy(x => x.FirstName)
                .Take(6)
                .Select(x => new SingleActorViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserActorRatings = x.ActorRates.Any() ? x.ActorRates.Average(y => y.Rate) : 0,
                })
                .ToList();

            return actors;
        }
    }
}
