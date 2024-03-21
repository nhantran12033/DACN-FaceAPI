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

namespace FaceAPI.ScheduleDetails
{
    public class EfCoreScheduleDetailRepository : EfCoreScheduleDetailRepositoryBase, IScheduleDetailRepository
    {
        public EfCoreScheduleDetailRepository(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}