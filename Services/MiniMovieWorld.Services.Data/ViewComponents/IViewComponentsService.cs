namespace MiniMovieWorld.Services.Data.ViewComponents
{
    using System.Collections.Generic;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IViewComponentsService
    {
        public ICollection<MovieViewModel> GetRecentlyAddedMovies();

        public ICollection<SingleActorViewModel> GetMostRatedActors();
    }
}
