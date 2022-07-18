using PM_CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }

    public class QueryDispatcher : IQueryDispatcher
    {
        private IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var service = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;
            
            return await service.HandleAsync(query, new CancellationToken());
        }
    }
}
