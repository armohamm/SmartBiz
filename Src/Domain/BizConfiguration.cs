using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Biz
{
    public class BizConfiguration : DbConfiguration
    {
        public BizConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}