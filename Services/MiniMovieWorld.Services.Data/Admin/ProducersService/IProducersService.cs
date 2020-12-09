namespace MiniMovieWorld.Services.Data.Admin.WritersService
{
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Producer;

    public interface IProducersService
    {
        public Task AddProducerAsync(ProducerInputModel producerInputModel);
    }
}
