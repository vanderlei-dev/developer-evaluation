using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing the <see cref="GetSaleCommand"/> requests.
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSaleCommand, SaleDto>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="GetSaleCommand"/>.
    /// </summary>
    /// <param name="repository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public GetSaleHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;        
    }

    /// <summary>
    /// Handles the <see cref="GetSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The <see cref="GetSaleCommand"/> command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The <see cref="SaleDto"/> if found.</returns>
    public async Task<SaleDto> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {        
        var sale = await _repository.GetByIdWithItemsAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        var result = _mapper.Map<SaleDto>(sale);

        return result;
    }
}
