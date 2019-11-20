﻿using Biod.Zebra.Library.EntityModels.Zebra;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Biod.Zebra.Library.Infrastructures;

/// <summary>
/// This model implements case count nesting as defined in https://bluedotglobal.atlassian.net/browse/PT-279, 
/// in order to address duplication when dealing with multiple location types/levels.
/// </summary>
namespace Biod.Zebra.Library.Models
{
    public class EventCaseCountModel
    {
        public DateTime EventDate { get; set; }

        public int GeonameId { get; set; }

        public int? Admin1GeonameId { get; set; }

        public int CountryGeonameId { get; set; }

        public int LocationType { get; set; }

        public int RawCaseCount { get; set; }

        public int ChildrenCaseCount { get; set; }

        public Dictionary<int, EventCaseCountModel> Children { get; set; } = new Dictionary<int, EventCaseCountModel>();

        // <auto-generated>
        //     This code was generated from by Visual Studio
        // </auto-generated>
        public override bool Equals(object obj)
        {
            return obj is EventCaseCountModel model &&
                   EventDate == model.EventDate &&
                   GeonameId == model.GeonameId &&
                   EqualityComparer<int?>.Default.Equals(Admin1GeonameId, model.Admin1GeonameId) &&
                   CountryGeonameId == model.CountryGeonameId &&
                   LocationType == model.LocationType &&
                   RawCaseCount == model.RawCaseCount &&
                   ChildrenCaseCount == model.ChildrenCaseCount;
        }

        // <auto-generated>
        //     This code was generated from by Visual Studio
        // </auto-generated>
        public override int GetHashCode()
        {
            var hashCode = -1724906143;
            hashCode = hashCode * -1521134295 + EventDate.GetHashCode();
            hashCode = hashCode * -1521134295 + GeonameId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(Admin1GeonameId);
            hashCode = hashCode * -1521134295 + CountryGeonameId.GetHashCode();
            hashCode = hashCode * -1521134295 + LocationType.GetHashCode();
            hashCode = hashCode * -1521134295 + RawCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChildrenCaseCount.GetHashCode();
            return hashCode;
        }

        public int GetNestedCaseCount()
        {
            return Math.Max(ChildrenCaseCount, RawCaseCount);
        }

        /// <summary>
        /// Obtains a list of positive delta for nested case counts given an event ID.
        /// For example, given the following raw data for previous and current:
        /// Toronto     4  -->  Toronto     5
        /// Ontario     1  -->  Ontario     3
        /// Canada      1  -->  Canada      1
        ///                -->  Montreal    6
        /// The resulting dictionary will include Toronto (1 case count) and Montreal (6 case counts).
        /// </summary>
        public static Dictionary<int, EventCaseCountModel> GetUpdatedCaseCountModelsForEvent(BiodZebraEntities dbContext, int eventId)
        {
            var currentCaseCount = dbContext.Xtbl_Event_Location
                .Include(e => e.ActiveGeoname)
                .Where(e => e.EventId == eventId)
                .ToDictionary(e => e.GeonameId, e => new EventCaseCountModel
                {
                    RawCaseCount = e.RepCases ?? 0,
                    EventDate = e.EventDate,
                    GeonameId = e.GeonameId,
                    Admin1GeonameId = e.ActiveGeoname.Admin1GeonameId,
                    CountryGeonameId = e.ActiveGeoname.CountryGeonameId ?? Constants.Geoname.UNKNOWN_COUNTRY,
                    LocationType = e.ActiveGeoname.LocationType ?? Constants.LocationType.CITY
                });
            BuildDependencyTree(currentCaseCount);
            ApplyNesting(currentCaseCount);

            var previousCaseCount = dbContext.Xtbl_Event_Location_history
                .Include(e => e.ActiveGeoname)
                .Where(e => e.EventId == eventId)
                .ToDictionary(e => e.GeonameId, e => new EventCaseCountModel
                {
                    RawCaseCount = e.RepCases ?? 0,
                    EventDate = e.EventDate,
                    GeonameId = e.GeonameId,
                    Admin1GeonameId = e.ActiveGeoname.Admin1GeonameId,
                    CountryGeonameId = e.ActiveGeoname.CountryGeonameId ?? Constants.Geoname.UNKNOWN_COUNTRY,
                    LocationType = e.ActiveGeoname.LocationType ?? Constants.LocationType.CITY
                });
            BuildDependencyTree(previousCaseCount);
            ApplyNesting(previousCaseCount);

            return GetIncreasedCaseCount(FlattenTree(previousCaseCount), FlattenTree(currentCaseCount));
        }

