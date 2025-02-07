using FalconOne.AI.Models.Users;
using Microsoft.ML.Data;

namespace FalconeOne.BLL.Interfaces
{
    public interface IUserPredictionService
    {
        CalibratedBinaryClassificationMetrics Evaluate();
        UserLockoutPrediction PredictNextLoginProbability(IEnumerable<UserLoginAttemptData> loginHistory);
        UserLockoutPrediction PredictUserLockout(UserLoginAttemptData data);
    }
}