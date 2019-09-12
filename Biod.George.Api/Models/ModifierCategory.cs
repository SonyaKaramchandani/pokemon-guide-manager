namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// Modifier Category
    /// </summary>
    [Table("bd.ModifierCategory")]
    public partial class ModifierCategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierCategory"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModifierCategory()
        {
            Conditions = new HashSet<Condition>();
            DiseasePreventions  = new HashSet<DiseasePrevention>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the category label.
        /// </summary>
        /// <value>
        /// The category label.
        /// </value>
        [Column("categoryLabel")]
        [Required]
        [StringLength(32)]
        public string categoryLabel { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Condition> Conditions { get; set; }

        /// <summary>
        /// Gets or sets the disease preventions.
        /// </summary>
        /// <value>
        /// The disease preventions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasePrevention> DiseasePreventions { get; set; }


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private ModifierCategory m_outer;
            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int id { get; private set; }
            /// <summary>
            /// Gets the label.
            /// </summary>
            /// <value>
            /// The label.
            /// </value>
            public string label { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="Projection"/> is selectable.
            /// </summary>
            /// <value>
            ///   <c>true</c> if selectable; otherwise, <c>false</c>.
            /// </value>
            public bool selectable { get; private set; }
            /// <summary>
            /// Gets or sets the default sort order.
            /// </summary>
            /// <value>
            /// The default sort order.
            /// </value>
            public int defaultSortOrder { get; set; }
            /// <summary>
            /// Gets the modifiers.
            /// </summary>
            /// <value>
            /// The modifiers.
            /// </value>
            public List<ModifierProjection> modifiers { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(ModifierCategory outer)
            {
                this.m_outer = outer;
                this.id = outer.id;
                this.label = outer.categoryLabel;
                this.defaultSortOrder = outer.id;
                this.selectable = (outer.categoryLabel == "Childhood Vaccines");
                this.modifiers = outer.Conditions.ToList().ConvertAll(c => c.GetModifierProjection()).OrderBy(mp => mp.id).ToList();
                this.modifiers.AddRange(outer.DiseasePreventions.Where(p => (p.diseaseId != 84)).ToList().ConvertAll(p => p.GetModifierProjection()).OrderBy(mp => mp.displayName).ToList());
                int mpc = 0;
                foreach (ModifierProjection mp in this.modifiers)
                    mp.defaultSortOrder = ++mpc;
            }
        } // class ModifierCategory.Projection

        /// <summary>
        /// Gets the projection.
        /// </summary>
        /// <returns></returns>
        public Projection getProjection()
        {
            return new Projection(this);
        }

    }
}
