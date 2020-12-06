namespace MiniMovieWorld.Data.Models
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Common.Models;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.DirectorMovies = new HashSet<DirectorMovie>();
        }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public Nationality Nationality { get; set; }

        public virtual ICollection<DirectorMovie> DirectorMovies { get; set; }
    }
}
