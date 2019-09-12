namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// Disease Seasonality
    /// </summary>
    [Table("bd.DiseaseSeasonality")]
    public partial class DiseaseSeasonality
    {
        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zone { get; set; }

        /// <summary>
        /// Gets or sets from month.
        /// </summary>
        /// <value>
        /// From month.
        /// </value>
        [Required]
        public int fromMonth { get; set; }

        /// <summary>
        /// Gets or sets to month.
        /// </summary>
        /// <value>
        /// To month.
        /// </value>
        [Required]
        public int toMonth { get; set; }

        /// <summary>
        /// Gets or sets the off season weight.
        /// </summary>
        /// <value>
        /// The off season weight.
        /// </value>
        [Required]
        public double offSeasonWeight { get; set; }

        /// <summary>
        /// Gets or sets the disease reference.
        /// </summary>
        /// <value>
        /// The disease reference.
        /// </value>
        public virtual Disease DiseaseRef { get; set; }
    }
}
