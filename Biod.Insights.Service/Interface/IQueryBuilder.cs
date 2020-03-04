using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IQueryBuilder<T1, T2>
    {
        /// <summary>
        /// Gets the raw queryable without any conditions, unless overriden
        /// </summary>
        IQueryable<T1> GetInitialQueryable();

        /// <summary>
        /// Overrides the initial queryable to allow injection of custom query conditions
        /// </summary>
        /// <param name="customQueryable">the customized queryable</param>
        IQueryBuilder<T1, T2> OverrideInitialQueryable(IQueryable<T1> customQueryable);
        
        /// <summary>
        /// Constructs the query after configuring the builder and executes into enumerable results 
        /// </summary>
        Task<IEnumerable<T2>> BuildAndExecute();
    }
}