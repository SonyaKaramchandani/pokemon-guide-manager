using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;
using BlueDot.DiseasesAPI;
using BlueDot.DiseasesAPI.Models;


namespace BlueDot.DiseasesAPI.Controllers
{
    /// <summary>
    /// Location Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class LocationController : ApiController
    {
        private const int SEASONALITY_ZONE_MARKER = -2;
        private const int COUNTRY_WATER_QUALITY_MARKER = -3;
        private const int WATER_QUALITY_MARKER = -4;
        private const int URBAN_MARKER = -5;
        private const int SANITATION_MARKER = -6;
        private const int OID_MARKER = -7;
        private const int DEVELOPED_MARKER = -8;


        /// <summary>
        /// Gets the column map.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Could not determine disease columns for the DiseaseConditions_GCS table.</exception>
        private Dictionary<int, int> GetColumnMap(SqlCommand cmd, string tableName)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            //cmd.CommandText = "SELECT COLUMN_NAME, ORDINAL_POSITION, DATA_TYPE FROM DiseasesAPI.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DiseaseConditions_GCS'";
            cmd.CommandText = "SELECT COLUMN_NAME, ORDINAL_POSITION, DATA_TYPE FROM DiseasesAPI.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                throw new Exception("Could not determine disease columns for the DiseaseConditions_GCS table.");
            }
            while (reader.Read())
            {
                string columnName = reader.GetString(0);
                Match dmatch = Regex.Match(columnName, @"D(\d+)_prev");
                if (dmatch.Success && reader.GetString(2) == "numeric")
                {
                    try
                    {
                        result[reader.GetInt32(1) - 1] = Convert.ToInt32(dmatch.Groups[1].Value);
                    }
                    catch (Exception e)
                    {
                        Debug.Assert(false);
                        Trace.TraceWarning("LocationController::GetColumnMap() - Unexpected column type schema for the map.DiseaseConditions_GCS table:  ", e);
                    }
                }
                else if (columnName == "Seasonality_Zone")
                {
                    result[SEASONALITY_ZONE_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "Country_Water_Quality")
                {
                    result[COUNTRY_WATER_QUALITY_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "Water_Quality")
                {
                    result[WATER_QUALITY_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "Urban_GridCode")
                {
                    result[URBAN_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "Sanitation_Level")
                {
                    result[SANITATION_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "OBJECTID")
                {
                    result[OID_MARKER] = reader.GetInt32(1) - 1;
                }
                else if (columnName == "Developed")
                {
                    result[DEVELOPED_MARKER] = reader.GetInt32(1) - 1;
                }
            } // while
            reader.Close();
            return result;
        } // GetColumnMap()


        private const double RISK_MAX = 100.0;

        private static readonly Level[] RISK_LEVELS = { new Level("Minimal", 0),
                                                        new Level("Low", 1),
                                                        new Level("Medium", 2),
                                                        new Level("High", 3) };


        class ContextSpecificMessage
        {
            public int id { get; private set; }
            public int sectionId { get; private set; }
            public string message { get; private set; }
            public string moreInfoUrl { get; private set; }

            public ContextSpecificMessage(int id, int sectionId, string message, string moreInfoUrl = "")
            {
                this.id = id;
                this.sectionId = sectionId;
                this.message = message;
                this.moreInfoUrl = moreInfoUrl;
            }
        } // class LocationController.ContextSpecificMessage


        class DiseaseRisk
        {
            public Level level { get; private set; }
            public double riskValue { get; private set; }

            public DiseaseRisk(Level level, double riskValue)
            {
                this.level = level;
                this.riskValue = riskValue;
            }
        } // class LocationController.DiseaseRisk


        class DiseaseRiskProjection
        {
            public int diseaseRiskId { get; private set; }
            public int diseaseId { get; private set; }
            public string url { get; private set; }
            public bool seasonal { get; private set; }
            public double mediaBuzz { get; private set; }
            public DiseaseRisk defaultRisk { get; private set; }
            public List<ContextSpecificMessage> contextSpecificMessages { get; private set; }

            public DiseaseRiskProjection(int polygonId, int diseaseId, string url, bool seasonal, double mediaBuzz, DiseaseRisk defaultRisk, List<ContextSpecificMessage> contextSpecificMessages)
            {
                this.diseaseRiskId = polygonId + (diseaseId << 24);   // Try to have a unique id for each disease in each "place"
                this.diseaseId = diseaseId;
                this.url = url;
                this.seasonal = seasonal;
                this.mediaBuzz = mediaBuzz;
                this.defaultRisk = defaultRisk;
                this.contextSpecificMessages = contextSpecificMessages;
            }
        } // class LocationController.DiseaseRiskProjection


        class PlaceInfo
        {
            public int polygonId { get; set; }

            private int _seasonalityZone;
            public int seasonalityZone
            {
                get { return _seasonalityZone; }
                set
                {
                    _seasonalityZone = value;
                    Debug.Assert(_seasonalityZone > 0 && _seasonalityZone <= 8);
                }
            }
            private double _waterQualityScore;
            public double waterQualityScore
            {
                get { return _waterQualityScore; }
                set
                {
                    _waterQualityScore = value;
                    Debug.Assert(_waterQualityScore >= 0.0);
                }
            }
            private bool _urban;
            public bool urban
            {
                get { return _urban; }
                set
                {
                    _urban = value;
                }
            }
            private double _developed;
            public double developed
            {
                get { return _developed; }
                set
                {
                    _developed = value;
                    Debug.Assert(_developed >= 0.0 && _developed <= 1.0);
                }
            }

            public Dictionary<int, double> diseasePrevalence { get; private set; }
            public Dictionary<int, List<ContextSpecificMessage>> contextSpecificMessages { get; private set; }


            public string waterQualityLevel()
            {
                if (this._waterQualityScore == 1.0)
                    return "Safe";
                else if (this._waterQualityScore >= 0.5)
                    return "Unreliable";
                return "Unsafe";
            } // waterQualityLevel()


            public PlaceInfo()
            {
                this.polygonId = -1;
                _seasonalityZone = 0;
                _waterQualityScore = 0.0;
                _urban = false;
                this.diseasePrevalence = new Dictionary<int, double>();
                this.contextSpecificMessages = new Dictionary<int, List<ContextSpecificMessage>>();
            }


            public void addContextSpecificMessageForDisease(int diseaseId, ContextSpecificMessage csm)
            {
                if (!this.contextSpecificMessages.ContainsKey(diseaseId))
                    this.contextSpecificMessages[diseaseId] = new List<ContextSpecificMessage>();
                this.contextSpecificMessages[diseaseId].Add(csm);
            } // addContextSpecificMessageForDisease()

            public List<ContextSpecificMessage> contextSpecificMessagesForDisease(int diseaseId)
            {
                if (!this.contextSpecificMessages.ContainsKey(diseaseId))
                    this.contextSpecificMessages[diseaseId] = new List<ContextSpecificMessage>();
                return contextSpecificMessages[diseaseId];
            } // contextSpecificMessagesForDisease()


            public override string ToString()
            {
                string result = "{ polygonId=" + this.polygonId.ToString() + ", seasonalityZone=" + this.seasonalityZone.ToString() + ", waterQualityScore=" + this.waterQualityScore.ToString() + ", urban=" + this.urban.ToString() + ", #CSMs=" + this.contextSpecificMessages.Count.ToString();
                result += ", Prevalences = {";
                foreach (KeyValuePair<int, double> kvp in this.diseasePrevalence)
                {
                    result += (" " + kvp.Key.ToString() + "=" + kvp.Value.ToString());
                }
                result += " } }";
                return result;
            } // PlaceInfo.ToString()
        } // class LocationController.PlaceInfo


        double GetMediaBuzzForDiseaseInPlace(Disease d, double latitude, double longitude)
        {
            // TODO:  calculate from GPHIN and/or HealthMap feed(s)
            if (d.diseaseId == 88)
                return 1.0;         // Hardcode for Zika for now...
            return 0.0;
        } // GetMediaBuzzForDiseaseInPlace()

        double GetMediaBuzzForDiseaseInPlace(Disease d, int geonameId)
        {
            // TODO:  calculate from GPHIN and/or HealthMap feed(s)
            if (d.diseaseId == 88)
                return 1.0;         // Hardcode for Zika for now...
            return 0.0;
        } // GetMediaBuzzForDiseaseInPlace()

        // TODO:  also query for contextSpecificMessages for this place here
        private PlaceInfo queryForPlace(SqlCommand cmd, Dictionary<int, int> columnMap, double latitude, double longitude)
        {
            const int numPrevalenceLevels = 10;
            const double radius = 50000.0;
            const double bandWidth = radius / numPrevalenceLevels;

            PlaceInfo result = new PlaceInfo();

            cmd.CommandText = "EXEC bd.diseaseConditionsAroundPoint_sp @latitude = " + latitude.ToString() + ", @longitude = " + longitude.ToString() + ", @radius = " + radius.ToString();
            SqlDataReader reader = cmd.ExecuteReader();

            bool forceHit = false;
            bool setConds = false;
            while (reader.Read())
            {
                result.polygonId = reader.GetInt32(columnMap[OID_MARKER]);

                Debug.Assert(!columnMap.ContainsKey(reader.FieldCount - 1), "LocationController::queryForPlace() - Distance column also mapped as a disease!");
                double metersToPolygon = 0;
                try
                {
                    metersToPolygon = reader.GetDouble(reader.FieldCount - 1);    // Last column should be Distance...
                    Debug.Assert(metersToPolygon >= 0.0);
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }

                // TAI:  Use gradient?
                if (metersToPolygon == 0 || (!setConds && metersToPolygon < 1000))
                {
                    // For each of these, don't want to abort the query if they're missing...
                    try { result.seasonalityZone = reader.GetInt16(columnMap[SEASONALITY_ZONE_MARKER]); } catch (Exception) { Debug.Assert(false); }
                    try { result.urban = (reader.GetInt16(columnMap[URBAN_MARKER]) != 0); } catch (Exception) { Debug.Assert(false); }
                    try { result.developed = (double)reader.GetDecimal(columnMap[DEVELOPED_MARKER]); } catch (Exception) { Debug.Assert(false); }
                    try
                    {
                        result.waterQualityScore = (double)reader.GetInt16(columnMap[COUNTRY_WATER_QUALITY_MARKER]);
                        if (0.0 == result.waterQualityScore)
                            result.waterQualityScore = (double)reader.GetDecimal(columnMap[WATER_QUALITY_MARKER]);   // Use city-level if not in a safe country
                    }
                    catch (Exception)
                    {
                        Debug.Assert(false);
                    }
                    setConds = true;
                }

                bool hitOne = false;
                foreach (KeyValuePair<int, int> kvp in columnMap)
                {
                    // TAI:  just use > 0?
                    if (kvp.Key == SEASONALITY_ZONE_MARKER || kvp.Key == COUNTRY_WATER_QUALITY_MARKER || kvp.Key == WATER_QUALITY_MARKER || kvp.Key == URBAN_MARKER || kvp.Key == SANITATION_MARKER || kvp.Key == OID_MARKER || kvp.Key == DEVELOPED_MARKER)
                        continue;

                    double prevalence = 0.0;
                    try
                    {
                        prevalence = (double)reader.GetDecimal(kvp.Key);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError("LocationController::queryForPlace() - Caught exception on column " + kvp.Key.ToString() + ":  ", e);
                        Debug.Assert(false);
                        continue;
                    }

                    if (prevalence <= 0.0)
                        continue;

                    hitOne = true;

                    if (forceHit)
                        metersToPolygon = 0.0;

                    int diseaseId = kvp.Value;

                    if (84 == diseaseId)
                        diseaseId = 83;        // HACK:  Combine the 2 malarias into 1.

                    if (metersToPolygon > 0.0)
                    {
                        if (diseaseId == 45)   // HACK:  Don't allow Traveller's Diarrhea (DBID=45) to cross borders.
                            continue;
                        if (prevalence == 1.0 && metersToPolygon < bandWidth)
                            prevalence = 0.5;
                        else
                        {
                            prevalence -= (1 + (int)(metersToPolygon / bandWidth));
                            prevalence = Math.Max(0.0, prevalence);
                        }
                    }

                    double prev;
                    if (result.diseasePrevalence.TryGetValue(diseaseId, out prev))
                        result.diseasePrevalence[diseaseId] = Math.Max(prevalence, prev);
                    else
                        result.diseasePrevalence[diseaseId] = prevalence;
                } // foreach

                forceHit = (!hitOne && metersToPolygon == 0.0);
            } // while

            reader.Close();

            return result;
        } // queryForPlace()

        // TODO:  also query for contextSpecificMessages for this place here
        private List<PlaceInfo> queryForPlace(SqlCommand cmd, Dictionary<int, int> columnMap, int geonameId)
        {
            const int numPrevalenceLevels = 10;
            const double radius = 50000.0;
            const double bandWidth = radius / numPrevalenceLevels;

            List<PlaceInfo> results = new List<PlaceInfo>();
            PlaceInfo result = new PlaceInfo();

            cmd.CommandText = "EXEC bd.diseaseConditionsByGeonameId_sp @GeonameId = " + geonameId.ToString();
            SqlDataReader reader = cmd.ExecuteReader();

            bool forceHit = false;
            bool setConds = false;
            while (reader.Read())
            {
                result.polygonId = reader.GetInt32(columnMap[OID_MARKER]); // commented it out because OID_MARKER is not available in DiseaseConditionsMaxValues

                Debug.Assert(!columnMap.ContainsKey(reader.FieldCount - 1), "LocationController::queryForPlace() - Distance column also mapped as a disease!");
                double metersToPolygon = 0;
                try
                {
                    metersToPolygon = reader.GetDouble(reader.FieldCount - 1);    // Last column should be Distance...
                    Debug.Assert(metersToPolygon >= 0.0);
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }

                // TAI:  Use gradient?
                if (metersToPolygon == 0 || (!setConds && metersToPolygon < 1000))
                {
                    // For each of these, don't want to abort the query if they're missing...
                    try { result.seasonalityZone = reader.GetInt16(columnMap[SEASONALITY_ZONE_MARKER]); } catch (Exception) { Debug.Assert(false); }
                    try { result.urban = (reader.GetInt16(columnMap[URBAN_MARKER]) != 0); } catch (Exception e) { var a = e.InnerException; Debug.Assert(false); }
                    try { result.developed = (double)reader.GetDecimal(columnMap[DEVELOPED_MARKER]); } catch (Exception) { Debug.Assert(false); }
                    try
                    {
                        result.waterQualityScore = (double)reader.GetInt16(columnMap[COUNTRY_WATER_QUALITY_MARKER]);
                        if (0.0 == result.waterQualityScore)
                            result.waterQualityScore = (double)reader.GetDecimal(columnMap[WATER_QUALITY_MARKER]);   // Use city-level if not in a safe country
                    }
                    catch (Exception)
                    {
                        Debug.Assert(false);
                    }
                    setConds = true;
                }

                bool hitOne = false;
                foreach (KeyValuePair<int, int> kvp in columnMap)
                {
                    // TAI:  just use > 0?
                    if (kvp.Key == SEASONALITY_ZONE_MARKER || kvp.Key == COUNTRY_WATER_QUALITY_MARKER || kvp.Key == WATER_QUALITY_MARKER || kvp.Key == URBAN_MARKER || kvp.Key == SANITATION_MARKER || kvp.Key == OID_MARKER || kvp.Key == DEVELOPED_MARKER)
                        continue;

                    double prevalence = 0.0;
                    try
                    {
                        prevalence = (double)reader.GetDecimal(kvp.Key);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError("LocationController::queryForPlace() - Caught exception on column " + kvp.Key.ToString() + ":  ", e);
                        Debug.Assert(false);
                        continue;
                    }

                    if (prevalence <= 0.0)
                        continue;

                    hitOne = true;

                    if (forceHit)
                        metersToPolygon = 0.0;

                    int diseaseId = kvp.Value;

                    if (84 == diseaseId)
                        diseaseId = 83;        // HACK:  Combine the 2 malarias into 1.

                    if (metersToPolygon > 0.0)
                    {
                        if (diseaseId == 45)   // HACK:  Don't allow Traveller's Diarrhea (DBID=45) to cross borders.
                            continue;
                        if (prevalence == 1.0 && metersToPolygon < bandWidth)
                            prevalence = 0.5;
                        else
                        {
                            prevalence -= (1 + (int)(metersToPolygon / bandWidth));
                            prevalence = Math.Max(0.0, prevalence);
                        }
                    }

                    double prev;
                    if (result.diseasePrevalence.TryGetValue(diseaseId, out prev))
                        result.diseasePrevalence[diseaseId] = Math.Max(prevalence, prev);
                    else
                        result.diseasePrevalence[diseaseId] = prevalence;
                } // foreach

                forceHit = (!hitOne && metersToPolygon == 0.0);
                results.Add(result);
            } // while

            reader.Close();

            return results;
        } // queryForPlace()

        // Note: for testing, Toronto is at lat=43.7, long=-79.4
        /// <summary>
        /// Get the disease risks for an array of places given their latitudes and longitudes.
        /// </summary>
        /// <param name="latitude">One or more latitudes between -90 and 90 degrees.</param>
        /// <param name="longitude">One or more longitudes between -360 and 360 degrees.</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        /// <returns></returns>
        [Route("location/risks")]
        public IHttpActionResult GetLocationArrayRisks([FromUri] double[] latitude, [FromUri] double[] longitude, string context = "web")
        {
            if (latitude.Length != longitude.Length)
                return BadRequest("Number of latitude and longitude parameters must match.");
            if (latitude.Length > 24)
                return BadRequest("Too many locations specified.");
            for (int l = 0; l < latitude.Length; ++l)
            {
                if (Math.Abs(latitude[l]) > 90 || Math.Abs(longitude[l]) > 360)
                    return BadRequest("Invalid latitude and/or longitude parameters.");
            } // for

            List<DiseaseRiskProjection> diseaseRisks = new List<DiseaseRiskProjection>();
            Dictionary<int, int> columnMap;
            try
            {
                using (var db = new DiseasesModel())
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;

                        // we need to figure out how each column maps to a disease since there may be missing disease fields... 
                        columnMap = GetColumnMap(cmd, "DiseaseConditions_GCS");
                        if (columnMap.Count == 0)
                        {
                            Trace.TraceError("LocationController::GetLocationArrayRisks() - No disease fields found in map.");
                            Debug.Assert(false);
                            return NotFound();
                        }
                        Debug.Assert(columnMap.ContainsKey(SEASONALITY_ZONE_MARKER), "LocationController::GetLocationArrayRisks() - No seasonality zones in map.");
                        Debug.Assert(columnMap.ContainsKey(COUNTRY_WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No country water quality found in map.");
                        Debug.Assert(columnMap.ContainsKey(WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No municiple water quality found in map.");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
                return InternalServerError();
            }
            try
            {
                using (var db = new DiseasesModel(latitude.Length > 1))
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        List<object> locationsArray = new List<object>();
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;

                        var diseases = db.Diseases.Include(d => d.DiseaseTransmissions).ToArray();

                        for (int l = 0; l < latitude.Length; ++l)
                        {
                            PlaceInfo placeInfo = queryForPlace(cmd, columnMap, latitude[l], longitude[l]);
                            Debug.Print("LocationController::GetLocationArrayRisks() - placeInfo = " + placeInfo.ToString());
                            var diseasePrevalenceLength = placeInfo.diseasePrevalence.Count;
                            for (var i = 0; i < diseasePrevalenceLength; i++)
                            {
                                KeyValuePair<int, double> kvp = placeInfo.diseasePrevalence.ElementAt(i);
                                int diseaseId = kvp.Key;
                                Disease d = diseases.SingleOrDefault(ds => ds.diseaseId == diseaseId);
                                if (d == null)
                                {
                                    continue;
                                }
                                if (context.StartsWith("global.bluedot.George.") && !d.prevalenceVetted)
                                    continue;
                                if (!d.DecodePresence(placeInfo.urban, placeInfo.developed))
                                    continue;
                                double txMultiplier = d.DiseaseTransmissions.OrderBy(t => t.rank).First().TransmissionMode.multiplier;
                                double risk = d.modelWeight * txMultiplier * kvp.Value * d.SeasonalRisk(placeInfo.seasonalityZone);
                                if (risk < 0.5)  // Round down to 0 since there's no way to increase risk (no activity modifiers right now)...
                                    continue;

                                DiseaseRisk defaultRisk = new DiseaseRisk(Level.ToLevel(risk, RISK_MAX, RISK_LEVELS), risk);

                                double mediaBuzz = GetMediaBuzzForDiseaseInPlace(d, latitude[l], longitude[l]);

                                diseaseRisks.Add(new DiseaseRiskProjection(placeInfo.polygonId,
                                                                           diseaseId,
                                                                           Url.Link("GetDiseasesByIds", new { id = diseaseId }),
                                                                           d.IsSeasonal(placeInfo.seasonalityZone),
                                                                           mediaBuzz,
                                                                           defaultRisk,
                                                                           placeInfo.contextSpecificMessagesForDisease(diseaseId)));

                            } // foreach

                            locationsArray.Add(new
                            {
                                locationId = placeInfo.polygonId,
                                latitude = latitude[l],
                                longitude = longitude[l],
                                diseaseRisks = diseaseRisks,
                                waterQuality = placeInfo.waterQualityLevel()
                            });
                        } // for l

                        if (locationsArray.Count >= 1)
                            return Ok(new { locations = locationsArray, cacheTag = Globals.GetCacheTag(db) });
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
            }

            return NotFound();
        } // GetLocationArrayRisks()

        // Note: for testing, Toronto is at lat=43.7, long=-79.4
        /// <summary>
        /// Get the disease risks for an array of places given their latitudes and longitudes and the tier of disease.
        /// </summary>
        /// <param name="latitude">One or more latitudes between -90 and 90 degrees.</param>
        /// <param name="longitude">One or more longitudes between -360 and 360 degrees.</param>
        /// <param name="tier">Disease tier: 1-High, 2-Intermediate, 3-Low</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        /// <returns></returns>
        [Route("location/riskswithtier")]
        public IHttpActionResult GetLocationArrayRisksByTier([FromUri] double[] latitude, [FromUri] double[] longitude, [FromUri] int tier, string context = "web")
        {
            if (latitude.Length != longitude.Length)
                return BadRequest("Number of latitude and longitude parameters must match.");
            if (latitude.Length > 24)
                return BadRequest("Too many locations specified.");
            for (int l = 0; l < latitude.Length; ++l)
            {
                if (Math.Abs(latitude[l]) > 90 || Math.Abs(longitude[l]) > 360)
                    return BadRequest("Invalid latitude and/or longitude parameters.");
            } // for

            List<DiseaseRiskProjection> diseaseRisks = new List<DiseaseRiskProjection>();
            Dictionary<int, int> columnMap;
            try
            {
                using (var db = new DiseasesModel())
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;

                        // we need to figure out how each column maps to a disease since there may be missing disease fields... 
                        columnMap = GetColumnMap(cmd, "DiseaseConditions_GCS");
                        if (columnMap.Count == 0)
                        {
                            Trace.TraceError("LocationController::GetLocationArrayRisks() - No disease fields found in map.");
                            Debug.Assert(false);
                            return NotFound();
                        }
                        Debug.Assert(columnMap.ContainsKey(SEASONALITY_ZONE_MARKER), "LocationController::GetLocationArrayRisks() - No seasonality zones in map.");
                        Debug.Assert(columnMap.ContainsKey(COUNTRY_WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No country water quality found in map.");
                        Debug.Assert(columnMap.ContainsKey(WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No municiple water quality found in map.");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
                return InternalServerError();
            }
            try
            {
                using (var db = new DiseasesModel(latitude.Length > 1))
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        List<object> locationsArray = new List<object>();
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        //filter out diseaseTier=tier diseases
                        List<int> diseaseIds = new List<int>();
                        cmd.CommandText = "Select DiseaseId From bd.DiseaseTiers Where TierId=" + tier.ToString();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                diseaseIds.Add(reader.GetInt32(0));
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }

                        for (int l = 0; l < latitude.Length; ++l)
                        {
                            PlaceInfo placeInfo = queryForPlace(cmd, columnMap, latitude[l], longitude[l]);
                            Debug.Print("LocationController::GetLocationArrayRisks() - placeInfo = " + placeInfo.ToString());
                            foreach (KeyValuePair<int, double> kvp in placeInfo.diseasePrevalence)
                            {
                                int diseaseId = kvp.Key;
                                //filter out tier
                                if (!diseaseIds.Contains(diseaseId))
                                    continue;
                                Disease d = db.Diseases.Find(diseaseId);
                                if (context.StartsWith("global.bluedot.George.") && !d.prevalenceVetted)
                                    continue;
                                double txMultiplier = d.DiseaseTransmissions.ToList().OrderBy(t => t.rank).First().TransmissionMode.multiplier;
                                double risk = d.modelWeight * txMultiplier * kvp.Value * d.SeasonalRisk(placeInfo.seasonalityZone);
                                if (risk < 0.5)  // Round down to 0 since there's no way to increase risk (no activity modifiers right now)...
                                    continue;
                                if (!d.DecodePresence(placeInfo.urban, placeInfo.developed))
                                    continue;

                                DiseaseRisk defaultRisk = new DiseaseRisk(Level.ToLevel(risk, RISK_MAX, RISK_LEVELS), risk);

                                double mediaBuzz = GetMediaBuzzForDiseaseInPlace(d, latitude[l], longitude[l]);

                                diseaseRisks.Add(new DiseaseRiskProjection(placeInfo.polygonId,
                                                                           diseaseId,
                                                                           //Url.Link("GetDiseasesByIds", new { id = diseaseId }),
                                                                           "",
                                                                           d.IsSeasonal(placeInfo.seasonalityZone),
                                                                           mediaBuzz,
                                                                           defaultRisk,
                                                                           placeInfo.contextSpecificMessagesForDisease(diseaseId)));

                            } // foreach

                            locationsArray.Add(new
                            {
                                locationId = placeInfo.polygonId,
                                latitude = latitude[l],
                                longitude = longitude[l],
                                diseaseRisks = diseaseRisks,
                                waterQuality = placeInfo.waterQualityLevel()
                            });
                        } // for l

                        if (locationsArray.Count >= 1)
                            return Ok(new { locations = locationsArray, cacheTag = Globals.GetCacheTag(db) });
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
            }

            return NotFound();
        } // GetLocationArrayRisksByTier()

        // Note: for testing, Ontario is at GeonameId = 6093943
        /// <summary>
        /// Get the disease risks for an array of places given their latitudes and longitudes.
        /// </summary>
        /// <param name="geonameId">Location Geoname identifier</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        /// <returns></returns>
        [Route("location/risksbygeonameId")]
        public IHttpActionResult GetLocationArrayRisksByGeonameId([FromUri] int[] geonameId, string context = "web")
        {
            if (geonameId.Length > 24)
                return BadRequest("Too many locations specified.");

            Dictionary<int, int> columnMap;
            try
            {
                using (var db = new DiseasesModel())
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;

                        // we need to figure out how each column maps to a disease since there may be missing disease fields... 
                        columnMap = GetColumnMap(cmd, "DiseaseConditionsMaxValues");
                        if (columnMap.Count == 0)
                        {
                            Trace.TraceError("LocationController::GetLocationArrayRisks() - No disease fields found in map.");
                            Debug.Assert(false);
                            return NotFound();
                        }
                        Debug.Assert(columnMap.ContainsKey(SEASONALITY_ZONE_MARKER), "LocationController::GetLocationArrayRisks() - No seasonality zones in map.");
                        Debug.Assert(columnMap.ContainsKey(COUNTRY_WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No country water quality found in map.");
                        Debug.Assert(columnMap.ContainsKey(WATER_QUALITY_MARKER), "LocationController::GetLocationArrayRisks() - No municiple water quality found in map.");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
                return InternalServerError();
            }
            try
            {
                using (var db = new DiseasesModel(geonameId.Length > 1))
                {
                    // Not using EntityFramework because not all of the disease fields have been added yet (i.e., the table schema isn't yet fixed).
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        List<DiseaseRiskProjection> diseaseRisks = new List<DiseaseRiskProjection>();
                        List<List<DiseaseRiskProjection>> diseaseRisksTempList = new List<List<DiseaseRiskProjection>>();
                        List<object> locationsArray = new List<object>();
                        connection.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;

                        var diseases = db.Diseases.Include(d => d.DiseaseTransmissions).ToArray();

                        for (int l = 0; l < geonameId.Length; ++l)
                        {
                            List<PlaceInfo> placeInfos = queryForPlace(cmd, columnMap, geonameId[l]);

                            foreach (PlaceInfo placeInfo in placeInfos)
                            {
                                diseaseRisks = new List<DiseaseRiskProjection>();
                                Debug.Print("LocationController::GetLocationArrayRisks() - placeInfo = " + placeInfo.ToString());

                                var diseasePrevalenceLength = placeInfo.diseasePrevalence.Count;
                                for (var i = 0; i < diseasePrevalenceLength; i++)
                                {
                                    KeyValuePair<int, double> kvp = placeInfo.diseasePrevalence.ElementAt(i);
                                    int diseaseId = kvp.Key;
                                    Disease d = diseases.SingleOrDefault(ds => ds.diseaseId == diseaseId);
                                    if (d == null)
                                    {
                                        continue;
                                    }
                                    if (context.StartsWith("global.bluedot.George.") && !d.prevalenceVetted)
                                        continue;
                                    if (!d.DecodePresence(placeInfo.urban, placeInfo.developed))
                                        continue;
                                    double txMultiplier = d.DiseaseTransmissions.OrderBy(t => t.rank).First().TransmissionMode.multiplier;
                                    double risk = d.modelWeight * txMultiplier * kvp.Value * d.SeasonalRisk(placeInfo.seasonalityZone);
                                    if (risk < 0.5)  // Round down to 0 since there's no way to increase risk (no activity modifiers right now)...
                                        continue;

                                    //find the max risk value if diseaseConditionsByGeonameId_sp returns multiple zones 
                                    if (diseaseRisksTempList.Any())
                                    {
                                        foreach (var row in diseaseRisksTempList)
                                        {
                                            var riskValue = row.Where(r => r.diseaseId == diseaseId).Select(x => x.defaultRisk.riskValue).SingleOrDefault();
                                            if (risk < riskValue)
                                            {
                                                risk = riskValue;
                                            }
                                        }
                                    }

                                    DiseaseRisk defaultRisk = new DiseaseRisk(Level.ToLevel(risk, RISK_MAX, RISK_LEVELS), risk);

                                    double mediaBuzz = GetMediaBuzzForDiseaseInPlace(d, geonameId[l]);

                                    diseaseRisks.Add(new DiseaseRiskProjection(placeInfo.polygonId,
                                                                               diseaseId,
                                                                               Url.Link("GetDiseasesByIds", new { id = diseaseId }),
                                                                               d.IsSeasonal(placeInfo.seasonalityZone),
                                                                               mediaBuzz,
                                                                               defaultRisk,
                                                                               placeInfo.contextSpecificMessagesForDisease(diseaseId)));

                                } // foreach
                                diseaseRisksTempList.Add(diseaseRisks);
                            }

                            locationsArray.Add(new
                            {
                                locationId = placeInfos.Select(x => x.polygonId).Max(),
                                //latitude = latitude[l],
                                //longitude = longitude[l],
                                diseaseRisks = diseaseRisks,
                                //waterQuality = placeInfo.waterQualityLevel()
                            });
                        } // for l

                        if (locationsArray.Count >= 1)
                            return Ok(new { locations = locationsArray, cacheTag = Globals.GetCacheTag(db) });
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("LocationController::GetLocationArrayRisks() - Caught unexpected exception:  ", e);
            }

            return NotFound();
        } // GetLocationArrayRisks()


        /*
         * Landscan (not projected): 43200x21600, 120 datapoints per degree.
         * Projection used was World-Gall-Sterographic (WGS) projection (WKID 54016).
         * @4000dpi, projected image was:  39979 x 30717 
         *
         * Cropped into RxC clips.
         * Device rectangles:
         *    iPhone <:         320 x  480, 163ppi
         *    iPhone 4:         640 x  960, 326ppi
         *    iPhone 5, 6zoom:  640 x 1136, 326ppi
         *    iPhone 6:         750 x 1334, 326ppi
         *    iPhone 6 Plus Z: 1125 x 2001, 401ppi
         *    iPhone 6 Plus:   1242 x 2208, 401ppi
         *    iPhone 7:        2034 x 3072, ???ppi
         *    BUT:  72 dpi?
         *    Android:
         *       ldpi:     ~120dpi
         *       mdpi:     ~160dpi
         *       hdpi:     ~240dpi
         *       xhdpi:    ~320dpi
         *       xxhdpi:   ~480dpi
         *       xxxhdpi:  ~640dpi
         */
        /// <summary>
        /// Get a map image for a specified location.
        /// </summary>
        /// <param name="latitude">Reference point latitude between -90 and 90 degrees.</param>
        /// <param name="longitude">Reference point longitude between -360 and 360 degrees.</param>
        /// <param name="pixelWidth">Width in pixels of the image.</param>
        /// <param name="pixelHeight">Height in pixels of the image.</param>
        /// <param name="percentFromTop">How far down the reference point should be in the image relative to the pixelHeight.  Default value .5.</param>
        /// <param name="percentFromLeft">How far from the left the reference point should be in the image relative to the pixelWidth.  Default value .5.</param>
        /// <param name="transparent">Optional bool to specify whether images will be transparent or black and white.  Default value is true.</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        /// <returns>
        /// Image content with type "image/png".
        /// </returns>
        [Route("location/image")]
        public IHttpActionResult GetLocationImage([FromUri] double latitude, [FromUri] double longitude,
                                                  [FromUri] int pixelWidth, [FromUri] int pixelHeight,
                                                  [FromUri] double percentFromTop = .5, [FromUri] double percentFromLeft = .5,
                                                  [FromUri] bool transparent = true,
                                                  [FromUri] string context = "web")
        {
            if (Math.Abs(latitude) > 90 || Math.Abs(longitude) > 360)
                return BadRequest("Invalid latitude and/or longitude parameter.");
            if (pixelWidth <= 0 || pixelHeight <= 0)
                return BadRequest("Requested dimensions must be positive.");
            if (percentFromTop < 0.0 || percentFromLeft < 0.0 || percentFromTop > 1.0 || percentFromLeft > 1.0)
                return BadRequest("percentFrom* parameters must be between 0 and 1.");

            const int MAP_WIDTH = 39979;
            const int MAP_HEIGHT = 30717;
            const int NUM_TILE_ROWS = 5;
            const int NUM_TILE_COLS = 5;
            // Note:  if these are not ints, tiles will differ by at most 1 pixel...
            int tileWidth = Convert.ToInt32((float)MAP_WIDTH / NUM_TILE_ROWS);
            int tileHeight = Convert.ToInt32((float)MAP_HEIGHT / NUM_TILE_COLS);

            if (pixelWidth > tileWidth || pixelHeight > tileHeight)
                return BadRequest("Requested dimensions too large.");

            Rectangle resultClipRect = new Rectangle(0, 0, pixelWidth, pixelHeight);
            Bitmap target = new Bitmap(resultClipRect.Width, resultClipRect.Height/*, PixelFormat.Format24bppRgb*/);

            // Figure out where latitude and longitude fall in the projected source "image space".
            double xp = MAP_WIDTH * (longitude + 180.0) / 360.0;
            double yp = MAP_HEIGHT * 0.5 * (Math.Tan(-latitude * Math.PI / 360.0) + 1.0);           // down from the top

            // Figure out origin of rectangle around that with the lat/long at the right spot within it.
            int xo = ((int)(xp - percentFromLeft * pixelWidth) + MAP_WIDTH) % MAP_WIDTH;
            int yo = ((int)(yp - percentFromTop * pixelHeight) + MAP_HEIGHT) % MAP_HEIGHT;

            List<int> tileRows = new List<int>();
            tileRows.Add(yo / tileHeight);
            int brRow = ((yo + pixelHeight) / tileHeight) % NUM_TILE_ROWS;
            if (brRow != tileRows.First())
                tileRows.Add(brRow);

            List<int> tileCols = new List<int>();
            tileCols.Add(xo / tileWidth);
            int brCol = ((xo + pixelWidth) / tileWidth) % NUM_TILE_COLS;
            if (brCol != tileCols.First())
                tileCols.Add(brCol);

            using (Graphics g = Graphics.FromImage(target))
            {
                int heightToDo = pixelHeight;
                int destTop = 0;
                string basePath = @"Images\" + (transparent ? "transparent" : "BW") + @"\landscan_tile-r";
                foreach (int tileRow in tileRows)
                {
                    int widthToDo = pixelWidth;
                    int destLeft = 0;
                    int srcTop = (tileRow == tileRows[0] ? (yo - tileHeight * tileRows[0]) : 0);
                    int srcHeight = Math.Min(heightToDo, tileHeight - srcTop);

                    foreach (int tileCol in tileCols)
                    {
                        // TAI:  Consider cacheing 4 most frequently used tiles...
                        string sourceTilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, basePath + tileRow.ToString() + "-c" + tileCol.ToString() + ".png");
                        Bitmap sourceTile = Image.FromFile(sourceTilePath, true) as Bitmap;

                        int srcLeft = (tileCol == tileCols[0] ? (xo - tileWidth * tileCols[0]) : 0);
                        int srcWidth = Math.Min(widthToDo, sourceTile.Width - srcLeft);
                        Debug.Assert(Math.Abs(sourceTile.Width - tileWidth) <= 1);

                        srcHeight = Math.Min(heightToDo, sourceTile.Height - srcTop);
                        Debug.Assert(Math.Abs(sourceTile.Height - tileHeight) <= 1);

                        Rectangle srcRect = new Rectangle(srcLeft, srcTop, srcWidth, srcHeight);
                        Rectangle destRect = new Rectangle(destLeft, destTop, srcWidth, srcHeight);

                        g.DrawImage(sourceTile, destRect, srcRect, GraphicsUnit.Pixel);

                        sourceTile.Dispose();

                        destLeft += srcWidth;
                        widthToDo -= srcWidth;
                    } // foreach tileCol
                    destTop += srcHeight;
                    heightToDo -= srcHeight;
                } // foreach tileRow
            }

            MemoryStream writeStream = new MemoryStream();
            target.Save(writeStream, ImageFormat.Png);

            // Content-Type:  application/octet-stream 
            // Content-Transfer-Encoding: base64
            //return Ok(new { mimeType = "image/png", base64_data = Convert.ToBase64String(writeStream.ToArray()) });

            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(writeStream.ToArray())
            };
            responseMsg.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            responseMsg.Content.Headers.ContentLength = writeStream.Length;
            //responseMsg.ReasonPhrase = "SUCCESS";
            //responseMsg.Content = new StringContent("SUCCESS");
            return ResponseMessage(responseMsg);
        } // GetLocationImage()

    } // class LocationController

} // namespace Bluedot.DiseaseAPI.Controllers
