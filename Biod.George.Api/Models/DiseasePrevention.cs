namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Prevention
    /// </summary>
    [Table("bd.DiseasePrevention")]
    public partial class DiseasePrevention
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiseasePrevention"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiseasePrevention()
        {
            DiseasePreventionMods = new HashSet<DiseasePreventionMod>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Required]
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required]
        public int type { get; set; }

        /// <summary>
        /// Gets or sets the risk reduction.
        /// </summary>
        /// <value>
        /// The risk reduction.
        /// </value>
        [Required]
        public double riskReduction { get; set; }

        /// <summary>
        /// Gets or sets the availability.
        /// </summary>
        /// <value>
        /// The availability.
        /// </value>
        [StringLength(64)]
        public string availability { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Required]
        public int category { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DiseasePrevention"/> is travel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if travel; otherwise, <c>false</c>.
        /// </value>
        public bool travel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DiseasePrevention"/> is oral.
        /// </summary>
        /// <value>
        ///   <c>true</c> if oral; otherwise, <c>false</c>.
        /// </value>
        public bool oral { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [StringLength(64)]
        public string duration { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Column(TypeName = "text")]
        public string notes { get; set; }

        /// <summary>
        /// Gets or sets the disease reference.
        /// </summary>
        /// <value>
        /// The disease reference.
        /// </value>
        public virtual Disease DiseaseRef { get; set; }

        /// <summary>
        /// Gets or sets the type of the prevention.
        /// </summary>
        /// <value>
        /// The type of the prevention.
        /// </value>
        public virtual PreventionType PreventionType { get; set; }

        /// <summary>
        /// Gets or sets the modifier category.
        /// </summary>
        /// <value>
        /// The modifier category.
        /// </value>
        public virtual ModifierCategory ModifierCategory { get; set; }

        /// <summary>
        /// Gets or sets the disease prevention mods.
        /// </summary>
        /// <value>
        /// The disease prevention mods.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasePreventionMod> DiseasePreventionMods { get; set; }


        /// <summary>
        /// The effectiveness levels
        /// </summary>
        public static readonly Level[] EFFECTIVENESS_LEVELS = { new Level("Minimal/None", 0),
                                                                new Level("Low", 1),
                                                                new Level("Medium", 2),
                                                                new Level("High", 3) };

        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private DiseasePrevention m_outer;
            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int id { get; private set; }
            /// <summary>
            /// Gets the type.
            /// </summary>
            /// <value>
            /// The type.
            /// </value>
            public string type { get; private set; }
            /// <summary>
            /// Gets the availability.
            /// </summary>
            /// <value>
            /// The availability.
            /// </value>
            public string availability { get; private set; }
            /// <summary>
            /// Gets the category.
            /// </summary>
            /// <value>
            /// The category.
            /// </value>
            public string category { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="Projection" /> is travel.
            /// </summary>
            /// <value>
            ///   <c>true</c> if travel; otherwise, <c>false</c>.
            /// </value>
            public bool travel { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="Projection"/> is oral.
            /// </summary>
            /// <value>
            ///   <c>true</c> if oral; otherwise, <c>false</c>.
            /// </value>
            public bool oral { get; private set; }
            /// <summary>
            /// Gets the effectiveness.
            /// </summary>
            /// <value>
            /// The effectiveness.
            /// </value>
            public Level effectiveness { get; private set; }
            /// <summary>
            /// Gets the risk reduction.
            /// </summary>
            /// <value>
            /// The risk reduction.
            /// </value>
            public double riskReduction { get; private set; }
            /// <summary>
            /// Gets the duration.
            /// </summary>
            /// <value>
            /// The duration.
            /// </value>
            public string duration { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(DiseasePrevention outer)
            {
                this.m_outer = outer;
                this.id = outer.id + Globals.PREVENTIONS_IDSTART;
                this.type = outer.PreventionType.type;
                this.availability = outer.availability;
                this.category = outer.ModifierCategory.categoryLabel;
                this.travel = outer.travel;
                this.oral = outer.oral;
                this.effectiveness = Level.ToLevel(outer.riskReduction, 1.0, EFFECTIVENESS_LEVELS);
                this.riskReduction = outer.riskReduction;
                this.duration = outer.duration;
            }
        } // class DiseasePrevention.Projection

        /// <summary>
        /// Gets the projection.
        /// </summary>
        /// <returns></returns>
        public Projection GetProjection()
        {
            return new Projection(this);
        }

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
