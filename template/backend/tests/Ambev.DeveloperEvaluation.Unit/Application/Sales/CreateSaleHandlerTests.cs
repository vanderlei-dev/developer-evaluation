using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.    
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventBus = Substitute.For<IEventBus>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _eventBus);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>    
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CreateSaleCommand(SaleTestData.GenerateValidSale());
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

        _mapper.Map<Sale>(saleDto).Returns(sale);
        _mapper.Map<SaleDto>(sale).Returns(saleDto);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act
        var saleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        saleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _eventBus.Received(1).Publish(Arg.Any<SaleCreatedEvent>());
    }
}
