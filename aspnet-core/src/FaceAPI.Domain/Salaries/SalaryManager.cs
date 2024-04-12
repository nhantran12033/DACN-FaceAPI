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

        public SalaryManagerBase(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public virtual async Task<Salary> CreateAsync(
        Guid? departmentId, Guid? titleId, double allowance, double basic, double bonus, string? code = null)
        {

            var salary = new Salary(
             GuidGenerator.Create(),
             departmentId, titleId, allowance, basic, bonus, code
             );

            return await _salaryRepository.InsertAsync(salary);
        }

        public virtual async Task<Salary> UpdateAsync(
            Guid id,
            Guid? departmentId, Guid? titleId, double allowance, double basic, double bonus, string? code = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var salary = await _salaryRepository.GetAsync(id);

            salary.DepartmentId = departmentId;
            salary.TitleId = titleId;
            salary.Allowance = allowance;
            salary.Basic = basic;
            salary.Bonus = bonus;
            salary.Total = allowance + basic + bonus;
            salary.Code = code;

            salary.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salaryRepository.UpdateAsync(salary);
        }

    }
}