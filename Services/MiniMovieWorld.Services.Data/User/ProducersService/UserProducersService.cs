namespace MiniMovieWorld.Services.Data.User.ProducersService
{
    using System.Linq;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Movies;
    using MiniMovieWorld.Web.ViewModels.Producers;

    public class UserProducersService : IUserProducersService
    {
        private readonly IDeletableEntityRepository<Producer> producersRepository;

        public UserProducersService(IDeletableEntityRepository<Producer> producersRepository)
        {
            this.producersRepository = producersRepository;
        }

        public SingleProducerViewModel GetProducerById(int id)
        {
            var producer = this.producersRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => new SingleProducerViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Age = x.Age,
                    ProducerBio = x.ProducerBio,
                    ProducerMovies = x.ProducerMovies.Select(y => new MovieViewModel
                    {
                        Id = y.Movie.Id,
                        Title = y.Movie.Title,
                    }).ToList(),
                })
                .FirstOrDefault();

            return producer;
        }
    }
}
