using FalconOne.DAL.Contracts;
using FalconOne.Models.Entities.Security;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class TenantUserSecurityGroupRepository : GenericRepository<TenantUserSecurityGroup>, ITenantUserSecurityGroupRepository
    {
        public TenantUserSecurityGroupRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}

