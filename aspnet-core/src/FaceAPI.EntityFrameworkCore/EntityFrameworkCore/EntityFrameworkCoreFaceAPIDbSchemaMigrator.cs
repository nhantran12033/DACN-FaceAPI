using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FaceAPI.Data;
using Volo.Abp.DependencyInjection;

namespace FaceAPI.EntityFrameworkCore;

public class EntityFrameworkCoreFaceAPIDbSchemaMigrator
    : IFaceAPIDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFaceAPIDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the FaceAPIDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FaceAPIDbContext>()
            .Database
            .MigrateAsync();
    }
}
