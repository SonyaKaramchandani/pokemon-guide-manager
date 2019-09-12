using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class BiodApiContext : DbContext
    {
        public virtual DbSet<ArticleFeed> ArticleFeed { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventCreationReasons> EventCreationReasons { get; set; }
        public virtual DbSet<EventPriorities> EventPriorities { get; set; }
        public virtual DbSet<GeoNameFeatureCodes> GeoNameFeatureCodes { get; set; }
        public virtual DbSet<Geonames> Geonames { get; set; }
        public virtual DbSet<GridStation> GridStation { get; set; }
        public virtual DbSet<HamType> HamType { get; set; }
        public virtual DbSet<Huffmodel25kmworldhexagon> Huffmodel25kmworldhexagon { get; set; }
        public virtual DbSet<LastJsonStrs> LastJsonStrs { get; set; }
        public virtual DbSet<Pathogens> Pathogens { get; set; }
        public virtual DbSet<PathogenTypes> PathogenTypes { get; set; }
        public virtual DbSet<Preventions> Preventions { get; set; }
        public virtual DbSet<ProcessedArticle> ProcessedArticle { get; set; }
        public virtual DbSet<StationDestinationAirport> StationDestinationAirport { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<Systems> Systems { get; set; }
        public virtual DbSet<TransmissionModes> TransmissionModes { get; set; }
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

        // Unable to generate entity type for table 'zebra.ZebraGrid'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=;Database=;user id=;password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "bd");

            modelBuilder.Entity<ArticleFeed>(entity =>
            {
                entity.ToTable("ArticleFeed", "surveillance");

                entity.Property(e => e.ArticleFeedId).ValueGeneratedNever();

                entity.Property(e => e.ArticleFeedName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Diseases>(entity =>
            {
                entity.HasKey(e => e.DiseaseId);

                entity.ToTable("Diseases", "disease");

                entity.Property(e => e.DiseaseId).ValueGeneratedNever();

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

                entity.Property(e => e.TreatmentAvailable)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event", "surveillance");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EventTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Summary).IsUnicode(false);
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

            modelBuilder.Entity<EventPriorities>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("EventPriorities", "surveillance");

                entity.Property(e => e.PriorityId).ValueGeneratedNever();

                entity.Property(e => e.PriorityTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
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

                entity.Property(e => e.ModificationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<GridStation>(entity =>
            {
                entity.HasKey(e => new { e.GridId, e.StationId, e.ValidFromDate });

                entity.ToTable("GridStation", "zebra");

                entity.HasIndex(e => e.StationId)
                    .HasName("idx_GridStation_StationId");

                entity.HasIndex(e => e.ValidFromDate)
                    .HasName("idx_GridStation_ValidFromDate");

                entity.Property(e => e.GridId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFromDate).HasColumnType("date");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Probability).HasColumnType("decimal(10, 8)");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.GridStation)
                    .HasForeignKey(d => d.StationId)
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
                entity.HasKey(e => e.Objectid);

                entity.ToTable("HUFFMODEL25KMWORLDHEXAGON");

                entity.Property(e => e.Objectid).HasColumnName("OBJECTID");

                entity.Property(e => e.GridId)
                    .HasColumnName("gridId")
                    .HasMaxLength(12);
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

                entity.HasIndex(e => e.CityId)
                    .HasName("idx_Stations_CityId");

                entity.Property(e => e.StationId).ValueGeneratedNever();

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.StateName).HasMaxLength(64);

                entity.Property(e => e.StatioType).HasColumnType("char(1)");

                entity.Property(e => e.StationCode).HasColumnType("char(3)");

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

                entity.Property(e => e.TransmissionMode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
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

                entity.HasOne(d => d.LocationGeoName)
                    .WithMany(p => p.XtblArticleLocation)
                    .HasForeignKey(d => d.LocationGeoNameId)
                    .HasConstraintName("FK_Xtbl_Article_Location_Location");
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
                entity.HasKey(e => new { e.EventId, e.GeonameId, e.EventDate });

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
