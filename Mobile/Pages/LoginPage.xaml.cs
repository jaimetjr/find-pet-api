namespace Mobile.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Open Clerk sign-up hosted page with WebAuthenticator
        var signUpUrl = "https://clerk.yourdomain.com/sign-up"; // <-- Replace with your real Clerk sign-up URL
        var redirectUri = "findpet://callback";

        try
        {
            var result = await WebAuthenticator.AuthenticateAsync(
                new Uri(signUpUrl),
                new Uri(redirectUri)
            );
            // TODO: Handle token/session if needed
        }
        catch (Exception ex)
        {
            await DisplayAlert("Sign Up Error", ex.Message, "OK");
        }
    }
}