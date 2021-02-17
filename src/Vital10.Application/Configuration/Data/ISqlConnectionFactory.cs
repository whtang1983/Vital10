using System.Data;

namespace Vital10.Application.Configuration.Data{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
