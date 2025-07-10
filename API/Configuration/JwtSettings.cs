namespace API.Configuration
{
    public class JwtSettings
    {
        public string Authority { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; } = false;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
    }

    public class AzureBlobStorageSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string ContainerName { get; set; } = string.Empty;
    }

    public class LocalizationSettings
    {
        public string DefaultLocalization { get; set; } = "en";
        public string[] SupportedCultures { get; set; } = { "en", "pt-BR" };
    }
} 