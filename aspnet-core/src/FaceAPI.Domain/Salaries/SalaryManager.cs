using FaceAPI.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Salaries
{
    public abstract class SalaryManagerBase : DomainService
    {
        protected ISalaryRepository _salaryRepository;
        protected IRepository<Department, Guid> _departmentRepository;

        public SalaryManagerBase(ISalaryRepository salaryRepository,
        IRepository<Department, Guid> departmentRepository)
        {
            _salaryRepository = salaryRepository;
            _departmentRepository = departmentRepository;
        }

        public virtual async Task<Salary> CreateAsync(
        List<Guid> departmentIds,
        double allowance, double basic, double bonus, string? code = null)
        {

            var salary = new Salary(
             GuidGenerator.Create(),
             allowance, basic, bonus, code
             );

            await SetDepartmentsAsync(salary, departmentIds);

            return await _salaryRepository.InsertAsync(salary);
        }

        public virtual async Task<Salary> UpdateAsync(
            Guid id,
            List<Guid> departmentIds,
        double allowance, double basic, double bonus, string? code = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var queryable = await _salaryRepository.WithDetailsAsync(x => x.Departments);
            var query = queryable.Where(x => x.Id == id);

            var salary = await AsyncExecuter.FirstOrDefaultAsync(query);

            salary.Allowance = allowance;
            salary.Basic = basic;
            salary.Bonus = bonus;
            salary.Total = salary.Allowance + salary.Basic + salary.Bonus;
            salary.Code = code;

            await SetDepartmentsAsync(salary, departmentIds);

            salary.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salaryRepository.UpdateAsync(salary);
        }

        private async Task SetDepartmentsAsync(Salary salary, List<Guid> departmentIds)
        {
            if (departmentIds == null || !departmentIds.Any())
            {
                salary.RemoveAllDepartments();
                return;
            }

            var query = (await _departmentRepository.GetQueryableAsync())
                .Where(x => departmentIds.Contains(x.Id))
                .Select(x => x.Id);

            var departmentIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!departmentIdsInDb.Any())
            {
                return;
            }

            salary.RemoveAllDepartmentsExceptGivenIds(departmentIdsInDb);

            foreach (var departmentId in departmentIdsInDb)
            {
                salary.AddDepartment(departmentId);
            }
        }

    }
}