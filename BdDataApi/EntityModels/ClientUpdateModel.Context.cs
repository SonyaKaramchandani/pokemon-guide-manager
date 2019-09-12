﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BdDataApi.EntityModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ClientUpdateEntities : DbContext
    {
        public ClientUpdateEntities()
            : base("name=ClientUpdateEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<clientUpdateApi_getSqlStatements_sp_Result> clientUpdateApi_getSqlStatements_sp(string client, Nullable<int> top, Nullable<bool> executedByClientStatus, Nullable<System.DateTime> clientUpdateStartDate, Nullable<System.DateTime> clientUpdateEndDate)
        {
            var clientParameter = client != null ?
                new ObjectParameter("Client", client) :
                new ObjectParameter("Client", typeof(string));
    
            var topParameter = top.HasValue ?
                new ObjectParameter("Top", top) :
                new ObjectParameter("Top", typeof(int));
    
            var executedByClientStatusParameter = executedByClientStatus.HasValue ?
                new ObjectParameter("ExecutedByClientStatus", executedByClientStatus) :
                new ObjectParameter("ExecutedByClientStatus", typeof(bool));
    
            var clientUpdateStartDateParameter = clientUpdateStartDate.HasValue ?
                new ObjectParameter("ClientUpdateStartDate", clientUpdateStartDate) :
                new ObjectParameter("ClientUpdateStartDate", typeof(System.DateTime));
    
            var clientUpdateEndDateParameter = clientUpdateEndDate.HasValue ?
                new ObjectParameter("ClientUpdateEndDate", clientUpdateEndDate) :
                new ObjectParameter("ClientUpdateEndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<clientUpdateApi_getSqlStatements_sp_Result>("clientUpdateApi_getSqlStatements_sp", clientParameter, topParameter, executedByClientStatusParameter, clientUpdateStartDateParameter, clientUpdateEndDateParameter);
        }
    }
}
