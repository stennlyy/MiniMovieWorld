namespace MiniMovieWorld.Web.ViewModels.Movies
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseRateInputModel
    {
        public int Id { get; set; }

        [Range(1, 10)]
        public double Rate { get; set; }
    }
}
