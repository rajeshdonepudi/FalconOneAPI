using FalconeOne.BLL.Interfaces;
using FalconOne.AI.Core;
using FalconOne.AI.Models.Users;
using FalconOne.AI.Pipelines;
using FalconOne.DAL.Contracts;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace FalconeOne.BLL.Services
{
    public class UserPredictionService : BaseService, IUserPredictionService
    {
        private readonly MLContext _mlContext;
        private string ModelsPath = @"C:\TrainedModels";

        private ITransformer _userLockoutPredictionModel;
        private PredictionEngine<UserLoginAttemptData, UserLockoutPrediction> _userLockoutPredictionEngine;

        public UserPredictionService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ITenantProvider tenantProvider, IIdenticonProvider identiconProvider) :
            base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
            _mlContext = FalconOneAI.GetMLContext();

            InitUserLockoutPredictionEngine();
        }

        public UserLockoutPrediction PredictUserLockout(UserLoginAttemptData data)
        {
            return _userLockoutPredictionEngine.Predict(data);
        }

        public UserLockoutPrediction PredictNextLoginProbability(IEnumerable<UserLoginAttemptData> loginHistory)
        {
            // Feature engineering from login history
            var aggregatedFeatures = AggregateLoginHistoryFeatures(loginHistory);

            // Predict the lockout probability for the next login attempt
            var prediction = _userLockoutPredictionEngine.Predict(aggregatedFeatures);

            return prediction;
        }

        // Private method to extract features from login history
        private UserLoginAttemptData AggregateLoginHistoryFeatures(IEnumerable<UserLoginAttemptData> loginHistory)
        {
            var historyList = loginHistory.ToList();

            // For simplicity, we calculate average and some features from the last X attempts
            var avgTimeSinceLastLogin = historyList.Average(attempt => attempt.TimeSinceLastLogin);
            var avgFailedAttempts = historyList.Average(attempt => attempt.NumberOfFailedAttempts);
            var lastDayOfWeek = historyList.Last().DayOfWeek;
            var successRate = historyList.Count(attempt => attempt.IsSucessfull) / (double)historyList.Count();

            // Generate new aggregated features for the next prediction
            return new UserLoginAttemptData
            {
                TimeSinceLastLogin = avgTimeSinceLastLogin, // Average of previous attempts
                NumberOfFailedAttempts = avgFailedAttempts, // Average failed attempts
                DayOfWeek = lastDayOfWeek, // The last attempt day of the week
                IsSucessfull = false // This field isn't used for prediction
            };
        }

        public CalibratedBinaryClassificationMetrics Evaluate()
        {
            var testData = new List<UserLoginAttemptData>
        {
            new UserLoginAttemptData { TimeSinceLastLogin = 12, NumberOfFailedAttempts = 1, DayOfWeek = 1, IsSucessfull = true },
            new UserLoginAttemptData { TimeSinceLastLogin = 48, NumberOfFailedAttempts = 0, DayOfWeek = 2, IsSucessfull = true },
            new UserLoginAttemptData { TimeSinceLastLogin = 24, NumberOfFailedAttempts = 3, DayOfWeek = 3, IsSucessfull = false },
        };

            IDataView dataView = _mlContext.Data.LoadFromEnumerable(testData);

            // Define the pipeline with the correct label column
            var pipeline = _mlContext.Transforms.Concatenate("Features",
                    nameof(UserLoginAttemptData.TimeSinceLastLogin),
                    nameof(UserLoginAttemptData.NumberOfFailedAttempts),
                    nameof(UserLoginAttemptData.DayOfWeek))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: nameof(UserLoginAttemptData.IsSucessfull)) // Corrected
                                                                        );

            // Fit the model
            var model = pipeline.Fit(dataView);

            // Make predictions on the test data
            var predictions = model.Transform(dataView);

            // Evaluate the model
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: nameof(UserLoginAttemptData.IsSucessfull)); // Corrected

            return metrics;
        }

        #region Private methods
        private void InitUserLockoutPredictionEngine()
        {
            DataViewSchema modelSchema;

            var model = Path.Combine(ModelsPath, TrainedModelNames.USER_LOCKOUT_PREDICTION);

            //ValidateModelPath(model);

            _userLockoutPredictionModel = _mlContext.Model.Load(model, out modelSchema);

            _userLockoutPredictionEngine = _mlContext.Model.CreatePredictionEngine<UserLoginAttemptData, UserLockoutPrediction>(_userLockoutPredictionModel);
        }
        #endregion
    }
}
