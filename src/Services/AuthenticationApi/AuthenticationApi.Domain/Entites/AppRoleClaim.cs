using Microsoft.AspNetCore.Identity;

namespace AuthenticationApi.Domain.Entites
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public string? Description { get; set; }
        public string? Group { get; set; }
        public virtual AppRole Role { get; set; }


        public AppRoleClaim() : base()
        {
        }
        public AppRoleClaim(string description = null, string group = null) : base()
        {
            Description = description;
            Group = group;
        }
    }


}
