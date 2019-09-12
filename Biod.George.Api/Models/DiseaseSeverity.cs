namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Severity
    /// </summary>
    [Table("bd.DiseaseSeverity")]
    public partial class DiseaseSeverity
    {
        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DiseaseSeverity"/> is chronic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if chronic; otherwise, <c>false</c>.
        /// </value>
        public bool chronic { get; set; }

        /// <summary>
        /// Gets or sets the treatment available.
        /// </summary>
        /// <value>
        /// The treatment available.
        /// </value>
        public double treatmentAvailable { get; set; }

        /// <summary>
        /// Gets or sets the disease reference.
        /// </summary>
        /// <value>
        /// The disease reference.
        /// </value>
        public virtual Disease DiseaseRef { get; set; }
    }
}
