namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Prevention Type
    /// </summary>
    [Table("bd.PreventionType")]
    public partial class PreventionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreventionType"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreventionType()
        {
            DiseasePreventions = new HashSet<DiseasePrevention>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required]
        [StringLength(64)]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the disease preventions.
        /// </summary>
        /// <value>
        /// The disease preventions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasePrevention> DiseasePreventions { get; set; }
    }
}
