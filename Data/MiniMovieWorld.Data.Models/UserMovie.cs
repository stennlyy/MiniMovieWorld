namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class UserMovie : BaseDeletableModel<int>
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
