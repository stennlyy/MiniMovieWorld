namespace MiniMovieWorld.Web.ViewModels.Actors
{
    using System.Collections.Generic;

    public class AllActorsViewModel
    {
        public ICollection<SingleActorViewModel> Actors { get; set; }
    }
}
