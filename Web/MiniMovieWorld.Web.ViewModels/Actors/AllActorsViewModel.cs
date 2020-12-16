namespace MiniMovieWorld.Web.ViewModels.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AllActorsViewModel
    {
        public ICollection<SingleActorViewModel> Actors { get; set; }
    }
}
