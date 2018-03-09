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
    public class RequestJob : IJob<RequestData>
    {
        public int Run(JobContext<RequestData> context)
        {
            RequestData data = context.Data;

            StreamReader reader = null;
            WebResponse response = null;
            try
            {       
				var watch = System.Diagnostics.Stopwatch.StartNew();
                
				
				//
                HttpWebRequest request = WebRequest.CreateHttp(data.URL);
                request.Method = "GET";
				if (!string.IsNullOrEmpty(data.User))
				{
					//request.Credentials = new NetworkCredential();
					String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(data.User + ":" + data.Password));
					request.Headers.Add("Authorization", "Basic " + encoded);
				}
					

				response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string content = reader.ReadToEnd();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                TimeSpan span = TimeSpan.FromMilliseconds(elapsedMs);
                context.Console.WriteLine("Request executed in: {0}", span.ToString(@"hh\:mm\:ss\:fff"));
                context.Console.WriteLine("Response size:       {0} octet(s)", content.Length);
                context.Console.WriteLine("Content:             {0}...",content.Substring(0, 64));
                context.Console.WriteLine("");

                return 0;
            }
            catch (Exception exp)
            {
                context.Console.WriteLine("ERROR: Failed to execute HTTP request.", exp);
                return -1;
            }
        }
    }
}
