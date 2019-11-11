using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

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

		//public string Email { get; set; }
		//public int ID { get; set; }

		//[StringLength(60, MinimumLength = 3)]
		//[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		//[Display(Name = "Name of Candle")]
		//[Required]
		//public string CandleName { get; set; }

		//[Display(Name = "Produced Date")]
		//[DataType(DataType.Date)]
		//[Required]
		public string CandleProducedDate { get; set; }
		//if you dont want date time but just want date, you can use DataType(DataType.Date)

		//[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		//[Display(Name = "Type of Candle")]
		//[Required]
		public string CandleType { get; set; }

		//[Display(Name = "Price of Candle")]
		//[DataType(DataType.Currency)] //set currency
		//[Range(1, 200)] //until 2hundred, cannot add more
		//[Required]
		public string Candleprice { get; set; }
	}
}
