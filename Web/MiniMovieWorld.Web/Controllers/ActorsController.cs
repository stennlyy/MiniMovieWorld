namespace MiniMovieWorld.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.User.ActorsService;

    public class ActorsController : BaseController
    {
        private readonly IUserActorsService userActorsService;

        public ActorsController(IUserActorsService userActorsService)
        {
            this.userActorsService = userActorsService;
        }

        public IActionResult GetActorById(int id)
        {
            var actor = this.userActorsService.GetActorById(id);

            return this.View(actor);
        }
    }
}
