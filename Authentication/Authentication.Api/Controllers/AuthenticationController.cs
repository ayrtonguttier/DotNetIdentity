using Authentication.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<Usuario> manager;

        public AuthenticationController(UserManager<Usuario> manager)
        {
            this.manager = manager;
        }

        [HttpGet]        
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken(Usuario model)
        {
            var user = await manager.FindByNameAsync(model.UserName);

            if (user == null)
                return NotFound();

            if (!await manager.CheckPasswordAsync(user, model.Senha))
                return Unauthorized();


            try
            {
                var handler = new JwtSecurityTokenHandler();
                var credentials = Auth.TokenOption.SigningCredentials;
                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserName));



                var tokenDate = DateTime.Now;

                var securityToken = handler.CreateJwtSecurityToken(new SecurityTokenDescriptor
                {
                    Issuer = Auth.TokenOption.Issuer,
                    Audience = Auth.TokenOption.Audience,
                    SigningCredentials = credentials,
                    Subject = identity,
                    IssuedAt = tokenDate,
                    Expires = tokenDate + Auth.TokenOption.ExpiresSpan,
                    NotBefore = tokenDate
                });


                return Ok(handler.WriteToken(securityToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}