namespace MiniMovieWorld.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Models;

    public class UserActor : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ActorId { get; set; }

        public Actor Actor { get; set; }

        public string Review { get; set; }
    }
}
