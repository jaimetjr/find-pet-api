using Application.Interfaces.Services;
using Serilog;

namespace Infrastructure.Services
{
    public class SerilogLoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public SerilogLoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void LogError(string message, Exception? exception = null, params object[] args)
        {
            if (exception != null)
                _logger.Error(exception, message, args);
            else
                _logger.Error(message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void LogCritical(string message, Exception? exception = null, params object[] args)
        {
            if (exception != null)
                _logger.Fatal(exception, message, args);
            else
                _logger.Fatal(message, args);
        }

        // Structured logging methods
        public void LogInformation(string message, object? data = null)
        {
            if (data != null)
                _logger.Information(message, data);
            else
                _logger.Information(message);
        }

        public void LogWarning(string message, object? data = null)
        {
            if (data != null)
                _logger.Warning(message, data);
            else
                _logger.Warning(message);
        }

        public void LogError(string message, Exception? exception = null, object? data = null)
        {
            if (exception != null && data != null)
                _logger.Error(exception, message, data);
            else if (exception != null)
                _logger.Error(exception, message);
            else if (data != null)
                _logger.Error(message, data);
            else
                _logger.Error(message);
        }

        public void LogDebug(string message, object? data = null)
        {
            if (data != null)
                _logger.Debug(message, data);
            else
                _logger.Debug(message);
        }

        public void LogCritical(string message, Exception? exception = null, object? data = null)
        {
            if (exception != null && data != null)
                _logger.Fatal(exception, message, data);
            else if (exception != null)
                _logger.Fatal(exception, message);
            else if (data != null)
                _logger.Fatal(message, data);
            else
                _logger.Fatal(message);
        }
    }
} 