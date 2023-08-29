using Maui.Popped;
using Microsoft.Extensions.Logging;

namespace Sample;

public static class MauiProgram
{
	public static MauiApp App { get; private set; }
	public static IServiceProvider Services { get; private set; }

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.UsePopped();

		App = builder.Build();
		Services = App.Services;
		return App;
	}
}

