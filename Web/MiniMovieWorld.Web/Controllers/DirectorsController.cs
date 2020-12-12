namespace MiniMovieWorld.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.User.DirectorsService;

    public class DirectorsController : BaseController
    {
        private readonly IUserDirectorsService userDirectorsService;

        public DirectorsController(IUserDirectorsService userDirectorsService)
        {
            this.userDirectorsService = userDirectorsService;
        }

        public IActionResult GetDirectorById(int id)
        {
            var director = this.userDirectorsService.GetDirectorById(id);

            return this.View(director);
        }
    }
}
