using MauiApplication = Microsoft.Maui.Controls.Application;
using Mobile.Pages;

namespace Mobile;

public partial class App : MauiApplication
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginPage>());
    }
}