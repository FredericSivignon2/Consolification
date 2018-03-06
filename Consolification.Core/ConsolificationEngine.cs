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
    public class ConsolificationEngine<T> where T: new()
    {
        private T data;
        private ILogWriter log;
        private IPasswordReader reader;
        private IConsoleWrapper console;

        public ConsolificationEngine()
        {
            this.data = new T();
            this.console = new DefaultConsoleWrapper();
            this.log = new DefaultLogWriter(this.console);
            this.reader = new DefaultPasswordReader(this.console);
        }

        public T Data
        {
            get { return data; }
        }

        public ILogWriter Logger
        {
            get
            {
                return this.log;
            }
            set
            {
                this.log = value;
            }
        }

        public IPasswordReader Reader
        {
            get
            {
                return this.reader;
            }
            set
            {
                this.reader = value;
            }
        }

        public IConsoleWrapper Console
        {
            get
            {
                return this.console;
            }
            set
            {
                this.console = value;
            }
        }

        public void Start(string[] args)
        {
            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(this.data, args);
            }
            catch (Exception e)
            {
                this.log.Fatal("", e);
            }
            
            
            if (parser.MustDisplayHelp)
            {
                HelpBuilder builder = new HelpBuilder(parser);
                string[] lines = builder.GetHelpLines();

                foreach (String line in lines)
                {
                    this.Logger.Info(line);
                }
                return;
            }

            foreach (ArgumentInfo argInfo in parser.ArgumentsInfo)
            {
                if (argInfo.Job != null)
                {
                    IJob<T> job = null;
                    try
                    {
                        job = (IJob<T>)Activator.CreateInstance(argInfo.Job.JobType);
                    }
                    catch (Exception exp)
                    {
                        this.log.Error("Cannot create Job instance.", exp);
                    }
                    
                    try
                    {
                        JobContext<T> context = new JobContext<T>()
                        {
                            Data = data,
                            Logger = this.log,
                            Reader = this.reader
                        };
                        
                        job.Run(context);
                    }
                    catch (Exception exp)
                    {
                        this.log.Error("Cannot run job instance.", exp);
                    }
                }
            }
        }
    }
}
