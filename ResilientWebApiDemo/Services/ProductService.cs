using ResilientWebApiDemo.Entity;
using System.Text.Json;

namespace ResilientWebApiDemo.Services
{
	public class ProductService : IProductService
	{
		public ProductService()
		{
			
		}
		public async Task<string> GetProductPriceByCode(string pCode)
		{

			if (string.IsNullOrEmpty(pCode))
			{
				pCode = "0839915011";
			}
			else
			{
				var product = await GetProductPricefromHMByCode(pCode);
				if (product.Product == null)
					return "Product Not Found";
				else
				{
					return $"Current price of product {pCode} is {product.Product.RedPrice.price.ToString()} {product.Product.RedPrice.currency}";
				}
			}

			return "";

		}

		private async Task<ProductPrice> GetProductPricefromHMByCode(string pCode)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://apidojo-hm-hennes-mauritz-v1.p.rapidapi.com/products/detail?lang=en&country=us&productcode=" + pCode),
				Headers =
					{
						{ "x-rapidapi-key", "API KEY HERE" },
						{ "x-rapidapi-host", "apidojo-hm-hennes-mauritz-v1.p.rapidapi.com" },
					},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(body))
				{
					var price = JsonSerializer.Deserialize<ProductPrice>(body);
					return price;
				}
				else
				{
					return new ProductPrice();
				}
			}
		}
	}
}
