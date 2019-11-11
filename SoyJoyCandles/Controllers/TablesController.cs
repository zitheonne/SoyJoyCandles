using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using SoyJoyCandles.Models;

namespace SoyJoyCandles.Controllers
{
    public class TablesController : Controller
    {

		public IActionResult Index()
        {
            return View();
        }

		public ActionResult CreateTable()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");
			ViewBag.Success = table.CreateIfNotExistsAsync().Result;
			ViewBag.TableName = table.Name;

			return View();
		}

		[HttpGet]
		public ActionResult AddEntity()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddEntity(CandleEntity candleEntity)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");

			//CandleEntity candle1 = new CandleEntity("African", "Musk");
			//candle1.CandleProducedDate = "21/09/2019";
			//candle1.CandleType = "Musky";
			//candle1.Candleprice = "95.40";

			CandleEntity candle1 = new CandleEntity(candleEntity.PartitionKey, candleEntity.RowKey);
			candle1.CandleProducedDate = candleEntity.CandleProducedDate;
			candle1.CandleType = candleEntity.CandleType;
			candle1.Candleprice = candleEntity.Candleprice;

			TableOperation insertOperation = TableOperation.Insert(candle1);

			try
			{
				TableResult results = table.ExecuteAsync(insertOperation).Result;
				ViewBag.TableName = table.Name;
				ViewBag.result = results.HttpStatusCode;
			}
			catch (Exception ex)
			{
				ViewBag.result = 101; //error page
			}
			
			return View();

		}

		public ActionResult AddEntities()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");

			CandleEntity candle1 = new CandleEntity("Australian", "Breeze");
			candle1.CandleProducedDate = "14/08/2019";
			candle1.CandleType = "Light";
			candle1.Candleprice = "75.80";

			CandleEntity candle2 = new CandleEntity("Malaysian", "Kuih");
			candle2.CandleProducedDate = "7/09/2019";
			candle2.CandleType = "Sweet";
			candle2.Candleprice = "88.90";

			return View();
		}

		public ActionResult GetPartition(int id=0)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");

			//TableQuery<CandleEntity> query = new TableQuery<CandleEntity>()
			//	.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Musk"));

			TableQuery<CandleEntity> query = new TableQuery<CandleEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, "")); //filter based on partition key

			List<CandleEntity> candles = new List<CandleEntity>();
			TableContinuationToken token = null;
			do
			{
				TableQuerySegment<CandleEntity> resultSegment = table.ExecuteQuerySegmentedAsync(query, token).Result;
				token = resultSegment.ContinuationToken;

				foreach (CandleEntity candle in resultSegment.Results)
				{
					candles.Add(candle);
				}
			}
			while (token != null);
			return View(candles);
		}

		//string partitionkey and string rowkey refer to the variable "params" in GetPartition.cshtml file
		//used to pass the data between the pages
		public ActionResult DeleteEntity(string partitionkey, string rowkey)
		{
			//create the azure table storage connection
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			//select the table from the table storage account
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");

			//execute the delete operation
			TableOperation deleteOperation = TableOperation.Delete(new CandleEntity(partitionkey, rowkey) { ETag = "*" });

			//retrieve the delete operation result
			TableResult result = table.ExecuteAsync(deleteOperation).Result;

			//once the delete operation is done, redirect the action to the GetPartition page.
			return RedirectToAction("GetPartition", "Tables");

		}


    }
}