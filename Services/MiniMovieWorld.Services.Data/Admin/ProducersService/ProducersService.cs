namespace MiniMovieWorld.Services.Data.Admin.WritersService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Producer;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly IDeletableEntityRepository<Nationality> nationalityRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProducersService(
            IDeletableEntityRepository<Producer> producersRepository,
            IDeletableEntityRepository<Nationality> nationalityRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.producersRepository = producersRepository;
            this.nationalityRepository = nationalityRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddProducerAsync(ProducerInputModel producerInputModel)
        {
            var nationality = this.nationalityRepository
                .All()
                .Where(x => x.NationName == producerInputModel.Nationality)
                .FirstOrDefault();

            if (nationality == null)
            {
                await this.nationalityRepository.AddAsync(new Nationality
                {
                    NationName = producerInputModel.Nationality.Trim(),
                });
            }

            var producer = this.producersRepository
                .All()
                .Where(x => x.FirstName == producerInputModel.FirstName && x.LastName == producerInputModel.LastName)
                .FirstOrDefault();

            var image = await this.UploadImageAsync(producerInputModel);

            if (producer == null)
            {
                var newProducer = new Producer
                {
                    Image = image,
                    FirstName = producerInputModel.FirstName.Trim(),
                    MiddleName = producerInputModel.MiddleName,
                    LastName = producerInputModel.LastName.Trim(),
                    ProducerBio = producerInputModel.ProducerBio,
                    Age = producerInputModel.Age,
                    Nationality = nationality,
                };

                await this.producersRepository.AddAsync(newProducer);
            }

            await this.producersRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(ProducerInputModel producerInputModel)
        {
            string fileName = null;

            if (producerInputModel.Image != null)
            {
                string uploadDirectory = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "producerImages");

                fileName = Guid.NewGuid().ToString() + "-" + producerInputModel.Image.FileName;

                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await producerInputModel.Image.CopyToAsync(fileStream);
                }
            }
            else if (producerInputModel.Image == null)
            {
                fileName = Path.GetFileName("defaultImage.png");
            }

            return fileName;
        }
    }
}
