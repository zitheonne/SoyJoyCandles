using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SoyJoyCandles.Models
{
	public class SoyJoyCandlesShop
	{
		[Required]
		public int ID { get; set; }

		[StringLength(60, MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		[Display(Name = "Name of Candle")]
		[Required]
		public string CandleName { get; set; }

		[Display(Name = "Produced Date")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime CandleProducedDate { get; set; }
		//if you dont want date time but just want date, you can use DataType(DataType.Date)

		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		[Display(Name = "Type of Candle")]
		[Required]
		public string CandleType { get; set; }

		[Display(Name = "Price of Candle")]
		[DataType(DataType.Currency)] //set currency
		[Range(1,200)] //until 2hundred, cannot add more
		[Required]
		public decimal Candleprice { get; set; }
	}
}
