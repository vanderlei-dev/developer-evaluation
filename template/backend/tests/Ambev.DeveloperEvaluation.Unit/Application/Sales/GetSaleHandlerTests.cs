using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;    
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.    
    /// </summary>
    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();        
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid get sale request is handled successfully.
    /// </summary>    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());
        Sale sale = new Sale();

        _mapper.Map<SaleDto>(sale).Returns(new SaleDto());

        _saleRepository.GetByIdWithItemsAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        
        // Act
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        saleResult.Should().NotBeNull();
        await _saleRepository.Received(1).GetByIdWithItemsAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());        
    }

    /// <summary>
    /// Tests that a invalid sale get request throws exception.
    /// </summary>    
    [Fact]
    public async Task Handle_InvalidReques_ThrowsException()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());

        // Act        
        var handle = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await handle.Should().ThrowAsync<KeyNotFoundException>();        
    }
}
