namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public string Synopsis { get; set; }

        public double UserRates { get; set; }

        public int Year { get; set; }

        public IEnumerable<ActorsViewModel> Actors { get; set; }

        public IEnumerable<ProducersViewModel> Producers { get; set; }

        public IEnumerable<DirectorsViewModel> Directors { get; set; }

        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
