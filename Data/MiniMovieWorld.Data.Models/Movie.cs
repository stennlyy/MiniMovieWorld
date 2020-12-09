namespace MiniMovieWorld.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Directors = new HashSet<DirectorMovie>();
            this.Producers = new HashSet<ProducerMovie>();
            this.Actors = new HashSet<ActorMovie>();
            this.MovieUsers = new HashSet<UserMovie>();
            this.Categories = new HashSet<CategoryMovie>();
        }

        public string Image { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public string Synopsis { get; set; }

        public virtual ICollection<DirectorMovie> Directors { get; set; }

        public virtual ICollection<ProducerMovie> Producers { get; set; }

        public virtual ICollection<ActorMovie> Actors { get; set; }

        public virtual ICollection<UserMovie> MovieUsers { get; set; }

        public virtual ICollection<CategoryMovie> Categories { get; set; }
    }
}
