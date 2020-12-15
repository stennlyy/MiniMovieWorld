namespace MiniMovieWorld.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.User;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class MoviesController : BaseController
    {
        private readonly IUserMoviesService userMoviesService;

        public MoviesController(IUserMoviesService userMoviesService)
        {
            this.userMoviesService = userMoviesService;
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
    }
}
