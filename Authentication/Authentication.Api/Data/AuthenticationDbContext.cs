
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using Authentication.Api.Data.Models;

namespace Authentication.Api.Data
{
    public class AuthenticationDbContext : IdentityDbContext<Usuario>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder
                .Entity<Usuario>(b =>
                {
                    b.ToTable("TB_USUARIO");
                    b.Ignore(x => x.Senha);
                });
        }

    }
}