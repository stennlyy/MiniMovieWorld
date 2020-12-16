namespace MiniMovieWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Services.Data.User;
    using MiniMovieWorld.Services.Data.User.RatesService;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class MoviesController : BaseController
    {
        private readonly IUserMoviesService userMoviesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRatesService ratesService;

        public MoviesController(
            IUserMoviesService userMoviesService,
            UserManager<ApplicationUser> userManager,
            IRatesService ratesService)
        {
            this.userMoviesService = userMoviesService;
            this.userManager = userManager;
            this.ratesService = ratesService;
        }

        public IActionResult GetMovieById(int id)
        {
            var movie = this.userMoviesService.GetMovie(id);

            return this.View(movie);
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 6;

            var movies = this.userMoviesService.GetAllMovies(id, ItemsPerPage);

            var movieViewModel = new AllMoviesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                MoviesCount = this.userMoviesService.GetMoviesCount(),
                Movies = movies,
            };

            return this.View(movieViewModel);
        }

        [HttpGet]
        public IActionResult FindMovieByInput(string input)
        {
            var movies = this.userMoviesService.SearchMovieByInput(input);

            var moviesViewModel = new AllMoviesViewModel
            {
                Input = input,
                Movies = movies,
            };

            return this.View(moviesViewModel);
        }

        [Authorize]
        public IActionResult RateMovie()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateMovie(RateMovieInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.ratesService.SetRating(inputModel.Id, user.Id, inputModel.Rate);

            return this.RedirectToAction("UserMovieCollection", "Users");
        }
    }
}
