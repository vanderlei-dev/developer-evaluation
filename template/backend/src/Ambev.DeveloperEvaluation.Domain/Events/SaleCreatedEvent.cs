using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : IBusinessEvent
    {
        public Sale Sale { get; }

        public SaleCreatedEvent(Sale user)
        {
            Sale = user;
        }
    }
}