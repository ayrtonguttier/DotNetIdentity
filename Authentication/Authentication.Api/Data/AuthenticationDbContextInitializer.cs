using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Api.Data
{
    public static class AuthenticationDbContextInitializer
    {

        public static void InitializeDb(this IServiceProvider applicationBuilder)
        {
            try
            {
                AuthenticationDbContext dbContext = applicationBuilder.GetService<AuthenticationDbContext>();

                dbContext.Database.EnsureCreated();

                if (!dbContext.Roles.Any(x => x.Name == "admin"))
                {
                    var manager = applicationBuilder.GetService<RoleManager<IdentityRole>>();

                    var adminRole = new IdentityRole();
                    adminRole.Name = "admin";

                    Task.WaitAll(manager.CreateAsync(adminRole));

                }

                if (!dbContext.Users.Any())
                {
                    var manager = applicationBuilder.GetService<UserManager<Usuario>>();

                    Usuario usuario = new Usuario()
                    {
                        Email = "adm@aguttier.com",
                        UserName = "admin",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };



                    Task.WaitAll(manager.CreateAsync(usuario, "admin@@123"));

                    Task.WaitAll(manager.AddToRoleAsync(usuario, "admin"));
                }
            }
            catch
            {

            }
        }
    }
}
