using FaceAPI.Titles;
using FaceAPI.Departments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using FaceAPI.Salaries;

namespace FaceAPI.Salaries
{
    public class SalariesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DepartmentsDataSeedContributor _departmentsDataSeedContributor;

        private readonly TitlesDataSeedContributor _titlesDataSeedContributor;

        public SalariesDataSeedContributor(ISalaryRepository salaryRepository, IUnitOfWorkManager unitOfWorkManager, DepartmentsDataSeedContributor departmentsDataSeedContributor, TitlesDataSeedContributor titlesDataSeedContributor)
        {
            _salaryRepository = salaryRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _departmentsDataSeedContributor = departmentsDataSeedContributor; _titlesDataSeedContributor = titlesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _departmentsDataSeedContributor.SeedAsync(context);
            await _titlesDataSeedContributor.SeedAsync(context);

            await _salaryRepository.InsertAsync(new Salary
            (
                id: Guid.Parse("6a893885-fe12-4a29-b8fb-2166312e1bca"),
                code: "7c67fa525d81415eae2b4a7cac2dce0a6b8679e450fc4bc7bcd9136f003c58ffd63696e39",
                allowance: 1778975207,
                basic: 4390915,
                bonus: 1346047789,
                departmentId: null,
                titleId: null
            ));

            
            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}