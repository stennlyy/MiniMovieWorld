namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class WriterMovie : BaseDeletableModel<int>
    {
        public int WriterId { get; set; }

        public virtual Writer Writer { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
