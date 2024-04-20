using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Timesheets
{
    public partial interface ITimesheetRepository : IRepository<Timesheet, Guid>
    {
        Task<TimesheetWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);
        Task<List<TimesheetWithNavigationProperties>> GetWithNavigationActivePropertiesAsync(
    Guid scheduleDetailId,
    CancellationToken cancellationToken = default
);
        Task<List<TimesheetWithNavigationProperties>> GetWithCodeNavigationPropertiesAsync(
    string code,
    CancellationToken cancellationToken = default
);
        Task<string> GenerateNextCode();
        Task<List<TimesheetWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            DateTime? timeMin = null,
            DateTime? timeMax = null,
            string? note = null,
            Guid? scheduleId = null,
            Guid? scheduleDetailId = null,
            Guid? scheduleFormatId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Timesheet>> GetListAsync(
                    string? filterText = null,
                    string? image = null,
                    string? code = null,
                    DateTime? timeMin = null,
                    DateTime? timeMax = null,
                    string? note = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            DateTime? timeMin = null,
            DateTime? timeMax = null,
            string? note = null,
            Guid? scheduleId = null,
            Guid? scheduleDetailId = null,
            Guid? scheduleFormatId = null,
            CancellationToken cancellationToken = default);
    }
}