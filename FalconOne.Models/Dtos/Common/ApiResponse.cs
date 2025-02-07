﻿namespace FalconOne.Models.DTOs.Common
{
    public record ApiResponse
    {
        public required string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
