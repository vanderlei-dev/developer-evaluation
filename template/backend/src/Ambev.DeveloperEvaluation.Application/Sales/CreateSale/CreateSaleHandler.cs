using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing the <see cref="CreateSaleCommand"/> requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleDto>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleHandler"/>.
    /// </summary>
    /// <param name="repository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public CreateSaleHandler(ISaleRepository repository, IMapper mapper, IEventBus eventBus)
    {
        _repository = repository;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    /// <summary>
    /// Handles the <see cref="CreateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The <see cref="CreateSaleCommand"/> command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created <see cref="SaleDto"/> details.</returns>
    public async Task<SaleDto> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(command.SaleDto);
        sale.CalculateTotal();

        var createdSale = await _repository.CreateAsync(sale, cancellationToken);
        _eventBus.Publish(new SaleCreatedEvent(createdSale));

        var result = _mapper.Map<SaleDto>(createdSale);

        return result;
    }
}
