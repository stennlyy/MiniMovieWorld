namespace MiniMovieWorld.Services.Data.User.DirectorsService
{
    using MiniMovieWorld.Web.ViewModels.Directors;

    public interface IUserDirectorsService
    {
        public SingleDirectorViewModel GetDirectorById(int id);
    }
}
