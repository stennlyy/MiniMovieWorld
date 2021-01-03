namespace MiniMovieWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Services.Data.User;
    using MiniMovieWorld.Services.Data.User.CommentsService;
    using MiniMovieWorld.Services.Data.User.UsersService;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Comments;
    using MiniMovieWorld.Web.ViewModels.Movies;

    [Authorize]
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IUserMoviesService moviesService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            ICommentsService commentsService,
            IUserMoviesService moviesService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.moviesService = moviesService;
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

        public IActionResult AddComment()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.AddUserCommentToMovie(inputModel.Id, currentUser.Id, inputModel.Comment);

            return this.RedirectToAction("GetMovieById", "Movies", new { id = inputModel.Id });
        }

        public IActionResult DisplayAllCommentsForMovie(int id)
        {
            var movieComments = this.commentsService.GetMovieComments(id);

            var movie = this.moviesService.GetMovie(id);

            var viewModel = new MovieAllCommentsViewModel
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                MovieComments = movieComments,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> FavouriteActors()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var actors = this.usersService.GetUserFavouriteActors(currentUser.Id);

            var viewModel = new AllActorsViewModel
            {
                Actors = actors,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.AddMovieToUserCollectionAsync(id, currentUser);

            return this.RedirectToAction("UserMovieCollection");
        }

        public async Task<IActionResult> AddActorToFavourites(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.AddActorToUserFavourites(id, currentUser);

            return this.RedirectToAction("UserMovieCollection");
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.RemoveMovieFromCollection(id, currentUser.Id);

            return this.RedirectToAction("UserMovieCollection");
        }

        public async Task<IActionResult> RemoveActor(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            await this.usersService.RemoveActor(id, currentUser.Id);

            return this.RedirectToAction("FavouriteActors");
        }
    }
}
