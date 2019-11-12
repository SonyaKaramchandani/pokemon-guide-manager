using Biod.Surveillance.ViewModels;
using Biod.Zebra.Library.EntityModels.Surveillance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Surveillance.Api
{
    public class SuggestedEventListApiController : ApiController
    {
        public List<SuggestedEventItemModel> Get()
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            IEnumerable<SuggestedEvent> suggestedEvent = db.SuggestedEvents.ToList();

            List<SuggestedEventItemModel> suggestedEventFormatted = new List<SuggestedEventItemModel>();
            foreach (var evt in suggestedEvent)
            {
                SuggestedEventItemModel evtItem = new SuggestedEventItemModel();

                evtItem.EventId = evt.SuggestedEventId.ToString();
                evtItem.EventTitle = evt.EventTitle;
                evtItem.StartDate = null;
                evtItem.EndDate = null;
                evtItem.ArticleCount = evt.ProcessedArticles.Where(s => s.HamTypeId != 1).Count();
                suggestedEventFormatted.Add(evtItem);
            }
            //return Json(suggestedEventFormatted);
            return suggestedEventFormatted;
        }
    }
}
