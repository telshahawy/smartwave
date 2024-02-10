using System;
using System.Diagnostics;
using Common.Logging;
using SW.Framework.Cqrs;

namespace SW.Framework.LoggingCore
{
    public sealed class QueryLoggingDecorator<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : class
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;
        private readonly ILog _logger;

        public QueryLoggingDecorator(IQueryHandler<TQuery, TResult> decorated,
            ILog logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public TResult Read(TQuery query)
        {
            TResult result;
            var queryName = query.GetType().Name;

            _logger.Debug($"Start reading query '{queryName}'" + Environment.NewLine);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                result = _decorated.Read(query);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception throw while reading query '{queryName}'" +
                              Environment.NewLine + ex.Message + Environment.NewLine, ex);
                throw;
            }
            finally
            {
                stopwatch.Stop();
            }

            _logger.Debug($"Executed query '{queryName}' in {stopwatch.Elapsed}." +
                         Environment.NewLine);

            return result;
        }
    }
}