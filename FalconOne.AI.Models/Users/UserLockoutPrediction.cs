using Microsoft.ML.Data;

namespace FalconOne.AI.Models.Users
{
    public class UserLockoutPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
