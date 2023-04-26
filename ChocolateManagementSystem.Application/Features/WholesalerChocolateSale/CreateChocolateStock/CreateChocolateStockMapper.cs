using AutoMapper;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;


public class CreateChocolateStockMapper : Profile
{
    public CreateChocolateStockMapper()
    {
        CreateMap<CreateChocolateStockCommand, WholesalerChocolateStock>();
    }
}