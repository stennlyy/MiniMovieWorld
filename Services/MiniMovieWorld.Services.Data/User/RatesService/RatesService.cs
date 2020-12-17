namespace MiniMovieWorld.Services.Data.User.RatesService
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;

    public class RatesService : IRatesService
    {
        private readonly IDeletableEntityRepository<UserRate> userRatingsRepository;
        private readonly IDeletableEntityRepository<UserActorRate> userActorRatingsRepository;

        public RatesService(
            IDeletableEntityRepository<UserRate> userRatingsRepository,
            IDeletableEntityRepository<UserActorRate> userActorRatingsRepository)
        {
            this.userRatingsRepository = userRatingsRepository;
            this.userActorRatingsRepository = userActorRatingsRepository;
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

        public async Task SetActorRating(int actorId, string userId, double valueRate)
        {
            var userActorRatings = this.userActorRatingsRepository
                .All()
                .Where(x => x.User.Id == userId && x.Actor.Id == actorId)
                .FirstOrDefault();

            if (userActorRatings == null)
            {
                userActorRatings = new UserActorRate
                {
                    UserId = userId,
                    ActorId = actorId,
                };

                await this.userActorRatingsRepository.AddAsync(userActorRatings);
            }

            userActorRatings.Rate = valueRate;

            await this.userActorRatingsRepository.SaveChangesAsync();
        }
    }
}
