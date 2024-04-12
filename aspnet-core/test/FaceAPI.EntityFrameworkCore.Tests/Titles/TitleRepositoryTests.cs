using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using FaceAPI.Titles;
using FaceAPI.EntityFrameworkCore;
using Xunit;

namespace FaceAPI.Titles
{
    public class TitleRepositoryTests : FaceAPIEntityFrameworkCoreTestBase
    {
        private readonly ITitleRepository _titleRepository;

        public TitleRepositoryTests()
        {
            _titleRepository = GetRequiredService<ITitleRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _titleRepository.GetListAsync(
                    code: "1dbac957490b47be8c208532ce7380e22b0f688b8f9",
                    name: "4a7c84f7b98a420a9e4403c40a3cd8452aa2ed2dc14b4e42bfd7620b2b3",
                    note: "d76a39a6"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _titleRepository.GetCountAsync(
                    code: "89f86dbba85f4",
                    name: "0b82071c76134fc6929c0f8125d07679193eb0d1a4f84440",
                    note: "e03357d69f2e4193adde6f238e5fd6b4b784991009e6405e9921eabcc45e8cb03380fdbc5cb04916"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}