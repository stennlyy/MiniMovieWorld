namespace MiniMovieWorld.Services.Data.Admin.CategoriesService
{
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Category;

    public interface ICategoriesService
    {
        public Task AddCategoryAsync(CategoryInputModel categoryInputModel);
    }
}
