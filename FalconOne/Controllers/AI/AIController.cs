using FalconeOne.BLL.Interfaces;
using FalconOne.AI.Models.Users;
using FalconOne.API.Attributes;
using FalconOne.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.AI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IMLModelService _mLModelService;
        private readonly IUserPredictionService userPredictionService;

        public AIController(IMLModelService mLModelService, IUserPredictionService userPredictionService)
        {
            _mLModelService = mLModelService;
            this.userPredictionService = userPredictionService;
        }

        [HttpGet("evaluate")]
        public async Task<IActionResult> Evaluate()
        {
            var result = userPredictionService.Evaluate();

            return Ok(result);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            var result = userPredictionService.PredictUserLockout(GenerateTestLoginAttempts(100).LastOrDefault());

            return Ok(result);  
        }

        private static List<UserLoginAttemptData> GenerateTestLoginAttempts(int numberOfAttempts)
        {
            var random = new Random();
            var testLoginAttempts = new List<UserLoginAttemptData>();

            for (int i = 1; i <= numberOfAttempts; i++)
            {
                testLoginAttempts.Add(new UserLoginAttemptData
                {
                    TimeSinceLastLogin = (float)(random.NextDouble() * 72), // Random time since last login (0-72 hours)
                    NumberOfFailedAttempts = (float)random.Next(0, 6), // Random failed attempts (0-5)
                    DayOfWeek = (float)random.Next(0, 7), // Random day of the week (0-6)
                    IsSucessfull = random.Next(0, 2) == 1 // Random success or failure
                });
            }

            return testLoginAttempts;
        }


        [HttpGet("trained-models")]
        [FalconOneAuthorize(PermissionPool.AI.VIEW_TRAINED_MODELS)]
        public IActionResult Get()
        {
            var result = _mLModelService.ViewAllTrainedModels();

            return Ok(result);
        }

        [HttpGet("train-model")]
        public async Task<IActionResult> TrainModel()
        {
            await _mLModelService.TrainModel();

            return Ok();
        }
    }
}
