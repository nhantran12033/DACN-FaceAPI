using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using FaceAPI.Salaries;
using FaceAPI.EntityFrameworkCore;
using Xunit;

namespace FaceAPI.Salaries
{
    public class SalaryRepositoryTests : FaceAPIEntityFrameworkCoreTestBase
    {
        private readonly ISalaryRepository _salaryRepository;

        public SalaryRepositoryTests()
        {
            _salaryRepository = GetRequiredService<ISalaryRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salaryRepository.GetListAsync(
                    code: "7c67fa525d81415eae2b4a7cac2dce0a6b8679e450fc4bc7bcd9136f003c58ffd63696e39"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salaryRepository.GetCountAsync(
                    code: "e41aa84ed745461684dc1b1220"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}