using System;
using System.Diagnostics;
using SW.Framework.Cqrs;
using Common.Logging;
using System.Threading.Tasks;

namespace SW.Framework.LoggingCore
{
    public sealed class CommandLoggingDecorator<TCommand> : ICommandDecorator<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly ILog _logger;

        public CommandLoggingDecorator(ICommandHandler<TCommand> decorated,
            ILog logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public  void Handle(TCommand command)
        {
            var commandName = command== null? "N/A" : command.GetType().Name;

            _logger.Info($"Start executing command '{commandName}'" + Environment.NewLine);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _decorated.Handle(command);
            }
            catch (Exception ex)
            {
                //HandleFault(command, ex);
                //_decorated.HandleFault(command, ex);
                _logger.Error($"Exception throw while executing command '{commandName}'" +
                              Environment.NewLine + ex.Message + Environment.NewLine, ex);
                throw;
            }
            finally
            {
                stopwatch.Stop();
            }

            _logger.Info($"Executed command '{commandName}' in {stopwatch.Elapsed}." +
                         Environment.NewLine);
        }

        //public void HandleFault(TCommand command, Exception exception)
        //{
        //    _decorated.HandleFault(command, exception);
        //}
    }
}