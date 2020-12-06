namespace MiniMovieWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MiniMovieWorld.Services.Data.Admin.ActorsService;
    using MiniMovieWorld.Services.Data.Admin.CategoriesService;
    using MiniMovieWorld.Services.Data.Admin.DirectorsService;
    using MiniMovieWorld.Services.Data.Admin.MoviesService;
    using MiniMovieWorld.Services.Data.Admin.WritersService;
    using MiniMovieWorld.Web.ViewModels.Admin.Actor;
    using MiniMovieWorld.Web.ViewModels.Admin.Category;
    using MiniMovieWorld.Web.ViewModels.Admin.Director;
    using MiniMovieWorld.Web.ViewModels.Admin.Movie;
    using MiniMovieWorld.Web.ViewModels.Admin.Writer;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IActorsService actorsService;
        private readonly IWritersService writersService;
        private readonly IDirectorsService directorsService;
        private readonly ICategoriesService categoriesService;
        private readonly IMoviesService moviesService;

        public AdminController(
            IActorsService actorsService,
            IWritersService writersService,
            IDirectorsService directorsService,
            ICategoriesService categoriesService,
            IMoviesService moviesService)
        {
            this.actorsService = actorsService;
            this.writersService = writersService;
            this.directorsService = directorsService;
            this.categoriesService = categoriesService;
            this.moviesService = moviesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult AddActor()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(ActorInputModel actorInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.actorsService.AddActorAsync(actorInputModel);

            return this.RedirectToAction("Index");
        }

        public IActionResult AddWriter()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWriter(WriterInputModel writerInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.writersService.AddWriterAsync(writerInputModel);

            return this.RedirectToAction("Index");
        }

        public IActionResult AddDirector()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector(DirectorInputModel directorInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.directorsService.AddDirectorAsync(directorInputModel);

            return this.RedirectToAction("Index");
        }

        public IActionResult AddMovie()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieInputModel movieInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.moviesService.AddMovieAsync(movieInputModel);

            return this.RedirectToAction("Index");
        }

        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryInputModel categoryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.AddCategoryAsync(categoryInputModel);

            return this.RedirectToAction("Index");
        }
    }
}
