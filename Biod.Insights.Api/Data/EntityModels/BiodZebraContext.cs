using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biod.Insights.Api.Data.EntityModels
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
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<AgentTypes> AgentTypes { get; set; }
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<AirportRanking> AirportRanking { get; set; }
        public virtual DbSet<AppRequestInfoLog> AppRequestInfoLog { get; set; }
        public virtual DbSet<ArticleFeed> ArticleFeed { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BiosecurityRisk> BiosecurityRisk { get; set; }
        public virtual DbSet<ConfigurationVariables> ConfigurationVariables { get; set; }
        public virtual DbSet<CountryProvinceShapes> CountryProvinceShapes { get; set; }
        public virtual DbSet<CountryProvinceShapesBak> CountryProvinceShapesBak { get; set; }
        public virtual DbSet<CustomGroups> CustomGroups { get; set; }
        public virtual DbSet<DiseaseEventDestinationAirport> DiseaseEventDestinationAirport { get; set; }
        public virtual DbSet<DiseaseEventDestinationGrid> DiseaseEventDestinationGrid { get; set; }
        public virtual DbSet<DiseaseEventPrevalence> DiseaseEventPrevalence { get; set; }
        public virtual DbSet<DiseaseSourceAirport> DiseaseSourceAirport { get; set; }
        public virtual DbSet<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual DbSet<DiseaseSpeciesSymptomatic> DiseaseSpeciesSymptomatic { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventCreationReasons> EventCreationReasons { get; set; }
        public virtual DbSet<EventDestinationAirport> EventDestinationAirport { get; set; }
        public virtual DbSet<EventDestinationAirportHistory> EventDestinationAirportHistory { get; set; }
        public virtual DbSet<EventDestinationAirportSpreadMd> EventDestinationAirportSpreadMd { get; set; }
        public virtual DbSet<EventDestinationGrid> EventDestinationGrid { get; set; }
        public virtual DbSet<EventDestinationGridHistory> EventDestinationGridHistory { get; set; }
        public virtual DbSet<EventDestinationGridSpreadMd> EventDestinationGridSpreadMd { get; set; }
        public virtual DbSet<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public virtual DbSet<EventGroupByFields> EventGroupByFields { get; set; }
        public virtual DbSet<EventImportationRisksByGeoname> EventImportationRisksByGeoname { get; set; }
        public virtual DbSet<EventImportationRisksByUser> EventImportationRisksByUser { get; set; }
        public virtual DbSet<EventImportationRisksByUserHistory> EventImportationRisksByUserHistory { get; set; }
        public virtual DbSet<EventOrderByFields> EventOrderByFields { get; set; }
        public virtual DbSet<EventPrevalence> EventPrevalence { get; set; }
        public virtual DbSet<EventPrevalenceHistory> EventPrevalenceHistory { get; set; }
        public virtual DbSet<EventPriorities> EventPriorities { get; set; }
        public virtual DbSet<EventSourceAirport> EventSourceAirport { get; set; }
        public virtual DbSet<EventSourceAirportSpreadMd> EventSourceAirportSpreadMd { get; set; }
        public virtual DbSet<EventSourceDestinationRisk> EventSourceDestinationRisk { get; set; }
        public virtual DbSet<EventTransLog> EventTransLog { get; set; }
        public virtual DbSet<GeoNameFeatureCodes> GeoNameFeatureCodes { get; set; }
        public virtual DbSet<GeonameAlternatenameEng> GeonameAlternatenameEng { get; set; }
        public virtual DbSet<GeonameOutbreakPotential> GeonameOutbreakPotential { get; set; }
        public virtual DbSet<Geonames> Geonames { get; set; }
        public virtual DbSet<GeonamesShapes> GeonamesShapes { get; set; }
        public virtual DbSet<GridCountry> GridCountry { get; set; }
        public virtual DbSet<GridProvince> GridProvince { get; set; }
        public virtual DbSet<GridStation> GridStation { get; set; }
        public virtual DbSet<GridStationBak> GridStationBak { get; set; }
        public virtual DbSet<HamType> HamType { get; set; }
        public virtual DbSet<Huffmodel25kmworldhexagon> Huffmodel25kmworldhexagon { get; set; }
        public virtual DbSet<InterventionSpecies> InterventionSpecies { get; set; }
        public virtual DbSet<Interventions> Interventions { get; set; }
        public virtual DbSet<LastJsonStrs> LastJsonStrs { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<OutbreakPotentialCategory> OutbreakPotentialCategory { get; set; }
        public virtual DbSet<ProcessedArticle> ProcessedArticle { get; set; }
        public virtual DbSet<ProcessedArticleTransLog> ProcessedArticleTransLog { get; set; }
        public virtual DbSet<RelevanceState> RelevanceState { get; set; }
        public virtual DbSet<RelevanceType> RelevanceType { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<StationDestinationAirport> StationDestinationAirport { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<Systems> Systems { get; set; }
        public virtual DbSet<TmpPplg> TmpPplg { get; set; }
        public virtual DbSet<TransmissionModes> TransmissionModes { get; set; }
        public virtual DbSet<UserAoisHistory> UserAoisHistory { get; set; }
        public virtual DbSet<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual DbSet<UserEmailType> UserEmailType { get; set; }
        public virtual DbSet<UserExternalIds> UserExternalIds { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserLoginTrans> UserLoginTrans { get; set; }
        public virtual DbSet<UserRolesTransLog> UserRolesTransLog { get; set; }
        public virtual DbSet<UserTransLog> UserTransLog { get; set; }
        public virtual DbSet<UvwCitySubset> UvwCitySubset { get; set; }
        public virtual DbSet<UvwDiseaseRelevanceByRole> UvwDiseaseRelevanceByRole { get; set; }
        public virtual DbSet<UvwDiseaseRelevanceByUser> UvwDiseaseRelevanceByUser { get; set; }
        public virtual DbSet<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual DbSet<XtblArticleEventTransLog> XtblArticleEventTransLog { get; set; }
        public virtual DbSet<XtblArticleLocation> XtblArticleLocation { get; set; }
        public virtual DbSet<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public virtual DbSet<XtblArticleLocationDiseaseTransLog> XtblArticleLocationDiseaseTransLog { get; set; }
        public virtual DbSet<XtblArticleLocationTransLog> XtblArticleLocationTransLog { get; set; }
        public virtual DbSet<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
        public virtual DbSet<XtblDiseaseAgents> XtblDiseaseAgents { get; set; }
        public virtual DbSet<XtblDiseaseAlternateName> XtblDiseaseAlternateName { get; set; }
        public virtual DbSet<XtblDiseaseCustomGroup> XtblDiseaseCustomGroup { get; set; }
        public virtual DbSet<XtblDiseaseInterventions> XtblDiseaseInterventions { get; set; }
        public virtual DbSet<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
        public virtual DbSet<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
        public virtual DbSet<XtblEventLocation> XtblEventLocation { get; set; }
        public virtual DbSet<XtblEventLocationHistory> XtblEventLocationHistory { get; set; }
        public virtual DbSet<XtblEventLocationTransLog> XtblEventLocationTransLog { get; set; }
        public virtual DbSet<XtblEventReason> XtblEventReason { get; set; }
        public virtual DbSet<XtblEventReasonTransLog> XtblEventReasonTransLog { get; set; }
        public virtual DbSet<XtblRelatedArticles> XtblRelatedArticles { get; set; }
        public virtual DbSet<XtblRelatedArticlesTransLog> XtblRelatedArticlesTransLog { get; set; }
        public virtual DbSet<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
        public virtual DbSet<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
        public virtual DbSet<ZebraPrevalence> ZebraPrevalence { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=db1dev.ad.bluedot.global;initial catalog=BiodZebra;persist security info=True;user id=bd;password=bd_data;MultipleActiveResultSets=True;App=EntityFramework", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcquisitionModes>(entity =>
            {
                entity.HasKey(e => e.AcquisitionModeId);

                entity.ToTable("AcquisitionModes", "disease");

                entity.Property(e => e.AcquisitionModeId).ValueGeneratedNever();

                entity.Property(e => e.AcquisitionModeLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DiseaseSource)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModalityInsightsDisplay).HasMaxLength(100);

                entity.Property(e => e.ModalityName).HasMaxLength(100);
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

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasIndex(e => e.Date)
                    .HasName("idx_ActivityLog_Date");

                entity.Property(e => e.ApplicationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Browser)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Exception).IsUnicode(false);

                entity.Property(e => e.HostAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.ServerName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Thread)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(128);
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

            modelBuilder.Entity<AirportRanking>(entity =>
            {
                entity.HasKey(e => new { e.StationId, e.StartDate, e.EndDate });

                entity.ToTable("AirportRanking", "zebra");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.CtryGeoname)
                    .WithMany(p => p.AirportRanking)
                    .HasForeignKey(d => d.CtryGeonameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AirportRanking_CtryGeonameId");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.AirportRanking)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AirportRanking_StationId");
            });

            modelBuilder.Entity<AppRequestInfoLog>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LogDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LogTransId).ValueGeneratedOnAdd();

                entity.Property(e => e.RequestIpaddress)
                    .IsRequired()
                    .HasColumnName("RequestIPAddress");
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

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
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
                    .HasMaxLength(256)
                    .IsUnicode(false)
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

            modelBuilder.Entity<ConfigurationVariables>(entity =>
            {
                entity.HasKey(e => e.ConfigurationVariableId)
                    .HasName("PK_ConfigurationVariable_ConfigurationVariableId");

                entity.ToTable("ConfigurationVariables", "bd");

                entity.HasIndex(e => new { e.Name, e.ApplicationName })
                    .HasName("UK_ConfigurationVariable_Name_ApplicationName")
                    .IsUnique();

                entity.Property(e => e.ConfigurationVariableId).ValueGeneratedNever();

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ValueType)
                    .IsRequired()
                    .HasMaxLength(128);
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
            });

            modelBuilder.Entity<CountryProvinceShapesBak>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CountryProvinceShapes_bak", "place");
            });

            modelBuilder.Entity<CustomGroups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("CustomGroups", "disease");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<DiseaseEventDestinationAirport>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.DestinationStationId });

                entity.ToTable("DiseaseEventDestinationAirport", "zebra");

                entity.HasIndex(e => e.DestinationStationId)
                    .HasName("idx_DestinationStationId");

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseEventDestinationAirport)
                    .HasForeignKey(d => d.DiseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiseaseEventDestinationAirport_EventId");
            });

            modelBuilder.Entity<DiseaseEventDestinationGrid>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.GridId });

                entity.ToTable("DiseaseEventDestinationGrid", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseEventDestinationGrid)
                    .HasForeignKey(d => d.DiseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiseaseEventDestinationGrid_EventId");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.DiseaseEventDestinationGrid)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiseaseEventDestinationGrid_GridId");
            });

            modelBuilder.Entity<DiseaseEventPrevalence>(entity =>
            {
                entity.HasKey(e => e.DiseaseId);

                entity.ToTable("DiseaseEventPrevalence", "zebra");

                entity.Property(e => e.DiseaseId).ValueGeneratedNever();

                entity.HasOne(d => d.Disease)
                    .WithOne(p => p.DiseaseEventPrevalence)
                    .HasForeignKey<DiseaseEventPrevalence>(d => d.DiseaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiseaseEventPrevalence_EventId");
            });

            modelBuilder.Entity<DiseaseSourceAirport>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SourceStationId });

                entity.ToTable("DiseaseSourceAirport", "zebra");

                entity.Property(e => e.Probability).HasColumnType("decimal(10, 6)");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseaseSourceAirport)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_DiseaseSourceAirport_DiseaseId");

                entity.HasOne(d => d.SourceStation)
                    .WithMany(p => p.DiseaseSourceAirport)
                    .HasForeignKey(d => d.SourceStationId)
                    .HasConstraintName("FK_DiseaseSourceAirportt_StationId");
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

            modelBuilder.Entity<EventCreationReasons>(entity =>
            {
                entity.HasKey(e => e.ReasonId);

                entity.ToTable("EventCreationReasons", "surveillance");

                entity.Property(e => e.ReasonId).ValueGeneratedNever();

                entity.Property(e => e.ReasonName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventDestinationAirport>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.DestinationStationId });

                entity.ToTable("EventDestinationAirport", "zebra");

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

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationAirport)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirport_EventId");
            });

            modelBuilder.Entity<EventDestinationAirportHistory>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.DestinationStationId });

                entity.ToTable("EventDestinationAirport_history", "zebra");

                entity.HasIndex(e => e.DestinationStationId)
                    .HasName("idx_DestinationStationId");

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationAirportHistory)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirport_history_EventId");
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

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationAirportSpreadMd)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirportSpreadMd_EventId");
            });

            modelBuilder.Entity<EventDestinationGrid>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GridId });

                entity.ToTable("EventDestinationGrid", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationGrid)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGrid_EventId");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.EventDestinationGrid)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGrid_GridId");
            });

            modelBuilder.Entity<EventDestinationGridHistory>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GridId });

                entity.ToTable("EventDestinationGrid_history", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationGridHistory)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGrid_history_EventId");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.EventDestinationGridHistory)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGrid_history_GridId");
            });

            modelBuilder.Entity<EventDestinationGridSpreadMd>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GridId });

                entity.ToTable("EventDestinationGridSpreadMd", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationGridSpreadMd)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGridSpreadMd_EventId");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.EventDestinationGridSpreadMd)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGridSpreadMd_GridId");
            });

            modelBuilder.Entity<EventDestinationGridV3>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GridId });

                entity.ToTable("EventDestinationGridV3", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationGridV3)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGridV3_EventId");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.EventDestinationGridV3)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationGridV3_GridId");
            });

            modelBuilder.Entity<EventGroupByFields>(entity =>
            {
                entity.ToTable("EventGroupByFields", "zebra");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColumnName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventImportationRisksByGeoname>(entity =>
            {
                entity.HasKey(e => new { e.GeonameId, e.EventId });

                entity.ToTable("EventImportationRisksByGeoname", "zebra");

                entity.HasIndex(e => e.EventId)
                    .HasName("idx_EventImportationRisksByGeoname");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MaxVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinVolume).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventImportationRisksByGeoname)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventImportationRisksByGeoname_EventId");

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.EventImportationRisksByGeoname)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_EventImportationRisksByGeonamer_GeonameId");
            });

            modelBuilder.Entity<EventImportationRisksByUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EventId });

                entity.ToTable("EventImportationRisksByUser", "zebra");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.LocalSpread).HasColumnName("localSpread");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MaxVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinVolume).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventImportationRisksByUser)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventImportationRisksByUser_EventId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventImportationRisksByUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_EventImportationRisksByUser_UserId");
            });

            modelBuilder.Entity<EventImportationRisksByUserHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EventId });

                entity.ToTable("EventImportationRisksByUser_history", "zebra");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MaxVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinVolume).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventImportationRisksByUserHistory)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventImportationRisksByUser_history_EventId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventImportationRisksByUserHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_EventImportationRisksByUser_history_UserId");
            });

            modelBuilder.Entity<EventOrderByFields>(entity =>
            {
                entity.ToTable("EventOrderByFields", "zebra");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColumnName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventPrevalence>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("EventPrevalence", "zebra");

                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.EventPrevalence)
                    .HasForeignKey<EventPrevalence>(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventPrevalence_EventId");
            });

            modelBuilder.Entity<EventPrevalenceHistory>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("EventPrevalence_history", "zebra");

                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.EventPrevalenceHistory)
                    .HasForeignKey<EventPrevalenceHistory>(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventPrevalence_history_EventId");
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

            modelBuilder.Entity<EventSourceAirport>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.SourceStationId });

                entity.ToTable("EventSourceAirport", "zebra");

                entity.Property(e => e.CityDisplayName).HasMaxLength(200);

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.Probability).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.StationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StationName).HasMaxLength(64);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSourceAirport)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSourceAirport_EventId");

                entity.HasOne(d => d.SourceStation)
                    .WithMany(p => p.EventSourceAirport)
                    .HasForeignKey(d => d.SourceStationId)
                    .HasConstraintName("FK_EventSourceAirport_StationId");
            });

            modelBuilder.Entity<EventSourceAirportSpreadMd>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.SourceStationId });

                entity.ToTable("EventSourceAirportSpreadMd", "zebra");

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

            modelBuilder.Entity<EventSourceDestinationRisk>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.SourceAirportId, e.DestinationAirportId });

                entity.ToTable("EventSourceDestinationRisk", "zebra");

                entity.HasIndex(e => e.DestinationAirportId)
                    .HasName("idx_EventSourceDestinationRisk_DestinationAirport");

                entity.HasIndex(e => e.SourceAirportId)
                    .HasName("idx_EventSourceDestinationRisk_SourceAirportId");

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.HasOne(d => d.DestinationAirport)
                    .WithMany(p => p.EventSourceDestinationRiskDestinationAirport)
                    .HasForeignKey(d => d.DestinationAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSourceDestinationRisk_DestinationAirportId");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSourceDestinationRisk)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventSourceDestinationRisk_EventId");

                entity.HasOne(d => d.SourceAirport)
                    .WithMany(p => p.EventSourceDestinationRiskSourceAirport)
                    .HasForeignKey(d => d.SourceAirportId)
                    .HasConstraintName("FK_EventSourceDestinationRisk_SourceAirportId");
            });

            modelBuilder.Entity<EventTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E548648593E6B03");

                entity.ToTable("EventTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EventMongoId).HasMaxLength(128);

                entity.Property(e => e.EventTitle).HasMaxLength(200);

                entity.Property(e => e.LastUpdatedByUserName).HasMaxLength(64);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<GeoNameFeatureCodes>(entity =>
            {
                entity.HasKey(e => e.FeatureCode);

                entity.ToTable("GeoNameFeatureCodes", "place");

                entity.Property(e => e.FeatureCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeonameAlternatenameEng>(entity =>
            {
                entity.HasKey(e => new { e.GeonameId, e.AlternatenameEng });

                entity.ToTable("GeonameAlternatenameEng", "place");

                entity.HasIndex(e => e.AlternateNameId)
                    .HasName("idx_GeonameAlternatenameEng_AlternateNameId")
                    .IsUnique();

                entity.HasIndex(e => e.AlternatenameEng)
                    .HasName("idx_GeonameAlternatenameEng_AlternatenameEng");

                entity.HasIndex(e => e.LocationType)
                    .HasName("idx_GeonameAlternatenameEng_LocationType");

                entity.Property(e => e.AlternatenameEng)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateNameId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.GeonameAlternatenameEng)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_GeonameAlternatenameEng_GeonameId");
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
            });

            modelBuilder.Entity<GeonamesShapes>(entity =>
            {
                entity.HasKey(e => e.GeonameId);

                entity.ToTable("GeonamesShapes", "place");

                entity.HasIndex(e => e.Geom)
                    .HasName("SPIX_Geonames_GEOM");

                entity.Property(e => e.GeonameId)
                    .HasColumnName("geonameId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Geom)
                    .HasColumnName("GEOM")
                    .HasColumnType("geometry");

                entity.Property(e => e.ShapeLastModified)
                    .HasColumnName("shapeLastModified")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<GridCountry>(entity =>
            {
                entity.HasKey(e => new { e.GridId, e.CountryGeonameId });

                entity.ToTable("GridCountry", "zebra");

                entity.HasIndex(e => e.CountryGeonameId)
                    .HasName("idx_CountryGeonameId");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);
            });

            modelBuilder.Entity<GridProvince>(entity =>
            {
                entity.HasKey(e => new { e.GridId, e.Adm1GeonameId });

                entity.ToTable("GridProvince", "zebra");

                entity.HasIndex(e => e.Adm1GeonameId)
                    .HasName("idx_Adm1GeonameId");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);
            });

            modelBuilder.Entity<GridStation>(entity =>
            {
                entity.HasKey(e => new { e.GridId, e.StationId, e.ValidFromDate });

                entity.ToTable("GridStation", "zebra");

                entity.HasIndex(e => e.Probability)
                    .HasName("idx_GridStation_Probability");

                entity.HasIndex(e => e.StationId)
                    .HasName("idx_GridStation_StationId");

                entity.HasIndex(e => e.ValidFromDate)
                    .HasName("idx_GridStation_ValidFromDate");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.Property(e => e.ValidFromDate).HasColumnType("date");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Probability).HasColumnType("decimal(10, 8)");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.GridStation)
                    .HasForeignKey(d => d.GridId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GridStation_GridId");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.GridStation)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GridStation_StationId");
            });

            modelBuilder.Entity<GridStationBak>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GridStation_bak", "zebra");

                entity.Property(e => e.GridId)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Probability).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.ValidFromDate).HasColumnType("date");
            });

            modelBuilder.Entity<HamType>(entity =>
            {
                entity.ToTable("HamType", "surveillance");

                entity.Property(e => e.HamTypeId).ValueGeneratedNever();

                entity.Property(e => e.HamTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Huffmodel25kmworldhexagon>(entity =>
            {
                entity.HasKey(e => e.GridId);

                entity.ToTable("HUFFMODEL25KMWORLDHEXAGON", "bd");

                entity.HasIndex(e => e.Shape)
                    .HasName("SIndx_huffmodel_SHAPE");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasMaxLength(12);

                entity.Property(e => e.Population).HasColumnName("population");

                entity.Property(e => e.Shape).HasColumnName("SHAPE");
            });

            modelBuilder.Entity<InterventionSpecies>(entity =>
            {
                entity.HasKey(e => new { e.InterventionId, e.SpeciesId });

                entity.ToTable("InterventionSpecies", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Duration)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RiskReduction).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.Intervention)
                    .WithMany(p => p.InterventionSpecies)
                    .HasForeignKey(d => d.InterventionId)
                    .HasConstraintName("FK_InterventionSpecies_InterventionId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.InterventionSpecies)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_InterventionSpecies_SpeciesId");
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

            modelBuilder.Entity<LastJsonStrs>(entity =>
            {
                entity.ToTable("LastJsonStrs", "bd");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JsonStr).IsRequired();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
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

            modelBuilder.Entity<ProcessedArticleTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E548648136E42AA");

                entity.ToTable("ProcessedArticleTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ArticleId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ArticleTitle).HasMaxLength(500);

                entity.Property(e => e.CertaintyScore).HasColumnType("decimal(18, 16)");

                entity.Property(e => e.FeedPublishedDate).HasColumnType("datetime");

                entity.Property(e => e.FeedSourceId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FeedUrl)
                    .HasColumnName("FeedURL")
                    .HasMaxLength(2000);

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

            modelBuilder.Entity<StationDestinationAirport>(entity =>
            {
                entity.HasKey(e => new { e.StationId, e.DestinationAirportId, e.ValidFromDate });

                entity.ToTable("StationDestinationAirport", "zebra");

                entity.HasIndex(e => e.DestinationAirportId)
                    .HasName("idx_StationDestinationAirport_DestinationAirport");

                entity.HasIndex(e => e.ValidFromDate)
                    .HasName("idx_StationDestinationAirport_ValidFromDate");

                entity.Property(e => e.ValidFromDate).HasColumnType("date");

                entity.HasOne(d => d.DestinationAirport)
                    .WithMany(p => p.StationDestinationAirportDestinationAirport)
                    .HasForeignKey(d => d.DestinationAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationDestinationAirport_DestinationAirportId");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.StationDestinationAirportStation)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_StationDestinationAirport_StationId");
            });

            modelBuilder.Entity<Stations>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.ToTable("Stations", "zebra");

                entity.HasIndex(e => e.CityGeonameId)
                    .HasName("idx_Stations_CityGeonameId");

                entity.HasIndex(e => e.CityId)
                    .HasName("idx_Stations_CityId");

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
            });

            modelBuilder.Entity<Symptoms>(entity =>
            {
                entity.HasKey(e => e.SymptomId);

                entity.ToTable("Symptoms", "disease");

                entity.Property(e => e.SymptomId).ValueGeneratedNever();

                entity.Property(e => e.DefinitionSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Symptom)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SymptomDefinition)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.System)
                    .WithMany(p => p.Symptoms)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Symptoms_SystemId");
            });

            modelBuilder.Entity<Systems>(entity =>
            {
                entity.HasKey(e => e.SystemId);

                entity.ToTable("Systems", "disease");

                entity.Property(e => e.SystemId).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.System)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TmpPplg>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp_PPLG", "place");

                entity.Property(e => e.CountryName).HasMaxLength(64);

                entity.Property(e => e.DisplayName).HasMaxLength(500);

                entity.Property(e => e.FeatureCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.ModificationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(200);
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

            modelBuilder.Entity<UserAoisHistory>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserAois_history", "zebra");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.AoiGeonameIds)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserAoisHistory)
                    .HasForeignKey<UserAoisHistory>(d => d.UserId)
                    .HasConstraintName("FK_UserAois_history_UserId");
            });

            modelBuilder.Entity<UserEmailNotification>(entity =>
            {
                entity.Property(e => e.AoiGeonameIds)
                    .IsRequired()
                    .HasMaxLength(256)
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

                entity.HasOne(d => d.EmailTypeNavigation)
                    .WithMany(p => p.UserEmailNotification)
                    .HasForeignKey(d => d.EmailType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.UserEmailNotification_dbo.UserEmailType_EmailTypeId");

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

            modelBuilder.Entity<UserEmailType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UserExternalIds>(entity =>
            {
                entity.HasKey(e => new { e.ExternalName, e.ExternalId })
                    .HasName("PK_dbo.UserExternalIds");

                entity.Property(e => e.ExternalName).HasMaxLength(128);

                entity.Property(e => e.ExternalId).HasMaxLength(256);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserExternalIds)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.UserExternalIds_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UserLoginTrans>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("idx_UserId");

                entity.Property(e => e.UserLoginTransId).HasColumnName("UserLoginTransID");

                entity.Property(e => e.LoginDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLoginTrans)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserLoginTrans_AspNetUsers");
            });

            modelBuilder.Entity<UserRolesTransLog>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId, e.ModifiedUtcdatetime });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.Property(e => e.ModifiedUtcdatetime)
                    .HasColumnName("ModifiedUTCDatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<UserTransLog>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ModifiedUtcdatetime });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.ModifiedUtcdatetime)
                    .HasColumnName("ModifiedUTCDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<UvwCitySubset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvw_CitySubset", "place");

                entity.Property(e => e.AsciiName).HasMaxLength(200);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(135)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<UvwDiseaseRelevanceByRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvw_DiseaseRelevanceByRole", "zebra");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RelevanceDescription).HasMaxLength(256);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.StateDescription)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UvwDiseaseRelevanceByUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvw_DiseaseRelevanceByUser", "zebra");

                entity.Property(e => e.DiseaseName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RelevanceDescription).HasMaxLength(256);

                entity.Property(e => e.StateDescription)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserEmail).HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<XtblArticleEvent>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.EventId })
                    .HasName("PK_Article_Event");

                entity.ToTable("Xtbl_Article_Event", "surveillance");

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

            modelBuilder.Entity<XtblArticleEventTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E5486484DD8A3BB");

                entity.ToTable("Xtbl_Article_EventTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ArticleId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<XtblArticleLocation>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.LocationGeoNameId })
                    .HasName("PK_Article_Location");

                entity.ToTable("Xtbl_Article_Location", "surveillance");

                entity.Property(e => e.ArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.XtblArticleLocation)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Xtbl_Article_Location_ProcessedArticle");
            });

            modelBuilder.Entity<XtblArticleLocationDisease>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.LocationGeoNameId, e.DiseaseId })
                    .HasName("PK_Article_Location_Disease");

                entity.ToTable("Xtbl_Article_Location_Disease", "surveillance");

                entity.HasIndex(e => e.DiseaseId)
                    .HasName("idx_Xtbl_Article_Location_Disease_DiseaseId");

                entity.HasIndex(e => e.LocationGeoNameId)
                    .HasName("idx_Xtbl_Article_Location_Disease_LocationGeoNameId");

                entity.Property(e => e.ArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.XtblArticleLocationDisease)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Xtbl_Article_Location_Disease_ProcessedArticle");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblArticleLocationDisease)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Article_Location_Disease_Diseases");

                entity.HasOne(d => d.LocationGeoName)
                    .WithMany(p => p.XtblArticleLocationDisease)
                    .HasForeignKey(d => d.LocationGeoNameId)
                    .HasConstraintName("FK_Xtbl_Article_Location_Disease_Geonames");
            });

            modelBuilder.Entity<XtblArticleLocationDiseaseTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E548648753F8B3B");

                entity.ToTable("Xtbl_Article_Location_DiseaseTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ArticleId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<XtblArticleLocationTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E548648B09F9E42");

                entity.ToTable("Xtbl_Article_LocationTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ArticleId)
                    .IsRequired()
                    .HasMaxLength(128);
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

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.XtblDiseaseAgents)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Xtbl_Disease_Agents_AgentId");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseAgents)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Agents_DiseaseId");
            });

            modelBuilder.Entity<XtblDiseaseAlternateName>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.AlternateName });

                entity.ToTable("Xtbl_Disease_AlternateName", "disease");

                entity.Property(e => e.AlternateName).HasMaxLength(200);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseAlternateName)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_AlternateName_DiseaseId");
            });

            modelBuilder.Entity<XtblDiseaseCustomGroup>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.GroupId });

                entity.ToTable("Xtbl_Disease_CustomGroup", "disease");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseCustomGroup)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_CustomGroup_Diseases");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.XtblDiseaseCustomGroup)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Xtbl_Disease_CustomGroup_CustomGroups");
            });

            modelBuilder.Entity<XtblDiseaseInterventions>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId, e.InterventionId });

                entity.ToTable("Xtbl_Disease_Interventions", "disease");

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

            modelBuilder.Entity<XtblDiseaseSymptom>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SpeciesId, e.SymptomId });

                entity.ToTable("Xtbl_Disease_Symptom", "disease");

                entity.Property(e => e.SpeciesId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Frequency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseSymptom)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Symptom_DiseaseId");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.XtblDiseaseSymptom)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_Xtbl_Disease_Symptom_SpeciesId");

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.XtblDiseaseSymptom)
                    .HasForeignKey(d => d.SymptomId)
                    .HasConstraintName("FK_Xtbl_Disease_Symptom_SymptomId");
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

            modelBuilder.Entity<XtblEventLocationTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E5486488A8F1C12");

                entity.ToTable("Xtbl_Event_LocationTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.EventDate).HasColumnType("date");
            });

            modelBuilder.Entity<XtblEventReason>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.ReasonId });

                entity.ToTable("Xtbl_Event_Reason", "surveillance");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.XtblEventReason)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Xtbl_Event_Reason_Event");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.XtblEventReason)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK_Xtbl_Event_Reason_Reason");
            });

            modelBuilder.Entity<XtblEventReasonTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E548648138745DC");

                entity.ToTable("Xtbl_Event_ReasonTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<XtblRelatedArticles>(entity =>
            {
                entity.HasKey(e => new { e.MainArticleId, e.RelatedArticleId });

                entity.ToTable("Xtbl_RelatedArticles", "surveillance");

                entity.Property(e => e.MainArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RelatedArticleId)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.MainArticle)
                    .WithMany(p => p.XtblRelatedArticles)
                    .HasForeignKey(d => d.MainArticleId)
                    .HasConstraintName("FK_Xtbl_RelatedArticles_MainArticleId");
            });

            modelBuilder.Entity<XtblRelatedArticlesTransLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__tmp_ms_x__5E5486483C607878");

                entity.ToTable("Xtbl_RelatedArticlesTransLog", "surveillance");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.MainArticleId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.RelatedArticleId)
                    .IsRequired()
                    .HasMaxLength(128);
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

            modelBuilder.Entity<ZebraPrevalence>(entity =>
            {
                entity.HasKey(e => new { e.EventDur, e.Lambda, e.DurInf });

                entity.ToTable("ZebraPrevalence", "zebra");

                entity.HasIndex(e => e.DurInf)
                    .HasName("idx_dur_inf");

                entity.HasIndex(e => e.Lambda)
                    .HasName("idx_lambda");

                entity.Property(e => e.EventDur).HasColumnName("event_dur");

                entity.Property(e => e.Lambda).HasColumnName("lambda");

                entity.Property(e => e.DurInf).HasColumnName("dur_inf");

                entity.Property(e => e.MinTrav).HasColumnName("min_trav");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
