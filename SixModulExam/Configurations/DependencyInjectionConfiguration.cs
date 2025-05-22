using FluentValidation;
using UserContacts.Bll.Dtos;
using UserContacts.Bll.Security;
using UserContacts.Bll.Services;
using UserContacts.Bll.Validators;
using UserContacts.Dal;
using UserContacts.Repository.Services;

namespace SixModulExam.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDataBase(this IServiceCollection services)
    {
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        services.AddScoped<IValidator<ContactCreateDto>, ContactCreateDtoValidator>();
        services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();
    }
}
