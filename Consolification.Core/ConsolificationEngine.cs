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
    public class ConsolificationEngine
    {
        private ArgumentsContainer container;
        private ILogWriter log;
        private IConsoleReader reader;

        public ConsolificationEngine(ArgumentsContainer container)
        {
            this.container = container;
            this.log = new DefaultLogWriter();
            this.reader = new DefaultConsoleReader();
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

        public void Start()
        {
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
