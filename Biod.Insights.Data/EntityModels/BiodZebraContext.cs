using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biod.Insights.Data.EntityModels
{
    public partial class BiodZebraContext : DbContext
    {
        public BiodZebraContext()
        {
        }

        public BiodZebraContext(DbContextOptions<BiodZebraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcquisitionModes> AcquisitionModes { get; set; }
        public virtual DbSet<ActiveGeonames> ActiveGeonames { get; set; }
        public virtual DbSet<AgentTypes> AgentTypes { get; set; }
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<ArticleFeed> ArticleFeed { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BiosecurityRisk> BiosecurityRisk { get; set; }
        public virtual DbSet<CountryProvinceShapes> CountryProvinceShapes { get; set; }
        public virtual DbSet<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual DbSet<DiseaseSpeciesSymptomatic> DiseaseSpeciesSymptomatic { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventDestinationAirportSpreadMd> EventDestinationAirportSpreadMd { get; set; }
        public virtual DbSet<EventExtensionSpreadMd> EventExtensionSpreadMd { get; set; }
        public virtual DbSet<EventImportationRisksByGeonameSpreadMd> EventImportationRisksByGeonameSpreadMd { get; set; }
        public virtual DbSet<EventPriorities> EventPriorities { get; set; }
        public virtual DbSet<EventSourceAirportSpreadMd> EventSourceAirportSpreadMd { get; set; }
        public virtual DbSet<GeonameOutbreakPotential> GeonameOutbreakPotential { get; set; }
        public virtual DbSet<Geonames> Geonames { get; set; }
        public virtual DbSet<HamType> HamType { get; set; }
        public virtual DbSet<Interventions> Interventions { get; set; }
        public virtual DbSet<OutbreakPotentialCategory> OutbreakPotentialCategory { get; set; }
        public virtual DbSet<ProcessedArticle> ProcessedArticle { get; set; }
        public virtual DbSet<RelevanceState> RelevanceState { get; set; }
        public virtual DbSet<RelevanceType> RelevanceType { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<TransmissionModes> TransmissionModes { get; set; }
        public virtual DbSet<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual DbSet<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
        public virtual DbSet<XtblDiseaseAgents> XtblDiseaseAgents { get; set; }
        public virtual DbSet<XtblDiseaseInterventions> XtblDiseaseInterventions { get; set; }
        public virtual DbSet<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
        public virtual DbSet<XtblEventLocation> XtblEventLocation { get; set; }
        public virtual DbSet<XtblEventLocationHistory> XtblEventLocationHistory { get; set; }
        public virtual DbSet<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
        public virtual DbSet<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=BiodZebraContext", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcquisitionModes>(entity =>
            {
                entity.HasKey(e => e.AcquisitionModeId);

                entity.ToTable("AcquisitionModes", "disease");

                entity.Property(e => e.AcquisitionModeId).ValueGeneratedNever();

                entity.Property(e => e.AcquisitionModeDefinitionLabel).HasMaxLength(500);

                entity.Property(e => e.AcquisitionModeLabel).HasMaxLength(100);
            });

            modelBuilder.Entity<ActiveGeonames>(entity =>
            {
                entity.HasKey(e => e.GeonameId);

                entity.ToTable("ActiveGeonames", "place");

                entity.HasIndex(e => e.Admin1GeonameId)
                    .HasName("idx_ActiveGeonames_Admin1GeonameId");

                entity.HasIndex(e => e.CountryGeonameId)
                    .HasName("idx_ActiveGeonames_CountryGeonameId");

                entity.HasIndex(e => e.DisplayName)
                    .HasName("idx_ActiveGeonames_DisplayName");

                entity.HasIndex(e => e.LocationType)
                    .HasName("idx_ActiveGeonames_LocationType");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_ActiveGeonames_Name");

                entity.HasIndex(e => e.Population)
                    .HasName("idx_ActiveGeonames_Population");

                entity.HasIndex(e => e.SearchSeq2)
                    .HasName("idx_ActiveGeonames_SearchSeq2");

                entity.Property(e => e.GeonameId).ValueGeneratedNever();

                entity.Property(e => e.CountryName).HasMaxLength(64);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(135)
                    .IsUnicode(false);

                entity.Property(e => e.FeatureCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LatPopWeighted).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LongPopWeighted).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ModificationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<AgentTypes>(entity =>
            {
                entity.HasKey(e => e.AgentTypeId);

                entity.ToTable("AgentTypes", "disease");

                entity.Property(e => e.AgentTypeId).ValueGeneratedNever();

                entity.Property(e => e.AgentType)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Agents>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.ToTable("Agents", "disease");

                entity.Property(e => e.AgentId).ValueGeneratedNever();

                entity.Property(e => e.Agent)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .HasConstraintName("FK_Agents_AgentTypeId");
            });

            modelBuilder.Entity<ArticleFeed>(entity =>
            {
                entity.ToTable("ArticleFeed", "surveillance");

                entity.Property(e => e.ArticleFeedId).ValueGeneratedNever();

                entity.Property(e => e.ArticleFeedName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.AoiGeonameIds)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.NewCaseNotificationEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NewOutbreakNotificationEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PeriodicNotificationEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.WeeklyOutbreakNotificationEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_dbo.AspNetUsers_dbo.UserGroup_GroupId");
            });

            modelBuilder.Entity<BiosecurityRisk>(entity =>
            {
                entity.HasKey(e => e.BiosecurityRiskCode);

                entity.ToTable("BiosecurityRisk", "disease");

                entity.Property(e => e.BiosecurityRiskCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BiosecurityRiskDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CountryProvinceShapes>(entity =>
            {
                entity.HasKey(e => e.GeonameId);

                entity.ToTable("CountryProvinceShapes", "place");

                entity.HasIndex(e => e.LocationType)
                    .HasName("idx_CountryProvinceShapes_LocationType");

                entity.HasIndex(e => e.SimplifiedShape)
                    .HasName("sidx_SimplifiedShape");

                entity.Property(e => e.GeonameId).ValueGeneratedNever();

                entity.Property(e => e.SimplifiedShapeText).IsUnicode(false);

                entity.HasOne(d => d.Geoname)
                    .WithOne(p => p.CountryProvinceShapes)
                    .HasForeignKey<CountryProvinceShapes>(d => d.GeonameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryProvinceShapes_Geoname");
            });

            modelBuilder.Entity<DiseaseSpeciesIncubation>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId });

                entity.ToTable("DiseaseSpeciesIncubation", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseSpeciesIncubation)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_DiseaseSpeciesIncubation_DiseaseId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.DiseaseSpeciesIncubation)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_DiseaseSpeciesIncubation_SpeciesId");
            });

            modelBuilder.Entity<DiseaseSpeciesSymptomatic>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId });

                entity.ToTable("DiseaseSpeciesSymptomatic", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseSpeciesSymptomatic)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_DiseaseSpeciesSymptomatic_DiseaseId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.DiseaseSpeciesSymptomatic)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_DiseaseSpeciesSymptomatic_SpeciesId");
            });

            modelBuilder.Entity<Diseases>(entity =>
            {
                entity.HasKey(e => e.DiseaseId);

                entity.ToTable("Diseases", "disease");

                entity.Property(e => e.DiseaseId).ValueGeneratedNever();

                entity.Property(e => e.BiosecurityRisk)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DiseaseType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Pronunciation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SeverityLevel)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TreatmentAvailable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BiosecurityRiskNavigation)
                    .WithMany(p => p.Diseases)
                    .HasForeignKey(d => d.BiosecurityRisk)
                    .HasConstraintName("FK_Diseases_BiosecurityRisk");

                entity.HasOne(d => d.ParentDisease)
                    .WithMany(p => p.InverseParentDisease)
                    .HasForeignKey(d => d.ParentDiseaseId)
                    .HasConstraintName("FK_Diseases_ParentDiseaseId");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event", "surveillance");

                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EventMongoId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.EventTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedByUserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.DiseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Disease");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_Event_PriorityId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_SpeciesId");
            });

            modelBuilder.Entity<EventDestinationAirportSpreadMd>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.DestinationStationId });

                entity.ToTable("EventDestinationAirportSpreadMd", "zebra");

                entity.HasIndex(e => e.DestinationStationId)
                    .HasName("idx_DestinationStationId");

                entity.Property(e => e.CityDisplayName).HasMaxLength(200);

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.StationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StationName).HasMaxLength(64);

                entity.HasOne(d => d.DestinationStation)
                    .WithMany(p => p.EventDestinationAirportSpreadMd)
                    .HasForeignKey(d => d.DestinationStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirportSpreadMd_DestinationStationId");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationAirportSpreadMd)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirportSpreadMd_EventId");
            });

            modelBuilder.Entity<EventExtensionSpreadMd>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("EventExtensionSpreadMd", "zebra");

                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.Property(e => e.MaxExportationProbabilityViaAirports).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MaxExportationVolumeViaAirports).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinExportationProbabilityViaAirports).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExportationVolumeViaAirports).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.EventExtensionSpreadMd)
                    .HasForeignKey<EventExtensionSpreadMd>(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventExtensionSpreadMd_EventId");
            });

            modelBuilder.Entity<EventImportationRisksByGeonameSpreadMd>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GeonameId });

                entity.ToTable("EventImportationRisksByGeonameSpreadMd", "zebra");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MaxVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinVolume).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventImportationRisksByGeonameSpreadMd)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventImportationRisksByGeonameSpreadMd_EventId");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.EventImportationRisksByGeonameSpreadMd)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_EventImportationRisksByGeonamerSpreadMd_GeonameId");
            });

            modelBuilder.Entity<EventPriorities>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("EventPriorities", "surveillance");

                entity.Property(e => e.PriorityId).ValueGeneratedNever();

                entity.Property(e => e.PriorityTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventSourceAirportSpreadMd>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.SourceStationId });

                entity.ToTable("EventSourceAirportSpreadMd", "zebra");

                entity.HasIndex(e => e.SourceStationId)
                    .HasName("idx_EventSourceAirportSpreadMd_SourceStationId");

                entity.Property(e => e.CityDisplayName).HasMaxLength(200);

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.StationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StationName).HasMaxLength(64);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSourceAirportSpreadMd)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventSourceAirportSpreadMd_EventId");

                entity.HasOne(d => d.SourceStation)
                    .WithMany(p => p.EventSourceAirportSpreadMd)
                    .HasForeignKey(d => d.SourceStationId)
                    .HasConstraintName("FK_EventSourceAirportSpreadMd_StationId");
            });

            modelBuilder.Entity<GeonameOutbreakPotential>(entity =>
            {
                entity.HasKey(e => new { e.GeonameId, e.DiseaseId });

                entity.ToTable("GeonameOutbreakPotential", "disease");

                entity.Property(e => e.EffectiveMessage)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.GeonameOutbreakPotential)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_GeonameOutbreakPotential_Disease");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.GeonameOutbreakPotential)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_GeonameOutbreakPotential_Geoname");
            });

            modelBuilder.Entity<Geonames>(entity =>
            {
                entity.HasKey(e => e.GeonameId);

                entity.ToTable("Geonames", "place");

                entity.HasIndex(e => e.Admin1GeonameId)
                    .HasName("idx_Geonames_Admin1GeonameId");

                entity.HasIndex(e => e.CountryGeonameId)
                    .HasName("idx_Geonames_CountryGeonameId");

                entity.HasIndex(e => e.DisplayName)
                    .HasName("idx_Geonames_DisplayName");

                entity.HasIndex(e => e.LocationType)
                    .HasName("idx_Geonames_LocationType");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_Geonames_Name");

                entity.HasIndex(e => e.Population)
                    .HasName("idx_Geonames_Population");

                entity.HasIndex(e => e.SearchSeq2)
                    .HasName("idx_Geonames_SearchSeq2");

                entity.Property(e => e.GeonameId).ValueGeneratedNever();

                entity.Property(e => e.CountryName).HasMaxLength(64);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(135)
                    .IsUnicode(false);

                entity.Property(e => e.FeatureCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LatPopWeighted).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.LongPopWeighted).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ModificationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.Admin1Geoname)
                    .WithMany(p => p.InverseAdmin1Geoname)
                    .HasForeignKey(d => d.Admin1GeonameId)
                    .HasConstraintName("FK_Geonames_Province");

                entity.HasOne(d => d.CountryGeoname)
                    .WithMany(p => p.InverseCountryGeoname)
                    .HasForeignKey(d => d.CountryGeonameId)
                    .HasConstraintName("FK_Geonames_Country");
            });

            modelBuilder.Entity<HamType>(entity =>
            {
                entity.ToTable("HamType", "surveillance");

                entity.Property(e => e.HamTypeId).ValueGeneratedNever();

                entity.Property(e => e.HamTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Interventions>(entity =>
            {
                entity.HasKey(e => e.InterventionId);

                entity.ToTable("Interventions", "disease");

                entity.Property(e => e.InterventionId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InterventionType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OutbreakPotentialCategory>(entity =>
            {
                entity.ToTable("OutbreakPotentialCategory", "disease");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EffectiveMessage)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EffectiveMessageDescription)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.MapThreshold)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rule)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ProcessedArticle>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.ToTable("ProcessedArticle", "surveillance");

                entity.HasIndex(e => e.ArticleFeedId)
                    .HasName("idx_ProcessedArticle_ArticleFeedId");

                entity.HasIndex(e => e.FeedPublishedDate)
                    .HasName("idx_ProcessedArticle_FeedPublishedDate");

                entity.HasIndex(e => e.HamTypeId)
                    .HasName("idx_ProcessedArticle_HamTypeId");

                entity.HasIndex(e => e.IsCompleted)
                    .HasName("idx_ProcessedArticle_IsCompleted");

                entity.HasIndex(e => e.SimilarClusterId)
                    .HasName("idx_ProcessedArticle_SimilarClusterId");

                entity.HasIndex(e => e.SystemLastModifiedDate)
                    .HasName("idx_ProcessedArticle_SystemLastModifiedDate");

                entity.HasIndex(e => e.UserLastModifiedDate)
                    .HasName("idx_ProcessedArticle_UserLastModifiedDate");

                entity.Property(e => e.ArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ArticleFeedType).HasMaxLength(256);

                entity.Property(e => e.ArticleTitle).HasMaxLength(500);

                entity.Property(e => e.CertaintyScore).HasColumnType("decimal(18, 16)");

                entity.Property(e => e.FeedPublishedDate).HasColumnType("datetime");

                entity.Property(e => e.FeedSourceId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FeedUrl)
                    .HasColumnName("FeedURL")
                    .HasMaxLength(2000);

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUpdatedByUserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalLanguage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalSourceUrl)
                    .HasColumnName("OriginalSourceURL")
                    .HasMaxLength(2000);

                entity.Property(e => e.SimilarClusterId).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.SystemLastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserLastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ArticleFeed)
                    .WithMany(p => p.ProcessedArticle)
                    .HasForeignKey(d => d.ArticleFeedId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProcessedArticle_ArticleFeed");

                entity.HasOne(d => d.HamType)
                    .WithMany(p => p.ProcessedArticle)
                    .HasForeignKey(d => d.HamTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ProcessedArticle_HamType");
            });

            modelBuilder.Entity<RelevanceState>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("RelevanceState", "zebra");

                entity.Property(e => e.StateId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<RelevanceType>(entity =>
            {
                entity.HasKey(e => e.RelevanceId);

                entity.ToTable("RelevanceType", "zebra");

                entity.Property(e => e.RelevanceId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.ToTable("Species", "disease");

                entity.Property(e => e.SpeciesId).ValueGeneratedNever();

                entity.Property(e => e.SpeciesName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stations>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.ToTable("Stations", "zebra");

                entity.HasIndex(e => e.CityGeonameId)
                    .HasName("idx_Stations_CityGeonameId");

                entity.HasIndex(e => e.CityId)
                    .HasName("idx_Stations_CityId");

                entity.HasIndex(e => e.ValidToDate)
                    .HasName("idx_Stations_ValidToDate");

                entity.Property(e => e.StationId).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.StatioType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StationGridName).HasMaxLength(200);

                entity.Property(e => e.ValidFromDate).HasColumnType("date");

                entity.Property(e => e.ValidToDate).HasColumnType("date");

                entity.HasOne(d => d.CityGeoname)
                    .WithMany(p => p.StationsCityGeoname)
                    .HasForeignKey(d => d.CityGeonameId)
                    .HasConstraintName("FK_Stations_CityGeoname");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.StationsGeoname)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_Stations_Geoname");
            });

            modelBuilder.Entity<TransmissionModes>(entity =>
            {
                entity.HasKey(e => e.TransmissionModeId);

                entity.ToTable("TransmissionModes", "disease");

                entity.Property(e => e.TransmissionModeId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransmissionMode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserEmailNotification>(entity =>
            {
                entity.Property(e => e.AoiGeonameIds)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserEmailNotification)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.UserEmailNotification_surveillance.Event_EventId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEmailNotification)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserEmailNotification_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<XtblArticleEvent>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.EventId })
                    .HasName("PK_Article_Event");

                entity.ToTable("Xtbl_Article_Event", "surveillance");

                entity.HasIndex(e => e.EventId)
                    .HasName("idx_Xtbl_Article_Event_EventId");

                entity.Property(e => e.ArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.XtblArticleEvent)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Xtbl_Article_Event_ProcessedArticle");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.XtblArticleEvent)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Xtbl_Article_Event_Event");
            });

            modelBuilder.Entity<XtblDiseaseAcquisitionMode>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId, e.AcquisitionModeId });

                entity.ToTable("Xtbl_Disease_AcquisitionMode", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcquisitionMode)
                    .WithMany(p => p.XtblDiseaseAcquisitionMode)
                    .HasForeignKey(d => d.AcquisitionModeId)
                    .HasConstraintName("FK_Xtbl_Disease_AcquisitionMode_AcquisitionModeId");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseAcquisitionMode)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_AcquisitionMode_DiseaseId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.XtblDiseaseAcquisitionMode)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_Xtbl_Disease_AcquisitionMode_SpeciesId");
            });

            modelBuilder.Entity<XtblDiseaseAgents>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.AgentId });

                entity.ToTable("Xtbl_Disease_Agents", "disease");

                entity.HasIndex(e => e.AgentId)
                    .HasName("idx_Xtbl_Disease_Agents_AgentId");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.XtblDiseaseAgents)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Xtbl_Disease_Agents_AgentId");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseAgents)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Agents_DiseaseId");
            });

            modelBuilder.Entity<XtblDiseaseInterventions>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId, e.InterventionId });

                entity.ToTable("Xtbl_Disease_Interventions", "disease");

                entity.HasIndex(e => e.InterventionId)
                    .HasName("idx_Xtbl_Disease_Interventions_InterventionId");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseInterventions)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Interventions_DiseaseId");

                entity.HasOne(d => d.Intervention)
                    .WithMany(p => p.XtblDiseaseInterventions)
                    .HasForeignKey(d => d.InterventionId)
                    .HasConstraintName("FK_Xtbl_Disease_Interventions_InterventionId");
            });

            modelBuilder.Entity<XtblDiseaseTransmissionMode>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId, e.TransmissionModeId });

                entity.ToTable("Xtbl_Disease_TransmissionMode", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseTransmissionMode)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_TransmissionMode_DiseaseId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.XtblDiseaseTransmissionMode)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_Xtbl_Disease_TransmissionMode_SpeciesId");

                entity.HasOne(d => d.TransmissionMode)
                    .WithMany(p => p.XtblDiseaseTransmissionMode)
                    .HasForeignKey(d => d.TransmissionModeId)
                    .HasConstraintName("FK_Xtbl_Disease_TransmissionMode_TransmissionModeId");
            });

            modelBuilder.Entity<XtblEventLocation>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GeonameId });

                entity.ToTable("Xtbl_Event_Location", "surveillance");

                entity.HasIndex(e => e.EventDate)
                    .HasName("idx_Xtbl_Event_Location_EventDate");

                entity.HasIndex(e => e.GeonameId)
                    .HasName("idx_Xtbl_Event_Location_GeonameId");

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.XtblEventLocation)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Xtbl_Event_Location_Event");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.XtblEventLocation)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_Xtbl_Event_Location_Geoname");
            });

            modelBuilder.Entity<XtblEventLocationHistory>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GeonameId, e.EventDateType });

                entity.ToTable("Xtbl_Event_Location_history", "surveillance");

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.XtblEventLocationHistory)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Xtbl_Event_Location_history_Event");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.XtblEventLocationHistory)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_Xtbl_Event_Location_history_Geoname");
            });

            modelBuilder.Entity<XtblRoleDiseaseRelevance>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.DiseaseId });

                entity.ToTable("Xtbl_Role_Disease_Relevance", "zebra");

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblRoleDiseaseRelevance)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Role_Disease_Relevance_Diseases");

                entity.HasOne(d => d.Relevance)
                    .WithMany(p => p.XtblRoleDiseaseRelevance)
                    .HasForeignKey(d => d.RelevanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xtbl_Role_Disease_Relevance_RelevanceType");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.XtblRoleDiseaseRelevance)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Xtbl_Role_Disease_Relevance_AspNetRoles");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.XtblRoleDiseaseRelevance)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xtbl_Role_Disease_Relevance_RelevanceState");
            });

            modelBuilder.Entity<XtblUserDiseaseRelevance>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DiseaseId });

                entity.ToTable("Xtbl_User_Disease_Relevance", "zebra");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblUserDiseaseRelevance)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_User_Disease_Relevance_Diseases");

                entity.HasOne(d => d.Relevance)
                    .WithMany(p => p.XtblUserDiseaseRelevance)
                    .HasForeignKey(d => d.RelevanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xtbl_User_Disease_Relevance_RelevanceType");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.XtblUserDiseaseRelevance)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xtbl_User_Disease_Relevance_RelevanceState");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.XtblUserDiseaseRelevance)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Xtbl_User_Disease_Relevance_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
