namespace BlueDot.DiseasesAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Diseases Model
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public partial class DiseasesModel : DbContext
    {
        /// <summary>
        /// Gets a value indicating whether [track objects].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [track objects]; otherwise, <c>false</c>.
        /// </value>
        public bool trackObjects { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiseasesModel"/> class.
        /// </summary>
        /// <param name="trackObjects">if set to <c>true</c> [track objects].</param>
        public DiseasesModel(bool trackObjects = false)
            : base("name=DiseasesAPI")
        {
            this.Configuration.AutoDetectChangesEnabled = false;    // Since this context is read only, can turn off change detection to help performance
            this.trackObjects = trackObjects;
            //this.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));    // For logging all DB queries...
        }

        // The following accessors are specialized to enforce that this model is read only
        /*  Note that Entity Framework requires that Set<T>() calls return the same instance each time that it is called 
         *  for a given context instance and entity type. Also, the non-generic DbSet returned by the Set method must 
         *  wrap the same underlying query and set of entities. */
        /// <summary>
        /// Gets the version infos.
        /// </summary>
        /// <value>
        /// The version infos.
        /// </value>
        public virtual DbQuery<VersionInfo> VersionInfos { get { return (this.trackObjects ?  Set<VersionInfo>()  :  Set<VersionInfo>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease incubations.
        /// </summary>
        /// <value>
        /// The disease incubations.
        /// </value>
        public virtual DbQuery<DiseaseIncubation> DiseaseIncubations { get { return (this.trackObjects ?  Set<DiseaseIncubation>()  :  Set<DiseaseIncubation>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease preventions.
        /// </summary>
        /// <value>
        /// The disease preventions.
        /// </value>
        public virtual DbQuery<DiseasePrevention> DiseasePreventions { get { return (this.trackObjects ?  Set<DiseasePrevention>()  :  Set<DiseasePrevention>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease prevention mods.
        /// </summary>
        /// <value>
        /// The disease prevention mods.
        /// </value>
        public virtual DbQuery<DiseasePreventionMod> DiseasePreventionMods { get { return (this.trackObjects ?  Set<DiseasePreventionMod>()  :  Set<DiseasePreventionMod>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease severity mods.
        /// </summary>
        /// <value>
        /// The disease severity mods.
        /// </value>
        public virtual DbQuery<DiseaseSeverityMod> DiseaseSeverityMods { get { return (this.trackObjects ?  Set<DiseaseSeverityMod>()  :  Set<DiseaseSeverityMod>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease transmissions.
        /// </summary>
        /// <value>
        /// The disease transmissions.
        /// </value>
        public virtual DbQuery<DiseaseTransmission> DiseaseTransmissions { get { return (this.trackObjects ?  Set<DiseaseTransmission>()  :  Set<DiseaseTransmission>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease seasonalities.
        /// </summary>
        /// <value>
        /// The disease seasonalities.
        /// </value>
        public virtual DbQuery<DiseaseSeasonality> DiseaseSeasonalities { get { return (this.trackObjects ?  Set<DiseaseSeasonality>()  :  Set<DiseaseSeasonality>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease mobile messages.
        /// </summary>
        /// <value>
        /// The disease mobile messages.
        /// </value>
        public virtual DbQuery<DiseaseMobileMessage> DiseaseMobileMessages { get { return (this.trackObjects ?  Set<DiseaseMobileMessage>()  :  Set<DiseaseMobileMessage>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease severities.
        /// </summary>
        /// <value>
        /// The disease severities.
        /// </value>
        public virtual DbQuery<DiseaseSeverity> DiseaseSeverities { get { return (this.trackObjects ?  Set<DiseaseSeverity>()  :  Set<DiseaseSeverity>().AsNoTracking()); } }
        /// <summary>
        /// Gets the disease symptoms.
        /// </summary>
        /// <value>
        /// The disease symptoms.
        /// </value>
        public virtual DbQuery<DiseaseSymptom> DiseaseSymptoms { get { return (this.trackObjects ?  Set<DiseaseSymptom>()  :  Set<DiseaseSymptom>().AsNoTracking()); } }
        // TAI:  Consider tracking the following since they're shared across diseases (but not now, since not used in Location queries)...
        /// <summary>
        /// Gets the modifier categories.
        /// </summary>
        /// <value>
        /// The modifier categories.
        /// </value>
        public virtual DbQuery<ModifierCategory> ModifierCategories { get { return (this.trackObjects ?  Set<ModifierCategory>()  :  Set<ModifierCategory>().AsNoTracking()); } }
        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public virtual DbQuery<Condition> Conditions { get { return (this.trackObjects ?  Set<Condition>()  :  Set<Condition>().AsNoTracking()); } }
        /// <summary>
        /// Gets the prevention types.
        /// </summary>
        /// <value>
        /// The prevention types.
        /// </value>
        public virtual DbQuery<PreventionType> PreventionTypes { get { return (this.trackObjects ?  Set<PreventionType>()  :  Set<PreventionType>().AsNoTracking()); } }
        /// <summary>
        /// Gets the mobile message sections.
        /// </summary>
        /// <value>
        /// The mobile message sections.
        /// </value>
        public virtual DbQuery<MobileMessageSection> MobileMessageSections { get { return (this.trackObjects ?  Set<MobileMessageSection>()  :  Set<MobileMessageSection>()); } }
        /// <summary>
        /// Gets the symptom categories.
        /// </summary>
        /// <value>
        /// The symptom categories.
        /// </value>
        public virtual DbQuery<SymptomCategory> SymptomCategories { get { return (this.trackObjects ?  Set<SymptomCategory>()  :  Set<SymptomCategory>().AsNoTracking()); } }
        // The following accessors have to return (tracked) DbSet<> objects given how they're used (using .Find()), so we just don't define any setters
        /// <summary>
        /// Gets the diseases.
        /// </summary>
        /// <value>
        /// The diseases.
        /// </value>
        public virtual DbSet<Disease> Diseases { get { return Set<Disease>(); } }
        /// <summary>
        /// Gets the symptoms.
        /// </summary>
        /// <value>
        /// The symptoms.
        /// </value>
        public virtual DbSet<Symptom> Symptoms { get { return Set<Symptom>(); } }
        // Always track the following objects since they're shared across diseases and used in Location queries that hit more than one disease at a time...
        /// <summary>
        /// Gets the transmission modes.
        /// </summary>
        /// <value>
        /// The transmission modes.
        /// </value>
        public virtual DbQuery<TransmissionMode> TransmissionModes { get { return Set<TransmissionMode>(); } }


        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <exception cref="InvalidOperationException">The database context is read-only.</exception>
        public override int SaveChanges()
        {
            throw new InvalidOperationException("The database context is read-only.");
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <exception cref="InvalidOperationException">The database context is read-only.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public override Task<int> SaveChangesAsync()
        {
            throw new InvalidOperationException("The database context is read-only.");
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // These are needed since there are no DbSet<*> properties due to above specialization for a read-only model.
            modelBuilder.Entity<DiseasePreventionMod>();
            modelBuilder.Entity<DiseaseSeverityMod>();

            modelBuilder.Entity<VersionInfo>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<ModifierCategory>()
                .Property(e => e.categoryLabel)
                .IsUnicode(false);

            modelBuilder.Entity<Condition>()
                .Property(e => e.conditionName)
                .IsUnicode(false);

            modelBuilder.Entity<Condition>()
                .Property(e => e.question)
                .IsUnicode(false);

            modelBuilder.Entity<Condition>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Symptom>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Symptom>()
                .Property(e => e.altNames)
                .IsUnicode(false);

            modelBuilder.Entity<Symptom>()
                .Property(e => e.definition)
                .IsUnicode(false);

            modelBuilder.Entity<Symptom>()
                .Property(e => e.definitionSource)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.colloquialNames)
                .IsUnicode(true);

            modelBuilder.Entity<Disease>()
                .Property(e => e.searchTerms)
                .IsUnicode(true);

            modelBuilder.Entity<Disease>()
                .Property(e => e.pronunciation)
                .IsUnicode(true);

            modelBuilder.Entity<Disease>()
                .Property(e => e.diseaseType)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.microbe)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.mapGranularity)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.environmentalFactors)
                .IsUnicode(false);

            modelBuilder.Entity<Disease>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<PreventionType>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<TransmissionMode>()
                .Property(e => e.mode)
                .IsUnicode(false);

            modelBuilder.Entity<TransmissionMode>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<TransmissionMode>()
                .Property(e => e.preventions)
                .IsUnicode(false);

            modelBuilder.Entity<DiseaseIncubation>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<DiseaseIncubation>()
                .Property(e => e.source)
                .IsUnicode(false);

            modelBuilder.Entity<DiseasePrevention>()
                .Property(e => e.availability)
                .IsUnicode(false);

            modelBuilder.Entity<DiseasePrevention>()
                .Property(e => e.duration)
                .IsUnicode(false);

            modelBuilder.Entity<DiseasePrevention>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<DiseaseTransmission>()
                .Property(e => e.agents)
                .IsUnicode(false);

            modelBuilder.Entity<DiseaseTransmission>()
                .Property(e => e.contact)
                .IsUnicode(false);

            modelBuilder.Entity<DiseaseTransmission>()
                .Property(e => e.actions)
                .IsUnicode(false);

            modelBuilder.Entity<MobileMessageSection>()
                .Property(e => e.sectionName)
                .IsUnicode(false);


            modelBuilder.Entity<Condition>()
                .HasRequired(e => e.ModifierCategory)
                .WithMany(e => e.Conditions)
                .HasForeignKey(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SymptomCategory>()
                .HasMany(e => e.Symptoms)
                .WithRequired(e => e.SymptomCategory)
                .HasForeignKey(e => e.symptomCategoryId);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.DiseaseSeasonalities)     // TODO:  Optional
                .WithRequired(e => e.DiseaseRef)
                .HasForeignKey(e => e.diseaseId);

            modelBuilder.Entity<Disease>()
                .HasRequired(e => e.DiseaseIncubations)
                .WithRequiredPrincipal(e => e.DiseaseRef);

            modelBuilder.Entity<Disease>()
                .HasRequired(e => e.DiseaseSeverity)
                .WithRequiredPrincipal(e => e.DiseaseRef);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.DiseaseSeverityMods)
                .WithRequired(e => e.DiseaseRef)
                .HasForeignKey(e => e.diseaseId);

            modelBuilder.Entity<Condition>()
                .HasMany(e => e.DiseaseSeverityMods)
                .WithRequired(e => e.ConditionRef)
                .HasForeignKey(e => e.conditionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.DiseaseTransmissions)
                .WithRequired(e => e.DiseaseRef)
                .HasForeignKey(e => e.diseaseId);

            modelBuilder.Entity<TransmissionMode>()
                .HasMany(e => e.DiseaseTransmissions)
                .WithRequired(e => e.TransmissionMode)
                .HasForeignKey(e => e.mode);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.DiseasePreventions)
                .WithRequired(e => e.DiseaseRef)
                .HasForeignKey(e => e.diseaseId);

            modelBuilder.Entity<DiseasePrevention>()
                .HasMany(e => e.DiseasePreventionMods)
                .WithRequired(e => e.DiseasePrevention)
                .HasForeignKey(e => e.prevention);

            modelBuilder.Entity<DiseasePrevention>()
                .HasRequired(e => e.ModifierCategory)
                .WithMany(e => e.DiseasePreventions)
                .HasForeignKey(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Condition>()
                .HasMany(e => e.DiseasePreventionMods)
                .WithRequired(e => e.ConditionRef)
                .HasForeignKey(e => e.conditionId);

            modelBuilder.Entity<PreventionType>()
                .HasMany(e => e.DiseasePreventions)
                .WithRequired(e => e.PreventionType)
                .HasForeignKey(e => e.type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MobileMessageSection>()
                .HasMany(e => e.DiseaseMobileMessages)
                .WithRequired(e => e.MobileMessageSection)
                .HasForeignKey(e => e.sectionId);

            modelBuilder.Entity<Disease>()
                .HasMany(e => e.DiseaseMobileMessages)
                .WithRequired(e => e.DiseaseRef)
                .HasForeignKey(e => e.diseaseId);

            modelBuilder.Entity<DiseaseSymptom>()
                .HasRequired(e => e.DiseaseRef)
                .WithMany(e => e.DiseaseSymptoms)
                .HasForeignKey(e => e.diseaseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiseaseSymptom>()
                .HasRequired(e => e.Symptom)
                .WithMany(e => e.DiseaseSymptoms)
                .HasForeignKey(e => e.symptomId)
                .WillCascadeOnDelete(false);
        }
    }
}
