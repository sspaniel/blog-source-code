using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Abstractions.Services;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Internal;
using Microsoft.Extensions.Logging;
using System;

namespace AdventureShare.Core.Implementations.RequestHandling
{
    public class GlobalErrorHandler : IErrorHandler
    {
        private readonly IMessageService _messageService;
        private readonly ILogger _logger;

        public GlobalErrorHandler(IMessageService messageService, ILoggerFactory loggerFactory)
        {
            _messageService = messageService;
            _logger = loggerFactory.CreateLogger("Adventure Share");
        }

        public void UserLoginFailed(string message, Exception error, CreateUserToken request)
        {
            try
            {
                var failure = new Failure
                {
                    Source = "Adventure Share",
                    Message = message,
                    Error = error,
                    Data = request
                };

                _messageService.PublishFailure(failure);
            }
            catch (Exception unhandledException)
            {
                _logger.LogError(unhandledException, "error handler failed");
            }
        }

        public void UpdateUserPasswordFailed(string message, Exception error, UpdateUserPassword request)
        {
            try
            {
                var failure = new Failure
                {
                    Source = "Adventure Share",
                    Message = message,
                    Error = error,
                    Data = request
                };

                _messageService.PublishFailure(failure);
            }
            catch (Exception unhandledException)
            {
                _logger.LogError(unhandledException, "error handler failed");
            }
        }
    }
}
