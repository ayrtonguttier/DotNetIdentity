using Authentication.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<Usuario> manager;

        public AuthenticationController(UserManager<Data.Models.Usuario> manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public IEnumerable<Usuario> GetUsers()
        {
            return manager.Users;
        }

        [HttpPost]
        public async Task<IdentityResult> CreateUser(Usuario user)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            return await manager.CreateAsync(user, user.Senha);
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateUser(Usuario model)
        {
            var user = await manager.FindByNameAsync(model.UserName);

            if (user == null)
                return NotFound();

            if (!await manager.CheckPasswordAsync(user, model.Senha))
                return Unauthorized();

            return Ok(user);
        }
    }
}