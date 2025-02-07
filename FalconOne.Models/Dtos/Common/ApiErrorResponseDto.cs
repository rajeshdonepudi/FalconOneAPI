namespace FalconOne.Models.DTOs.Common
{
    public record DetailedException : ApiErrorResponseDto
    {
        public string Exception { get; set; }
    }

    public record ApiErrorResponseDto
    {
        public required string Message { get; set; }
    }
}
