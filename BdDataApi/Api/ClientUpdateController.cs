using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BdDataApi.EntityModels;
using BdDataApi.Models;
using GeoNamesModel;
using System.Configuration;

namespace BdDataApi.Api
{
    #region get the SQL satatments
    public class ClientUpdateController : ApiController
    {
        readonly ClientUpdateEntities _context = new ClientUpdateEntities();
        public IEnumerable<clientUpdateApi_getSqlStatements_sp_Result> Get(string Client, int Top, bool ExecutedByClientStatus, DateTime ClientUpdateStartDate, DateTime ClientUpdateEndDate)
        {
            _context.Database.CommandTimeout = ConfigVariables.CommandTimeout;
            return _context.clientUpdateApi_getSqlStatements_sp(Client, Top, ExecutedByClientStatus, ClientUpdateStartDate, ClientUpdateEndDate);
        }
    }
    #endregion
}
