using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    /// <summary>
    /// The main entry point class for the 'Consolification' framework, that will allow you
    /// to simplify the writing of a Console application, by providing all the plumbing to
    /// handle arguments, even in complex scenarios.
    /// </summary>
    /// <typeparam name="T">A type that represents data populated by given Console application arguments.</typeparam>
    /// <remarks>
    /// 
    /// </remarks>
    public class ConsolificationEngine<T> where T: new()
    {  
        #region Public Constructor
        public ConsolificationEngine()
        {
            this.Data = new T();
            this.Console = new DefaultConsoleWrapper();
            this.Reader = new DefaultPasswordReader(this.Console);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets an instance of the class data associated with this instance.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Gets or sets the <cref>Consolification.Core.IPasswordReader</cref> interface implementation associated with this instance.
        /// </summary>
        public IPasswordReader Reader { get; set; }

        /// <summary>
        /// Gets or sets the <cref>Consolification.Core.IConsoleWrapper</cref> interface implementation associated with this instance.
        /// </summary>
        public IConsoleWrapper Console { get; set; }

        /// <summary>
        /// Gets or sets the default return value when the related Console Application execution is successful.
        /// </summary>
        public int ResultDefaultValue { get; set; } = 0;

        /// <summary>
        /// Gets or sets the return value when the Console Application has displayed its help text.
        /// </summary>
        public int ResultDisplayHelp { get; set; } = 1;

        /// <summary>
        /// Gets or sets the return value when the Console Application cannot create its associated Job.
        /// </summary>
        public int ResultCannotCreateJob { get; set; } = 2;

        /// <summary>
        /// Gets or sets the return value when the Console Application fails to execute its Job.
        /// </summary>
        public int ResultCannotExecuteJob { get; set; } = 3;

        /// <summary>
        /// Gets or sets the return value when the Console Application cannot parse given arguments.
        /// </summary>
        public int ResultCannotParseArguments { get; set; } = 4;
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the engine with the given arguments.
        /// </summary>
        /// <param name="args">An array of strings that contains all arguments given to the application.</param>
        /// <returns>A return code depending of execution result.</returns>
        public int Start(string[] args)
        {
            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(this.Data, args);
            }
            catch (Exception e)
            {
                this.Console.WriteLine("ERROR while parsing arguments.", e);
                return ResultCannotParseArguments;
            }            
            
            if (parser.MustDisplayHelp)
            {
                DisplayHelp(parser);
                return ResultDisplayHelp;
            }
            
            int finalResult = ResultDefaultValue;
            if (parser.MainJob != null)
            {
                finalResult = ExecuteJob(parser.MainJob);
            }
            
            return finalResult;
        }
        #endregion

        #region Private Methods
        private int ExecuteJob(CIJobAttribute jobAttr)
        {
            IJob<T> job = null;
            try
            {
                job = (IJob<T>)Activator.CreateInstance(jobAttr.JobType);
            }
            catch (Exception exp)
            {
                this.Console.WriteLine("ERROR: Cannot create main Job instance.", exp);
                return ResultCannotCreateJob;
            }

            try
            {
                JobContext<T> context = new JobContext<T>()
                {
                    Data = this.Data,
                    Reader = this.Reader,
                    Console = this.Console
                };

                return job.Run(context);
            }
            catch (Exception exp)
            {
                this.Console.WriteLine("ERROR: Cannot run job instance. {0}", exp.FullMessage());
                return ResultCannotExecuteJob;
            }
        }

        private void DisplayHelp(ArgumentsParser parser)
        {
            HelpBuilder builder = new HelpBuilder(parser);
            string[] lines = builder.GetHelpLines();

            foreach (String line in lines)
            {
                this.Console.WriteLine(line);
            }

        }
        #endregion
    }
}
