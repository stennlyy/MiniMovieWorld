namespace MiniMovieWorld.Services.Data.User.RatesService
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;

    public class RatesService : IRatesService
    {
        private readonly IDeletableEntityRepository<UserRate> userRatingsRepository;

        public RatesService(IDeletableEntityRepository<UserRate> userRatingsRepository)
        {
            this.userRatingsRepository = userRatingsRepository;
        }

        public async Task SetRating(int movieId, string userId, double valueRate)
        {
            var userRatings = this.userRatingsRepository
                .All()
                .Where(x => x.Movie.Id == movieId && x.User.Id == userId)
                .FirstOrDefault();

            if (userRatings == null)
            {
                userRatings = new UserRate
                {
                    UserId = userId,
                    MovieId = movieId,
                };

                await this.userRatingsRepository.AddAsync(userRatings);
            }

            userRatings.Rate = valueRate;

            await this.userRatingsRepository.SaveChangesAsync();
        }
    }
}
