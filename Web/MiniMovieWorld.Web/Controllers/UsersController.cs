namespace MiniMovieWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Services.Data.User.UsersService;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public UsersController(UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public async Task<IActionResult> UserMovieCollection()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var movies = this.usersService.GetUserMovieCollection(currentUser.Id);

            var viewModel = new AllMoviesViewModel
            {
                Movies = movies,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.AddMovieToUserCollectionAsync(id, currentUser);

            return this.RedirectToAction("UserMovieCollection");
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.RemoveMovieFromCollection(id, currentUser.Id);

            return this.RedirectToAction("UserMovieCollection");
        }
    }
}
