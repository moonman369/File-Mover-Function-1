using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.IO.Compression;

namespace File_Mover_Function_1
{
    public static class FileMover1
    {
        [FunctionName("FileMover1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string body = new StreamReader(req.Body, Encoding.UTF8).ReadToEnd();
                log.LogInformation(body);

                MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(body));

                ZipArchive archive = new ZipArchive(req.Body, ZipArchiveMode.Create, true);

                return new HttpResponseMessage
                {
                    Content = new StringContent(body, Encoding.Default, @"text/plain"),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (System.Exception e)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(e.Message, Encoding.Default, @"text/plain"),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }
    }
}
