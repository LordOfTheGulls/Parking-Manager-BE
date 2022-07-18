using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries
{
    public interface IQueryHandler<TQuery, TQueryResult> where TQuery: IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken token);
    }
}
