using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    /// <summary>
    /// Provides extension methods to the <cref>System.Exception</cref> class.
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Appends Exception message and all InnerException messages recursively
        /// to provide a full exception message.
        /// </summary>
        /// <param name="exception">The exception to parse</param>
        /// <returns></returns>
        public static string FullMessage(this Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            if (exception is AggregateException)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                    builder.Append(exception.Message);

                AggregateException aggrException = exception as AggregateException;
                foreach (Exception currentException in aggrException.InnerExceptions)
                {
                    if (builder.Length > 0)
                        builder.Append(Environment.NewLine);

                    builder.Append(currentException.Message);
                    ProcessReflectionTypeLoadException(currentException, builder);

                    Exception innerException = currentException.InnerException;
                    while (innerException != null)
                    {
                        if (builder.Length > 0)
                            builder.Append(Environment.NewLine);

                        builder.Append(innerException.Message);
                        ProcessReflectionTypeLoadException(exception, builder);

                        innerException = innerException.InnerException;
                    }

                    if (builder.Length > 0)
                        builder.Append(Environment.NewLine);
                }
            }
            else
            {
                while (exception != null)
                {
                    if (builder.Length > 0)
                        builder.Append(Environment.NewLine);

                    if (!string.IsNullOrEmpty(exception.Message))
                        builder.Append(exception.Message);

                    ProcessReflectionTypeLoadException(exception, builder);

                    exception = exception.InnerException;
                }
            }
            return builder.ToString();
        }

        #region Private Static Methods		
        /// <summary>
        /// Processes the ReflectionTypeLoadException and its LoaderExceptions property.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="builder">The builder.</param>
        private static void ProcessReflectionTypeLoadException(Exception exception, StringBuilder builder)
        {
            if (exception != null &&
                exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException reflecException = (ReflectionTypeLoadException)exception;
                if (reflecException != null)
                {
                    foreach (Exception exp in reflecException.LoaderExceptions)
                    {
                        builder.Append(Environment.NewLine);
                        builder.Append(exp.FullMessage());
                    }
                }
            }
        }
        #endregion
    }
}

