using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Cqrs;

namespace SW.Framework.Cqrs
{
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        public QueryProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private TQueryResponse ProcessQuery<TQuery, TQueryResponse>(TQuery query)
            where TQuery : class
            where TQueryResponse : class
        {
            if (query == null) throw new Exception("Query can't be null");
            return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResponse>>().Read(query);
        }

        public async Task<TQueryResponse> ProcessQueryAsync<TQuery, TQueryResponse>(TQuery query)
            where TQuery : class
            where TQueryResponse : class
        {
            return await new TaskFactory<TQueryResponse>().StartNew(() => ProcessQuery<TQuery, TQueryResponse>(query));
        }
    }
}