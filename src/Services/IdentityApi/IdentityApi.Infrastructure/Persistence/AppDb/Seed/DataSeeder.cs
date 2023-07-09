using IdentityApi.Domain.Entites;
using IdentityApi.Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Shared.Constants;
using Shared.Logs.Services;

namespace IdentityApi.Infrastructure.Persistence.AppDb.Seed
{
    public class DataSeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogServices _logServices;

        public DataSeeder(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ILogServices logServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logServices = logServices;
        }

        public async Task Initialize()
        {
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            var adminRole = new AppRole(Roles.Admin, "Role administrador com todas as permissões");
            var userRole = new AppRole(Roles.User, "Role básica com permissões padrões.");

            var adminRoleInDb = await _roleManager.FindByNameAsync(adminRole.Name);
            var userRoleInDb = await _roleManager.FindByNameAsync(userRole.Name);

            if (adminRoleInDb is null)
            {
                await _roleManager.CreateAsync(adminRole);
                adminRoleInDb = await _roleManager.FindByNameAsync(adminRole.Name);

            }
            if (userRoleInDb is null)
                await _roleManager.CreateAsync(userRole);

            

            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                await _roleManager.AddPermissionClaim(adminRoleInDb!, permission);
            }


        }
        private async Task SeedUsers()
        {
            var adminUser = new AppUser()
            {
                Nome = "Administrador",
                Sobrenome = "Questrade",
                UserName = "admin",
                Email = "admin@questrade.com.br",
                Ativo = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var adminUserInDb = await _userManager.FindByNameAsync(adminUser.UserName);
            if (adminUserInDb is null)
            {
                await _userManager.CreateAsync(adminUser, User.AdminPassword);
                adminUserInDb = await _userManager.FindByNameAsync(adminUser.UserName);
                var result = await _userManager.AddToRoleAsync(adminUserInDb, Roles.Admin);
                if (result.Succeeded)
                {
                    _logServices.WriteMessage($"Adicionado usuário admin padrão com sucesso!");
                }
                else
                {
                    foreach (var erro in result.Errors)
                    {
                        _logServices.WriteMessage($"Erro ao criar admin padrão: {erro.Description}");
                    }
                }
            }

           

        }
    }
}
