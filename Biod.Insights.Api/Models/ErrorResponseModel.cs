using System.Collections.Generic;

namespace Biod.Insights.Api.Models
{
    /// <summary>
    /// Model for the response when an error has occurred
    /// </summary>
    public class ErrorResponseModel
    {
        public IEnumerable<string> Errors { get; set; }
    }
}