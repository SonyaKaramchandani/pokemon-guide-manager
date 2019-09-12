using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biod.Surveillance.SyncConsole.Client.EntityModels;
using System.Data.Entity.Migrations;

namespace Biod.Surveillance.SyncConsole.Client
{
    class EventSyncer
    {
        internal static void Sync()
        {
            Console.WriteLine("Event syncing starting...");
            try
            {
                var count = FetchAndUpdateEvents();
                Console.WriteLine($"Event syncing [Success] {count} events synced");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Event syncing [Failed] {exception}");
                throw;
            }
            Console.WriteLine("Event syncing complete...");
        }

        static int FetchAndUpdateEvents()
        {
            surveillance_Event[] events = null;
            surveillance_Xtbl_Event_Location[] eventLocations = null;
            place_Geonames[] geonames = null;
            using (var sourceDb = new SurveillanceEntities())
            {
                eventLocations = sourceDb.Xtbl_Event_Location.AsNoTracking()
                    .Where(e => e.Event.IsPublished.Value)
                    .Select(el => new surveillance_Xtbl_Event_Location
                    {
                        Id = Guid.NewGuid(),
                        EventId = el.EventId,
                        GeonameId = el.GeonameId,
                        EventDate = el.EventDate,
                        SuspCases = el.SuspCases,
                        ConfCases = el.ConfCases,
                        RepCases = el.RepCases,
                        Deaths = el.Deaths
                    })
                    .ToArray();

                geonames = sourceDb.Geonames.AsNoTracking().Take(1000)
                    //.Where(geoname => eventLocations.Any(el => el.GeonameId == geoname.GeonameId))
                    .Select(geoname => new place_Geonames
                    {
                        GeonameId = geoname.GeonameId,
                        Name = geoname.Name,
                        LocationType = geoname.LocationType,
                        Admin1GeonameId = geoname.Admin1GeonameId,
                        CountryGeonameId = geoname.CountryGeonameId,
                        DisplayName = geoname.DisplayName,
                        Alternatenames = geoname.Alternatenames,
                        ModificationDate = geoname.ModificationDate,
                        FeatureCode = geoname.FeatureCode,
                        CountryName = geoname.CountryName,
                        Latitude = geoname.Latitude,
                        Longitude = geoname.Longitude,
                        Population = geoname.Population,
                        SearchSeq2 = null,
                        Shape = null,
                        LatPopWeighted = null,
                        LongPopWeighted = null
                    }).ToArray();

                events = sourceDb.Events.AsNoTracking()
                    .Where(e => e.IsPublished.Value)
                    .Select(e => new surveillance_Event
                    {
                        EventId = e.EventId,
                        EventTitle = e.EventTitle,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        LastUpdatedDate = e.LastUpdatedDate,
                        PriorityId = e.PriorityId,
                        IsPublished = e.IsPublished,
                        Summary = e.Summary,
                        Notes = e.Notes,
                        DiseaseId = e.DiseaseId,
                        CreatedDate = e.CreatedDate,
                        EventMongoId = e.EventMongoId,
                        LastUpdatedByUserName = e.LastUpdatedByUserName,
                        IsLocalOnly = e.IsLocalOnly,
                        HasOutlookReport = e.HasOutlookReport
                    }).ToArray();
            }

            using (var targetDb = new ClientHealthmapEntities())
            using (var transaction = targetDb.Database.BeginTransaction())
            {
                try
                {
                    targetDb.Database.ExecuteSqlCommand("DELETE FROM [surveillance].[surveillance_Xtbl_Event_Location];" +
                                                        "DELETE FROM [place].[place_Geonames];" +
                                                        "DELETE FROM [surveillance].[surveillance_Event]");
                    
                    targetDb.surveillance_Event.AddRange(events);
                    targetDb.place_Geonames.AddRange(geonames);
                    targetDb.surveillance_Xtbl_Event_Location.AddRange(eventLocations);

                    targetDb.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return events.Count();
        }
    }
}
