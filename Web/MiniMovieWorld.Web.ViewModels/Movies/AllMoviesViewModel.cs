namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    public class AllMoviesViewModel : PagingViewModel
    {
        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}
