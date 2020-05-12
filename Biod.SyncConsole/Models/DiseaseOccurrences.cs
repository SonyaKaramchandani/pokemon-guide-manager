using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.SyncConsole.Models
{
    public class DiseaseOccurrence
    {
        public int OccurrenceId { get; set; }
        public int DiseaseId { get; set; }
        public string Disease { get; set; }
        public double Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastModified { get; set; }
        public Metrics Metrics { get; set; }
        public Places Places { get; set; }
        public DataSources DataSources { get; set; }
    }

    public class Metrics
    {
        public int MetricId { get; set; }
        public string Metric { get; set; }
        public string MetricType { get; set; }
        public bool IsAggregatable { get; set; }
    }

    public class Places
    {
        public int PlaceId { get; set; }
        public string Place { get; set; }
        public int PlaceTypeId { get; set; }
        public string placeType { get; set; }
        public int GeonameId { get; set; }
    }

    public class DataSources
    {
        public int DataSourceId { get; set; }
        public string DataSource { get; set; }
        public string SourceType { get; set; }
        public int ParentId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }
    }
}
