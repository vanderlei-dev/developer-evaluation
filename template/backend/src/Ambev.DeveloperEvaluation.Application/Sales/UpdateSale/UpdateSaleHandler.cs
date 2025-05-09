using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing the <see cref="UpdateSaleCommand"/> requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleDto>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateSaleHandler"/>.
    /// </summary>
    /// <param name="repository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public UpdateSaleHandler(ISaleRepository repository, IMapper mapper, IEventBus eventBus)
    {
        _repository = repository;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Handles the <see cref="UpdateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The <see cref="UpdateSaleCommand"/> command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated <see cref="SaleDto"/> details.</returns>
    public async Task<SaleDto> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var existingSale = await _repository.GetByIdWithItemsAsync(command.SaleDto.Id, cancellationToken);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {command.SaleDto.Id} not found");

        var updatedSale = _mapper.Map(command.SaleDto, existingSale);

        updatedSale.CalculateTotal();
        updatedSale.CheckCancelledItems();

        await _repository.UpdateAsync(updatedSale, cancellationToken);

        if (updatedSale.Cancelled)
            _eventBus.Publish(new SaleCancelledEvent(updatedSale));
        else
            _eventBus.Publish(new SaleModifiedEvent(updatedSale));

        var result = _mapper.Map<SaleDto>(updatedSale);
        return result;
    }
}
