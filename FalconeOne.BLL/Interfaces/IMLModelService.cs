using FalconOne.Models.Dtos.AI;

namespace FalconeOne.BLL.Interfaces
{
    public interface IMLModelService
    {
        Task TrainModel();
        List<TrainedModelFileInfoDto> ViewAllTrainedModels();
    }
}