namespace MiniMovieWorld.Services.Data.User.ActorsService
{
    using System.Linq;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Actors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UserActorsService : IUserActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public UserActorsService(IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public SingleActorViewModel GetActorById(int id)
        {
            var actor = this.actorsRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => new SingleActorViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    ActorBio = x.ActorBio,
                    Age = x.Age,
                    ActorMovies = x.ActorMovies.Select(y => new MovieViewModel
                    {
                        Id = y.Movie.Id,
                        Title = y.Movie.Title,
                    }).ToList(),
                })
                .FirstOrDefault();

            return actor;
        }
    }
}
