using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Staffs
{
    public partial interface IStaffRepository : IRepository<Staff, Guid>
    {
        Task<StaffWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);
        Task<StaffWithNavigationProperties> GetWithNavigationCodePropertiesAsync(
    string code,
    CancellationToken cancellationToken = default
);
        Task<List<StaffWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            Guid? timesheetId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Staff>> GetListAsync(
                    string? filterText = null,
                    string? image = null,
                    string? code = null,
                    string? name = null,
                    string? sex = null,
                    DateTime? birthdayMin = null,
                    DateTime? birthdayMax = null,
                    DateTime? startWorkMin = null,
                    DateTime? startWorkMax = null,
                    string? phone = null,
                    string? email = null,
                    string? address = null,
                    int? debtMin = null,
                    int? debtMax = null,
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
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            Guid? timesheetId = null,
            CancellationToken cancellationToken = default);
    }
}