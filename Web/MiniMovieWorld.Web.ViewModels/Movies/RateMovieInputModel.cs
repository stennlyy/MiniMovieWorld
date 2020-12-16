namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RateMovieInputModel
    {
        public int Id { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }
    }
}
