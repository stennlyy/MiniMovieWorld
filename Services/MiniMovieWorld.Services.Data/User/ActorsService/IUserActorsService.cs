namespace MiniMovieWorld.Services.Data.User.ActorsService
{
    using MiniMovieWorld.Web.ViewModels.Actors;

    public interface IUserActorsService
    {
        public SingleActorViewModel GetActorById(int id);
    }
}
