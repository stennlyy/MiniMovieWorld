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

        public IActionResult All()
        {
            var movies = this.userMoviesService.GetAllMovies();

            var movieViewModel = new AllMoviesViewModel
            {
                Movies = movies,
            };

            return this.View(movieViewModel);
        }
    }
}
