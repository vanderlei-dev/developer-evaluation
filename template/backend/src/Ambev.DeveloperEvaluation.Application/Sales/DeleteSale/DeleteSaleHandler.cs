using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing the <see cref="DeleteSaleHandler"/> requests.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteSaleCommand"/>.
    /// </summary>
    /// <param name="repository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public DeleteSaleHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;        
    }

    /// <summary>
    /// Handles the <see cref="DeleteSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The <see cref="DeleteSaleCommand"/> command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<bool> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {        
        var result = await _repository.DeleteAsync(command.Id, cancellationToken);
        if (result is false)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");        

        return result;
    }
}
