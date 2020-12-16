namespace MiniMovieWorld.Services.Data.User.UsersService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IUsersService
    {
        public Task RemoveActor(int actorId, string userId);

        public ICollection<SingleActorViewModel> GetUserFavouriteActors(string userId);

        public Task AddActorToUserFavourites(int actorId, ApplicationUser user);

        public ICollection<MovieViewModel> GetUserMovieCollection(string userId);

        public Task AddMovieToUserCollectionAsync(int movieId, ApplicationUser userId);

        public Task RemoveMovieFromCollection(int movieId, string userId);
    }
}
