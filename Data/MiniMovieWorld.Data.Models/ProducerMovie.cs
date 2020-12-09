namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class ProducerMovie : BaseDeletableModel<int>
    {
        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
