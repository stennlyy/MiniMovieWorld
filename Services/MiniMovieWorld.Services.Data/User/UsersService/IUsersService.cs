namespace MiniMovieWorld.Services.Data.User.UsersService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IUsersService
    {
        public IEnumerable<MovieViewModel> GetUserMovieCollection(string userId);

        public Task AddMovieToUserCollectionAsync(int movieId, ApplicationUser userId);

        public Task RemoveMovieFromCollection(int movieId, string userId);
    }
}
