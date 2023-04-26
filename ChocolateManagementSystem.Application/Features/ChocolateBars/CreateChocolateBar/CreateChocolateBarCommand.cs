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
    private readonly IChocolateSystemContext _context;
    private readonly IMapper _mapper;

    public CreateChocolateBarCommandHandler(IChocolateSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateChocolateBarCommand request, CancellationToken cancellationToken)
    {
        var newChocolateBar = _mapper.Map<ChocolateBar>(request);
        _context.ChocolateBars.Add(newChocolateBar);
        await _context.SaveChangesAsync(cancellationToken);
        return newChocolateBar.Id;
    }
}

