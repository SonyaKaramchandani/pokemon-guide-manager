using Biod.Insights.Api.Data.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class BiodZebraContext
    {
        public virtual DbSet<usp_SearchGeonames_Result> usp_SearchGeonames_Result { get; set; }
        public virtual DbSet<usp_ZebraEventGetEventSummary_Result> usp_ZebraEventGetEventSummary_Result { get; set; }
        public virtual DbSet<usp_ZebraEventGetCaseCountByEventId_Result> usp_ZebraEventGetCaseCountByEventId_Result { get; set; }
        public virtual DbSet<usp_ZebraDiseaseGeLocalCaseCount_Result> usp_ZebraDiseaseGeLocalCaseCount_Result { get; set; }
        public virtual DbSet<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result> usp_ZebraDataRenderSetImportationRiskByGeonameId_Result { get; set; }
        public virtual DbSet<usp_ZebraEventGetArticlesByEventId_Result> usp_ZebraEventGetArticlesByEventId_Result { get; set; }
        public virtual DbSet<usp_ZebraPlaceGetGridIdByGeonameId_Result> usp_ZebraPlaceGetGridIdByGeonameId_Result { get; set; }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<usp_SearchGeonames_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraEventGetEventSummary_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraEventGetCaseCountByEventId_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraDiseaseGeLocalCaseCount_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraEventGetArticlesByEventId_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraPlaceGetGridIdByGeonameId_Result>(entity => { entity.HasNoKey(); });
        }
    }
}