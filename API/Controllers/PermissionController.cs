using API.Application.Permissions.Queries.GetAllPermissions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await Mediator.Send(new GetAllPermissionsQuery()).ConfigureAwait(false);
            return Ok(permissions);
        }
    }
}
