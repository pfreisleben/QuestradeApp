using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Domain.Entites
{
    public class AppUser : IdentityUser<string>
    {
        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public bool Ativo { get; set; }

        public AppUser()
        {
        }
        
        
    }
}