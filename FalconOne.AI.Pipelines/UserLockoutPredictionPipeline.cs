using FalconOne.AI.Core;
using FalconOne.AI.Models.Users;
using Microsoft.ML;

namespace FalconOne.AI.Pipelines
{
    public static class UserLockoutPredictionPipeline
    {
        public static IEstimator<ITransformer> Get()
        {
            var context = FalconOneAI.GetMLContext();

            var pipeline = context.Transforms.Concatenate("Features", nameof(UserLoginAttemptData.TimeSinceLastLogin),
                                                      nameof(UserLoginAttemptData.NumberOfFailedAttempts),
                                                      nameof(UserLoginAttemptData.DayOfWeek))
    .Append(context.Transforms.NormalizeMinMax("Features"))
    .Append(context.BinaryClassification.Trainers.FastTree(labelColumnName: nameof(UserLoginAttemptData.IsSucessfull))); // Use the Boolean label directly


            pipeline.Append(context.BinaryClassification.Trainers.LbfgsLogisticRegression());

            return pipeline;
        }
    }
}
