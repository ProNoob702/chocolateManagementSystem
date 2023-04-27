using AutoMapper;
using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;
using ChocolateManagementSystem.Application.Tests.MockData;
using FluentAssertions;

namespace ChocolateManagementSystem.Application.Tests
{
    public class CreateChocolateBarCommandTest
    {
        private readonly IMapper _mapper;
        private readonly IChocolateBarsRepository _chocolateBarsRepository;
        private readonly IChocolateFactoryRepository _chocolateFactoryRepository;
        private readonly CreateChocolateBarCommandValidator _validator;
        private readonly CreateChocolateBarCommandHandler _handler;

        public CreateChocolateBarCommandTest()
        {
            _chocolateBarsRepository = new MockChocolateBarsRepository();
            _chocolateFactoryRepository = new MockChocolateFactoryRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CreateChocolateBarMapper>();
            });

            _mapper = mapperConfig.CreateMapper();

            _validator = new CreateChocolateBarCommandValidator(_chocolateBarsRepository, _chocolateFactoryRepository);
            _handler = new CreateChocolateBarCommandHandler(_chocolateBarsRepository, _mapper);
        }

        [Fact]
        public async Task Valid_ChocolateBar_Added()
        {
            var cmd = new CreateChocolateBarCommand()
            {
                Name = "Chocotest",
                Cacao = 9.5M,
                Price = 10,
                FactoryId = 1,
            };

            var validationRes = await _validator.ValidateAsync(cmd);
            var result = await _handler.Handle(cmd, CancellationToken.None);
            var chocolatesList = await _chocolateBarsRepository.ListAllAsync(CancellationToken.None);

            validationRes.IsValid.Should().Be(true);
            result.Should().BeOfType(typeof(int));
            result.Should().Be(4);
            chocolatesList.Count.Should().Be(4);
        }

        [Fact]
        public async Task InvalidFactoryId_ChocolateBar_Added()
        {
            var cmd = new CreateChocolateBarCommand()
            {
                Name = "Chocotest",
                Cacao = 9.5M,
                Price = 10,
                FactoryId = 8,
            };

            var validationRes = await _validator.ValidateAsync(cmd);
            validationRes.Errors.Should().NotBeEmpty();
            validationRes.Errors.First().ErrorMessage.Should().Be("The specified factory should exists.");
        }
    }
}