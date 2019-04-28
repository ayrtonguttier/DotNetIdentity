using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
namespace Authentication.Api.Data.Models
{
    public class Usuario : IdentityUser
    {
                public string Senha { get; set; }
    }
}