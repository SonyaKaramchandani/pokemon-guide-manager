namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Symptom
    /// </summary>
    [Table("bd.Symptom")]
    public partial class Symptom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Symptom"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Symptom()
        {
            DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        /// <summary>
        /// Gets or sets the symptom identifier.
        /// </summary>
        /// <value>
        /// The symptom identifier.
        /// </value>
        public int symptomId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(256)]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the symptom category identifier.
        /// </summary>
        /// <value>
        /// The symptom category identifier.
        /// </value>
        [Required]
        public int symptomCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the alt names.
        /// </summary>
        /// <value>
        /// The alt names.
        /// </value>
        [StringLength(256)]
        public string altNames { get; set; }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>
        /// The definition.
        /// </value>
        [Column(TypeName = "text")]
        public string definition { get; set; }

        /// <summary>
        /// Gets or sets the definition source.
        /// </summary>
        /// <value>
        /// The definition source.
        /// </value>
        [StringLength(256)]
        public string definitionSource { get; set; }

        /// <summary>
        /// Gets or sets the symptom category.
        /// </summary>
        /// <value>
        /// The symptom category.
        /// </value>
        public virtual SymptomCategory SymptomCategory { get; set; }

        /// <summary>
        /// Gets or sets the disease symptoms.
        /// </summary>
        /// <value>
        /// The disease symptoms.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private Symptom m_outer;
            /// <summary>
            /// Gets the symptom identifier.
            /// </summary>
            /// <value>
            /// The symptom identifier.
            /// </value>
            public int symptomId { get; private set; }
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            public string name { get; private set; }
            /// <summary>
            /// Gets the category identifier.
            /// </summary>
            /// <value>
            /// The category identifier.
            /// </value>
            public int categoryId { get; private set; }
            /// <summary>
            /// Gets the definition.
            /// </summary>
            /// <value>
            /// The definition.
            /// </value>
            public string definition { get; private set; }
            /// <summary>
            /// Gets the definition source.
            /// </summary>
            /// <value>
            /// The definition source.
            /// </value>
            public string definitionSource { get; private set; }
            /// <summary>
            /// Gets the alternate names.
            /// </summary>
            /// <value>
            /// The alternate names.
            /// </value>
            public List<string> alternateNames { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(Symptom outer)
            {
                this.m_outer = outer;
                this.symptomId = outer.symptomId;
                this.name = outer.name;
                this.categoryId = outer.symptomCategoryId;
                this.definition = outer.definition;
                this.definitionSource = outer.definitionSource;
                this.alternateNames = outer.altNames.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToList();
            }
        } // class Symptom.Projection

        /// <summary>
        /// Gets the projection.
        /// </summary>
        /// <returns></returns>
        public Projection GetProjection()
        {
            return new Projection(this);
        } // GetProjection()
    }
}
