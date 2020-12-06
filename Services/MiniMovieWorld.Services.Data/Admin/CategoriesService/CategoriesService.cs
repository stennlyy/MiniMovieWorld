namespace MiniMovieWorld.Services.Data.Admin.CategoriesService
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniMovieWorld.Data.Common.Repositories;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Web.ViewModels.Admin.Category;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task AddCategoryAsync(CategoryInputModel categoryInputModel)
        {
            var category = this.categoriesRepository
                .All()
                .Where(x => x.CategoryName == categoryInputModel.CategoryName)
                .FirstOrDefault();

            if (category == null)
            {
                await this.categoriesRepository.AddAsync(new Category
                {
                    CategoryName = categoryInputModel.CategoryName,
                });

                await this.categoriesRepository.SaveChangesAsync();
            }
        }
    }
}
