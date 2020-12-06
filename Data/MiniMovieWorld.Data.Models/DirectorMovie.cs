namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class DirectorMovie : BaseDeletableModel<int>
    {
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
