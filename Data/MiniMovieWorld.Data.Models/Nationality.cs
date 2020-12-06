namespace MiniMovieWorld.Data.Models
{
    using MiniMovieWorld.Data.Common.Models;

    public class Nationality : BaseDeletableModel<int>
    {
        public string NationName { get; set; }
    }
}
