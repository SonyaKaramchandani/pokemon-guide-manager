using Biod.Surveillance.SyncConsole.Client.Models;
using Biod.Surveillance.SyncConsole.Client.EntityModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Biod.Zebra.Library.Infrastructures.Notification;

namespace SurveillanceToClientDatabaseSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Fetching Surveillance Articles...");
                var artArr = FetchArticle();
                Console.WriteLine("Updating healthmap_AlertArticles and healthmap_AlertArticlesContent...");
                var artDisDict = UpdateArticle(artArr);
                Console.WriteLine("Updating healthmap_Disease...");
                UpdateDisease();
                Console.WriteLine("Updating healthmap_DiseaseAlertArticles...");
                UpdateDiseaseArticle(artDisDict);
                Console.WriteLine("Completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error has occurred!");
                var message = string.Format("{0}\n{1}\n", ex.Message, ex.StackTrace);
                var emailTo = ConfigurationManager.AppSettings.Get("emailRecipientListUponError");
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponError");
                var emailList = emailTo.Split(',');
                SendMail(emailList, subject, message);
            }
        }

        static ICollection<ProcessedArticle> FetchArticle() {
            ICollection<ProcessedArticle> retVal = null;

            var cutoff = ConfigVariables.CleanupDays;
            using (var dbCH = new ClientHealthmapEntities())
            {
                try
                {
                    var cutoff1 = dbCH.healthmap_AlertArticles.OrderByDescending(y => y.LastUpdateTime).FirstOrDefault().LastUpdateTime.Date;
                    var cutoff2 = new DateTime();
                    var noDisease = dbCH.healthmap_AlertArticles.Where(x => x.healthmap_Disease.Count() == 0);
                    if (noDisease.Count() > 0) {
                        cutoff2 = noDisease.OrderBy(z => z.LastUpdateTime).FirstOrDefault().LastUpdateTime.Date;
                    }
                    if (cutoff2 != DateTime.MinValue && cutoff2 < cutoff1)
                    {
                        cutoff = cutoff2;
                    }
                    else
                    {
                        cutoff = cutoff1;
                    }
                    cutoff = cutoff.AddDays(-1);
                }
                catch { 
                    //do nothing 
                }
            }

            using (var db = new SurveillanceEntities())
            {
                //var expDate = DateTime.Now.AddDays(-30).Date;//only want articles that was publish in the last 30 days
                //Grab articles that were published within the past 90 days
                var arr = db.ProcessedArticles.Where(x =>
                    x.IsCompleted == true &&
                    x.UserLastModifiedDate != null &&
                    x.UserLastModifiedDate >= cutoff &&
                    (x.HamTypeId == 2 || x.HamTypeId == 3) &&
                    x.FeedPublishedDate >= ConfigVariables.CleanupDays &&
                    x.Xtbl_Article_Location_Disease.Count > 0 &&
                    !String.IsNullOrEmpty(x.ArticleTitle) 
                ).ToList();
               
                arr.Select(
                    y => new ProcessedArticle
                    {
                        ArticleFeed = y.ArticleFeed,
                        Xtbl_Article_Location_Disease = y.Xtbl_Article_Location_Disease.ToList().Select(
                            z => new Xtbl_Article_Location_Disease { Geoname = z.Geoname }).ToList()
                    }
                ).ToList();
                
                retVal = arr;
            }
            return retVal;
        }

        static Dictionary<string, int> UpdateArticle(ICollection<ProcessedArticle> input)
        {
            var ArticleIdDiseaseIdDict = new Dictionary<string, int>();
            if (input.Count > 0)
            {
                using (var db = new ClientHealthmapEntities())
                {
                    foreach (var art in input)
                    {

                        var existRows = db.healthmap_AlertArticles.Where(x => x.ArticleId.StartsWith(art.ArticleId + "&bd&"));
                        if (existRows.Count() > 0)
                        {
                            var artWithGeo = art.Xtbl_Article_Location_Disease.Where(y => y.LocationGeoNameId > -1);

                            if (existRows.Count() == artWithGeo.Count())//update current rows
                            {
                                var seqId = 0;
                                for (int i = 0; i < art.Xtbl_Article_Location_Disease.Count; i++)
                                {
                                    if (art.Xtbl_Article_Location_Disease.ElementAt(i).LocationGeoNameId > -1)
                                    {
                                        var curR = db.healthmap_AlertArticles.Where(x => x.ArticleId.StartsWith(art.ArticleId + "&bd&" + seqId.ToString())).FirstOrDefault();
                                        curR = AssignArticle(curR, art, art.Xtbl_Article_Location_Disease.ElementAt(i).Geoname, seqId, false);
                                        ArticleIdDiseaseIdDict.Add(curR.ArticleId, art.Xtbl_Article_Location_Disease.ElementAt(i).DiseaseId);
                                        seqId += 1;
                                    }
                                }
                            }
                            else//remove current rows, then add new
                            {
                                db.healthmap_AlertArticles.RemoveRange(existRows);
                                var seqId = 0;
                                for (int i = 0; i < art.Xtbl_Article_Location_Disease.Count; i++)
                                {
                                    if (art.Xtbl_Article_Location_Disease.ElementAt(i).LocationGeoNameId > -1)
                                    {
                                        var r = new healthmap_AlertArticles();
                                        r = AssignArticle(r, art, art.Xtbl_Article_Location_Disease.ElementAt(i).Geoname, seqId, true);
                                        db.healthmap_AlertArticles.Add(r);
                                        ArticleIdDiseaseIdDict.Add(r.ArticleId, art.Xtbl_Article_Location_Disease.ElementAt(i).DiseaseId);
                                        seqId += 1;
                                    }
                                }
                            }

                        }
                        else
                        {
                            var seqId = 0;
                            for (int i = 0; i < art.Xtbl_Article_Location_Disease.Count; i++)
                            {
                                if (art.Xtbl_Article_Location_Disease.ElementAt(i).LocationGeoNameId > -1)
                                {
                                    var r = new healthmap_AlertArticles();
                                    r = AssignArticle(r, art, art.Xtbl_Article_Location_Disease.ElementAt(i).Geoname, seqId, true);
                                    db.healthmap_AlertArticles.Add(r);
                                    ArticleIdDiseaseIdDict.Add(r.ArticleId, art.Xtbl_Article_Location_Disease.ElementAt(i).DiseaseId);
                                    seqId += 1;
                                }
                            }
                        }
                    }

                    db.SaveChanges();
                }

            }

            return ArticleIdDiseaseIdDict;
        }

        private static healthmap_AlertArticles AssignArticle(healthmap_AlertArticles toArticle, ProcessedArticle fromArticle, Geoname geoNameItem, int seqId, bool isInsert)
        {
            var locationConverter = new Dictionary<int?, int>();
            // geoname location type is key
            // healthmap location type is value
            locationConverter.Add(6, 1);//country
            locationConverter.Add(4, 2);//province
            locationConverter.Add(2, 3);//city
            
            //var r = new healthmap_AlertArticles();
            toArticle.ArticleId = fromArticle.ArticleId + "&bd&" + seqId.ToString();
            if (!String.IsNullOrEmpty(fromArticle.OriginalLanguage) && fromArticle.OriginalLanguage != "en")
            {
                toArticle.ArticleId += "&trto=en&trfr=" + fromArticle.OriginalLanguage.Substring(0, 2);
            }
            toArticle.Title = fromArticle.ArticleTitle;
            toArticle.Description = String.IsNullOrEmpty(fromArticle.ArticleBody) ? fromArticle.ArticleBody : fromArticle.ArticleBody.Substring(0, fromArticle.ArticleBody.Length > 250 ? 250 : fromArticle.ArticleBody.Length);
            toArticle.PublicationDate = fromArticle.FeedPublishedDate;
            toArticle.SourceUrl = String.IsNullOrEmpty(fromArticle.FeedURL) ? fromArticle.OriginalSourceURL : fromArticle.FeedURL;
            toArticle.Author = fromArticle.ArticleFeed.ArticleFeedName;
            toArticle.LocationName = geoNameItem.DisplayName;
            toArticle.Lat = Convert.ToDouble(geoNameItem.Latitude);
            toArticle.Long = Convert.ToDouble(geoNameItem.Longitude);
            toArticle.Point = geoNameItem.Latitude.ToString() + " " + geoNameItem.Longitude.ToString();
            toArticle.SourceTypeId = 2;
            toArticle.LastUpdateTime = DateTime.Now;
            toArticle.GeonameId = geoNameItem.GeonameId;
            int outVal;
            toArticle.LocationTypeId = geoNameItem.LocationType == null ? 5 : (locationConverter.TryGetValue(geoNameItem.LocationType, out outVal) ? outVal : 5);//5 represents missing
            var hm = new HealthmapEntities();
            //var hmLocObj = hm.healthmap_getBdLocation_fn(geoNameItem.CountryName).FirstOrDefault();
            toArticle.BdCtryTeryId = 0;//hmLocObj == null ? 0 : hmLocObj.BdCtryTeryId.GetValueOrDefault();
            toArticle.BdProvinceId = 0;
            toArticle.BdLocationTypeId = toArticle.LocationTypeId;//5;
            toArticle.CountryName = geoNameItem.CountryName;
            toArticle.CountryGeonameId = geoNameItem.CountryGeonameId;

            if (isInsert) {
                var ac = new healthmap_AlertArticlesContent();
                ac.mainSource = fromArticle.ArticleFeed.ArticleFeedName;
                ac.FullDescription = fromArticle.ArticleBody;
                toArticle.healthmap_AlertArticlesContent = ac;
            }
            else { 
                toArticle.healthmap_AlertArticlesContent.mainSource = fromArticle.ArticleFeed.ArticleFeedName;
                toArticle.healthmap_AlertArticlesContent.FullDescription = fromArticle.ArticleBody;
            }

            return toArticle;
        }

        static void UpdateDisease() {

            using (var dbS = new SurveillanceEntities())
            {
                if (dbS.Diseases.Count() > 0)
                {
                    using (var dbCH = new ClientHealthmapEntities())
                    {
                        var doSave = false;

                        foreach (var hd in dbCH.healthmap_Disease)//remove rows
                        {
                            var sdRow = dbS.Diseases.Where(y => y.DiseaseId == hd.DiseaseId).FirstOrDefault();
                            if (sdRow == null)
                            {
                                dbCH.healthmap_Disease.Remove(hd);
                                doSave = true;
                            }
                        }

                        foreach (var sd in dbS.Diseases)//update existing rows and insert new rows
                        {
                            var existRow = dbCH.healthmap_Disease.Where(x => x.DiseaseId == sd.DiseaseId).FirstOrDefault();
                            if (existRow != null)
                            {
                                existRow.Name = sd.DiseaseName;
                                existRow.Description = null;
                                existRow.IsOfInterest = 1;
                                doSave = true;
                            }
                            else
                            {
                                var newRow = new healthmap_Disease();
                                newRow.DiseaseId = Convert.ToInt16(sd.DiseaseId);
                                newRow.Name = sd.DiseaseName;
                                newRow.Description = null;
                                newRow.IsOfInterest = 1;
                                dbCH.healthmap_Disease.Add(newRow);
                                doSave = true;
                            }
                        }

                        if (doSave) { 
                            dbCH.SaveChanges();
                        }
                    }
                }

            }
        }

        static void UpdateDiseaseArticle(Dictionary<string, int> inputDict) {
            using (var dbCH = new ClientHealthmapEntities()) { 
                foreach (var item in inputDict)
                {
                    var articleId = item.Key;
                    var diseaseId = item.Value;

                    var art = dbCH.healthmap_AlertArticles.Where(x => x.ArticleId == articleId).FirstOrDefault();
                    var d = dbCH.healthmap_Disease.Where(y => y.DiseaseId == diseaseId).FirstOrDefault();
                    if (art != null && d != null)
                    {
                        var ds = art.healthmap_Disease;
                        ds.Clear();

                        ds.Add(d);
                    }
                }

                dbCH.SaveChanges();
            }
        }

        //*******************************************************
        /// <summary>
        /// Send email to sender.
        /// </summary>
        /// <param name="mailRecipientList">The mail recipient list.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static async Task SendMail(string[] mailRecipientList, string subject, string message)
        {
            try
            {
                var mail = new EmailMessage();
                var emailClient = new EmailClient();

                foreach (string recipient in mailRecipientList)
                {
                    mail.To.Add(recipient);
                }
                mail.Subject = subject;
                mail.Body = message;
                await emailClient.SendEmailAsync(mail);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: ", exc);
            }
        }

    }
}
