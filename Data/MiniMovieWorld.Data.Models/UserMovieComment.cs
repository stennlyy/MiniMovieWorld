namespace MiniMovieWorld.Data.Models
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Common.Models;

    public class UserMovieComment : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public virtual string Comment { get; set; }
    }
}
