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

        public ConsolificationEngine(ArgumentsContainer container)
        {
            this.container = container;
        }

        public void Start()
        {
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

                    }


                    try
                    {
                        job.Run(this.container);
                    }
                    catch (Exception exp)
                    {

                    }
                }
            }
        }
    }
}
