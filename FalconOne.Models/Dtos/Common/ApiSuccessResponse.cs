namespace FalconOne.Models.DTOs.Common
{
    public record ApiSuccessResponse : ApiResponse
    {
        public required object Data { get; set; }
    }
}
