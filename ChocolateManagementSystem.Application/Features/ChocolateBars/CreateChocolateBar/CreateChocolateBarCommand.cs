using AutoMapper;
using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Domain.Entities;
using MediatR;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;

public class CreateChocolateBarCommand : IRequest<int>
{
    public string Name { get; set; }
    public decimal Cacao { get; set; }
    public decimal Price { get; set; }
    public int FactoryId { get; set; }
}

public class CreateChocolateBarCommandHandler : IRequestHandler<CreateChocolateBarCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IChocolateBarsRepository _chocolateBarsRepository;

    public CreateChocolateBarCommandHandler(IChocolateBarsRepository chocolateBarsRepository, IMapper mapper)
    {
        _chocolateBarsRepository = chocolateBarsRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateChocolateBarCommand request, CancellationToken cancellationToken)
    {
        var newChocolateBar = _mapper.Map<ChocolateBar>(request);
        newChocolateBar = await _chocolateBarsRepository.AddAsync(newChocolateBar, cancellationToken);
        return newChocolateBar.Id;
    }
}

