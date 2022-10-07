using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using FireSignage.Services;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.ListView.Hosting;
using FireSignage.Views;
using FireSignage.Controls;
using FireSignage.Renderers;

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
                fonts.AddFont("fa6brands.otf", "fab6");
                fonts.AddFont("fa6regular.otf", "far6");
                fonts.AddFont("fa6solid.otf", "fas6");

                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands.ttf", "fab");
                fonts.AddFont("fa-regular.ttf", "far");
                fonts.AddFont("fa-solid.ttf", "fas");
                fonts.AddFont("Montserrat-Regular.ttf", "MontserratReg");
                fonts.AddFont("Montserrat-Medium.ttf", "MontserratMed");
                fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
            })

        .ConfigureMauiHandlers(handlers =>
         {
             handlers.AddHandler(typeof (SKRenderView), typeof(IRenderer));
         });

        //builder.Services.AddSingleton<PremadeService>();
        //builder.Services.AddSingleton<PremadeViewModel>();
        //builder.Services.AddSingleton<PremadeSignView>();

        builder.ConfigureSyncfusionListView();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
	}
}

