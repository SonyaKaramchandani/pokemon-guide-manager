using Biod.Insights.Api.Data.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class BiodZebraContext
    {
        public virtual DbSet<usp_SearchGeonames_Result> usp_SearchGeonames_Result { get; set; }
        public virtual DbSet<usp_ZebraEventGetEventSummary_Result> usp_ZebraEventGetEventSummary_Result { get; set; }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<usp_SearchGeonames_Result>(entity => { entity.HasNoKey(); });
            modelBuilder.Entity<usp_ZebraEventGetEventSummary_Result>(entity => { entity.HasNoKey(); });
        }
    }
}