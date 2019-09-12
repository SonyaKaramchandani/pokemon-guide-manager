using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BdDataApi.Models
{
    public class EventMapInfoModel
    {
        public EventMapInfoModel(int EventId)
        {
            MapViewId = "MapView" + EventId;
            HasMap = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("HasMap"));
        }
        public string MapViewId { get; set; }
        public Boolean HasMap { get; set; }
    }
}