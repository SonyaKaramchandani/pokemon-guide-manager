namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;


    public partial class GNModel : DbContext
    {
        public GNModel()
            : base("name=GeoNamesConnection")
        {
            this.Configuration.AutoDetectChangesEnabled = false;    // Since this context is read only, can turn off change detection to help performance
            Database.SetInitializer<GNModel>(null);                 // The database was created independently of this model.
            //this.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));    // For logging all DB queries...
        }


        public virtual DbSet<AdminCode> AdminCodes { get; set; }
        public virtual DbSet<AlternateName> AlternateNames { get; set; }
        public virtual DbSet<FeatureClass> FeatureClasses { get; set; }
        public virtual DbSet<FeatureCode> FeatureCodes { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<ISOLanguageCode> ISOLanguageCodes { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<AdminHierarchy> AdminHierarchies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Shape> Shapes { get; set; }
        public virtual DbSet<UserTag> UserTags { get; set; }
        public virtual DbSet<BDAlternateName> BDAlternateNames { get; set; }


        private class PlaceEquals : IEqualityComparer<Place>
        {
            bool IEqualityComparer<Place>.Equals(Place left, Place right)
            {
                return (left.id == right.id);
            }

            int IEqualityComparer<Place>.GetHashCode(Place obj)
            {
                return obj.id;
            }
        }


        // TODO:  allow containedIn to be a geonameId as well (like in geonames.py) - use C# generics
        private bool FilterUncontained(HashSet<Place> resultSet, string required, bool queryAlternateNames)
        {
            BDAlternateName cachedRequiredAlt = BDAlternateNames.Where(n => n.alternateName == required).FirstOrDefault();
            resultSet.RemoveWhere(delegate (Place p) {
                foreach (Place parent in GetHierarchyForPlace(p))
                {
                    if (parent.name == required)
                        return false;
                    if (!queryAlternateNames)
                        continue;
                    if (null != cachedRequiredAlt  &&  parent.id == cachedRequiredAlt.geonameid)
                        return false;
                    if (null != parent.alternatenames  &&  parent.alternatenames.Split(new char[]{','}).Contains(required))
                        return false;
                } // foreach
                return true;
            });
            return (resultSet.Count() > 0);
        }


        /**
         * Lookup all GeoName records that correspond with a particular name.
         *
         * @param candidateName:  the name you want to resolve
         * @param placeType:      optionally restrict search to this type of featureCode (e.g., "ADM1", "PPLA", "TERR", etc.)
         *                        See http://www.geonames.org/export/codes.html for full list.
         * @param containedIn:    optional name of a place that must be part of the administrative hierarchy
         * @param queryAlternateNames:  if True, will also check alternate names/spellings/languages
         *
         * @return a set of Place objects that are called candidateName
         */
        // TAI:  also restrict based on type (colloquial, historical, short, etc.) of alternate name?
        public HashSet<Place> PlacesForName(string candidateName, string placeType = null, string containedIn = null, bool queryAlternateNames = true)
        {
            HashSet<Place> result = new HashSet<Place>(new PlaceEquals());
            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = "SELECT * FROM bd.AlternateNames WHERE alternateName=@cand";
            foreach (BDAlternateName an in BDAlternateNames.SqlQuery(query, new object[] { new SqlParameter("@cand", candidateName) }).AsNoTracking())
            {
                if (null == placeType  ||  an.Place.featureCode == placeType)
                    result.Add(an.Place);
            } // foreach
            if (result.Count() > 0  &&  (null == containedIn  ||  FilterUncontained(result, containedIn, queryAlternateNames)))
                return result;

            parameters.Add(new SqlParameter("@cand", candidateName));
            query = "SELECT * FROM GeoNames.Place WHERE name=@cand OR asciiname=@cand";
            if (placeType != null)
            {
                parameters.Add(new SqlParameter("@placeType", placeType));
                query += " AND featureCode=@placeType";
            }
            foreach (Place nm in this.Places.SqlQuery(query, parameters.ToArray()).AsNoTracking())
                result.Add(nm);

            if (queryAlternateNames)
            {
                query = "SELECT * FROM GeoNames.AlternateNames WHERE alternateName=@cand";
                foreach (AlternateName an in AlternateNames.SqlQuery(query, new object[] { new SqlParameter("@cand", candidateName) }).AsNoTracking())
                {
                    if (null == placeType  ||  an.Place.featureCode == placeType)
                        result.Add(an.Place);
                } // foreach
            }

            if (null != containedIn)
                FilterUncontained(result, containedIn, queryAlternateNames);

            return result;
        }


        /**
         * Gets the geographical hierarchy containing a place.  
         * For example, for Toronto, this could return Place objects in order 
         * for each of "Ontario", "Canada", "North America", and "Earth".
         * 
         * @param p:     a Place
         * @param type:  the GeoNames hierarchy to use (defaults to "ADM")
         * @return a list of Place objects in order of the branch of the administrative hierarchy that contains p
         */
        public List<Place> GetHierarchyForPlace(Place p, string type = "ADM")
        {
            List<Place> result = new List<Place>();

            if (null == p.countryCode  ||  p.countryCode.Length == 0)
                return result;

            AdminCode ac = this.AdminCodes.SqlQuery("SELECT TOP 1 * FROM GeoNames.AdminCode WHERE code = @adminCode", new object[] { new SqlParameter("@adminCode", p.GetAdminCode()) }).AsNoTracking().FirstOrDefault();
            if (null == ac)
                return result;

            // Walk up the tree..
            Place hit = this.Places.SqlQuery("SELECT * FROM GeoNames.Place WHERE id = "  + ac.id.ToString()).AsNoTracking().FirstOrDefault();
            string queryStr = "SELECT P.* FROM GeoNames.Place AS P INNER JOIN GeoNames.AdminHierarchy AS AH ON P.id = AH.parentId WHERE AH.type = '" + type + "' AND AH.childId = " ;
            while (null != hit)
            {
                result.Add(hit);
                hit = this.Places.SqlQuery(queryStr + hit.id.ToString()).AsNoTracking().FirstOrDefault();
            } // while

            return result;
        }


        public override int SaveChanges()
        {
            throw new InvalidOperationException("The database context is read-only.");
        }

        public override Task<int> SaveChangesAsync()
        {
            throw new InvalidOperationException("The database context is read-only.");
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminCode>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<AdminCode>()
                .Property(e => e.ucs2Name)
                .IsUnicode(true);

            modelBuilder.Entity<AdminCode>()
                .Property(e => e.asciiName)
                .IsUnicode(false);

            modelBuilder.Entity<AdminCode>()
                .HasMany(e => e.ParentAdminHierarchies)
                .WithRequired(e => e.ChildAdminCode)
                .HasForeignKey(e => e.childId);

            modelBuilder.Entity<AdminCode>()
                .HasMany(e => e.ChildAdminHierarchies)
                .WithRequired(e => e.ParentAdminCode)
                .HasForeignKey(e => e.parentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AlternateName>()
                .Property(e => e.isolanguage)
                .IsUnicode(false);

            modelBuilder.Entity<AlternateName>()
                .Property(e => e.alternateName)
                .IsUnicode(true);

            modelBuilder.Entity<BDAlternateName>()
                .Property(e => e.alternateName)
                .IsUnicode(true);

            modelBuilder.Entity<FeatureClass>()
                .Property(e => e.@class)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FeatureClass>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<FeatureClass>()
                .HasMany(e => e.FeatureCodes)
                .WithRequired(e => e.FeatureClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeatureClass>()
                .HasMany(e => e.Places)
                .WithRequired(e => e.FeatureClassRef)
                .HasForeignKey(e => e.featureClass)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeatureCode>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<FeatureCode>()
                .Property(e => e.@class)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FeatureCode>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<FeatureCode>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<FeatureCode>()
                .HasMany(e => e.Places)
                .WithRequired(e => e.FeatureCodeRef)
                .HasForeignKey(e => e.featureCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.name)
                .IsUnicode(true);

            modelBuilder.Entity<Place>()
                .Property(e => e.asciiname)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.alternatenames)
                .IsUnicode(true);

            modelBuilder.Properties<decimal>().Configure(d => d.HasPrecision(24, 20));
            /*
            modelBuilder.Entity<Place>()
                .Property(e => e.latitude)
                .HasPrecision(24, 20);

            modelBuilder.Entity<Place>()
                .Property(e => e.longitude)
                .HasPrecision(24, 20);
             */

            modelBuilder.Entity<Place>()
                .Property(e => e.featureClass)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.featureCode)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.countryCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.cc2)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.admin1Code)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.admin2Code)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.admin3Code)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.admin4Code)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.timezone)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .HasOptional(e => e.AdminCode)
                .WithRequired(e => e.Place);

            modelBuilder.Entity<Country>()
                .HasRequired(e => e.Place)
                .WithOptional(e => e.Country);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.Shapes)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.AlternateNames)
                .WithRequired(e => e.Place);

            modelBuilder.Entity<ISOLanguageCode>()
                .Property(e => e.ISO_639_3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ISOLanguageCode>()
                .Property(e => e.ISO_639_2)
                .IsUnicode(false);

            modelBuilder.Entity<ISOLanguageCode>()
                .Property(e => e.ISO_639_1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ISOLanguageCode>()
                .Property(e => e.languageName)
                .IsUnicode(false);

            modelBuilder.Entity<TimeZone>()
                .Property(e => e.countryCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TimeZone>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<TimeZone>()
                .HasMany(e => e.Places)
                .WithOptional(e => e.TimeZoneRef)
                .HasForeignKey(e => e.timezone);

            modelBuilder.Entity<AdminHierarchy>()
                .Property(e => e.type)
                .IsUnicode(true);

            modelBuilder.Entity<Country>()
                .Property(e => e.ISO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.ISO3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.FIPS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.country)
                .IsUnicode(true);

            modelBuilder.Entity<Country>()
                .Property(e => e.capital)
                .IsUnicode(true);

            modelBuilder.Entity<Country>()
                .Property(e => e.continent)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.topLevelDomain)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.currencyCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.currencyName)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.phoneCode)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.postalCodeFormat)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.postalCodeRegex)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.languages)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.neighbours)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.equivalentFIPScode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Shape>()
                .Property(e => e.geoJSON)
                .IsUnicode(false);

            modelBuilder.Entity<UserTag>()
                .Property(e => e.tag)
                .IsUnicode(true);
        }
    }
}
