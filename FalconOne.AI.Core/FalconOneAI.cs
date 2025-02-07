using Microsoft.ML;

namespace FalconOne.AI.Core
{
    public static class FalconOneAI
    {
        public static MLContext GetMLContext()
        {
            return new MLContext();
        }
    }
}
