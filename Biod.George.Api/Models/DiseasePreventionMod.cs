namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Prevention Mod
    /// </summary>
    [Table("bd.DiseasePreventionMod")]
    public partial class DiseasePreventionMod
    {
        /// <summary>
        /// Gets or sets the prevention.
        /// </summary>
        /// <value>
        /// The prevention.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prevention { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int conditionId { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        [Required]
        public int messageId { get; set; }


        /// <summary>
        /// Gets or sets the disease prevention.
        /// </summary>
        /// <value>
        /// The disease prevention.
        /// </value>
        public virtual DiseasePrevention DiseasePrevention { get; set; }

        /// <summary>
        /// Gets or sets the condition reference.
        /// </summary>
        /// <value>
        /// The condition reference.
        /// </value>
        public virtual Condition ConditionRef { get; set; }
    }
}
