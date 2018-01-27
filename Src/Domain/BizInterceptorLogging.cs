using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using Serilog;

namespace Biz
{
    public class BizInterceptorLogging : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Log.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Error executing command: {0}", command.CommandText);
#endif
            }
            else
            {
                Trace("SQL Database", "BizInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Log.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Error executing command: {0}", command.CommandText);
#endif
            }
            else
            {
                Trace("SQL Database", "BizInterceptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Log.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Error executing command: {0}", command.CommandText);
#endif
            }
            else
            {
                Trace("SQL Database", "BizInterceptor.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }

        private void Trace(string componentName, string method, TimeSpan timespan)
        {
            Trace(componentName, method, timespan, "");
        }

        private void Trace(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            Trace(componentName, method, timespan, string.Format(fmt, vars));
        }

        private void Trace(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            Log.Debug(message);
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
        }
    }
}