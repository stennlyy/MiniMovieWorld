namespace MiniMovieWorld.Web.ViewModels.Admin.Movie
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MiniMovieWorld.Web.ViewModels.Admin.Actor;
    using MiniMovieWorld.Web.ViewModels.Admin.Category;
    using MiniMovieWorld.Web.ViewModels.Admin.Director;
    using MiniMovieWorld.Web.ViewModels.Admin.Producer;

    public class MovieInputModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        public double Duration { get; set; }

        [MinLength(10)]
        [MaxLength(1000)]
        public string Synopsis { get; set; }

        public IEnumerable<ActorInputModel> Actors { get; set; }

        public IEnumerable<ProducerInputModel> Producers { get; set; }

        public IEnumerable<DirectorInputModel> Directors { get; set; }

        public IEnumerable<CategoryInputModel> Categories { get; set; }
    }
}
