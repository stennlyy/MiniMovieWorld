namespace MiniMovieWorld.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    public class MovieAllCommentsViewModel
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public ICollection<MovieCommentsViewModel> MovieComments { get; set; }
    }
}
