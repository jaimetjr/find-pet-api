using Mobile.ViewModel;

namespace Mobile.Views;

public partial class SignInView : ContentView
{
    public SignInView()
    {
        InitializeComponent();
        BindingContext = new SignInViewModel();
    }

    private void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        var vm = (SignInViewModel)BindingContext;
        vm.IsPassword = !vm.IsPassword;
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        var vm = (SignInViewModel)BindingContext;
        await vm.SignInAsync();
    }
}