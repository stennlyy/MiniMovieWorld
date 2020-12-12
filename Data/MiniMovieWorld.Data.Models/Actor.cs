namespace MiniMovieWorld.Data.Models
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Common.Models;

    public class Actor : BaseDeletableModel<int>
    {
        public Actor()
        {
            this.ActorMovies = new HashSet<ActorMovie>();
        }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ActorBio { get; set; }

        public int? Age { get; set; }

        public Nationality Nationality { get; set; }

        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
    }
}
