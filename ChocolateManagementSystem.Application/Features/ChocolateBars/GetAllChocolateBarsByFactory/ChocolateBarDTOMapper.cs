using AutoMapper;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBarsByFactory;

public class ChocolateBarDTOMapper : Profile
{
    public ChocolateBarDTOMapper()
    {
        CreateMap<ChocolateBar, ChocolateBarDTO>();
    }
}