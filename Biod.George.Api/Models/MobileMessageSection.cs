namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// Mobile Message Section
    /// </summary>
    [Table("bd.MobileMessageSection")]
    public partial class MobileMessageSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileMessageSection"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MobileMessageSection()
        {
            DiseaseMobileMessages = new HashSet<DiseaseMobileMessage>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        /// <value>
        /// The name of the section.
        /// </value>
        [Required]
        [StringLength(64)]
        public string sectionName { get; set; }

        /// <summary>
        /// Gets or sets the disease mobile messages.
        /// </summary>
        /// <value>
        /// The disease mobile messages.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseMobileMessage> DiseaseMobileMessages { get; set; }
    }
}
