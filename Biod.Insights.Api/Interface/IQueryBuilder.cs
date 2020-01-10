using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biod.Insights.Api.Interface
{
    public interface IQueryBuilder<T>
    {
        Task<IEnumerable<T>> BuildAndExecute();
    }
}