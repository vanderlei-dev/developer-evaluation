namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Interface for an event bus that handles publishing events.     
    /// </summary>
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : class;
    }
}