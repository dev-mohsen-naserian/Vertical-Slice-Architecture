using FluentValidation;

namespace Source.Features.Settings;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services) 
    {
        services.AddSingleton<DapperContext>();
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        services.AddCarter();
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}
