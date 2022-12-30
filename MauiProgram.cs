using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using FireSignage.Services;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.ListView.Hosting;
using FireSignage.Views;
using FireSignage.Controls;
using FireSignage.Renderers;
using SkiaSharp.Views.Maui.Controls.Hosting;
using FireSignage.Views.Testing;
using Microsoft.Extensions.DependencyInjection;
using FireSignage.Views.LoginFlow;
using FireSignage.Viewmodels;
using FireSignage.Views.Dashboards;

namespace FireSignage;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder

            .UseMauiApp<App>()
            .UseSkiaSharp(true)
            .UseSkiaSharp()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fa6brands.otf", "fab6");
                fonts.AddFont("fa6regular.otf", "far6");
                fonts.AddFont("fa6solid.otf", "fas6");
                fonts.AddFont("UIFontIcons.ttf", "FontIcons");


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
             handlers.AddHandler(typeof(SKRenderView), typeof(IRenderer));
         });


        builder.ConfigureSyncfusionListView();
        builder.Services.AddTransient<PremadeService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<RealmService>();
        builder.Services.AddSingleton<GigViewModel>();
        builder.Services.AddSingleton<GigDashBoard>();
        builder.Services.AddSingleton<TabbedLogin>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        return builder.Build();
	}
}

