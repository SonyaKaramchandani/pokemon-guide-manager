using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.Api.Surveillance
{
    public class MockZebraUpdateService : IZebraUpdateService
    {
        /// <summary>
        /// Mocks the service call with computable values to be verified when the output is passed in to the stored procedure
        /// </summary>
        public Task<string> GetMinMaxCasesService(string gridId, string cases)
        {
            int gridIdValue = string.IsNullOrEmpty(cases) ? 1 : int.Parse(gridId);
            int casesValue = string.IsNullOrEmpty(cases) ? 1 : int.Parse(cases);

            // Create a fake deterministic result computable from the grid id and cases
            var task = new TaskCompletionSource<string>();
            task.SetResult($"{gridIdValue},{casesValue},{gridIdValue + casesValue},{gridIdValue * casesValue}");
            return task.Task;
        }

        /// <summary>
        /// Mocks the service call with computable values to be verified when the output is passed in to the stored procedure
        /// </summary>
        public Task<string> GetMinMaxPrevalenceService(
            string mineventcasesoverpopsize,
            string maxeventcasesoverpopsize,
            string diseaseincubation,
            string diseasesymptomatic,
            string eventstartdate,
            string eventenddate)
        {
            double minCasesOverPopSizeValue = double.Parse(mineventcasesoverpopsize);
            double maxCasesOverPopSizeValue = double.Parse(maxeventcasesoverpopsize);
            decimal diseaseIncubationValue = decimal.Parse(diseaseincubation);
            decimal diseaseSymptomaticValue = decimal.Parse(diseasesymptomatic);
            DateTime eventStartDateValue = DateTime.Parse(eventstartdate);
            DateTime eventEndDateValue = string.IsNullOrEmpty(eventenddate) ? DateTime.Now : DateTime.Parse(eventenddate);

            double diff = (eventEndDateValue - eventStartDateValue).TotalDays;

            // Create a fake deterministic result computable from the params
            var task = new TaskCompletionSource<string>();
            task.SetResult($"{minCasesOverPopSizeValue + maxCasesOverPopSizeValue + (double)diseaseIncubationValue + (double)diseaseSymptomaticValue},{diff}");
            return task.Task;
        }
    }
}
