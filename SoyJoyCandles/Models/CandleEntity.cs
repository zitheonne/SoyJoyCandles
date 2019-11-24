using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations;

namespace SoyJoyCandles.Models
{
	public class CandleEntity : TableEntity
	{
		public CandleEntity(string originName, string typeName)
		{
			this.PartitionKey = originName; //unique identifier
			this.RowKey = typeName;
		}

		public CandleEntity() { }
		
		public string CandleProducedDate { get; set; }

		public string CandleType { get; set; }

		public string Candleprice { get; set; }

		[Key]
		public int IDProduct { get; set; }
	}
}
