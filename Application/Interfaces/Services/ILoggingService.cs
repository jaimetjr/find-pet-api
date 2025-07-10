namespace Application.Interfaces.Services
{
    public interface ILoggingService
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception? exception = null, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogCritical(string message, Exception? exception = null, params object[] args);
        
        // Structured logging methods
        void LogInformation(string message, object? data = null);
        void LogWarning(string message, object? data = null);
        void LogError(string message, Exception? exception = null, object? data = null);
        void LogDebug(string message, object? data = null);
        void LogCritical(string message, Exception? exception = null, object? data = null);
    }
} 