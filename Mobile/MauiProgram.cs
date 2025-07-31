using Microsoft.Extensions.Logging;
using Mobile.Pages;
using Mobile.ViewModel;

namespace Mobile
{
    public static class MauiProgram
    {
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //builder.Services.AddHttpClient("Api", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7035/api");
            //});
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();

            return builder.Build();
        }
    }
}
