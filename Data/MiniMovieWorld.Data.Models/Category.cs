namespace MiniMovieWorld.Data.Models
{
    using System.Collections.Generic;

    using MiniMovieWorld.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.CategoryMovies = new HashSet<CategoryMovie>();
        }

        public string CategoryName { get; set; }

        public virtual ICollection<CategoryMovie> CategoryMovies { get; set; }
    }
}
