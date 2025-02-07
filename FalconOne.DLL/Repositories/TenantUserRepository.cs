using FalconOne.DAL.Contracts;
using FalconOne.Models.Entities.Tenants;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class TenantUserRepository : GenericRepository<TenantUser>, ITenantUserRepository
    {
        public TenantUserRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}

