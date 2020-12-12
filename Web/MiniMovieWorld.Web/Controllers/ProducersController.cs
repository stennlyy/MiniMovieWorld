namespace MiniMovieWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.User.ProducersService;

    public class ProducersController : BaseController
    {
        private readonly IUserProducersService userProducersService;

        public ProducersController(IUserProducersService userProducersService)
        {
            this.userProducersService = userProducersService;
        }

        public IActionResult GetProducerById(int id)
        {
            var producer = this.userProducersService.GetProducerById(id);

            return this.View(producer);
        }
    }
}
