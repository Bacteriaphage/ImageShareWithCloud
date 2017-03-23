using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Core;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ImageSharingWebRole.Models;
using Microsoft.Azure;

namespace ImageSharingWebRole.DAL
{
    public class LogContext : TableEntity
    {
        public const string LOG_TABLE_NAME = "imgviews";

        public LogContext()
        {
        }

        public static void AddLogEntry(string username, ImageView image) //9
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageString"));
            //CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient client = storageAccount.CreateCloudTableClient();

            CloudTable table = client.GetTableReference(LOG_TABLE_NAME);
            table.CreateIfNotExists();

            LogEntry entry = new LogEntry(image.Id);
            entry.UserId = username;
            entry.Caption = image.Caption;
            entry.ImageId = image.Id;
            entry.Uri = image.Uri;

            TableOperation insertOperation = TableOperation.Insert(entry);

            table.Execute(insertOperation);
        }

        public static IEnumerable<LogEntry> Select(int id)
        {
            //CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                                     CloudConfigurationManager.GetSetting("AzureStorageString"));
            CloudTableClient client = storageAccount.CreateCloudTableClient();

            CloudTable table = client.GetTableReference(LOG_TABLE_NAME);

            DateTime required = DateTime.UtcNow;

            required = required.AddDays(-7 * id);

            IEnumerable<LogEntry> results = from entity in table.CreateQuery<LogEntry>()
                                            where entity.PartitionKey == required.ToString("MMddyyyy")
                                            select new LogEntry
                                            {
                                                PartitionKey = entity.PartitionKey,
                                                RowKey = entity.RowKey,
                                                Caption = entity.Caption,
                                                Uri = entity.Uri,
                                                UserId = entity.UserId,
                                                ImageId = entity.ImageId,
                                                EntryDate = entity.EntryDate
                                            };
            return results;
        }
        
        public static void CreateTable(){
            
            CloudTableClient client = GetClient();

            CloudTable table = client.GetTableReference(LOG_TABLE_NAME);

            table.CreateIfNotExists();
        }

        protected static CloudTableClient GetClient(){
            //CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                                     CloudConfigurationManager.GetSetting("AzureStorageString"));
            CloudTableClient client = storageAccount.CreateCloudTableClient();

            return client;
        }
        
    }
}