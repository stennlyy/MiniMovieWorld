namespace MiniMovieWorld.Web.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class BaseInputModel
    {
        public IFormFile Image { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [Range(12, 90)]
        public int? Age { get; set; }

        public string Nationality { get; set; }
    }
}
