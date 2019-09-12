using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biod.Api.Core.Models
{
    public partial class BiodZebraContext : DbContext
    {
        public virtual DbSet<AirportRanking> AirportRanking { get; set; }
        public virtual DbSet<ArticleFeed> ArticleFeed { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BiosecurityRisk> BiosecurityRisk { get; set; }
        public virtual DbSet<CountryProvinceShapes> CountryProvinceShapes { get; set; }
        public virtual DbSet<DiseaseConditionsGcs> DiseaseConditionsGcs { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventCreationReasons> EventCreationReasons { get; set; }
        public virtual DbSet<EventDestinationAirport> EventDestinationAirport { get; set; }
        public virtual DbSet<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public virtual DbSet<EventGroupByFields> EventGroupByFields { get; set; }
        public virtual DbSet<EventlocationTmp> EventlocationTmp { get; set; }
        public virtual DbSet<EventlocationTmp012019> EventlocationTmp012019 { get; set; }
        public virtual DbSet<EventlocationTmp012019Spat> EventlocationTmp012019Spat { get; set; }
        public virtual DbSet<EventlocationTmpSpatialjoi> EventlocationTmpSpatialjoi { get; set; }
        public virtual DbSet<EventOrderByFields> EventOrderByFields { get; set; }
        public virtual DbSet<EventPriorities> EventPriorities { get; set; }
        public virtual DbSet<EventSourceAirport> EventSourceAirport { get; set; }
        public virtual DbSet<EventSourceGrid> EventSourceGrid { get; set; }
        public virtual DbSet<GeonameAlternatenameEng> GeonameAlternatenameEng { get; set; }
        public virtual DbSet<GeoNameFeatureCodes> GeoNameFeatureCodes { get; set; }
        public virtual DbSet<Geonames> Geonames { get; set; }
        public virtual DbSet<GeonamesShapesArchive> GeonamesShapesArchive { get; set; }
        public virtual DbSet<GridCountry> GridCountry { get; set; }
        public virtual DbSet<GridProvince> GridProvince { get; set; }
        public virtual DbSet<GridStation> GridStation { get; set; }
        public virtual DbSet<HamType> HamType { get; set; }
        public virtual DbSet<Huffmodel25kmworldhexagon> Huffmodel25kmworldhexagon { get; set; }
        public virtual DbSet<LastJsonStrs> LastJsonStrs { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<OutbreakPotentialCategory> OutbreakPotentialCategory { get; set; }
        public virtual DbSet<Pathogens> Pathogens { get; set; }
        public virtual DbSet<PathogenTypes> PathogenTypes { get; set; }
        public virtual DbSet<Preventions> Preventions { get; set; }
        public virtual DbSet<ProcessedArticle> ProcessedArticle { get; set; }
        public virtual DbSet<ProvisionMarkerDss> ProvisionMarkerDss { get; set; }
        public virtual DbSet<SchemaInfoDss> SchemaInfoDss { get; set; }
        public virtual DbSet<ScopeConfigDss> ScopeConfigDss { get; set; }
        public virtual DbSet<ScopeInfoDss> ScopeInfoDss { get; set; }
        public virtual DbSet<StationDestinationAirport> StationDestinationAirport { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<Systems> Systems { get; set; }
        public virtual DbSet<TransmissionModes> TransmissionModes { get; set; }
        public virtual DbSet<UserLoginTrans> UserLoginTrans { get; set; }
        public virtual DbSet<UserRolesTransLog> UserRolesTransLog { get; set; }
        public virtual DbSet<UserTransLog> UserTransLog { get; set; }
        public virtual DbSet<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual DbSet<XtblArticleLocation> XtblArticleLocation { get; set; }
        public virtual DbSet<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public virtual DbSet<XtblDiseaseAlternateName> XtblDiseaseAlternateName { get; set; }
        public virtual DbSet<XtblDiseasePathogens> XtblDiseasePathogens { get; set; }
        public virtual DbSet<XtblDiseasePreventions> XtblDiseasePreventions { get; set; }
        public virtual DbSet<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
        public virtual DbSet<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
        public virtual DbSet<XtblEventLocation> XtblEventLocation { get; set; }
        public virtual DbSet<XtblEventReason> XtblEventReason { get; set; }
        public virtual DbSet<XtblRelatedArticles> XtblRelatedArticles { get; set; }

        public BiodZebraContext(DbContextOptions<BiodZebraContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "bd");

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

            modelBuilder.Entity<ArticleFeed>(entity =>
            {
                entity.ToTable("ArticleFeed", "surveillance");

                entity.Property(e => e.ArticleFeedId).ValueGeneratedNever();

                entity.Property(e => e.ArticleFeedName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.ToTable("AspNetRoles", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.ToTable("AspNetUserClaims", "dbo");

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
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });

                entity.ToTable("AspNetUserLogins", "dbo");

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
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("AspNetUserRoles", "dbo");

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
                entity.ToTable("AspNetUsers", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EmailNotificationEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<BiosecurityRisk>(entity =>
            {
                entity.HasKey(e => e.BiosecurityRiskCode);

                entity.ToTable("BiosecurityRisk", "disease");

                entity.Property(e => e.BiosecurityRiskCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

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

                entity.Property(e => e.GeonameId).ValueGeneratedNever();
            });

            modelBuilder.Entity<DiseaseConditionsGcs>(entity =>
            {
                entity.HasKey(e => e.Objectid);

                entity.ToTable("DiseaseConditions_GCS", "map");

                entity.Property(e => e.Objectid)
                    .HasColumnName("OBJECTID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryWaterQuality).HasColumnName("Country_Water_Quality");

                entity.Property(e => e.D101Prev)
                    .HasColumnName("D101_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D103Prev)
                    .HasColumnName("D103_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D104Prev)
                    .HasColumnName("D104_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D107Prev)
                    .HasColumnName("D107_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D108Prev)
                    .HasColumnName("D108_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D109Prev)
                    .HasColumnName("D109_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D10Prev)
                    .HasColumnName("D10_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D110Prev)
                    .HasColumnName("D110_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D11Prev)
                    .HasColumnName("D11_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D12Prev)
                    .HasColumnName("D12_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D13Prev)
                    .HasColumnName("D13_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D14Prev)
                    .HasColumnName("D14_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D15Prev)
                    .HasColumnName("D15_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D16Prev)
                    .HasColumnName("D16_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D17Prev)
                    .HasColumnName("D17_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D18Prev)
                    .HasColumnName("D18_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D19Prev)
                    .HasColumnName("D19_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D1Prev)
                    .HasColumnName("D1_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D20Prev)
                    .HasColumnName("D20_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D21Prev)
                    .HasColumnName("D21_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D22Prev)
                    .HasColumnName("D22_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D23Prev)
                    .HasColumnName("D23_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D24Prev)
                    .HasColumnName("D24_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D25Prev)
                    .HasColumnName("D25_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D26Prev)
                    .HasColumnName("D26_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D27Prev)
                    .HasColumnName("D27_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D28Prev)
                    .HasColumnName("D28_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D29Prev)
                    .HasColumnName("D29_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D2Prev)
                    .HasColumnName("D2_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D30Prev)
                    .HasColumnName("D30_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D31Prev)
                    .HasColumnName("D31_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D32Prev)
                    .HasColumnName("D32_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D33Prev)
                    .HasColumnName("D33_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D34Prev)
                    .HasColumnName("D34_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D35Prev)
                    .HasColumnName("D35_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D36Prev)
                    .HasColumnName("D36_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D37Prev)
                    .HasColumnName("D37_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D38Prev)
                    .HasColumnName("D38_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D39Prev)
                    .HasColumnName("D39_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D3Prev)
                    .HasColumnName("D3_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D40Prev)
                    .HasColumnName("D40_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D41Prev)
                    .HasColumnName("D41_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D42Prev)
                    .HasColumnName("D42_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D43Prev)
                    .HasColumnName("D43_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D44Prev)
                    .HasColumnName("D44_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D45Prev)
                    .HasColumnName("D45_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D46Prev)
                    .HasColumnName("D46_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D47Prev)
                    .HasColumnName("D47_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D48Prev)
                    .HasColumnName("D48_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D49Prev)
                    .HasColumnName("D49_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D4Prev)
                    .HasColumnName("D4_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D50Prev)
                    .HasColumnName("D50_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D51Prev)
                    .HasColumnName("D51_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D52Prev)
                    .HasColumnName("D52_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D53Prev)
                    .HasColumnName("D53_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D54Prev)
                    .HasColumnName("D54_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D55Prev)
                    .HasColumnName("D55_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D56Prev)
                    .HasColumnName("D56_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D57Prev)
                    .HasColumnName("D57_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D58Prev)
                    .HasColumnName("D58_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D59Prev)
                    .HasColumnName("D59_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D5Prev)
                    .HasColumnName("D5_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D60Prev)
                    .HasColumnName("D60_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D61Prev)
                    .HasColumnName("D61_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D62Prev)
                    .HasColumnName("D62_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D63Prev)
                    .HasColumnName("D63_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D64Prev)
                    .HasColumnName("D64_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D65Prev)
                    .HasColumnName("D65_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D66Prev)
                    .HasColumnName("D66_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D67Prev)
                    .HasColumnName("D67_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D68Prev)
                    .HasColumnName("D68_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D69Prev)
                    .HasColumnName("D69_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D6Prev)
                    .HasColumnName("D6_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D70Prev)
                    .HasColumnName("D70_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D71Prev)
                    .HasColumnName("D71_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D72Prev)
                    .HasColumnName("D72_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D73Prev)
                    .HasColumnName("D73_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D74Prev)
                    .HasColumnName("D74_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D75Prev)
                    .HasColumnName("D75_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D76Prev)
                    .HasColumnName("D76_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D77Prev)
                    .HasColumnName("D77_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D78Prev)
                    .HasColumnName("D78_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D7Prev)
                    .HasColumnName("D7_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D80Prev)
                    .HasColumnName("D80_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D81Prev)
                    .HasColumnName("D81_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D82Prev)
                    .HasColumnName("D82_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D83Prev)
                    .HasColumnName("D83_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D84Prev)
                    .HasColumnName("D84_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D85Prev)
                    .HasColumnName("D85_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D86Prev)
                    .HasColumnName("D86_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D87Prev)
                    .HasColumnName("D87_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D88Prev)
                    .HasColumnName("D88_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D89Prev)
                    .HasColumnName("D89_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D8Prev)
                    .HasColumnName("D8_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D90Prev)
                    .HasColumnName("D90_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D91Prev)
                    .HasColumnName("D91_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D92Prev)
                    .HasColumnName("D92_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D95Prev)
                    .HasColumnName("D95_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D98Prev)
                    .HasColumnName("D98_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.D9Prev)
                    .HasColumnName("D9_prev")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.Developed).HasColumnType("numeric(, 8)");

                entity.Property(e => e.SanitationLevel).HasColumnName("Sanitation_Level");

                entity.Property(e => e.SeasonalityZone).HasColumnName("Seasonality_Zone");

                entity.Property(e => e.ShapeStarea)
                    .HasColumnName("Shape.STArea()")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.ShapeStlength)
                    .HasColumnName("Shape.STLength()")
                    .HasColumnType("numeric(, 8)");

                entity.Property(e => e.UrbanGridCode).HasColumnName("Urban_GridCode");

                entity.Property(e => e.WaterQuality)
                    .HasColumnName("Water_Quality")
                    .HasColumnType("numeric(, 8)");
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

                entity.Property(e => e.IncubationAverageDays).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IncubationMaximumDays).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IncubationMinimumDays).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Pronunciation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SeverityLevel)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SymptomaticAverageDays).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SymptomaticMaximumDays).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SymptomaticMinimumDays).HasColumnType("decimal(10, 2)");

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

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_Event_PriorityId");
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

                entity.Property(e => e.CityDisplayName).HasMaxLength(200);

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 5)");

                entity.Property(e => e.MaxExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MaxProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinExpVolume).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.MinProb).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.StationCode).HasColumnType("char(3)");

                entity.Property(e => e.StationName).HasMaxLength(64);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventDestinationAirport)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventDestinationAirport_EventId");
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

            modelBuilder.Entity<EventlocationTmp>(entity =>
            {
                entity.HasKey(e => e.Objectid1);

                entity.ToTable("EVENTLOCATION_TMP");

                entity.Property(e => e.Objectid1).HasColumnName("OBJECTID_1");

                entity.Property(e => e.ObjectId).HasColumnName("objectID");
            });

            modelBuilder.Entity<EventlocationTmp012019>(entity =>
            {
                entity.HasKey(e => e.Objectid1);

                entity.ToTable("EVENTLOCATION_TMP012019");

                entity.Property(e => e.Objectid1).HasColumnName("OBJECTID_1");

                entity.Property(e => e.ObjectId).HasColumnName("objectID");
            });

            modelBuilder.Entity<EventlocationTmp012019Spat>(entity =>
            {
                entity.HasKey(e => e.Objectid1);

                entity.ToTable("EVENTLOCATION_TMP012019_SPAT");

                entity.Property(e => e.Objectid1).HasColumnName("OBJECTID_1");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasMaxLength(12);

                entity.Property(e => e.JoinCount).HasColumnName("Join_Count");

                entity.Property(e => e.ObjectId).HasColumnName("objectID");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.Property(e => e.TargetFid).HasColumnName("TARGET_FID");
            });

            modelBuilder.Entity<EventlocationTmpSpatialjoi>(entity =>
            {
                entity.HasKey(e => e.Objectid1);

                entity.ToTable("EVENTLOCATION_TMP_SPATIALJOI");

                entity.Property(e => e.Objectid1).HasColumnName("OBJECTID_1");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasMaxLength(12);

                entity.Property(e => e.JoinCount).HasColumnName("Join_Count");

                entity.Property(e => e.ObjectId).HasColumnName("objectID");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.Property(e => e.TargetFid).HasColumnName("TARGET_FID");
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

                entity.Property(e => e.StationCode).HasColumnType("char(3)");

                entity.Property(e => e.StationName).HasMaxLength(64);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSourceAirport)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSourceAirport_EventId");

                entity.HasOne(d => d.SourceStation)
                    .WithMany(p => p.EventSourceAirport)
                    .HasForeignKey(d => d.SourceStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSourceAirport_StationId");
            });

            modelBuilder.Entity<EventSourceGrid>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GridId });

                entity.ToTable("EventSourceGrid", "zebra");

                entity.HasIndex(e => e.GridId)
                    .HasName("idx_GridId");

                entity.Property(e => e.GridId).HasMaxLength(12);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSourceGrid)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventSourceGrid_EventId");
            });

            modelBuilder.Entity<GeonameAlternatenameEng>(entity =>
            {
                entity.HasKey(e => new { e.GeonameId, e.AlternatenameEng });

                entity.ToTable("GeonameAlternatenameEng", "place");

                entity.HasIndex(e => e.AlternatenameEng)
                    .HasName("idx_GeonameAlternatenameEng_AlternatenameEng");

                entity.HasIndex(e => e.LocationType)
                    .HasName("idx_GeonameAlternatenameEng_LocationType");

                entity.Property(e => e.AlternatenameEng)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Geoname)
                    .WithMany(p => p.GeonameAlternatenameEng)
                    .HasForeignKey(d => d.GeonameId)
                    .HasConstraintName("FK_GeonameAlternatenameEng_GeonameId");
            });

            modelBuilder.Entity<GeoNameFeatureCodes>(entity =>
            {
                entity.HasKey(e => e.FeatureCode);

                entity.ToTable("GeoNameFeatureCodes", "place");

                entity.Property(e => e.FeatureCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PlaceType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
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

                entity.Property(e => e.GeonameId).ValueGeneratedNever();

                entity.Property(e => e.CountryName).HasMaxLength(64);

                entity.Property(e => e.DisplayName).HasMaxLength(500);

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

            modelBuilder.Entity<GeonamesShapesArchive>(entity =>
            {
                entity.HasKey(e => e.GeonameId);

                entity.ToTable("GeonamesShapes_archive", "place");

                entity.Property(e => e.GeonameId)
                    .HasColumnName("geonameId")
                    .ValueGeneratedNever();

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

                entity.ToTable("HUFFMODEL25KMWORLDHEXAGON");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasMaxLength(12)
                    .ValueGeneratedNever();

                entity.Property(e => e.Population).HasColumnName("population");
            });

            modelBuilder.Entity<LastJsonStrs>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JsonStr).IsRequired();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory", "dbo");

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

            modelBuilder.Entity<Pathogens>(entity =>
            {
                entity.HasKey(e => e.PathogenId);

                entity.ToTable("Pathogens", "disease");

                entity.Property(e => e.PathogenId).ValueGeneratedNever();

                entity.Property(e => e.Pathogen)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.PathogenType)
                    .WithMany(p => p.Pathogens)
                    .HasForeignKey(d => d.PathogenTypeId)
                    .HasConstraintName("FK_Pathogens_PathogenTypeId");
            });

            modelBuilder.Entity<PathogenTypes>(entity =>
            {
                entity.HasKey(e => e.PathogenTypeId);

                entity.ToTable("PathogenTypes", "disease");

                entity.Property(e => e.PathogenTypeId).ValueGeneratedNever();

                entity.Property(e => e.PathogenType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Preventions>(entity =>
            {
                entity.HasKey(e => e.PreventionId);

                entity.ToTable("Preventions", "disease");

                entity.Property(e => e.PreventionId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Duration)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreventionType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RiskReduction).HasColumnType("decimal(4, 2)");
            });

            modelBuilder.Entity<ProcessedArticle>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.ToTable("ProcessedArticle", "surveillance");

                entity.Property(e => e.ArticleId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ArticleTitle).HasMaxLength(500);

                entity.Property(e => e.CertaintyScore).HasColumnType("decimal(18, 16)");

                entity.Property(e => e.FeedPublishedDate).HasColumnType("date");

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

            modelBuilder.Entity<ProvisionMarkerDss>(entity =>
            {
                entity.HasKey(e => new { e.OwnerScopeLocalId, e.ObjectId });

                entity.ToTable("provision_marker_dss", "DataSync");

                entity.Property(e => e.OwnerScopeLocalId).HasColumnName("owner_scope_local_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ProvisionDatetime)
                    .HasColumnName("provision_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProvisionLocalPeerKey).HasColumnName("provision_local_peer_key");

                entity.Property(e => e.ProvisionScopeLocalId).HasColumnName("provision_scope_local_id");

                entity.Property(e => e.ProvisionScopePeerKey).HasColumnName("provision_scope_peer_key");

                entity.Property(e => e.ProvisionScopePeerTimestamp).HasColumnName("provision_scope_peer_timestamp");

                entity.Property(e => e.ProvisionTimestamp).HasColumnName("provision_timestamp");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnName("version")
                    .IsRowVersion();
            });

            modelBuilder.Entity<SchemaInfoDss>(entity =>
            {
                entity.HasKey(e => new { e.SchemaMajorVersion, e.SchemaMinorVersion });

                entity.ToTable("schema_info_dss", "DataSync");

                entity.Property(e => e.SchemaMajorVersion).HasColumnName("schema_major_version");

                entity.Property(e => e.SchemaMinorVersion).HasColumnName("schema_minor_version");

                entity.Property(e => e.SchemaExtendedInfo)
                    .IsRequired()
                    .HasColumnName("schema_extended_info")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ScopeConfigDss>(entity =>
            {
                entity.HasKey(e => e.ConfigId);

                entity.ToTable("scope_config_dss", "DataSync");

                entity.Property(e => e.ConfigId)
                    .HasColumnName("config_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ConfigData)
                    .IsRequired()
                    .HasColumnName("config_data")
                    .HasColumnType("xml");

                entity.Property(e => e.ScopeStatus)
                    .HasColumnName("scope_status")
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<ScopeInfoDss>(entity =>
            {
                entity.HasKey(e => e.SyncScopeName);

                entity.ToTable("scope_info_dss", "DataSync");

                entity.Property(e => e.SyncScopeName)
                    .HasColumnName("sync_scope_name")
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.ScopeConfigId).HasColumnName("scope_config_id");

                entity.Property(e => e.ScopeId)
                    .HasColumnName("scope_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ScopeLocalId)
                    .HasColumnName("scope_local_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ScopeRestoreCount)
                    .HasColumnName("scope_restore_count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScopeSyncKnowledge).HasColumnName("scope_sync_knowledge");

                entity.Property(e => e.ScopeTimestamp)
                    .HasColumnName("scope_timestamp")
                    .IsRowVersion();

                entity.Property(e => e.ScopeTombstoneCleanupKnowledge).HasColumnName("scope_tombstone_cleanup_knowledge");

                entity.Property(e => e.ScopeUserComment).HasColumnName("scope_user_comment");
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

                entity.Property(e => e.StatioType).HasColumnType("char(1)");

                entity.Property(e => e.StationCode).HasColumnType("char(3)");

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

            modelBuilder.Entity<UserLoginTrans>(entity =>
            {
                entity.ToTable("UserLoginTrans", "dbo");

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

                entity.ToTable("UserRolesTransLog", "dbo");

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

                entity.ToTable("UserTransLog", "dbo");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.ModifiedUtcdatetime)
                    .HasColumnName("ModifiedUTCDatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<XtblArticleEvent>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.EventId });

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

            modelBuilder.Entity<XtblArticleLocation>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.LocationGeoNameId });

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
                entity.HasKey(e => new { e.ArticleId, e.LocationGeoNameId, e.DiseaseId });

                entity.ToTable("Xtbl_Article_Location_Disease", "surveillance");

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

            modelBuilder.Entity<XtblDiseasePathogens>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.PathogenId });

                entity.ToTable("Xtbl_Disease_Pathogens", "disease");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseasePathogens)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Pathogens_DiseaseId");

                entity.HasOne(d => d.Pathogen)
                    .WithMany(p => p.XtblDiseasePathogens)
                    .HasForeignKey(d => d.PathogenId)
                    .HasConstraintName("FK_Xtbl_Disease_Pathogens_PathogenId");
            });

            modelBuilder.Entity<XtblDiseasePreventions>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.PreventionId });

                entity.ToTable("Xtbl_Disease_Preventions", "disease");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseasePreventions)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Preventions_DiseaseId");

                entity.HasOne(d => d.Prevention)
                    .WithMany(p => p.XtblDiseasePreventions)
                    .HasForeignKey(d => d.PreventionId)
                    .HasConstraintName("FK_Xtbl_Disease_Preventions_PreventionId");
            });

            modelBuilder.Entity<XtblDiseaseSymptom>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.SymptomId });

                entity.ToTable("Xtbl_Disease_Symptom", "disease");

                entity.Property(e => e.Frequency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseSymptom)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_Symptom_DiseaseId");

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.XtblDiseaseSymptom)
                    .HasForeignKey(d => d.SymptomId)
                    .HasConstraintName("FK_Xtbl_Disease_Symptom_SymptomId");
            });

            modelBuilder.Entity<XtblDiseaseTransmissionMode>(entity =>
            {
                entity.HasKey(e => new { e.DiseaseId, e.TransmissionModeId });

                entity.ToTable("Xtbl_Disease_TransmissionMode", "disease");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.XtblDiseaseTransmissionMode)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_Xtbl_Disease_TransmissionMode_DiseaseId");

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
        }
    }
}
