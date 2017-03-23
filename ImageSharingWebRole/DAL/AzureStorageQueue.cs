using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ImageSharingWebRole.DAL
{
    public class AzureStorageQueue
    {
        
        public CloudStorageAccount storageAccount;
        public CloudQueueClient queueClient;
        public CloudQueue queue;
        
        public AzureStorageQueue()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageString"));
            //storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference("myqueue");
            queue.CreateIfNotExists();
            
        }
        public void addMessage(String message)
        {
           
            CloudQueueMessage cloudMessage = new CloudQueueMessage(message);
            queue.AddMessage(cloudMessage);
        }
        public int size()
        {
            queue.FetchAttributes();
            int? queuesize = queue.ApproximateMessageCount;
            return (int)queuesize;
        }
        public List<String> peekAllMessage()
        {
            queue.FetchAttributes();
            List<String> QueueMessage = new List<String>();
            queue.FetchAttributes();
            int? queuesize = queue.ApproximateMessageCount;
            if (queuesize == 0) return QueueMessage;
            foreach(CloudQueueMessage cloudMessage in queue.PeekMessages((int)queuesize))
            {
                QueueMessage.Add(cloudMessage.AsString);
            }

            return QueueMessage;
        }

        public void ClearMessage()
        {
            queue.Clear();
           
        }
        
    }

}