namespace MiniMovieWorld.Web.ViewComponents.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class RecentlyAddedMoviesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public RecentlyAddedMoviesViewComponent(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var movies = this.moviesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(5)
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

            var result = new AllMoviesViewModel
            {
                Movies = movies,
            };

            return this.View(result);
        }
    }
}
