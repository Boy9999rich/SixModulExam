using Microsoft.AspNetCore.Authorization;
using UserContacts.Bll.Services;
using UserContacts.Dal.Entities;

namespace SixModulExam.EndPoints
{
    public static class RoleEndPoints
    {
        public static void MapRoleEndpoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/api/role")
                .RequireAuthorization()
                .WithTags("UserRole Management");

            userGroup.MapGet("/get-all-roles", [Authorize(Roles = "Admin, SuperAdmin")]
            async (IRoleService _roleService) =>
            {
                var roles = await _roleService.GetAllRolesAsync();
                return Results.Ok(roles);
            })
            .WithName("GetAllUsers");

            userGroup.MapGet("/get-all-users-by-role", [Authorize(Roles = "Admin, SuperAdmin")]
            async (string role, IRoleService _roleService) =>
            {
                var users = await _roleService.GetAllUsersByRoleAsync(role);
                return Results.Ok(users);
            })
            .WithName("GetUsersByRole");

            userGroup.MapGet("/PostRole", [Authorize(Roles = "SuperAdmin")]
            async (UserRole role, IRoleService _roleService) =>
            {
                var users = await _roleService.AddRole(role);
                return Results.Ok(users);
            })
            .WithName("GetUsersByRole");


        }
    }
}
