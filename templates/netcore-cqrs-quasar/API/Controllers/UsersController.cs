using System;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Persistence.Identity;
using API.ViewModels;
namespace API.Controllers
{
    public class UsersController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string status, [FromQuery] string search, [FromQuery] string sortBy, [FromQuery] bool descending, [FromQuery] int page, [FromQuery] int size = -1)
        {
            return Ok(await Mediator.Send(new List.Query { Search = search, SortBy = sortBy, Descending = descending, Page = page, Size = size }));
        }

        // GET: api/Users
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await Mediator.Send(new Authenticate.Query { Username = loginVM.username, Password = loginVM.password });
            return user == null ? BadRequest() : Ok(user);
        }

        // GET: api/Users
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            return Ok(await Mediator.Send(new Register.Command
            {
                User = {
                    UserName = registerVM.Username,
                    Email = registerVM.Email,
                    EmailConfirmed = true,
                    FullName = registerVM.FullName,
                    Role = registerVM.Role,
                    Password = registerVM.Password
                }
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(String id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(new UpdateUser.Command { User = user });
            return Ok();
        }

        [HttpPut("block-user/{id}")]
        public async Task<IActionResult> BlockUser(String id)
        {
            await Mediator.Send(new BlockUser.Command { Id = id });
            return Ok();
        }

        [HttpPut("change-role/{role}/{id}")]
        public async Task<IActionResult> ChangeUserRole(String role, String id)
        {
            await Mediator.Send(new ChangeRole.Command { Id = id, NewRole = role });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
