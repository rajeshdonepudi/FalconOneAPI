using FalconOne.DAL.Contracts;
using FalconOne.Models.Entities.Tags;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}

