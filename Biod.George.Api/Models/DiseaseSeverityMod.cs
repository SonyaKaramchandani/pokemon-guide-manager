namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Disease Severity Mod
    /// </summary>
    [Table("bd.DiseaseSeverityMod")]
    public partial class DiseaseSeverityMod
    {
        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int conditionId { get; set; }

        /// <summary>
        /// Gets or sets the addend.
        /// </summary>
        /// <value>
        /// The addend.
        /// </value>
        public int addend { get; set; }

        /// <summary>
        /// Gets or sets the condition parameter.
        /// </summary>
        /// <value>
        /// The condition parameter.
        /// </value>
        public double conditionParameter { get; set; }

        /// <summary>
        /// Gets or sets the condition reference.
        /// </summary>
        /// <value>
        /// The condition reference.
        /// </value>
        public virtual Condition ConditionRef { get; set; }

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
            private DiseaseSeverityMod m_outer;
            /// <summary>
            /// Gets the condition.
            /// </summary>
            /// <value>
            /// The condition.
            /// </value>
            public int condition { get; private set; }
            /// <summary>
            /// Gets the addend.
            /// </summary>
            /// <value>
            /// The addend.
            /// </value>
            public int addend { get; private set; }
            /// <summary>
            /// Gets the condition parameter.
            /// </summary>
            /// <value>
            /// The condition parameter.
            /// </value>
            public double conditionParameter { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(DiseaseSeverityMod outer)
            {
                this.m_outer = outer;
                this.condition = outer.conditionId + Globals.CONDITIONS_IDSTART;
                this.addend = outer.addend;
                this.conditionParameter = outer.conditionParameter;
            }
        } // class DiseaseSeverityMod.Projection

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
