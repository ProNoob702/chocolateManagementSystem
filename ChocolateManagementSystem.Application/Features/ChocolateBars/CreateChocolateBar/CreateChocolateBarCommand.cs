using ChocolateManagementSystem.Persistence.Context;
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

    public CreateChocolateBarCommandHandler(IChocolateSystemContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateChocolateBarCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

