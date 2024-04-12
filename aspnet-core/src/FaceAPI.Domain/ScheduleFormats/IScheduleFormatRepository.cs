using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.ScheduleFormats
{
    public partial interface IScheduleFormatRepository : IRepository<ScheduleFormat, Guid>
    {
        Task<List<ScheduleFormat>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            int? fromHoursMin = null,
            int? fromHoursMax = null,
            int? toHoursMin = null,
            int? toHoursMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            int? fromHoursMin = null,
            int? fromHoursMax = null,
            int? toHoursMin = null,
            int? toHoursMax = null,
            string? note = null,
            CancellationToken cancellationToken = default);
    }
}