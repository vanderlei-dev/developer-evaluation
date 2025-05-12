using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.    
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventBus = Substitute.For<IEventBus>();
        _handler = new UpdateSaleHandler(_saleRepository, _mapper, _eventBus);
    }

    /// <summary>
    /// Tests that a valid sale update request without cancelled items is handled successfully.
    /// </summary>    
    [Fact]
    public async Task Handle_ValidRequestWithoutCancelledItems_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new UpdateSaleCommand(SaleTestData.GenerateValidSale(cancelled: false));
        var saleDto = command.SaleDto;

        var sale = new Sale
        {
            Number = saleDto.Number,
            BranchId = saleDto.BranchId,
            UserId = saleDto.UserId,
            Items = saleDto.Items.Select(saleDto => new SaleItem
            {
                ProductId = saleDto.ProductId,
                Quantity = saleDto.Quantity,
                UnitPrice = saleDto.UnitPrice,
                Discount = saleDto.Discount ?? 0,
                Total = saleDto.Total ?? 0,
                Cancelled = saleDto.Cancelled ?? false
            }).ToList()
        };

        _mapper.Map(saleDto, sale).Returns(sale);
        _mapper.Map<SaleDto>(sale).Returns(saleDto);

        _saleRepository.GetByIdWithItemsAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        saleResult.Should().NotBeNull();
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _eventBus.Received(1).Publish(Arg.Any<SaleModifiedEvent>());
    }

    /// <summary>
    /// Tests that a valid sale update request with cancelled items is handled successfully.
    /// </summary>    
    [Fact]
    public async Task Handle_ValidRequestWithCancelledItems_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new UpdateSaleCommand(SaleTestData.GenerateValidSale(cancelled: true));
        var saleDto = command.SaleDto;

        var sale = new Sale
        {
            Number = saleDto.Number,
            BranchId = saleDto.BranchId,
            UserId = saleDto.UserId,
            Items = saleDto.Items.Select(saleDto => new SaleItem
            {
                ProductId = saleDto.ProductId,
                Quantity = saleDto.Quantity,
                UnitPrice = saleDto.UnitPrice,
                Discount = saleDto.Discount ?? 0,
                Total = saleDto.Total ?? 0,
                Cancelled = saleDto.Cancelled ?? false
            }).ToList()
        };

        _mapper.Map(saleDto, sale).Returns(sale);
        _mapper.Map<SaleDto>(sale).Returns(saleDto);

        _saleRepository.GetByIdWithItemsAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        saleResult.Should().NotBeNull();
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _eventBus.Received(1).Publish(Arg.Any<SaleCancelledEvent>());
    }

    /// <summary>
    /// Tests that a invalid sale update request throws exception.
    /// </summary>    
    [Fact]
    public async Task Handle_InvalidReques_ThrowsException()
    {
        // Arrange
        var command = new UpdateSaleCommand(SaleTestData.GenerateValidSale(cancelled: false));

        // Act        
        var handle = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await handle.Should().ThrowAsync<KeyNotFoundException>();        
    }
}
