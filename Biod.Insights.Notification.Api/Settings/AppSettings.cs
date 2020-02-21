using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Insights.Notification.Api.Settings
{
    /// <summary>
    /// The application settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or sets the support email.
        /// </summary>
        /// <value>
        /// The support email.
        /// </value>
        public string SupportEmail { get; set; }
        /// <summary>
        /// Gets or sets the company URL.
        /// </summary>
        /// <value>
        /// The company URL.
        /// </value>
        public string CompanyUrl { get; set; }
        /// <summary>
        /// Gets or sets the email recipient list upon error.
        /// </summary>
        /// <value>
        /// The email recipient list upon error.
        /// </value>
        public string EmailRecipientListUponError { get; set; }
        /// <summary>
        /// Gets or sets the email subject upon error.
        /// </summary>
        /// <value>
        /// The email subject upon error.
        /// </value>
        public string EmailSubjectUponError { get; set; }
    }
}
