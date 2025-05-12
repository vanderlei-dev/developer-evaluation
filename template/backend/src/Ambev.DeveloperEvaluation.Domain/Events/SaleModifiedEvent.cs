using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : IBusinessEvent
    {
        public Sale Sale { get; }

        public SaleModifiedEvent(Sale user)
        {
            Sale = user;
        }
    }
}