using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Domain.Entites;

public class AppRole : IdentityRole
{
    public string Description { get; set; }
    public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }

    public AppRole() : base()
    {
        RoleClaims = new HashSet<AppRoleClaim>();
    }
    public AppRole(string name, string description = null) : base(name)
    {
        Description = description;
        RoleClaims = new HashSet<AppRoleClaim>();
    }
}
