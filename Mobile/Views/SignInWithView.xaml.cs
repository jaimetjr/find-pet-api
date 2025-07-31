namespace Mobile.Views;

public partial class SignInWithView : ContentView
{
	public SignInWithView()
	{
		InitializeComponent();
	}

    private const string ClerkRedirectUri = "findpet://callback";
    private const string ClerkGoogleOAuthUrl = "https://clerk.yourdomain.com/oauth/social/google";
    private const string ClerkAppleOAuthUrl = "https://clerk.yourdomain.com/oauth/social/apple";
    private const string ClerkFacebookOAuthUrl = "https://clerk.yourdomain.com/oauth/social/facebook";

    // Google
    private async void OnGoogleClicked(object sender, EventArgs e)
    {
        await ClerkOAuthAsync(ClerkGoogleOAuthUrl);
    }

    // Apple
    private async void OnAppleClicked(object sender, EventArgs e)
    {
        await ClerkOAuthAsync(ClerkAppleOAuthUrl);
    }

    // Facebook
    private async void OnFacebookClicked(object sender, EventArgs e)
    {
        await ClerkOAuthAsync(ClerkFacebookOAuthUrl);
    }

    private async Task ClerkOAuthAsync(string oAuthUrl)
    {
        try
        {
            var result = await WebAuthenticator.AuthenticateAsync(
                new Uri(oAuthUrl),
                new Uri(ClerkRedirectUri));
            // TODO: handle token/session as you do in your auth flow
        }
        catch (Exception ex)
        {
            // Optionally handle/capture login errors
            //await Application.Current.MainPage.DisplayAlert("Login Error", ex.Message, "OK");
        }
    }
}