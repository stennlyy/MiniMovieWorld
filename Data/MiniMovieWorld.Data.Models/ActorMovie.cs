namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class ActorMovie : BaseDeletableModel<int>
    {
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
