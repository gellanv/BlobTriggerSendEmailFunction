using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendEmailFunction.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SendEmailFunction
{
    [StorageAccount("BlobConnectionString")]
    public class SendEmailFunction
    {
        private readonly SendEmailService sendEmailService;
        public SendEmailFunction(SendEmailService _sendMailService)
        {
            sendEmailService = _sendMailService;
        }

        [FunctionName("SendEmailFunction")]
        public async Task Run([BlobTrigger("filesfolder/{name}")] Stream myBlob,
                        string name,
                        IDictionary<string, string> metadata,
                        ILogger log)
        {
            string email = metadata["email"];
            try
            {
                await sendEmailService.SendMail(name, email);
                log.LogInformation($"Blob trigger function Processed blob\n Name:{name}, \nEmail:{email}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"An error occurred while processing the blob\n Name:{name}, \nEmail:{email}, \nError: {ex.Message}");
            }
        }
    }
}