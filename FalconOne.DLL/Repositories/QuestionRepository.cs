using FalconOne.DAL.Contracts;
using FalconOne.Extensions.EntityFramework;
using FalconOne.Extensions.Enumerations;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.QuestionAndAnswer;
using FalconOne.Models.Entities.QuestionAndAnswer;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public async Task<PagedList<QuestionListDto>> GetAllQuestionsAsync(FilterQuestionsDto model, CancellationToken cancellationToken)
        {
            var result = await _context.Questions.OrderByDescending(x => x.CreatedOn).Select(x => new QuestionListDto
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                QuestionTypeName = x.Type.GetDisplayName()!
            }).ToPagedListAsync(model, cancellationToken);

            return result;
        }
    }
}

