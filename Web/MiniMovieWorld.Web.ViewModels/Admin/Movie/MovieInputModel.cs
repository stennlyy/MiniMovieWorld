namespace MiniMovieWorld.Web.ViewModels.Admin.Movie
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MiniMovieWorld.Web.ViewModels.Admin.Actor;
    using MiniMovieWorld.Web.ViewModels.Admin.Category;
    using MiniMovieWorld.Web.ViewModels.Admin.Director;
    using MiniMovieWorld.Web.ViewModels.Admin.Writer;

    public class MovieInputModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        public double Duration { get; set; }

        [MinLength(10)]
        [MaxLength(1000)]
        public string Synopsis { get; set; }

        public IEnumerable<ActorInputModel> Actors { get; set; }

        public IEnumerable<WriterInputModel> Writers { get; set; }

        public IEnumerable<DirectorInputModel> Directors { get; set; }

        public IEnumerable<CategoryInputModel> Categories { get; set; }
    }
}
