using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace FaceAPI.Titles
{
    public class TitlesAppServiceTests : FaceAPIApplicationTestBase
    {
        private readonly ITitlesAppService _titlesAppService;
        private readonly IRepository<Title, Guid> _titleRepository;

        public TitlesAppServiceTests()
        {
            _titlesAppService = GetRequiredService<ITitlesAppService>();
            _titleRepository = GetRequiredService<IRepository<Title, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _titlesAppService.GetListAsync(new GetTitlesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b246566e-22ba-43f0-928c-1a4d68328904")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _titlesAppService.GetAsync(Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TitleCreateDto
            {
                Code = "3b4e515ae48c4bb69540f1e0bfb3f24238b284b516d84f068e8f34460bae3f084b24e2791f7e46f0",
                Name = "d94c0abd11d24c1ba84e6398dbe7c47a828186122e4e4d669930a",
                Note = "057631b6938f45b8ab76aa2d"
            };

            // Act
            var serviceResult = await _titlesAppService.CreateAsync(input);

            // Assert
            var result = await _titleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("3b4e515ae48c4bb69540f1e0bfb3f24238b284b516d84f068e8f34460bae3f084b24e2791f7e46f0");
            result.Name.ShouldBe("d94c0abd11d24c1ba84e6398dbe7c47a828186122e4e4d669930a");
            result.Note.ShouldBe("057631b6938f45b8ab76aa2d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TitleUpdateDto()
            {
                Code = "b0da1d95ac4d4e88832248357c449e7f204c3dda197b4edf9602066d206cb77430b1b6",
                Name = "3dcecd8f1ee140448c496f1d12692165a60ad1c6e2df4d149ef6",
                Note = "aa29eb39c930459693bca66fd8703d0d1bfca69672c847da"
            };

            // Act
            var serviceResult = await _titlesAppService.UpdateAsync(Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"), input);

            // Assert
            var result = await _titleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b0da1d95ac4d4e88832248357c449e7f204c3dda197b4edf9602066d206cb77430b1b6");
            result.Name.ShouldBe("3dcecd8f1ee140448c496f1d12692165a60ad1c6e2df4d149ef6");
            result.Note.ShouldBe("aa29eb39c930459693bca66fd8703d0d1bfca69672c847da");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _titlesAppService.DeleteAsync(Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"));

            // Assert
            var result = await _titleRepository.FindAsync(c => c.Id == Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"));

            result.ShouldBeNull();
        }
    }
}