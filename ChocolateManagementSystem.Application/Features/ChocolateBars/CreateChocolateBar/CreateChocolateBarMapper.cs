using AutoMapper;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;

public class ChocolateBarDTOMapper : Profile
{
    public ChocolateBarDTOMapper()
    {
        CreateMap<CreateChocolateBarCommand, ChocolateBar>();
    }
}