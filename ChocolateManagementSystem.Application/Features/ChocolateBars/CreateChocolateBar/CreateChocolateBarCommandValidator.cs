using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;

public class CreateChocolateBarCommandValidator : AbstractValidator<CreateChocolateBarCommand> 
{ 

    private readonly IChocolateSystemContext _context;

    public CreateChocolateBarCommandValidator(IChocolateSystemContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

        RuleFor(v => v.Cacao)
           .NotNull().WithMessage("Cacao is required.")
           .InclusiveBetween(1, 100).WithMessage("Cacao percentage must be between 1,100.");

        RuleFor(v => v.Price)
          .NotNull().WithMessage("Price is required.")
          .GreaterThan(0).WithMessage("Price must be positive.");

        RuleFor(v => v.FactoryId)
         .NotNull().WithMessage("FactoryId is required.")
          .MustAsync(FactoryShouldExists).WithMessage("The specified factory should exists.");
    }

    public async Task<bool> BeUniqueName(string newChocolateName, CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.AllAsync(x => x.Name != newChocolateName, cancellationToken);
    }

    public async Task<bool> FactoryShouldExists(int factoryId, CancellationToken cancellationToken)
    {
        return await _context.ChocolateFactories.FirstOrDefaultAsync(x => x.Id == factoryId, cancellationToken) != null;
    }
}
