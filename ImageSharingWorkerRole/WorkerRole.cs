using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Diagnostics;
using System.Configuration;
using ImageSharingWebRole.Models;
using ImageSharingWebRole.DAL;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Azure;

namespace ImageSharingWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        string QueueName = ValidationQueue.QueueName;

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        ApplicationDbContext db = new ApplicationDbContext();
        public override void Run()
        {
            //while (!isStopped)
            //{
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            Client.OnMessage(OnMessageReceived);

            CompletedEvent.WaitOne();
            //}
        }

        public void OnMessageReceived(BrokeredMessage receivedMessage)
        {
            try
            {
                Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                ValidationRequest request = receivedMessage.GetBody<ValidationRequest>();

                Image image = db.Images.Find(request.imageId);

                if (ImageStorage.Validate(request.imageId))
                {
                    image.Validated = true;
                    db.SaveChanges();
                }
                else
                {
                    ImageStorage.DeleteFile(null, request.imageId);
                    db.Images.Remove(image);
                    db.SaveChanges();
                }

                receivedMessage.Complete();
            }
            catch
            {
                // Handle any Message
            }

        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            //string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}