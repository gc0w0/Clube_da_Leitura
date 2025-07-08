using Clube_da_Leitura.WebApp.Profiles;

namespace Clube_da_Leitura.WebApp.Config
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AmigoProfile>();
            });
        }
    }
}
