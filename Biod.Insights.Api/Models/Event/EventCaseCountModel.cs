using System;
using System.Collections.Generic;
using System.Linq;

namespace Biod.Insights.Api.Models.Event
{
    /// <summary>
    /// Model used in computing Nesting calculations as defined in https://bluedotglobal.atlassian.net/browse/PT-279
    /// in order to address duplication when dealing with multiple location types/levels.
    ///
    /// <see cref="Biod.Insights.Api.Service.CaseCountService"/>
    /// </summary>
    public class EventCaseCountModel
    {
        public DateTime EventDate { get; set; }

        public int GeonameId { get; set; }

        public int? Admin1GeonameId { get; set; }

        public int CountryGeonameId { get; set; }

        public int LocationType { get; set; }

        // Fields relating to case/death counts
        public int RawRepCaseCount { get; set; }

        public int ChildrenRepCaseCount { get; set; }

        public bool HasRepCaseNestingApplied { get; set; }

        public int RawConfCaseCount { get; set; }

        public int ChildrenConfCaseCount { get; set; }

        public bool HasConfCaseNestingApplied { get; set; }
        
        public int RawSuspCaseCount { get; set; }

        public int ChildrenSuspCaseCount { get; set; }

        public bool HasSuspCaseNestingApplied { get; set; }

        public int RawDeathCount { get; set; }

        public int ChildrenDeathCount { get; set; }

        public bool HasDeathNestingApplied { get; set; }

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
                   RawRepCaseCount == model.RawRepCaseCount &&
                   ChildrenRepCaseCount == model.ChildrenRepCaseCount &&
                   RawConfCaseCount == model.RawConfCaseCount &&
                   ChildrenConfCaseCount == model.ChildrenConfCaseCount &&
                   RawSuspCaseCount == model.RawSuspCaseCount &&
                   ChildrenSuspCaseCount == model.ChildrenSuspCaseCount &&
                   RawDeathCount == model.RawDeathCount &&
                   ChildrenDeathCount == model.ChildrenDeathCount;
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
            hashCode = hashCode * -1521134295 + RawRepCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChildrenRepCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + RawConfCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChildrenConfCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + RawSuspCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChildrenSuspCaseCount.GetHashCode();
            hashCode = hashCode * -1521134295 + RawDeathCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChildrenDeathCount.GetHashCode();
            return hashCode;
        }

        public int GetNestedRepCaseCount()
        {
            var larger = Math.Max(ChildrenRepCaseCount, RawRepCaseCount);
            HasRepCaseNestingApplied = larger != RawRepCaseCount;
            return larger;
        }

        public int GetNestedConfCaseCount()
        {
            var larger = Math.Max(ChildrenConfCaseCount, RawConfCaseCount);
            HasConfCaseNestingApplied = larger != RawConfCaseCount;
            return larger;
        }

        public int GetNestedSuspCaseCount()
        {
            var larger = Math.Max(ChildrenSuspCaseCount, RawSuspCaseCount);
            HasSuspCaseNestingApplied = larger != RawSuspCaseCount;
            return larger;
        }

        public int GetNestedDeathCount()
        {
            var larger = Math.Max(ChildrenDeathCount, RawDeathCount);
            HasDeathNestingApplied = larger != RawDeathCount;
            return larger;
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
                .Where(e => e.LocationType == (int) Constants.LocationType.City)
                .ToList()
                .ForEach(e =>
                {
                    if (e.Admin1GeonameId != null && rawData.ContainsKey((int) e.Admin1GeonameId))
                    {
                        rawData[(int) e.Admin1GeonameId].Children.Add(e.GeonameId, e);
                        rawData.Remove(e.GeonameId);
                    }
                });

            // Add each non-country location as the corresponding country's child, if available
            rawData.Values
                .Where(e => e.LocationType != (int) Constants.LocationType.Country)
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
            foreach (var node in dependencyTree.Values)
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

            node.Children = new Dictionary<int, EventCaseCountModel>();
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
                node.ChildrenRepCaseCount = 0;
                node.ChildrenConfCaseCount = 0;
                node.ChildrenSuspCaseCount = 0;
                node.ChildrenDeathCount = 0;
                return;
            }

            foreach (EventCaseCountModel child in node.Children.Values)
            {
                ApplyNesting(child);
            }

            node.ChildrenRepCaseCount = node.Children.Sum(c => c.Value.GetNestedRepCaseCount());
            node.ChildrenConfCaseCount = node.Children.Sum(c => c.Value.GetNestedConfCaseCount());
            node.ChildrenSuspCaseCount = node.Children.Sum(c => c.Value.GetNestedSuspCaseCount());
            node.ChildrenDeathCount = node.Children.Sum(c => c.Value.GetNestedDeathCount());
        }
    }
}