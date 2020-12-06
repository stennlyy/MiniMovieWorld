namespace MiniMovieWorld.Web.Areas.Administration.Controllers
{
    using MiniMovieWorld.Common;
    using MiniMovieWorld.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
