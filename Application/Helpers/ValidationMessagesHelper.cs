using System.Resources;

namespace Application.Helpers
{
    public static class ValidationMessagesHelper
    {
        private static readonly ResourceManager ResourceManager = 
            new ResourceManager("Application.Resources.ValidationMessages", typeof(ValidationMessagesHelper).Assembly);

        public static string GetMessage(string key)
        {
            return ResourceManager.GetString(key) ?? key;
        }

        public static string GetMessage(string key, string culture)
        {
            return ResourceManager.GetString(key, new System.Globalization.CultureInfo(culture)) ?? key;
        }
    }
} 