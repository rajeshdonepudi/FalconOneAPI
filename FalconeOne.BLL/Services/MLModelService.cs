using FalconeOne.BLL.Interfaces;
using FalconOne.AI.Core;
using FalconOne.AI.Models.Users;
using FalconOne.AI.Pipelines;
using FalconOne.DAL.Contracts;
using FalconOne.Models.Dtos.AI;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;

namespace FalconeOne.BLL.Services
{
    public class MLModelService : BaseService, IMLModelService
    {
        private readonly MLContext _mLContext;

        public MLModelService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ITenantProvider tenantProvider,
            IIdenticonProvider identiconProvider) :
            base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
            _mLContext = FalconOneAI.GetMLContext();
        }

        public async Task TrainModel()
        {
            string solutionDirectory = Directory.GetCurrentDirectory();

            var loginAttempts = await _userManager.Users.SelectMany(x => x.LoginAttempts).ToListAsync();

            var transformedData = TransformLoginAttempts(loginAttempts);

            IDataView dataView = _mLContext.Data.LoadFromEnumerable(transformedData);

            var pipeline = UserLockoutPredictionPipeline.Get();

            var model = pipeline.Fit(dataView);

            string trainedModelsPath = @"C:\TrainedModels";

            if (!Directory.Exists(trainedModelsPath))
            {
                Directory.CreateDirectory(trainedModelsPath);
            }

            string modelFilePath = Path.Combine(trainedModelsPath, TrainedModelNames.USER_LOCKOUT_PREDICTION);

            _mLContext.Model.Save(model, dataView.Schema, modelFilePath);
        }

        public List<TrainedModelFileInfoDto> ViewAllTrainedModels()
        {
            var models = new List<TrainedModelFileInfoDto>();

            string solutionDirectory = Directory.GetCurrentDirectory();

            string trainedModelsPath = @"C:\TrainedModels";

            if (Directory.Exists(trainedModelsPath))
            {
                string[] files = Directory.GetFiles(trainedModelsPath, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    long fileSizeInBytes = fileInfo.Length;
                    string fileSize = FormatFileSize(fileSizeInBytes);
                    string fileName = Path.GetFileName(file);

                    models.Add(new TrainedModelFileInfoDto
                    {
                        Name = fileName,
                        Size = fileSize,
                        LastTrained = fileInfo.LastWriteTime,
                    });
                }
            }
            return models;
        }

        private static string FormatFileSize(long bytes)
        {
            if (bytes >= 1024 * 1024)
                return $"{(bytes / (1024 * 1024.0)):F2} MB";
            else if (bytes >= 1024)
                return $"{(bytes / 1024.0):F2} KB";
            else
                return $"{bytes} Bytes";
        }
    }
}
