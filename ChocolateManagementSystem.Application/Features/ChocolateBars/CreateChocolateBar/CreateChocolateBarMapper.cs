using AutoMapper;
using ChocolateManagementSystem.Domain.Entities;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;

public class CreateChocolateBarMapper : Profile
{
    public CreateChocolateBarMapper()
    {
        CreateMap<CreateChocolateBarCommand, ChocolateBar>();
    }
}