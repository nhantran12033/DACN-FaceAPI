using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Titles
{
    public partial interface ITitleRepository : IRepository<Title, Guid>
    {
        Task<List<Title>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
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
            string? note = null,
            CancellationToken cancellationToken = default);
    }
}