namespace MiniMovieWorld.Web.ViewComponents.Movies
{
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.ViewComponents;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class RecentlyAddedMoviesViewComponent : ViewComponent
    {
        private readonly IViewComponentsService viewComponentsService;

        public RecentlyAddedMoviesViewComponent(IViewComponentsService viewComponentsService)
        {
            this.viewComponentsService = viewComponentsService;
        }

        public IViewComponentResult Invoke()
        {
            var movies = this.viewComponentsService.GetRecentlyAddedMovies();

            var result = new AllMoviesViewModel
            {
                Movies = movies,
            };

            return this.View(result);
        }
    }
}
