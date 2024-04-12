using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.ScheduleDetails
{
    public partial interface IScheduleDetailRepository : IRepository<ScheduleDetail, Guid>
    {
        Task<ScheduleDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ScheduleDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            Guid? scheduleFormatId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ScheduleDetail>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    DateTime? fromDateMin = null,
                    DateTime? fromDateMax = null,
                    DateTime? toDateMin = null,
                    DateTime? toDateMax = null,
                    string? note = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            Guid? scheduleFormatId = null,
            CancellationToken cancellationToken = default);
    }
}