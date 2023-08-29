namespace Maui.Popped;

public static class PoppedHostExtensions
{
    public static MauiAppBuilder UsePopped(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(h => h.AddHandler<IPoppedContentView, PoppedContentHandler>());
        builder.Services.AddPopped();
        return builder;
    }

    public static IServiceCollection AddPopped(this IServiceCollection services)
    {
        services.AddSingleton<IPoppedNavigationService, PoppedNavigationService>();
        return services;
    }
}