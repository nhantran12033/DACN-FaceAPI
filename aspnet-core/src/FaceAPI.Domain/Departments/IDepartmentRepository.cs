using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Departments
{
    public partial interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<DepartmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DepartmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            Guid? titleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Department>> GetListAsync(
                    string? filterText = null,
                    string? code = null,
                    string? name = null,
                    DateTime? dateMin = null,
                    DateTime? dateMax = null,
                    string? note = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            Guid? titleId = null,
            CancellationToken cancellationToken = default);
    }
}