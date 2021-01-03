namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentInputModel
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(200)]
        public string Comment { get; set; }
    }
}
