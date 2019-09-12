namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Symptom Category
    /// </summary>
    [Table("bd.SymptomCategory")]
    public partial class SymptomCategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymptomCategory"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SymptomCategory()
        {
            Symptoms = new HashSet<Symptom>();
        }

        /// <summary>
        /// Gets or sets the symptom category identifier.
        /// </summary>
        /// <value>
        /// The symptom category identifier.
        /// </value>
        public int symptomCategoryId { get; set; }

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
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Column(TypeName = "text")]
        public string notes { get; set; }

        /// <summary>
        /// Gets or sets the symptoms.
        /// </summary>
        /// <value>
        /// The symptoms.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Symptom> Symptoms { get; set; }


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private SymptomCategory m_outer;
            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int id { get; private set; }
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            public string name { get; private set; }
            /// <summary>
            /// Gets the notes.
            /// </summary>
            /// <value>
            /// The notes.
            /// </value>
            public string notes { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(SymptomCategory outer)
            {
                this.m_outer = outer;
                this.id = outer.symptomCategoryId;
                this.name = outer.name;
                this.notes = outer.notes;
            }
        } // class SymptomCategory.Projection

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
