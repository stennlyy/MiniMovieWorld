namespace MiniMovieWorld.Services.Data.User
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IUserMoviesService
    {
        public ICollection<MovieViewModel> SearchMovieByInput(string input);

        public MovieViewModel GetMovie(int id);

        public ICollection<MovieViewModel> GetAllMovies(int page, int itemsPerPage);

        public int GetMoviesCount();
    }
}
