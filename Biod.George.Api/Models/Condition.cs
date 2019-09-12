namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Condition
    /// </summary>
    [Table("bd.Condition")]
    public partial class Condition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Condition()
        {
            DiseaseSeverityMods = new HashSet<DiseaseSeverityMod>();
            DiseasePreventionMods = new HashSet<DiseasePreventionMod>();
        }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        public int conditionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the condition.
        /// </summary>
        /// <value>
        /// The name of the condition.
        /// </value>
        [Column("condition")]
        [Required]
        [StringLength(64)]
        public string conditionName { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Required]
        public int category { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        [Required]
        [StringLength(256)]
        public string question { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column(TypeName = "text")]
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the modifier category.
        /// </summary>
        /// <value>
        /// The modifier category.
        /// </value>
        public virtual ModifierCategory ModifierCategory { get; set; }

        /// <summary>
        /// Gets or sets the disease severity mods.
        /// </summary>
        /// <value>
        /// The disease severity mods.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseSeverityMod> DiseaseSeverityMods { get; set; }

        /// <summary>
        /// Gets or sets the disease prevention mods.
        /// </summary>
        /// <value>
        /// The disease prevention mods.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasePreventionMod> DiseasePreventionMods { get; set; }

        /// <summary>
        /// Gets the modifier projection.
        /// </summary>
        /// <returns></returns>
        public ModifierProjection GetModifierProjection()
        {
            return new ModifierProjection(this);
        }
    }
}
