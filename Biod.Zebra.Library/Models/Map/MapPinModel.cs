using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Models.Map
{
    /// <summary>
    /// Model containing all the necessary properties in order for pins to display properly on the map
    /// </summary>
    public class MapPinModel
    {
        public IEnumerable<usp_ZebraDashboardGetEventsMap_Result> EventsMap { get; set; }
        
        public IEnumerable<MapPinEventModel> MapPinEventModels { get; set; }

        public static MapPinModel FromEventsInfoViewModel(EventsInfoViewModel eventsInfoViewModel)
        {
            return new MapPinModel
            {
                EventsMap = eventsInfoViewModel.EventsMap,
                MapPinEventModels = eventsInfoViewModel.EventsInfo.Select(MapPinEventModel.FromEventsInfoModel)
            };
        }
    }
}