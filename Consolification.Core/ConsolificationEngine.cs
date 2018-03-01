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
    public class ConsolificationEngine<T> where T: ArgumentsContainer, new()
    {
        private T container;
        private ILogWriter log;
        private IConsoleReader reader;

        public ConsolificationEngine()
        {
            this.container = new T();
            this.log = new DefaultLogWriter();
            this.reader = new DefaultConsoleReader();
        }

        public T Data
        {
            get { return container; }
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

        public IConsoleReader Reader
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

        public void Start(string[] args)
        {
            try
            {
                this.container.Initialize(args);
            }
            catch (Exception e)
            {
                this.log.Fatal("", e);
            }
            
            
            if (this.container.MustDisplayHelp)
            {
                HelpBuilder builder = new HelpBuilder(this.container);
                string[] lines = builder.GetHelpLines();

                foreach (String line in lines)
                {
                    this.Logger.Info(line);
                }
                return;
            }

            foreach (ArgumentInfo argInfo in this.container.ArgumentsInfo)
            {
                if (argInfo.Job != null)
                {
                    IJob job = null;
                    try
                    {
                        job = (IJob)Activator.CreateInstance(argInfo.Job.JobType);
                    }
                    catch (Exception exp)
                    {
                        this.log.Error("Cannot create Job instance.", exp);
                    }
                    
                    try
                    {
                        JobContext context = new JobContext()
                        {
                            Container = this.container,
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
