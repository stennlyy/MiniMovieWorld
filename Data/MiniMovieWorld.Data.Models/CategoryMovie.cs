namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class CategoryMovie : BaseDeletableModel<int>
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
