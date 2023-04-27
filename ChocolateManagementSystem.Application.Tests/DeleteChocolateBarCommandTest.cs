using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;
using ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;
using ChocolateManagementSystem.Application.Tests.MockData;
using FluentAssertions;

namespace ChocolateManagementSystem.Application.Tests
{
    public class DeleteChocolateBarCommandTest
    {
        private readonly IChocolateBarsRepository _chocolateBarsRepository;
        private readonly DeleteChocolateBarCommandValidator _validator;
        private readonly DeleteChocolateBarCommandHandler _handler;

        public DeleteChocolateBarCommandTest()
        {
            _chocolateBarsRepository = new MockChocolateBarsRepository();
            _validator = new DeleteChocolateBarCommandValidator(_chocolateBarsRepository);
            _handler = new DeleteChocolateBarCommandHandler(_chocolateBarsRepository);
        }

        [Fact]
        public async Task Valid_ChocolateBar_Deleted()
        {
            var cmd = new DeleteChocolateBarCommand()
            {
               ChocolateBarId = 1
            };

            var validationRes = await _validator.ValidateAsync(cmd);
            await _handler.Handle(cmd, CancellationToken.None);
            var chocolatesList = await _chocolateBarsRepository.ListAllAsync(CancellationToken.None);

            validationRes.IsValid.Should().Be(true);
            chocolatesList.Count.Should().Be(2);
        }

        [Fact]
        public async Task InValid_ChocolateBar_Deleted()
        {
            var cmd = new DeleteChocolateBarCommand()
            {
                ChocolateBarId = 8
            };

            var validationRes = await _validator.ValidateAsync(cmd);
            validationRes.Errors.Should().NotBeEmpty();
            validationRes.Errors.First().ErrorMessage.Should().Be("The specified ChocolateBarId should exists.");
        }
    }
}
