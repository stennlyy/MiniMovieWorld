namespace MiniMovieWorld.Web.ViewModels.Movies
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;
    }
}
