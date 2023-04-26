using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;

public class DeleteChocolateBarCommandValidator : AbstractValidator<DeleteChocolateBarCommand>
{

    private readonly IChocolateSystemContext _context;

    public DeleteChocolateBarCommandValidator(IChocolateSystemContext context)
    {
        _context = context;

        RuleFor(v => v.ChocolateBarId)
            .NotNull().WithMessage("ChocolateBarId is required.")
            .MustAsync(ChocolateBarIdShouldExists).WithMessage("The specified ChocolateBarId should exists.");
    }

    public async Task<bool> ChocolateBarIdShouldExists(int chocolateId, CancellationToken cancellationToken)
    {
        return await _context.ChocolateBars.FirstOrDefaultAsync(x => x.Id == chocolateId, cancellationToken) != null;
    }
}