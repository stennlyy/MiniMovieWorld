namespace MiniMovieWorld.Services.Data.User.RatesService
{
    using System.Threading.Tasks;

    public interface IRatesService
    {
        public Task SetRating(int movieId, string userId, double valueRate);
    }
}
