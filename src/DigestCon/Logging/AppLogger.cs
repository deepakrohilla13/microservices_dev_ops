using System;
using Microsoft.Extensions.Logging;

namespace DigestCon.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private ILogger<T> _logger;

        public AppLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Fatal(string message)
        {
            _logger.LogCritical(message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Trace(string message)
        {
            _logger.LogCritical(message);
        }





        public void Debug(string message, Exception ex)
        {
            _logger.LogDebug(message, ex);
        }

        public void Error(string message, Exception ex)
        {
            _logger.LogError(message, ex);
        }

        public void Fatal(string message, Exception ex)
        {
            _logger.LogCritical(message, ex);
        }

        public void Info(string message, Exception ex)
        {
            _logger.LogInformation(message, ex);
        }

        public void Trace(string message, Exception ex)
        {
            _logger.LogCritical(message, ex);
        }
    }
}