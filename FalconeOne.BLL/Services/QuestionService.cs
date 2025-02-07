using FalconeOne.BLL.Helpers;
using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.QuestionAndAnswer;
using FalconOne.Models.Entities.QuestionAndAnswer;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        public QuestionService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, ITenantProvider tenantProvider,
            IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
        }

        public async Task UpsertQuestionAsync(BaseQuestionDto model, CancellationToken cancellationToken)
        {
            if (model.IsNew)
            {
                var uniqueOptions = new List<QuestionOptionDto>();

                foreach (var option in model.Options)
                {
                    if (!uniqueOptions.Any(x => x.Name == option.Name))
                    {
                        uniqueOptions.Add(option);
                    }
                }

                await _unitOfWork.QuestionRepository.AddAsync(new Question
                {
                    Name = model.Name,
                    Type = model.Type,
                    CreatedOn = DateTime.UtcNow,
                    Options = uniqueOptions.Select(o => new QuestionOption
                    {
                        Name = o.Name, 
                        Value = o.Value,
                        CreatedOn = DateTime.UtcNow,
                        CreatedByUserId = USER_ID,
                    }).ToList()
                }, cancellationToken);
            }
            else
            {
                if (!model.Id.HasValue)
                {
                    throw new BusinessException(ErrorMessages.SOMETHING_WENT_WRONG);
                }

                var question = await _unitOfWork.QuestionRepository.GetByIdAsync(model.Id.GetValueOrDefault(), cancellationToken);

                var existingOptions = question.Options.ToList();

                foreach (var modelOption in model.Options)
                {
                    var existingOption = existingOptions.FirstOrDefault(o => o.Id == modelOption.Id);

                    if (existingOption == null)
                    {
                        // Add new option
                        var newOption = new QuestionOption
                        {
                            QuestionId = question.Id,
                            Value = modelOption.Value,
                            CreatedOn = DateTime.UtcNow,
                        };
                        question.Options.Add(newOption);
                    }
                    else
                    {
                        // Update existing option
                        existingOption.Value = modelOption.Value;
                        existingOption.ModifiedOn = DateTime.UtcNow;
                    }
                }

                // Remove options not in the model
                var modelOptionIds = model.Options.Select(o => o.Id).ToList();
                var optionsToRemove = existingOptions.Where(o => !modelOptionIds.Contains(o.Id)).ToList();

                foreach (var optionToRemove in optionsToRemove)
                {
                    question.Options.Remove(optionToRemove);
                }
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<QuestionListDto>> GetAllQuestions(FilterQuestionsDto model, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.QuestionRepository.GetAllQuestionsAsync(model, cancellationToken);

            return result;
        }

        public async Task DeleteQuestionAsync(Guid questionId, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId, cancellationToken);

            _unitOfWork.QuestionRepository.Remove(question);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
