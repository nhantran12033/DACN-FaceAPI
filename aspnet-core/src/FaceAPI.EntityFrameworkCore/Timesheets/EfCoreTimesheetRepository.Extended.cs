using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using FaceAPI.EntityFrameworkCore;

namespace FaceAPI.Timesheets
{
    public class EfCoreTimesheetRepository : EfCoreTimesheetRepositoryBase, ITimesheetRepository
    {
        public EfCoreTimesheetRepository(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}