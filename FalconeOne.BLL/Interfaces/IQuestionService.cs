using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.QuestionAndAnswer;

namespace FalconeOne.BLL.Interfaces
{
    public interface IQuestionService
    {
        Task DeleteQuestionAsync(Guid questionId, CancellationToken cancellationToken);
        Task<PagedList<QuestionListDto>> GetAllQuestions(FilterQuestionsDto model, CancellationToken cancellationToken);
        Task UpsertQuestionAsync(BaseQuestionDto model, CancellationToken cancellationToken);
    }
}
