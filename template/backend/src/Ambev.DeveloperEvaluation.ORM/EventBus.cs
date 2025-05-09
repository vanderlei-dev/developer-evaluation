using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    /// <summary>
    /// Simple implementation of IEventBus that logs events to the console for demostration purposes.
    /// </summary>
    public class EventBus : IEventBus 
    {
        private ILogger<EventBus> _logger;

        public EventBus(ILogger<EventBus> logger)
        {
            _logger = logger;
        }

        public void Publish<T>(T @event) where T : class
        {
            _logger.LogInformation("Event published [{EventName}].", @event.GetType().Name);
        }
    }
}