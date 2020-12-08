namespace MiniMovieWorld.Services.Data.Admin.WritersService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Writer;

    public class WritersService : IWritersService
    {
        private readonly IDeletableEntityRepository<Writer> writersRepository;
        private readonly IDeletableEntityRepository<Nationality> nationalityRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public WritersService(
            IDeletableEntityRepository<Writer> writersRepository,
            IDeletableEntityRepository<Nationality> nationalityRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.writersRepository = writersRepository;
            this.nationalityRepository = nationalityRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddWriterAsync(WriterInputModel writerInputModel)
        {
            var nationality = this.nationalityRepository
                .All()
                .Where(x => x.NationName == writerInputModel.Nationality)
                .FirstOrDefault();

            if (nationality == null)
            {
                await this.nationalityRepository.AddAsync(new Nationality
                {
                    NationName = writerInputModel.Nationality.Trim(),
                });
            }

            var writer = this.writersRepository
                .All()
                .Where(x => x.FirstName == writerInputModel.FirstName && x.LastName == writerInputModel.LastName)
                .FirstOrDefault();

            var image = await this.UploadImageAsync(writerInputModel);

            if (writer == null)
            {
                var newWriter = new Writer
                {
                    Image = image,
                    FirstName = writerInputModel.FirstName.Trim(),
                    MiddleName = writerInputModel.MiddleName.Trim(),
                    LastName = writerInputModel.LastName.Trim(),
                    Age = writerInputModel.Age,
                    Nationality = nationality,
                };

                await this.writersRepository.AddAsync(newWriter);
            }

            await this.writersRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(WriterInputModel writerInputModel)
        {
            string fileName = null;

            if (writerInputModel.Image != null)
            {
                string uploadDirectory = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "writerImages");

                fileName = Guid.NewGuid().ToString() + "-" + writerInputModel.Image.FileName;

                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await writerInputModel.Image.CopyToAsync(fileStream);
                }
            }
            else if (writerInputModel.Image == null)
            {
                fileName = Path.GetFileName("defaultImage.png");
            }

            return fileName;
        }
    }
}
