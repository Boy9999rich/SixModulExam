using UserContacts.Repository.Settings;

namespace SixModulExam.Configurations
{
    public static class JWTSettingsConfiguration
    {
        public static void ConfigureJwtSettings(this WebApplicationBuilder builder)
        {
            var jwtSection = builder.Configuration.GetSection("Jwt");

            var lifetime = jwtSection["Lifetime"];
            var securityKey = jwtSection["SecurityKey"];
            var audience = jwtSection["Audience"];
            var issuer = jwtSection["Issuer"];

            var jwtSettings = new JwtAppSettings(issuer, audience, securityKey, lifetime);

            builder.Services.AddSingleton<JwtAppSettings>(jwtSettings);
        }
    }
}
