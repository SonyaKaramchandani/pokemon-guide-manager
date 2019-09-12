using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BdDataApi.EntityModels;
using BdDataApi.Models;
using GeoNamesModel;

namespace BdDataApi.Api
{
    #region get the main healthmap tables records
    public class HealthmapAlertArticlesController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticles_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticles_sp(ConfigVariables.RetrieveDate);
        }
    }

    public class HealthmapAlertArticlesArchiveController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticlesArchive_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticlesArchive_sp(ConfigVariables.RetrieveDate);
        }
    }

    public class HealthmapAlertArticlesContentController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticlesContent_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticlesContent_sp(ConfigVariables.RetrieveDate);
        }
    }

    public class HealthmapAlertArticlesContentArchiveController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticlesContentArchive_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticlesContentArchive_sp(ConfigVariables.RetrieveDate);
        }
    }

    public class HealthmapAlertLocationTypeController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertLocationType_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertLocationType_sp();
        }
    }

    public class HealthmapAlertSourceTypeController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertSourceType_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertSourceType_sp();
        }
    }

    public class HealthmapDiseaseController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getDisease_sp_Result> Get()
        {
            return _context.healthmapApi_getDisease_sp();
        }
    }

    public class HealthmapDiseaseAlertArticlesController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getDiseaseAlertArticles_sp_Result> Get()
        {
            return _context.healthmapApi_getDiseaseAlertArticles_sp(ConfigVariables.RetrieveDate);
        }
    }

    public class HealthmapDiseaseAlertArticlesArchiveController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getDiseaseAlertArticlesArchive_sp_Result> Get()
        {
            return _context.healthmapApi_getDiseaseAlertArticlesArchive_sp(ConfigVariables.RetrieveDate);
        }
    }

    #endregion 

    #region get the updated healthmap tables records
    public class HealthmapAlertArticlesUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticlesUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticlesUpdated_sp();
        }
    }

    public class HealthmapAlertArticlesContentUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertArticlesContentUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertArticlesContentUpdated_sp();
        }
    }

    public class HealthmapAlertLocationTypeUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertLocationTypeUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertLocationTypeUpdated_sp();
        }
    }

    public class HealthmapAlertSourceTypeUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getAlertSourceTypeUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getAlertSourceTypeUpdated_sp();
        }
    }

    public class HealthmapDiseaseUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getDiseaseUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getDiseaseUpdated_sp();
        }
    }

    public class HealthmapDiseaseAlertArticlesUpdatedController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmapApi_getDiseaseAlertArticlesUpdated_sp_Result> Get()
        {
            return _context.healthmapApi_getDiseaseAlertArticlesUpdated_sp();
        }
    }
    #endregion

    #region get BD location by location name
    public class HealthmapBdLocationController : ApiController
    {
        readonly HealthmapEntities _context = new HealthmapEntities();
        public IEnumerable<healthmap_getBdLocation_fn_Result> Get(string locationName)
        {
            return _context.healthmap_getBdLocation_fn(locationName);
        }
    }

    public class PlacesForNameController : ApiController
    {
        public string Get(string candidateName, string placeType = null, string containedIn = null, bool queryAlternateNames = true)
        {
            var toReturn = string.Empty;
            using (var dbmodel = new GNModel())
            {
                //HashSet<Place> hits = dbmodel.PlacesForName("La Vaca Blanca", null, "Dominican Rep", true);
                HashSet<Place> hits = dbmodel.PlacesForName(candidateName, placeType, containedIn, queryAlternateNames);
                if (hits.Any())
                    toReturn= "Hit " + hits.Count() + " results for query.  The first one had geonameId=" + hits.First().id;
            }
            return toReturn;
        }
    }


    #endregion
}
