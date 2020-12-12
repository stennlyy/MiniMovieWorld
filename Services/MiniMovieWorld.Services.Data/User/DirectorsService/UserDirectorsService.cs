namespace MiniMovieWorld.Services.Data.User.DirectorsService
{
    using System.Linq;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Directors;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class UserDirectorsService : IUserDirectorsService
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;

        public UserDirectorsService(IDeletableEntityRepository<Director> directorsRepository)
        {
            this.directorsRepository = directorsRepository;
        }

        public SingleDirectorViewModel GetDirectorById(int id)
        {
            var director = this.directorsRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => new SingleDirectorViewModel
                {
                    Id = x.Id,
                    Image = x.Image,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Age = x.Age,
                    DirectorMovies = x.DirectorMovies.Select(y => new MovieViewModel
                    {
                        Id = y.Movie.Id,
                        Title = y.Movie.Title,
                    }).ToList(),
                })
                .FirstOrDefault();

            return director;
        }
    }
}
