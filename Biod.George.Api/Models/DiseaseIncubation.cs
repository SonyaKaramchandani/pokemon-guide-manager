namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Incubation
    /// </summary>
    [Table("bd.DiseaseIncubation")]
    public partial class DiseaseIncubation
    {
        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the minimum days.
        /// </summary>
        /// <value>
        /// The minimum days.
        /// </value>
        public double minimumDays { get; set; }

        /// <summary>
        /// Gets or sets the maximum days.
        /// </summary>
        /// <value>
        /// The maximum days.
        /// </value>
        public double maximumDays { get; set; }

        /// <summary>
        /// Gets or sets the average days.
        /// </summary>
        /// <value>
        /// The average days.
        /// </value>
        public double averageDays { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Column(TypeName = "text")]
        public string notes { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        [StringLength(512)]
        public string source { get; set; }

        /// <summary>
        /// Gets or sets the disease reference.
        /// </summary>
        /// <value>
        /// The disease reference.
        /// </value>
        public virtual Disease DiseaseRef { get; set; }


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private DiseaseIncubation m_outer;
            /// <summary>
            /// Gets the minimum days.
            /// </summary>
            /// <value>
            /// The minimum days.
            /// </value>
            public double minimumDays { get; private set; }
            /// <summary>
            /// Gets the maximum days.
            /// </summary>
            /// <value>
            /// The maximum days.
            /// </value>
            public double maximumDays { get; private set; }
            /// <summary>
            /// Gets the average days.
            /// </summary>
            /// <value>
            /// The average days.
            /// </value>
            public double averageDays { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(DiseaseIncubation outer)
            {
                this.m_outer = outer;
                this.minimumDays = outer.minimumDays;
                this.maximumDays = outer.maximumDays;
                this.averageDays = outer.averageDays;
            } 
        } // class DiseaseIncubation.Projection

        /// <summary>
        /// Gets the projection.
        /// </summary>
        /// <returns></returns>
        public Projection GetProjection()
        {
            return new Projection(this);
        }
    }
}
