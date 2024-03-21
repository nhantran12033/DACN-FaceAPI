using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.ScheduleDetails
{
    public partial interface IScheduleDetailRepository : IRepository<ScheduleDetail, Guid>
    {
        Task<List<ScheduleDetail>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromMin = null,
            DateTime? fromMax = null,
            DateTime? toMin = null,
            DateTime? toMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromMin = null,
            DateTime? fromMax = null,
            DateTime? toMin = null,
            DateTime? toMax = null,
            string? note = null,
            CancellationToken cancellationToken = default);
    }
}