﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Diseases.SyncConsole.EntityModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BiodSurveillanceModelEntities : DbContext
    {
        public BiodSurveillanceModelEntities()
            : base("name=BiodSurveillanceModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<string> usp_UpdateDiseaseApi_main(string json_1, string json_2, string json_3)
        {
            var json_1Parameter = json_1 != null ?
                new ObjectParameter("Json_1", json_1) :
                new ObjectParameter("Json_1", typeof(string));
    
            var json_2Parameter = json_2 != null ?
                new ObjectParameter("Json_2", json_2) :
                new ObjectParameter("Json_2", typeof(string));
    
            var json_3Parameter = json_3 != null ?
                new ObjectParameter("Json_3", json_3) :
                new ObjectParameter("Json_3", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("usp_UpdateDiseaseApi_main", json_1Parameter, json_2Parameter, json_3Parameter);
        }
    
        public virtual ObjectResult<string> usp_UpdateSurveillanceApi_main(string serviceDomainName)
        {
            var serviceDomainNameParameter = serviceDomainName != null ?
                new ObjectParameter("serviceDomainName", serviceDomainName) :
                new ObjectParameter("serviceDomainName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("usp_UpdateSurveillanceApi_main", serviceDomainNameParameter);
        }
    }
}
