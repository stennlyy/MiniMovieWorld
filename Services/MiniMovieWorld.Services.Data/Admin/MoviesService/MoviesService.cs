﻿namespace MiniMovieWorld.Services.Data.Admin.MoviesService
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
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MoviesService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Actor> actorsRepository,
            IDeletableEntityRepository<Producer> producersRepository,
            IDeletableEntityRepository<Director> directorsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.moviesRepository = moviesRepository;
            this.actorsRepository = actorsRepository;
            this.producersRepository = producersRepository;
            this.directorsRepository = directorsRepository;
            this.categoriesRepository = categoriesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddMovieAsync(MovieInputModel movieInputModel)
        {
            var movie = new Movie
            {
                Title = movieInputModel.Title.Trim(),
                Year = movieInputModel.Year,
                Duration = TimeSpan.FromMinutes(movieInputModel.Duration),
                Synopsis = movieInputModel.Synopsis.Trim(),
            };

            this.AddActorsToMovie(movieInputModel, movie);
            this.AddDirectorsToMovie(movieInputModel, movie);
            this.AddProducersToMovie(movieInputModel, movie);
            await this.AddCategoriesToMovieAsync(movieInputModel, movie);

            var image = await this.UploadImageAsync(movieInputModel);

            movie.Image = image;

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
            var currentDirectorName = string.Empty;

            try
            {
                foreach (var currentDirector in movieInputModel.Directors)
                {
                    if (string.IsNullOrEmpty(currentDirector.FirstName))
                    {
                        continue;
                    }

                    currentDirectorName = currentDirector.FirstName;

                    var names = currentDirector.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                    var director = this.directorsRepository
                        .All()
                        .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                        .FirstOrDefault();

                    if (director == null)
                    {
                        throw new InvalidOperationException($"Invalid Actor! - {currentDirector}");
                    }

                    movie.Directors.Add(new DirectorMovie
                    {
                        Director = director,
                        Movie = movie,
                    });
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Invalid Director! - {currentDirectorName}");
            }
        }

        private void AddProducersToMovie(MovieInputModel movieInputModel, Movie movie)
        {
            var currentProducerName = string.Empty;

            try
            {
                foreach (var currentProducer in movieInputModel.Producers)
                {
                    if (string.IsNullOrEmpty(currentProducer.FirstName))
                    {
                        continue;
                    }

                    currentProducerName = currentProducer.FirstName;

                    var names = currentProducer.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                    var producer = this.producersRepository
                        .All()
                        .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                        .FirstOrDefault();

                    if (producer == null)
                    {
                        throw new InvalidOperationException($"Invalid Actor! - {currentProducer}");
                    }

                    movie.Producers.Add(new ProducerMovie
                    {
                        Producer = producer,
                        Movie = movie,
                    });
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Invalid Producer! - {currentProducerName}");
            }
        }

        private void AddActorsToMovie(MovieInputModel movieInputModel, Movie movie)
        {
            var currentActorName = string.Empty;

            try
            {
                foreach (var currentActor in movieInputModel.Actors)
                {
                    if (string.IsNullOrEmpty(currentActor.FirstName))
                    {
                        continue;
                    }

                    currentActorName = currentActor.FirstName;

                    var names = currentActor.FirstName.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

                    var actor = this.actorsRepository
                        .All()
                        .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                        .FirstOrDefault();

                    if (actor == null)
                    {
                        throw new InvalidOperationException($"Invalid Actor! - {currentActorName}");
                    }

                    movie.Actors.Add(new ActorMovie
                    {
                        Actor = actor,
                        Movie = movie,
                    });
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Invalid Actor! - {currentActorName}");
            }
        }
    }
}
