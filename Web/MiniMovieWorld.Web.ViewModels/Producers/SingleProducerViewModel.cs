namespace MiniMovieWorld.Web.ViewModels.Producers
{
    using System.Collections.Generic;

    using MiniMovieWorld.Web.ViewModels.Movies;

    public class SingleProducerViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public IEnumerable<MovieViewModel> ProducerMovies { get; set; }
    }
}
