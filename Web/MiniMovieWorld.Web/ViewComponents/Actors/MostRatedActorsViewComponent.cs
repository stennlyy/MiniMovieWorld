namespace MiniMovieWorld.Web.ViewComponents.Actors
{
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.ViewComponents;
    using MiniMovieWorld.Web.ViewModels.Actors;

    public class MostRatedActorsViewComponent : ViewComponent
    {
        private readonly IViewComponentsService viewComponentsService;

        public MostRatedActorsViewComponent(IViewComponentsService viewComponentsService)
        {
            this.viewComponentsService = viewComponentsService;
        }

        public IViewComponentResult Invoke()
        {
            var actors = this.viewComponentsService.GetMostRatedActors();

            var result = new AllActorsViewModel
            {
                Actors = actors,
            };

            return this.View(result);
        }
    }
}
