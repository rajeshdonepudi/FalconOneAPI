using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.QuestionAndAnswer;
using FalconOne.Models.Entities.QuestionAndAnswer;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<PagedList<QuestionListDto>> GetAllQuestionsAsync(FilterQuestionsDto model, CancellationToken cancellationToken);
    }
}
