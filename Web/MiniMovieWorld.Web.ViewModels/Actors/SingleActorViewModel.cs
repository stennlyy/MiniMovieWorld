namespace MiniMovieWorld.Web.ViewModels.Actors
{
    using System.Collections.Generic;

    using MiniMovieWorld.Web.ViewModels.Movies;

    public class SingleActorViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ActorBio { get; set; }

        public int? Age { get; set; }

        public double UserActorRatings { get; set; }

        public IEnumerable<MovieViewModel> ActorMovies { get; set; }
    }
}
