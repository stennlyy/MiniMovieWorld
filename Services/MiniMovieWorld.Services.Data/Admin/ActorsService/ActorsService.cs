namespace MiniMovieWorld.Services.Data.Admin.ActorsService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Actor;
    using MiniMovieWorld.Web.ViewModels.Movies;

    public class ActorsService : IActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IDeletableEntityRepository<Nationality> nationalityRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ActorsService(
            IDeletableEntityRepository<Actor> actorsRepository,
            IDeletableEntityRepository<Nationality> nationalityRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.actorsRepository = actorsRepository;
            this.nationalityRepository = nationalityRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<ActorsViewModel> GetAllActors()
        {
            var actors = this.actorsRepository
                .AllAsNoTracking()
                .Select(x => new ActorsViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToList();

            return actors;
        }

        public async Task AddActorAsync(ActorInputModel actorInputModel)
        {
            var nationality = this.nationalityRepository
                .All()
                .Where(x => x.NationName == actorInputModel.Nationality)
                .FirstOrDefault();

            if (nationality == null)
            {
                await this.nationalityRepository.AddAsync(new Nationality
                {
                    NationName = actorInputModel.Nationality.Trim(),
                });
            }

            var actor = this.actorsRepository
                .All()
                .Where(x => x.FirstName == actorInputModel.FirstName && x.LastName == actorInputModel.LastName)
                .FirstOrDefault();

            if (actor == null)
            {
                string image = await this.UploadImageAsync(actorInputModel);

                var newActor = new Actor
                {
                    Image = image,
                    FirstName = actorInputModel.FirstName.Trim(),
                    MiddleName = actorInputModel.MiddleName,
                    LastName = actorInputModel.LastName.Trim(),
                    ActorBio = actorInputModel.ActorBio,
                    Age = actorInputModel.Age,
                    Nationality = nationality,
                };

                await this.actorsRepository.AddAsync(newActor);
            }

            await this.actorsRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(ActorInputModel actorInputModel)
        {
            string fileName = null;

            if (actorInputModel.Image != null)
            {
                string uploadDirectory = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "actorImages");

                fileName = Guid.NewGuid().ToString() + "-" + actorInputModel.Image.FileName;

                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await actorInputModel.Image.CopyToAsync(fileStream);
                }
            }
            else if (actorInputModel.Image == null)
            {
                fileName = Path.GetFileName("defaultImage.png");
            }

            return fileName;
        }
    }
}
