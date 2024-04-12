using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Salaries
{
    public partial interface ISalaryRepository : IRepository<Salary, Guid>
    {
        Task<SalaryWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid departmentId, 
    Guid titleId,
    CancellationToken cancellationToken = default
);

        Task<List<SalaryWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Salary>> GetListAsync(
                    string? filterText = null,
                    string? code = null,
                    double? allowanceMin = null,
                    double? allowanceMax = null,
                    double? basicMin = null,
                    double? basicMax = null,
                    double? bonusMin = null,
                    double? bonusMax = null,
                    double? totalMin = null,
                    double? totalMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            CancellationToken cancellationToken = default);
    }
}