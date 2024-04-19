using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Schedules
{
    public partial interface IScheduleRepository : IRepository<Schedule, Guid>
    {
        Task<ScheduleWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);
        Task<ScheduleWithNavigationProperties> GetWithCodeNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);
        Task<List<ScheduleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            Guid? staffId = null,
            Guid? scheduleDetailId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Schedule>> GetListAsync(
                    string? filterText = null,
                    string? code = null,
                    string? name = null,
                    DateTime? dateFromMin = null,
                    DateTime? dateFromMax = null,
                    DateTime? dateToMin = null,
                    DateTime? dateToMax = null,
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
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            Guid? staffId = null,
            Guid? scheduleDetailId = null,
            CancellationToken cancellationToken = default);
    }
}