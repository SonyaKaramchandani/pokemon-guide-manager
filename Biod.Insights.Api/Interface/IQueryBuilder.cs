using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Insights.Api.Interface
{
    public interface IQueryBuilder<T1, T2>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T1> GetInitialQueryable();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T2>> BuildAndExecute();
    }
}