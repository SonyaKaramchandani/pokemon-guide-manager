using System.Diagnostics;


namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DiseasesAPI;




    /// <summary>
    /// Disease
    /// </summary>
    [Table("bd.Disease")]
    public partial class Disease
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Disease"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disease()
        {
            DiseasePreventions = new HashSet<DiseasePrevention>();
            DiseaseSeverityMods = new HashSet<DiseaseSeverityMod>();
            DiseaseTransmissions = new HashSet<DiseaseTransmission>();
            DiseaseSeasonalities = new HashSet<DiseaseSeasonality>();
            DiseaseMobileMessages = new HashSet<DiseaseMobileMessage>();
            DiseaseSymptoms = new HashSet<DiseaseSymptom>();
        }

        /// <summary>
        /// Gets or sets the disease identifier.
        /// </summary>
        /// <value>
        /// The disease identifier.
        /// </value>
        public int diseaseId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(64)]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the colloquial names.
        /// </summary>
        /// <value>
        /// The colloquial names.
        /// </value>
        [StringLength(256)]
        public string colloquialNames { get; set; }

        /// <summary>
        /// Gets or sets the search terms.
        /// </summary>
        /// <value>
        /// The search terms.
        /// </value>
        [StringLength(256)]
        public string searchTerms { get; set; }

        /// <summary>
        /// Gets or sets the pronunciation.
        /// </summary>
        /// <value>
        /// The pronunciation.
        /// </value>
        [StringLength(64)]
        public string pronunciation { get; set; }

        /// <summary>
        /// Gets or sets the type of the disease.
        /// </summary>
        /// <value>
        /// The type of the disease.
        /// </value>
        [Required]
        [StringLength(32)]
        public string diseaseType { get; set; }

        /// <summary>
        /// Gets or sets the microbe.
        /// </summary>
        /// <value>
        /// The microbe.
        /// </value>
        [StringLength(64)]
        public string microbe { get; set; }

        /// <summary>
        /// Gets or sets the map granularity.
        /// </summary>
        /// <value>
        /// The map granularity.
        /// </value>
        [StringLength(32)]
        public string mapGranularity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [extent vetted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [extent vetted]; otherwise, <c>false</c>.
        /// </value>
        public bool extentVetted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [prevalence vetted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [prevalence vetted]; otherwise, <c>false</c>.
        /// </value>
        public bool prevalenceVetted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can use for analytics.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can use for analytics; otherwise, <c>false</c>.
        /// </value>
        public bool canUseForAnalytics { get; set; }

        /// <summary>
        /// Gets or sets the presence bitmask.
        /// </summary>
        /// <value>
        /// The presence bitmask.
        /// </value>
        public int presenceBitmask { get; set; }

        /// <summary>
        /// Gets or sets the preventability.
        /// </summary>
        /// <value>
        /// The preventability.
        /// </value>
        public double preventability { get; set; }

        /// <summary>
        /// Gets or sets the model weight.
        /// </summary>
        /// <value>
        /// The model weight.
        /// </value>
        public double modelWeight { get; set; }

        /// <summary>
        /// Gets or sets the environmental factors.
        /// </summary>
        /// <value>
        /// The environmental factors.
        /// </value>
        [StringLength(64)]
        public string environmentalFactors { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Column(TypeName = "text")]
        public string notes { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        [Required]
        public DateTime lastModified { get; set; }


        /// <summary>
        /// Gets or sets the disease incubations.
        /// </summary>
        /// <value>
        /// The disease incubations.
        /// </value>
        public virtual DiseaseIncubation DiseaseIncubations { get; set; }

        /// <summary>
        /// Gets or sets the disease preventions.
        /// </summary>
        /// <value>
        /// The disease preventions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseasePrevention> DiseasePreventions { get; set; }

        /// <summary>
        /// Gets or sets the disease severity mods.
        /// </summary>
        /// <value>
        /// The disease severity mods.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseSeverityMod> DiseaseSeverityMods { get; set; }

        /// <summary>
        /// Gets or sets the disease severity.
        /// </summary>
        /// <value>
        /// The disease severity.
        /// </value>
        public virtual DiseaseSeverity DiseaseSeverity { get; set; }

        /// <summary>
        /// Gets or sets the disease transmissions.
        /// </summary>
        /// <value>
        /// The disease transmissions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseTransmission> DiseaseTransmissions { get; set; }

        /// <summary>
        /// Gets or sets the disease seasonalities.
        /// </summary>
        /// <value>
        /// The disease seasonalities.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseSeasonality> DiseaseSeasonalities { get; set; }

        /// <summary>
        /// Gets or sets the disease mobile messages.
        /// </summary>
        /// <value>
        /// The disease mobile messages.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseMobileMessage> DiseaseMobileMessages { get; set; }

        /// <summary>
        /// Gets or sets the disease symptoms.
        /// </summary>
        /// <value>
        /// The disease symptoms.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }


        /// <summary>
        /// Determines whether the specified seasonality zone is seasonal.
        /// </summary>
        /// <param name="seasonalityZone">The seasonality zone.</param>
        /// <returns>
        ///   <c>true</c> if the specified seasonality zone is seasonal; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSeasonal(int seasonalityZone)
        {
            foreach (DiseaseSeasonality ds in this.DiseaseSeasonalities)
            {
                if (ds.zone == seasonalityZone)
                    return true;
            } // foreach
            return false;
        } // IsSeasonal()

        /// <summary>
        /// Seasonals the risk.
        /// </summary>
        /// <param name="seasonalityZone">The seasonality zone.</param>
        /// <returns></returns>
        public double SeasonalRisk(int seasonalityZone)
        {
            DateTime now = DateTime.Now;
            foreach (DiseaseSeasonality ds in this.DiseaseSeasonalities)
            {
                if (ds.zone == seasonalityZone  &&
                    ((ds.fromMonth <= ds.toMonth  &&  (ds.fromMonth > now.Month  ||  ds.toMonth < now.Month))  ||
                     (ds.fromMonth > ds.toMonth  &&  ds.fromMonth > now.Month  &&  ds.toMonth < now.Month)))
                    return ds.offSeasonWeight;
            } // foreach
            return 1.0;
        } // SeasonalRisk()


        /// <summary>
        /// Decodes the presence.
        /// </summary>
        /// <param name="city">if set to <c>true</c> [city].</param>
        /// <param name="developed">The developed.</param>
        /// <returns></returns>
        public bool DecodePresence(bool city, double developed)
        {
            int mask = 1 << (2 - (int)(2 * developed));
            if (!city) 
                mask <<= 4;
            return ((this.presenceBitmask & mask) != 0);
        } // DecodePresence()


        /// <summary>
        /// 
        /// </summary>
        public class DiseaseMessageSection
        {
            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int id { get; private set; }
            /// <summary>
            /// Gets the section number.
            /// </summary>
            /// <value>
            /// The section number.
            /// </value>
            public int sectionNumber { get; private set; }
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            public string name { get; private set; }
            /// <summary>
            /// Gets the default text intro.
            /// </summary>
            /// <value>
            /// The default text intro.
            /// </value>
            public string defaultTextIntro { get; private set; }
            /// <summary>
            /// Gets the default text body.
            /// </summary>
            /// <value>
            /// The default text body.
            /// </value>
            public string defaultTextBody { get; private set; }
            /// <summary>
            /// Gets the conditional text.
            /// </summary>
            /// <value>
            /// The conditional text.
            /// </value>
            public string conditionalText { get; private set; }
            /// <summary>
            /// Gets the modifiers.
            /// </summary>
            /// <value>
            /// The modifiers.
            /// </value>
            public List<int> modifiers { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DiseaseMessageSection"/> class.
            /// </summary>
            /// <param name="id">The identifier.</param>
            /// <param name="sectionNumber">The section number.</param>
            /// <param name="name">The name.</param>
            /// <param name="modifiers">The modifiers.</param>
            public DiseaseMessageSection(int id, int sectionNumber, string name, List<int> modifiers = null)
            {
                this.id = id;
                this.sectionNumber = sectionNumber;
                this.name = name;
                this.defaultTextIntro = "";
                this.defaultTextBody = "";
                this.conditionalText = "";
                this.modifiers = (modifiers == null ?  new List<int>()  :  modifiers);
            }

            /// <summary>
            /// Sets the section text.
            /// </summary>
            /// <param name="dmms">The DMMS.</param>
            public void setSectionText(ICollection<DiseaseMobileMessage> dmms)
            {
                Regex trailingBRs = new Regex(@"(\s|<br/>)+$");
                Regex leadingBRs = new Regex(@"^(\s|<br/>)+");
                foreach (DiseaseMobileMessage dmm in dmms)
                {
                    if (dmm.sectionId == this.sectionNumber)
                    {
                        this.defaultTextIntro = dmm.message;
                        int firstParagraphEnd = this.defaultTextIntro.IndexOf("<br/>");
                        if (-1 != firstParagraphEnd)
                        {
                            if ((firstParagraphEnd + 5) < this.defaultTextIntro.Length)
                                this.defaultTextBody = this.defaultTextIntro.Substring(firstParagraphEnd + 5);
                            this.defaultTextIntro = this.defaultTextIntro.Substring(0, firstParagraphEnd);
                        }
                        Match ctmatch = Regex.Match(this.defaultTextBody, @"(^|(<br/>))(<conditional .+</conditional>)<br/>");
                        if (ctmatch.Success)
                        {
                            for (int gid = ctmatch.Groups.Count - 1;  gid >= 1;  --gid)
                                this.defaultTextBody = this.defaultTextBody.Remove(ctmatch.Groups[gid].Index, ctmatch.Groups[gid].Length);
                            for (int gid = 1;  gid < ctmatch.Groups.Count;  ++gid)
                            {
                                this.conditionalText += ctmatch.Groups[gid].Value;
                                this.conditionalText += "<br/>";
                            } // foreach
                        }
                        this.defaultTextIntro = leadingBRs.Replace(this.defaultTextIntro, "");
                        this.defaultTextBody = leadingBRs.Replace(this.defaultTextBody, "");
                        this.conditionalText = leadingBRs.Replace(this.conditionalText, "");
                        this.defaultTextIntro = trailingBRs.Replace(this.defaultTextIntro, "");
                        this.defaultTextBody = trailingBRs.Replace(this.defaultTextBody, "");
                        this.conditionalText = trailingBRs.Replace(this.conditionalText, "");
                    }
                } // foreach
            }
        } // class DiseaseMessageSection


        private static readonly Level[] SEVERITY_LEVELS = { new Level("minimal", 0),
                                                            new Level("mild", 1),
                                                            new Level("moderate", 2),
                                                            new Level("severe", 3) };


        /// <summary>
        /// Projection
        /// </summary>
        public class Projection
        {
            private Disease m_outer;
            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int id { get; private set; }
            /// <summary>
            /// Gets the disease.
            /// </summary>
            /// <value>
            /// The disease.
            /// </value>
            public string disease { get; private set; }
            /// <summary>
            /// Gets the type.
            /// </summary>
            /// <value>
            /// The type.
            /// </value>
            public string type { get; private set; }
            /// <summary>
            /// Gets the pronunciation.
            /// </summary>
            /// <value>
            /// The pronunciation.
            /// </value>
            public string pronunciation { get; private set; }
            /// <summary>
            /// Gets the microbe.
            /// </summary>
            /// <value>
            /// The microbe.
            /// </value>
            public string microbe { get; private set; }
            /// <summary>
            /// Gets the colloquial names.
            /// </summary>
            /// <value>
            /// The colloquial names.
            /// </value>
            public List<string> colloquialNames { get; private set; }
            /// <summary>
            /// Gets the search terms.
            /// </summary>
            /// <value>
            /// The search terms.
            /// </value>
            public List<string> searchTerms { get; private set; }
            /// <summary>
            /// Gets the transmission modes.
            /// </summary>
            /// <value>
            /// The transmission modes.
            /// </value>
            public List<string> transmissionModes { get; private set; }
            /// <summary>
            /// Gets the incubation.
            /// </summary>
            /// <value>
            /// The incubation.
            /// </value>
            public DiseaseIncubation.Projection incubation { get; private set; }
            /// <summary>
            /// Gets the default severity level.
            /// </summary>
            /// <value>
            /// The default severity level.
            /// </value>
            public Level defaultSeverityLevel { get; private set; }
            /// <summary>
            /// Gets the severity modifiers.
            /// </summary>
            /// <value>
            /// The severity modifiers.
            /// </value>
            public List<DiseaseSeverityMod.Projection> severityModifiers { get; private set; }
            /// <summary>
            /// Gets a value indicating whether this <see cref="Projection"/> is chronic.
            /// </summary>
            /// <value>
            ///   <c>true</c> if chronic; otherwise, <c>false</c>.
            /// </value>
            public bool chronic { get; private set; }
            /// <summary>
            /// Gets the treatability.
            /// </summary>
            /// <value>
            /// The treatability.
            /// </value>
            public double treatability { get; private set; }
            /// <summary>
            /// Gets the preventability.
            /// </summary>
            /// <value>
            /// The preventability.
            /// </value>
            public double preventability { get; private set; }
            /// <summary>
            /// Gets the preventions.
            /// </summary>
            /// <value>
            /// The preventions.
            /// </value>
            public List<DiseasePrevention.Projection> preventions { get; private set; }
            /// <summary>
            /// Gets the message sections.
            /// </summary>
            /// <value>
            /// The message sections.
            /// </value>
            public List<DiseaseMessageSection> messageSections { get; private set; }
            /// <summary>
            /// Gets the symptoms.
            /// </summary>
            /// <value>
            /// The symptoms.
            /// </value>
            public List<DiseaseSymptom.Projection> symptoms { get; private set; }
            /// <summary>
            /// Gets the last modified.
            /// </summary>
            /// <value>
            /// The last modified.
            /// </value>
            public double lastModified { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Projection"/> class.
            /// </summary>
            /// <param name="outer">The outer.</param>
            public Projection(Disease outer)
            {
                this.m_outer = outer;
                this.id = outer.diseaseId;
                this.disease = (outer.name.StartsWith("Malaria ") ?  "Malaria"  :  outer.name);   // HACK for Matt
                this.type = outer.diseaseType;
                this.pronunciation = outer.pronunciation;
                this.microbe = outer.microbe;
                this.colloquialNames = outer.colloquialNames.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToList();
                this.searchTerms = outer.searchTerms.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToList();
                this.transmissionModes = outer.DiseaseTransmissions.OrderBy(d => d.rank).ToList().ConvertAll(t => t.GetIconNames()).SelectMany(s => s).ToList();
                this.incubation = outer.DiseaseIncubations.GetProjection();
                this.defaultSeverityLevel = Level.ToLevel(outer.DiseaseSeverity.level, 3.0, SEVERITY_LEVELS, 0.0);
                this.severityModifiers = outer.DiseaseSeverityMods.ToList().ConvertAll(m => m.GetProjection());
                this.chronic = outer.DiseaseSeverity.chronic;
                this.treatability = outer.DiseaseSeverity.treatmentAvailable;
                this.preventability = outer.preventability;
                this.preventions = outer.DiseasePreventions.ToList().ConvertAll(p => p.GetProjection());
                this.symptoms = outer.DiseaseSymptoms.ToList().ConvertAll(s => s.GetProjection());
                TimeSpan tdiff = outer.lastModified - Globals.referenceDatum;
                this.lastModified = Math.Floor(tdiff.TotalSeconds);

                int baseSectionId = 100 * this.id;

                this.messageSections = new List<DiseaseMessageSection>() {
                    new DiseaseMessageSection(baseSectionId + 1, 1, "Overview"),
                    new DiseaseMessageSection(baseSectionId + 2, 2, "Transmission"),
                    new DiseaseMessageSection(baseSectionId + 3, 3, "Medical Prevention", outer.DiseasePreventions.ToList().ConvertAll(p => p.id + Globals.PREVENTIONS_IDSTART)),
                    new DiseaseMessageSection(baseSectionId + 4, 4, "Symptoms and Severity", outer.DiseaseSeverityMods.ToList().ConvertAll(m => m.conditionId + Globals.CONDITIONS_IDSTART)),
                    new DiseaseMessageSection(baseSectionId + 5, 5, "Recovery and Treatments")
                };
                foreach (DiseaseMessageSection dsm in this.messageSections)
                    dsm.setSectionText(outer.DiseaseMobileMessages);
            }
        } // class Disease.Projection

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
