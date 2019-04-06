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
            AuthenticationDbContext dbContext = applicationBuilder.GetService<AuthenticationDbContext>();

            dbContext.Database.EnsureCreated();

            if (!dbContext.Users.Any())
            {
                var manager = applicationBuilder.GetService<UserManager<Usuario>>();

                Usuario usuario = new Usuario()
                {
                    Email = "adm@aguttier.com",
                    UserName = "admin",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                manager.CreateAsync(usuario, "admin@@123");
            }
        }

    }
}