        /// <summary>
        /// Build a tree from the raw data to represent direct connections that will be used for nesting.
        /// Note that the models are updated in place.
        /// For example, given the following raw data:
        /// Toronto - 4 cases
        /// Ontario - 1 case
        /// Canada - 1 case
        /// Montreal - 2 cases
        /// United States - 3 cases
        /// The output tree will have the following structure:
        ///        Canada       United States
        ///        /    \
        ///    Ontario Montreal
        ///     /
        /// Toronto    
        /// </summary>
        /// <returns>Restructured model dictionary with only country-level case counts in the dictionary values</returns>
        public static void BuildDependencyTree(Dictionary<int, EventCaseCountModel> rawData)
        {
            // Add each cities as the corresponding province's child, if available
            rawData.Values
                .Where(e => e.LocationType == Constants.LocationType.CITY)
                .ToList()
                .ForEach(e =>
                {
                    if (e.Admin1GeonameId != null && rawData.ContainsKey((int)e.Admin1GeonameId))
                    {
                        rawData[(int)e.Admin1GeonameId].Children.Add(e.GeonameId, e);
                        rawData.Remove(e.GeonameId);
                    }
                });

            // Add each non-country location as the corresponding country's child, if available
            rawData.Values
                .Where(e => e.LocationType != Constants.LocationType.COUNTRY)
                .ToList()
                .ForEach(e =>  
                {
                    if (rawData.ContainsKey(e.CountryGeonameId))
                    {
                        rawData[e.CountryGeonameId].Children.Add(e.GeonameId, e);
                        rawData.Remove(e.GeonameId);
                    }
                });
        }

        /// <summary>
        /// Flattens a dependency tree built with <see cref="BuildDependencyTree(Dictionary{int, EventCaseCountModel})"/>
        /// by applying <see cref="FlattenTree(EventCaseCountModel, Dictionary{int, EventCaseCountModel})"/> to each root node.
        /// For example, given the following tree:
        /// The output tree will have the following structure:
        ///        Canada       United States
        ///        /    \
        ///    Ontario Montreal
        ///     /
        /// Toronto    
        /// This will be flattened to:  Toronto, Montreal, Ontario, Canada, United States
        /// </summary>
        public static Dictionary<int, EventCaseCountModel> FlattenTree(Dictionary<int, EventCaseCountModel> dependencyTree)
        {
            var result = new Dictionary<int, EventCaseCountModel>();
            foreach (EventCaseCountModel node in dependencyTree.Values)
            {
                FlattenTree(node, result);
            }

            return result;
        }

        private static void FlattenTree(EventCaseCountModel node, Dictionary<int, EventCaseCountModel> flattenedTree)
        {
            if (node.Children.Count == 0)
            {
                flattenedTree.Add(node.GeonameId, node);
                return;
            }

            foreach (EventCaseCountModel child in node.Children.Values)
            {
                FlattenTree(child, flattenedTree);
            }
            flattenedTree.Add(node.GeonameId, node);
        }

        /// <summary>
        /// Applies <see cref="ApplyNesting(EventCaseCountModel)"/> for each root node.
        /// This assumes that <paramref name="nodes"/> is a tree built with <see cref="BuildDependencyTree(Dictionary{int, EventCaseCountModel})"/>
        /// </summary>
        public static void ApplyNesting(Dictionary<int, EventCaseCountModel> nodes)
        {
            foreach (EventCaseCountModel node in nodes.Values)
            {
                ApplyNesting(node);
            }
        }

        /// <summary>
        /// Calculates the nested values as defined in 
        /// https://bluedotglobal.atlassian.net/browse/PT-279. This is done in order to address 
        /// duplication when dealing with multiple location types/levels.
        /// Note that the values are updated in place.
        /// For example, given the following raw data:
        /// Toronto - 4 cases
        /// Ontario - 1 case
        /// Canada - 1 case
        /// This should be nested as follows:
        /// Toronto - 4 cases
        /// Ontario - 4 cases
        /// Canada - 4 cases
        /// </summary>
        private static void ApplyNesting(EventCaseCountModel node)
        {
            if (node.Children.Count == 0)
            {
                node.ChildrenCaseCount = 0;
                return;
            }

            foreach (EventCaseCountModel child in node.Children.Values)
            {
                ApplyNesting(child);
            }
            node.ChildrenCaseCount = node.Children.Sum(c => c.Value.GetNestedCaseCount());
        }

        /// <summary>
        /// Calculates the difference between two case count dictionaries. 
        /// This assumes that input dictionaries have nesting applied on them.
        /// As such, if the case-count difference is caused by the sum of its children, 
        /// that case count model is not included in the result.
        /// For example, given the following nested case counts for previous and current:
        /// Toronto     4  -->  Toronto     5
        /// Ontario     4  -->  Ontario     5
        /// Canada      4  -->  Canada      11
        ///                -->  Montreal    6
        /// The resulting list will include Toronto and Montreal.
        /// </summary>
        public static Dictionary<int, EventCaseCountModel> GetIncreasedCaseCount(Dictionary<int, EventCaseCountModel> previous, Dictionary<int, EventCaseCountModel> current)
        {
            return current.Values
                .Where(e => !previous.ContainsKey(e.GeonameId) || (e.RawCaseCount > e.ChildrenCaseCount && e.RawCaseCount > previous[e.GeonameId].RawCaseCount))
                .ToDictionary(e => e.GeonameId, e =>
                    {
                        if (!previous.ContainsKey(e.GeonameId))  // new location
                        {
                            e.RawCaseCount = e.GetNestedCaseCount();
                        }
                        else  // existing location
                        {
                            e.RawCaseCount = e.ChildrenCaseCount == 0 ?
                                e.RawCaseCount - previous[e.GeonameId].RawCaseCount :  // Get difference of raw case counts for leaf nodes
                                e.RawCaseCount - e.ChildrenCaseCount;  // Get difference between nested case count and sum of children nodes' case counts
                        }
                        e.ChildrenCaseCount = 0;

                        return e;
                    }
                );
        }
    }
}