namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// Version Info
    /// </summary>
    [Table("bd.VersionInfo")]
    public partial class VersionInfo
    {
        /// <summary>
        /// Gets or sets the model version.
        /// </summary>
        /// <value>
        /// The model version.
        /// </value>
        [Key]
        public double modelVersion { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Column(TypeName = "text")]
        public string notes { get; set; }
    }
}
