namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    public class AllMoviesViewModel : PagingViewModel
    {
        public string Input { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }

        public int Count => this.Movies.Count;
    }
}
