using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;    
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.    
    /// </summary>
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();         
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    /// <summary>
    /// Tests that a valid delete sale request is handled successfully.
    /// </summary>    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.NewGuid());        

        _saleRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);
        
        // Act
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        saleResult.Should().BeTrue();
        await _saleRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());        
    }

    /// <summary>
    /// Tests that a invalid sale get request throws exception.
    /// </summary>    
    [Fact]
    public async Task Handle_InvalidReques_ThrowsException()
    {
        // Arrange
        var command = new DeleteSaleCommand(Guid.NewGuid());

        // Act        
        var handle = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await handle.Should().ThrowAsync<KeyNotFoundException>();        
    }
}
