namespace MiniMovieWorld.Services.Data.Admin.DirectorsService
{
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Director;

    public interface IDirectorsService
    {
        public Task AddDirectorAsync(DirectorInputModel directorInputModel);
    }
}
