using System;
using Biod.Zebra.Library.EntityModels;
using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class SourceNameHelper
    { 
        public static List<SourceNameModel> GetSourceName(IEnumerable<usp_ZebraEventGetArticlesByEventId_Result> articles) {
            var retVal = new List<SourceNameModel>();

            //var tracker = new List<string>();
            //foreach (var item in articles)
            //{
            //    var uid = item.SeqId.ToString() + item.DisplayName + item.FullName;
            //    if (tracker.IndexOf(uid) == -1)
            //    {
            //        tracker.Add(uid);
            //        retVal.Add(new SourceNameModel
            //        {
            //            SeqId = Convert.ToInt16(item.SeqId),
            //            DisplayName = item.DisplayName,
            //            FullNameList = new List<string> { item.FullName }
            //        });
            //    }
            //}

            var sourceNames = articles.GroupBy(a => new { a.SeqId, a.DisplayName, a.FullName }).
                OrderBy(b => b.Key.SeqId).ThenBy(c => c.Key.DisplayName).ThenBy(d => d.Key.FullName).
                Select(e => new { e.Key.SeqId, e.Key.DisplayName, e.Key.FullName });

            var prevDisplayName = "";
            foreach (var item in sourceNames)
            {
                if (item.DisplayName != null && item.DisplayName.Length > 0 && item.SeqId != null)
                {
                    if (item.DisplayName != prevDisplayName)
                    {
                        retVal.Add(new SourceNameModel
                        {
                            SeqId = Convert.ToInt16(item.SeqId),
                            DisplayName = item.DisplayName,
                            FullNameList = new List<string> { item.FullName }
                        });
                        prevDisplayName = item.DisplayName;
                    }
                    else
                    {
                        retVal[retVal.Count - 1].FullNameList.Add(item.FullName);
                    }
                }
            }

            return retVal;
        }
    }
}
