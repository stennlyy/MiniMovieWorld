using System.ComponentModel.DataAnnotations;

namespace MiniMovieWorld.Web.ViewModels.Admin.Category
{
    public class CategoryInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
    }
}
