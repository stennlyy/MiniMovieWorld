namespace MiniMovieWorld.Services.Data.Admin.MoviesService
{
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Movie;

    public interface IMoviesService
    {
        public Task AddMovieAsync(MovieInputModel movieInputModel);
    }
}
