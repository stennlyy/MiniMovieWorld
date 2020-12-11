namespace MiniMovieWorld.Services.Data.User
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IUserMoviesService
    {
        public MovieViewModel GetMovie(int id);

        public IEnumerable<MovieViewModel> GetAllMovies(int page, int itemsPerPage);

        public int GetMoviesCount();
    }
}
