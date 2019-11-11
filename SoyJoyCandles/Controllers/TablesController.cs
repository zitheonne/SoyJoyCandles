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
			CloudTable table = tableClient.GetTableReference("TestTable");
			ViewBag.Success = table.CreateIfNotExistsAsync().Result;
			ViewBag.TableName = table.Name;
			return View();
		}

    }
}