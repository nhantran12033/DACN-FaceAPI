using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace FaceAPI.Departments
{
    public class DepartmentsAppServiceTests : FaceAPIApplicationTestBase
    {
        private readonly IDepartmentsAppService _departmentsAppService;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public DepartmentsAppServiceTests()
        {
            _departmentsAppService = GetRequiredService<IDepartmentsAppService>();
            _departmentRepository = GetRequiredService<IRepository<Department, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _departmentsAppService.GetListAsync(new GetDepartmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Department.Id == Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2")).ShouldBe(true);
            result.Items.Any(x => x.Department.Id == Guid.Parse("e0f9f448-881f-4013-94ca-df88c9fa759e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _departmentsAppService.GetAsync(Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DepartmentCreateDto
            {
                Code = "cfae4751a02f41b689a893cdbe1c6427f244319e7cb8420a8376ab7c97b6fb66f72b570a729b4221af",
                Name = "9538adf0377f4479b3cce86524c062881",
                Date = new DateTime(2011, 1, 19),
                Note = "3848c5163af14df6a984983bac938015d0afdafaff4744a4b50a72ece999b8db98a1ee9ae597485bb4"
            };

            // Act
            var serviceResult = await _departmentsAppService.CreateAsync(input);

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("cfae4751a02f41b689a893cdbe1c6427f244319e7cb8420a8376ab7c97b6fb66f72b570a729b4221af");
            result.Name.ShouldBe("9538adf0377f4479b3cce86524c062881");
            result.Date.ShouldBe(new DateTime(2011, 1, 19));
            result.Note.ShouldBe("3848c5163af14df6a984983bac938015d0afdafaff4744a4b50a72ece999b8db98a1ee9ae597485bb4");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DepartmentUpdateDto()
            {
                Code = "d742df5f2e664082b2703c715d79c8ad224318f68dd",
                Name = "b7c1d536fa2d428185",
                Date = new DateTime(2007, 10, 5),
                Note = "f275ad0212b443a483bf21dbf720d146eaf114d7c4b"
            };

            // Act
            var serviceResult = await _departmentsAppService.UpdateAsync(Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"), input);

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d742df5f2e664082b2703c715d79c8ad224318f68dd");
            result.Name.ShouldBe("b7c1d536fa2d428185");
            result.Date.ShouldBe(new DateTime(2007, 10, 5));
            result.Note.ShouldBe("f275ad0212b443a483bf21dbf720d146eaf114d7c4b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _departmentsAppService.DeleteAsync(Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"));

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == Guid.Parse("c9488700-be07-443f-b0c3-edb970564bc2"));

            result.ShouldBeNull();
        }
    }
}