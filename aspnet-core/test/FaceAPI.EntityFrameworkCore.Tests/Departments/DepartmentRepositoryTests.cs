using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using FaceAPI.Departments;
using FaceAPI.EntityFrameworkCore;
using Xunit;

namespace FaceAPI.Departments
{
    public class DepartmentRepositoryTests : FaceAPIEntityFrameworkCoreTestBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentRepositoryTests()
        {
            _departmentRepository = GetRequiredService<IDepartmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _departmentRepository.GetListAsync(
                    code: "b647dd069c254d1d869a56326b533aa9e98d396f76d440149ab54c4589e6fbcee61694576c96466d8167d0",
                    name: "a8d3a1ea6421408da711d469fe2366e877d288c57",
                    note: "4d33cab6742140c0ba1e659491902c324d21317fba374031a8d83daeaee23cbc6"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _departmentRepository.GetCountAsync(
                    code: "a326b0b0db47432aa548c1b98457333f78b257b109bb48609fd8305b957618b0",
                    name: "553a6b1ed1",
                    note: "5750a2a8724c4af6bc401543a5f2c6d7b8d682fd5f3c4162a75bb77093"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}