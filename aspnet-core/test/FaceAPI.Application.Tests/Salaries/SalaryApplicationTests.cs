using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace FaceAPI.Salaries
{
    public class SalariesAppServiceTests : FaceAPIApplicationTestBase
    {
        private readonly ISalariesAppService _salariesAppService;
        private readonly IRepository<Salary, Guid> _salaryRepository;

        public SalariesAppServiceTests()
        {
            _salariesAppService = GetRequiredService<ISalariesAppService>();
            _salaryRepository = GetRequiredService<IRepository<Salary, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salariesAppService.GetListAsync(new GetSalariesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Salary.Id == Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca")).ShouldBe(true);
            result.Items.Any(x => x.Salary.Id == Guid.Parse("f1d9fe0b-75b2-46af-bf89-984f26334dbd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salariesAppService.GetAsync(Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalaryCreateDto
            {
                Code = "313e10e14",
                Allowance = 1601818521,
                Basic = 1255821316,
                Bonus = 21848641,
                Total = 510901899
            };

            // Act
            var serviceResult = await _salariesAppService.CreateAsync(input);

            // Assert
            var result = await _salaryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("313e10e14");
            result.Allowance.ShouldBe(1601818521);
            result.Basic.ShouldBe(1255821316);
            result.Bonus.ShouldBe(21848641);
            result.Total.ShouldBe(510901899);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalaryUpdateDto()
            {
                Code = "74ca5f37b2eb4713958f742fc00398a13aea1dfcef2148c8bf6890a",
                Allowance = 1495114733,
                Basic = 2054687748,
                Bonus = 753640916,
                Total = 356002092
            };

            // Act
            var serviceResult = await _salariesAppService.UpdateAsync(Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"), input);

            // Assert
            var result = await _salaryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("74ca5f37b2eb4713958f742fc00398a13aea1dfcef2148c8bf6890a");
            result.Allowance.ShouldBe(1495114733);
            result.Basic.ShouldBe(2054687748);
            result.Bonus.ShouldBe(753640916);
            result.Total.ShouldBe(356002092);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salariesAppService.DeleteAsync(Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"));

            // Assert
            var result = await _salaryRepository.FindAsync(c => c.Id == Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"));

            result.ShouldBeNull();
        }
    }
}