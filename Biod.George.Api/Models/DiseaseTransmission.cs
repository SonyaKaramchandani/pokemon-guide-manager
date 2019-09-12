namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    /// <summary>
    /// Disease Transmission
    /// </summary>
    [Table("bd.DiseaseTransmission")]
    public partial class DiseaseTransmission
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
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mode { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public int rank { get; set; }

        /// <summary>
        /// Gets or sets the agents.
        /// </summary>
        /// <value>
        /// The agents.
        /// </value>
        [StringLength(64)]
        public string agents { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        [StringLength(64)]
        public string contact { get; set; }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        [StringLength(64)]
        public string actions { get; set; }

        /// <summary>
        /// Gets or sets the disease reference.
        /// </summary>
        /// <value>
        /// The disease reference.
        /// </value>
        public virtual Disease DiseaseRef { get; set; }

        /// <summary>
        /// Gets or sets the transmission mode.
        /// </summary>
        /// <value>
        /// The transmission mode.
        /// </value>
        public virtual TransmissionMode TransmissionMode { get; set; }

        /// <summary>
        /// Gets the icon names.
        /// </summary>
        /// <returns></returns>
        public List<string> GetIconNames()
        {
            if (TransmissionMode.mode.Contains("Food/Water"))
                return new List<string>(){ "modeWaterborne", "modeFoodborne" };
            else if (TransmissionMode.mode.Contains("Food "))
                return new List<string>(){ "modeFoodborne" };
            else if (TransmissionMode.mode.Contains("Water "))
                return new List<string>(){ "modeWaterborne" };
            else if (TransmissionMode.mode.Contains("Vector - Insect"))
                return agents.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(m => "modeVector" + m).ToList();
            else
                return new List<string>(){ "mode" + TransmissionMode.mode.Replace(" - ", "") };
        }
    }
}
