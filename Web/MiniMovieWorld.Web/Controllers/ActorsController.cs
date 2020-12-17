namespace MiniMovieWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Services.Data.User.ActorsService;
    using MiniMovieWorld.Services.Data.User.RatesService;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class ActorsController : BaseController
    {
        private readonly IUserActorsService userActorsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRatesService ratesService;

        public ActorsController(
            IUserActorsService userActorsService,
            UserManager<ApplicationUser> userManager,
            IRatesService ratesService)
        {
            this.userActorsService = userActorsService;
            this.userManager = userManager;
            this.ratesService = ratesService;
        }

        public IActionResult GetActorById(int id)
        {
            var actor = this.userActorsService.GetActorById(id);

            return this.View(actor);
        }

        [Authorize]
        public IActionResult RateActor()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateActor(RateActorInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.ratesService.SetActorRating(inputModel.Id, user.Id, inputModel.Rate);

            return this.RedirectToAction("FavouriteActors", "Users");
        }
    }
}
