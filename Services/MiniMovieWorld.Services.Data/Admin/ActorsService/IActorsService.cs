namespace MiniMovieWorld.Services.Data.Admin.ActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Actor;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public interface IActorsService
    {
        public IEnumerable<ActorsViewModel> GetAllActors();

        public Task AddActorAsync(ActorInputModel actorInputModel);
    }
}
