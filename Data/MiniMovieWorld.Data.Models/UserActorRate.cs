namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class UserActorRate : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ActorId { get; set; }

        public Actor Actor { get; set; }

        public double Rate { get; set; }
    }
}
