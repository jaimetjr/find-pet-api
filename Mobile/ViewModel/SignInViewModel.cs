using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModel
{
    public class SignInViewModel
    {
        private string email;
        private string password;
        private string emailError;
        private string passwordError;
        private string mainError;
        private bool isPassword = true;
        private bool isSubmitting;

        public string Email
        {
            get => email; set { email = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => password; set { password = value; OnPropertyChanged(); }
        }
        public string EmailError
        {
            get => emailError; set { emailError = value; OnPropertyChanged(); }
        }
        public string PasswordError
        {
            get => passwordError; set { passwordError = value; OnPropertyChanged(); }
        }
        public string MainError
        {
            get => mainError; set { mainError = value; OnPropertyChanged(); }
        }
        public bool HasMainError => !string.IsNullOrEmpty(MainError);

        public bool IsPassword
        {
            get => isPassword; set { isPassword = value; OnPropertyChanged(); }
        }
        public bool IsSubmitting
        {
            get => isSubmitting; set { isSubmitting = value; OnPropertyChanged(); }
        }
        public bool IsNotSubmitting => !IsSubmitting;
        public bool CanSubmit => !IsSubmitting;

        public string PasswordToggleText => IsPassword ? "Show" : "Hide";

        // Colors, theming, etc.
        public Color InputTextColor => Colors.Black;
        public Color EmailBorderColor => string.IsNullOrEmpty(EmailError) ? Colors.Gray : Colors.Crimson;
        public Color PasswordBorderColor => string.IsNullOrEmpty(PasswordError) ? Colors.Gray : Colors.Crimson;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async Task SignInAsync()
        {
            IsSubmitting = true;
            MainError = string.Empty;
            // TODO: Add validation (see below for Zod-like equivalent)
            if (string.IsNullOrEmpty(Email))
            {
                EmailError = "Email is required";
            }
            if (string.IsNullOrEmpty(Password))
            {
                PasswordError = "Password is required";
            }
            if (!string.IsNullOrEmpty(EmailError) || !string.IsNullOrEmpty(PasswordError))
            {
                IsSubmitting = false;
                return;
            }
            // TODO: Call Clerk API (or AuthService) and handle errors
            // Example:
            /*
            var result = await _clerkService.LoginAsync(Email, Password);
            if (!result.Success)
                MainError = result.ErrorMessage;
            */
            IsSubmitting = false;
        }
    }
}
