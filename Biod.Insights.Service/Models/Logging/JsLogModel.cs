using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.Logging
{
    public class JsLogModel
    {
        /// <summary>
        /// The level of the log
        /// </summary>
        [Required]
        [RegularExpression(@"^critical|fatal|error|exception|warn(ing)?|info(rmation)?|debug|trace$", 
            ErrorMessage = "Must be one of: critical, fatal, error, exception, warning, information, debug, trace")]
        public string LogLevel { get; set; }
        
        /// <summary>
        /// The message to be saved in the log
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}