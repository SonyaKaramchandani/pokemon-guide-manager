namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// Transmission Mode
    /// </summary>
    [Table("bd.TransmissionMode")]
    public partial class TransmissionMode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionMode"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransmissionMode()
        {
            DiseaseTransmissions = new HashSet<DiseaseTransmission>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        [Required]
        [StringLength(64)]
        public string mode { get; set; }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public double multiplier { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column(TypeName = "text")]
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the preventions.
        /// </summary>
        /// <value>
        /// The preventions.
        /// </value>
        [Column(TypeName = "text")]
        public string preventions { get; set; }

        /// <summary>
        /// Gets or sets the disease transmissions.
        /// </summary>
        /// <value>
        /// The disease transmissions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseTransmission> DiseaseTransmissions { get; set; }
    }
}
