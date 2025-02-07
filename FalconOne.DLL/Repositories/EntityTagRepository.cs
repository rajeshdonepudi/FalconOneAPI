using FalconOne.DAL.Contracts;
using FalconOne.Models.Entities.Mails;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class EntityTagRepository : GenericRepository<EntityTag>, IEntityTagRepository
    {
        public EntityTagRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}

