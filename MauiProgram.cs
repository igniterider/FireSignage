using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using FireSignage.Services;
using Syncfusion.Maui.Core.Hosting;

using FireSignage.Views;


namespace FireSignage;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands.ttf", "fab");
                fonts.AddFont("fa-regular.ttf", "far");
                fonts.AddFont("fa-solid.ttf", "fas");
                fonts.AddFont("Montserrat-Regular.ttf", "MontserratReg");
                fonts.AddFont("Montserrat-Medium.ttf", "MontserratMed");
                fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
            });

        builder.Services.AddSingleton<PremadeService>();
        builder.Services.AddSingleton<PremadeViewModel>();
        builder.Services.AddSingleton<PremadeSignView>();

        return builder.Build();
	}
}

