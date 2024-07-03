using System.Net.WebSockets;
using X.PagedList.Web.Common;

namespace TestWeb.Models.Home
{
	public class CartItem
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Image { get; set; }
		public int Quantity { get; set;}
		public decimal? Price { get; set;}
		public decimal? Total => Price * Quantity;
		
	}
}
