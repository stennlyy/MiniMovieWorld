namespace MiniMovieWorld.Services.Data.Admin.DirectorsService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Director;

    public class DirectorsService : IDirectorsService
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;
        private readonly IDeletableEntityRepository<Nationality> nationalitiesRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DirectorsService(
            IDeletableEntityRepository<Director> directorsRepository,
            IDeletableEntityRepository<Nationality> nationalitiesRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.directorsRepository = directorsRepository;
            this.nationalitiesRepository = nationalitiesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddDirectorAsync(DirectorInputModel directorInputModel)
        {
            var nationality = this.nationalitiesRepository
                .All()
                .Where(x => x.NationName == directorInputModel.Nationality)
                .FirstOrDefault();

            if (nationality == null)
            {
                await this.nationalitiesRepository.AddAsync(new Nationality
                {
                    NationName = directorInputModel.Nationality.Trim(),
                });
            }

            var director = this.directorsRepository
                .All()
                .Where(x => x.FirstName == directorInputModel.FirstName && x.LastName == directorInputModel.LastName)
                .FirstOrDefault();

            if (director == null)
            {
                var image = await this.UploadImageAsync(directorInputModel);

                var newDrector = new Director
                {
                    Image = image,
                    FirstName = directorInputModel.FirstName.Trim(),
                    MiddleName = directorInputModel.MiddleName,
                    LastName = directorInputModel.LastName.Trim(),
                    DirectorBio = directorInputModel.DirectorBio,
                    Age = directorInputModel.Age,
                    Nationality = nationality,
                };

                await this.directorsRepository.AddAsync(newDrector);
            }

            await this.directorsRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(DirectorInputModel directorInputModel)
        {
            string fileName = null;

            if (directorInputModel.Image != null)
            {
                string uploadDirectory = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "directorImages");

                fileName = Guid.NewGuid().ToString() + "-" + directorInputModel.Image.FileName;

                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await directorInputModel.Image.CopyToAsync(fileStream);
                }
            }
            else if (directorInputModel.Image == null)
            {
                fileName = Path.GetFileName("defaultImage.png");
            }

            return fileName;
        }
    }
}
