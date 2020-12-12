namespace MiniMovieWorld.Services.Data.User.ProducersService
{
    using MiniMovieWorld.Web.ViewModels.Producers;

    public interface IUserProducersService
    {
        public SingleProducerViewModel GetProducerById(int id);
    }
}
