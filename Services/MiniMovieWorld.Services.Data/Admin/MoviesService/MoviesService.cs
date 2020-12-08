namespace MiniMovieWorld.Services.Data.Admin.MoviesService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Movie;

    public class MoviesService : IMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IDeletableEntityRepository<Writer> writersRepository;
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Actor> actorsRepository,
            IDeletableEntityRepository<Writer> writersRepository,
            IDeletableEntityRepository<Director> directorsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.moviesRepository = moviesRepository;
            this.actorsRepository = actorsRepository;
            this.writersRepository = writersRepository;
            this.directorsRepository = directorsRepository;
            this.categoriesRepository = categoriesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddMovieAsync(MovieInputModel movieInputModel)
        {
            var image = await this.UploadImageAsync(movieInputModel);

            var movie = new Movie
            {
                Image = image,
                Title = movieInputModel.Title.Trim(),
                Duration = TimeSpan.FromMinutes(movieInputModel.Duration),
                Synopsis = movieInputModel.Synopsis.Trim(),
            };

            this.AddActorsToMovie(movieInputModel, movie);
            this.AddWritersToMovie(movieInputModel, movie);
            this.AddDirectorsToMovie(movieInputModel, movie);
            await this.AddCategoriesToMovieAsync(movieInputModel, movie);

            await this.moviesRepository.AddAsync(movie);
            await this.moviesRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(MovieInputModel movieInputModel)
        {
            string fileName = null;

            if (movieInputModel.Image != null)
            {
                string uploadDirectory = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "movieImages");

                fileName = Guid.NewGuid().ToString() + "-" + movieInputModel.Image.FileName;

                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await movieInputModel.Image.CopyToAsync(fileStream);
                }
            }

            return fileName;
        }

        private async Task AddCategoriesToMovieAsync(MovieInputModel movieInputModel, Movie movie)
        {
            foreach (var currentCategory in movieInputModel.Categories)
            {
                if (string.IsNullOrEmpty(currentCategory.CategoryName))
                {
                    continue;
                }

                var category = this.categoriesRepository
                    .All()
                    .Where(x => x.CategoryName == currentCategory.CategoryName)
                    .FirstOrDefault();

                if (category == null)
                {
                    var newCategory = new Category
                    {
                        CategoryName = currentCategory.CategoryName.Trim(),
                    };

                    await this.categoriesRepository.AddAsync(newCategory);
                }

                movie.Categories.Add(new CategoryMovie
                {
                    Category = category,
                    Movie = movie,
                });
            }
        }

        private void AddDirectorsToMovie(MovieInputModel movieInputModel, Movie movie)
        {
            foreach (var currentDirector in movieInputModel.Directors)
            {
                if (string.IsNullOrEmpty(currentDirector.FirstName))
                {
                    continue;
                }

                var names = currentDirector.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                var director = this.directorsRepository
                    .All()
                    .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                    .FirstOrDefault();

                movie.Directors.Add(new DirectorMovie
                {
                    Director = director,
                    Movie = movie,
                });
            }
        }

        private void AddWritersToMovie(MovieInputModel movieInputModel, Movie movie)
        {
            foreach (var currentWriter in movieInputModel.Writers)
            {
                if (string.IsNullOrEmpty(currentWriter.FirstName))
                {
                    continue;
                }

                var names = currentWriter.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                var writer = this.writersRepository
                    .All()
                    .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                    .FirstOrDefault();

                movie.Writers.Add(new WriterMovie
                {
                    Writer = writer,
                    Movie = movie,
                });
            }
        }

        private void AddActorsToMovie(MovieInputModel movieInputModel, Movie movie)
        {
            foreach (var currentActor in movieInputModel.Actors)
            {
                if (string.IsNullOrEmpty(currentActor.FirstName))
                {
                    continue;
                }

                var names = currentActor.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                var actor = this.actorsRepository
                    .All()
                    .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                    .FirstOrDefault();

                movie.Actors.Add(new ActorMovie
                {
                    Actor = actor,
                    Movie = movie,
                });
            }
        }
    }
}
