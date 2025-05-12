using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent: IBusinessEvent
    {
        public Sale Sale { get; }

        public SaleCancelledEvent(Sale user)
        {
            Sale = user;
        }
    }
}