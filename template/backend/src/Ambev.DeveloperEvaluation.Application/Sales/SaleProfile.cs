using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Profile for mapping sale related entities
/// </summary>
public class SaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for sales operations
    /// </summary>
    public SaleProfile()
    {
        CreateMap<SaleDto, Sale>();                        
        CreateMap<Sale, SaleDto>();

        CreateMap<SaleItemDto, SaleItem>();
        CreateMap<SaleItem, SaleItemDto>();
    }
}
