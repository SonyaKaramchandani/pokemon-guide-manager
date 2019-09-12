using BdDataApi.EntityModels;
using System;
using System.Collections.Generic;

namespace BdDataApi.Models
{
    public class EventInfoModel
    {
        public EventInfoModel()
        {
            EventArticles = new List<usp_GetZebraEventArticlesByEventId_Result>();
            EventMapInfo = new EventMapInfoModel(EventId);
        }
        public int EventId { get; set; }
        public int EventGeonameId { get; set; }
        public string DiseaseName { get; set; }
        public string LocationName { get; set; }
        public string MicrobeType { get; set; }
        public string TransmittedBy { get; set; }
        public string IncubationPeriod { get; set; }
        public string Vaccination { get; set; }
        public string Brief { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CasesReported { get; set; }
        public int CaseSuspected { get; set; }
        public int CaseConfirmed { get; set; }
        public int Deaths { get; set; }
        public string Reasons { get; set; }
        public string PriorityTitle { get; set; }
        public string Email { get; set; }
        public string DestinationDisplayName { get; set; }
        public string EventTitle { get; set; }
        public List<usp_GetZebraEventArticlesByEventId_Result> EventArticles { get; set; }
        public EventMapInfoModel EventMapInfo { get; set; }
    }
}