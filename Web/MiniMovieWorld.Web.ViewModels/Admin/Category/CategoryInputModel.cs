namespace MiniMovieWorld.Web.ViewModels.Admin.Category
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
    }
}
