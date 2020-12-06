namespace MiniMovieWorld.Services.Data.Admin.WritersService
{
    using System.Threading.Tasks;

    using MiniMovieWorld.Web.ViewModels.Admin.Writer;

    public interface IWritersService
    {
        public Task AddWriterAsync(WriterInputModel writerInputModel);
    }
}
