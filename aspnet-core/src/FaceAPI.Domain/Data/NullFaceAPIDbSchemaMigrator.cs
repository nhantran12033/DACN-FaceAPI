using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FaceAPI.Data;

/* This is used if database provider does't define
 * IFaceAPIDbSchemaMigrator implementation.
 */
public class NullFaceAPIDbSchemaMigrator : IFaceAPIDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
