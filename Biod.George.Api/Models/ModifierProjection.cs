namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Diagnostics;
    using System.Linq;
    using DiseasesAPI;

    /// <summary>
    /// Modifier Projection
    /// </summary>
    public class ModifierProjection
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; private set; }
        /// <summary>
        /// Gets the modifier.
        /// </summary>
        /// <value>
        /// The modifier.
        /// </value>
        public string modifier { get; private set; }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string displayName { get; private set; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string type { get; private set; }
        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>
        /// The predicate.
        /// </value>
        public string predicate { get; private set; }
        /// <summary>
        /// Gets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string question { get; private set; }
        /// <summary>
        /// Gets the warn about vaccinations.
        /// </summary>
        /// <value>
        /// The warn about vaccinations.
        /// </value>
        public List<object> warnAboutVaccinations { get; private set; }
        /// <summary>
        /// Gets or sets the default sort order.
        /// </summary>
        /// <value>
        /// The default sort order.
        /// </value>
        public int defaultSortOrder { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierProjection"/> class.
        /// </summary>
        /// <param name="outer">The outer.</param>
        public ModifierProjection(DiseasePrevention outer)
        {
            this.id = outer.id + Globals.PREVENTIONS_IDSTART;
            // TODO:  distinguish among vaccine types if there's more than 1
            this.modifier = outer.DiseaseRef.name;
            this.displayName = outer.DiseaseRef.name;
            string[] colloquialNames = outer.DiseaseRef.colloquialNames.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToArray();
            if (outer.PreventionType.type == "Prophylaxis")
            {
                if (colloquialNames.Length > 0  &&  colloquialNames[0].Length > 0)
                    this.question = "Are or will you be taking <b>anti-" + colloquialNames[0] + " medication</b>?";
                else
                    this.question = "Are or will you be taking <b>anti-" + this.displayName + " medication</b>?";
                this.displayName += " [prophylaxis]";
            }
            else
            {
                this.question = "Have you been vaccinated against <b>" + this.displayName + "</b>?";
                if (colloquialNames.Length > 0  &&  colloquialNames[0].Length > 0)
                    this.displayName += " (" + colloquialNames[0] + ")";
            }
            this.type = "boolean";
            this.predicate = "";
            this.warnAboutVaccinations = new List<object>();
            foreach (DiseasePreventionMod dpm in outer.DiseasePreventionMods)
                this.warnAboutVaccinations.Add(new { conditionModifierId = dpm.conditionId + Globals.CONDITIONS_IDSTART, messageId = dpm.messageId });
            this.defaultSortOrder = -1;
            // HACK:  Combining both malarias into one
            if (this.modifier.StartsWith("Malaria "))
            { 
                this.modifier = "Malaria";
                this.displayName = "Malaria [prophylaxis]";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierProjection"/> class.
        /// </summary>
        /// <param name="outer">The outer.</param>
        public ModifierProjection(Condition outer)
        {
            this.id = outer.conditionId + Globals.CONDITIONS_IDSTART;
            this.modifier = outer.conditionName;
            this.warnAboutVaccinations = new List<object>();
            foreach (DiseasePreventionMod dpm in outer.DiseasePreventionMods)
                this.warnAboutVaccinations.Add(new { preventionModifierId = dpm.prevention + Globals.PREVENTIONS_IDSTART, messageId = dpm.messageId });
            this.defaultSortOrder = -1;

            this.type = "boolean";
            this.predicate = "";
            this.displayName = this.modifier.Split(new string[]{" ("}, StringSplitOptions.None)[0];
            this.question = outer.question;
            if (this.modifier == "Sex")
            {
                this.type = "list={Male,Female}";
                this.predicate = "$diseaseParameter == $uiChoice";
            }
            else if (this.displayName != this.modifier)
            {
                // UGLY!!!
                if (this.modifier == "Birth year (age>N)") 
                {
                    this.type = "year";
                    this.predicate = "($currentYear - $uiYear) > $diseaseParameter";
                }
                else if (this.modifier == "Birth year (age<N)") 
                {
                    this.type = "year";
                    this.predicate = "($currentYear - $uiYear) < $diseaseParameter";
                }
                else if (this.modifier == "Pregnant (third trimester)")
                {
                    this.type = "month";
                    this.predicate = "($uiMonth - $currentMonth) < 3";
                }
                else if (this.modifier == "Pregnant (second trimester)")
                {
                    this.type = "month";
                    this.predicate = "($uiMonth - $currentMonth) >= 3  &&  ($uiMonth - $currentMonth) < 6";
                }
                else if (this.modifier == "Pregnant (first trimester)")
                {
                    this.type = "month";
                    this.predicate = "($uiMonth - $currentMonth) >= 6";
                }
                else
                {
                    Debug.Assert(false);
                }
            }
        }
    }
}
