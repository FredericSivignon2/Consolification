using Consolification.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    public class RequestJob : IJob
    {
        public void Run(JobContext context)
        {
            RequestData data = context.Container as RequestData;

            StreamReader reader = null;
            WebResponse response = null;
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                //String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("i051238" + ":" + "Amiga920!"));
                HttpWebRequest request = WebRequest.CreateHttp(data.URL);
                request.Method = "GET";
                //request.Headers.Add("Authorization", "Basic " + encoded);

                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string content = reader.ReadToEnd();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                TimeSpan span = TimeSpan.FromMilliseconds(elapsedMs);
                context.Logger.InfoFormat("Request executed in: {0}", span.ToString(@"hh\:mm\:ss\:fff"));
                context.Logger.InfoFormat("Response size:       {0} octet(s)", content.Length);
                context.Logger.InfoFormat("Content:             {0}...",content.Substring(0, 64));
                context.Logger.Info("");
            }
            catch (Exception exp)
            {
                context.Logger.Error("Failed to execute HTTP request.", exp);
            }
        }
    }
}
