using API.Application.Permissions.DTO;
using API.Application.Users.Commands.AddUser;
using API.Application.Users.Commands.AssignPermission;
using API.Application.Users.Commands.DeleteUser;
using API.Application.Users.Commands.EditUser;
using API.Application.Users.Commands.UnassignPermission;
using API.Application.Users.DTO;
using API.Application.Users.Queries.GetAllUsers;
using API.Application.Users.Queries.GetUserById;
using API.Application.Users.Queries.GetUserPermissions;
using API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQueryParameters parameters)
        {
            var users = await Mediator.Send(new GetAllUsersQuery(parameters.PageSize, parameters.PageNumber,
                                                       parameters.SortOrder, parameters.SearchString));
            return Ok(users);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var users = await Mediator.Send(new GetUserByIdQuery(userId));
            return Ok(users);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] EditUserDTO userDTO)
        {
            if(userId == 0)
            {
                return BadRequest("Please provide a valid user");
            }

            var updatedUser = await Mediator.Send(new EditUserCommand(userId, userDTO.FirstName,
                userDTO.LastName, userDTO.Email, userDTO.Status)).ConfigureAwait(false);
            return Ok(updatedUser);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO userDTO)
        {
            var newUser = await Mediator.Send(new AddUserCommand(userDTO.FirstName, userDTO.LastName,
                                        userDTO.Email, userDTO.Status, userDTO.Username, userDTO.Password))
                                        .ConfigureAwait(false);
            return Ok(newUser);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var newUser = await Mediator.Send(new DeleteUserCommand(userId))
                                        .ConfigureAwait(false);
            return Ok(newUser);
        }
        [HttpGet("{userId}/Permissions")]
        public async Task<IActionResult> GetUserPermissions(int userId)
        {
            var success = await Mediator.Send(new GetUserPermissionsQuery(userId));

            return Ok(success);
        }
        [HttpPost("{userId}/Permissions")]
        public async Task<IActionResult> AssignPermissionToUser(int userId, [FromBody] int permissionId)
        {
            var success = await Mediator.Send(new AssignPermissionCommand(userId, permissionId));

            return Ok(success);
        }
        [HttpDelete("{userId}/Permissions/{permissionId}")]
        public async Task<IActionResult> UnassignPermissionFromUser(int userId, int permissionId)
        {
            var success = await Mediator.Send(new UnassignPermissionCommand(userId, permissionId));

            return Ok(success);
        }
    }
}
