using System.Threading.Tasks;

namespace FaceAPI.Data;

public interface IFaceAPIDbSchemaMigrator
{
    Task MigrateAsync();
}
