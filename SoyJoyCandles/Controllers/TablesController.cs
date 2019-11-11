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

		public ActionResult AddEntity()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot Configuration = builder.Build();
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(Configuration["ConnectionStrings:AzureStorageConnectionString-1"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("CandleTable");

			CandleEntity candle1 = new CandleEntity("African", "Musk");
			candle1.CandleProducedDate = "21/09/2019";
			candle1.CandleType = "Musky";
			candle1.Candleprice = "95.40";

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

    }
}