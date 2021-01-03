namespace MiniMovieWorld.Services.Data.User.CommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        public Task AddUserCommentToMovie(int movieId, string userId, string commentText);

        public ICollection<MovieCommentsViewModel> GetMovieComments(int movieId);
    }
}
