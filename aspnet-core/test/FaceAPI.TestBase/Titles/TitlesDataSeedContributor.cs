using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using FaceAPI.Titles;

namespace FaceAPI.Titles
{
    public class TitlesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITitleRepository _titleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TitlesDataSeedContributor(ITitleRepository titleRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _titleRepository = titleRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _titleRepository.InsertAsync(new Title
            (
                id: Guid.Parse("f4502bc7-2b85-4327-8a46-37d33b662153"),
                code: "1dbac957490b47be8c208532ce7380e22b0f688b8f9",
                name: "4a7c84f7b98a420a9e4403c40a3cd8452aa2ed2dc14b4e42bfd7620b2b3",
                note: "d76a39a6"
            ));

            await _titleRepository.InsertAsync(new Title
            (
                id: Guid.Parse("b246566e-22ba-43f0-928c-1a4d68328904"),
                code: "89f86dbba85f4",
                name: "0b82071c76134fc6929c0f8125d07679193eb0d1a4f84440",
                note: "e03357d69f2e4193adde6f238e5fd6b4b784991009e6405e9921eabcc45e8cb03380fdbc5cb04916"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}