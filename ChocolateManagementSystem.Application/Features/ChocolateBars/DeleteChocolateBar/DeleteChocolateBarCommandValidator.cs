using ChocolateManagementSystem.Application.Common.Interfaces;
using FluentValidation;

namespace ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;

public class DeleteChocolateBarCommandValidator : AbstractValidator<DeleteChocolateBarCommand>
{

    private readonly IChocolateBarsRepository _chocolateBarsRepository;

    public DeleteChocolateBarCommandValidator(IChocolateBarsRepository chocolateBarsRepository)
    {
       _chocolateBarsRepository = chocolateBarsRepository;

        RuleFor(v => v.ChocolateBarId)
            .NotNull().WithMessage("ChocolateBarId is required.")
            .MustAsync(ChocolateBarIdShouldExists).WithMessage("The specified ChocolateBarId should exists.");
    }

    public async Task<bool> ChocolateBarIdShouldExists(int chocolateId, CancellationToken cancellationToken)
    {
        return await _chocolateBarsRepository.GetByIdAsync(chocolateId, cancellationToken) != null;
    }
}