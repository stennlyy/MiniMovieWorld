namespace MiniMovieWorld.Services.Data.User.CommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<UserMovieComment> userCommentsRepository;

        public CommentsService(
            IDeletableEntityRepository<UserMovieComment> userCommentsRepository)
        {
            this.userCommentsRepository = userCommentsRepository;
        }

        public async Task AddUserCommentToMovie(int movieId, string userId, string commentText)
        {
            var userComments = new UserMovieComment
            {
                MovieId = movieId,
                UserId = userId,
                Comment = commentText,
            };

            await this.userCommentsRepository.AddAsync(userComments);

            await this.userCommentsRepository.SaveChangesAsync();
        }

        public ICollection<MovieCommentsViewModel> GetMovieComments(int movieId)
        {
            var movieComments = this.userCommentsRepository
                .AllAsNoTracking()
                .Select(x => new MovieCommentsViewModel
                {
                    Username = x.User.UserName,
                    CreatedOn = x.CreatedOn,
                    Comment = x.Comment,
                })
                .OrderBy(x => x.CreatedOn)
                .ToList();

            return movieComments;
        }
    }
}
