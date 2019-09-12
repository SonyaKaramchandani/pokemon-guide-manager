namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Symptom
    /// </summary>
    [Table("bd.DiseaseSymptom")]
    public partial class DiseaseSymptom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiseaseSymptom"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiseaseSymptom()
        {
        }

        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int diseaseId { get; set; }


        /// <summary>
        /// Gets or sets the symptom identifier.
        /// </summary>
        /// <value>
        /// The symptom identifier.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int symptomId { get; set; }


        /// <summary>
        /// Gets or sets the association score.
        /// </summary>
        /// <value>
        /// The association score.
        /// </value>
        [Required]
        public double associationScore { get; set; }


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
        /// Gets or sets the symptom.
        /// </summary>
        /// <value>
        /// The symptom.
        /// </value>
        public virtual Symptom Symptom { get; set; }


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private DiseaseSymptom m_outer;
            /// <summary>
            /// Gets the symptom identifier.
            /// </summary>
            /// <value>
            /// The symptom identifier.
            /// </value>
            public int symptomId { get; private set; }
            /// <summary>
            /// Gets the association score.
            /// </summary>
            /// <value>
            /// The association score.
            /// </value>
            public double associationScore { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(DiseaseSymptom outer)
            {
                this.m_outer = outer;
                this.symptomId = outer.symptomId;
                this.associationScore = outer.associationScore;
            }
        } // class DiseaseSymptom.Projection

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
