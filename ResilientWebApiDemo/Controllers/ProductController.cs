using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ResilientWebApiDemo.Services;

namespace ResilientWebApiDemo.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Asp.Versioning.ApiVersion("1.0")]
	[Asp.Versioning.ApiVersion("2.0")]
	public class ProductController : ControllerBase
	{
		IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		[Asp.Versioning.MapToApiVersion("1.0")]
		public async Task<IActionResult> GetProductById(string pCode)
		{
			var response = await _productService.GetProductPriceByCode(pCode);

			if (!string.IsNullOrWhiteSpace(response))
			{
				response += " from v1";
			}

			return Ok(response);
		}


		[HttpGet]
		[Asp.Versioning.MapToApiVersion("2.0")]
		public async Task<IActionResult> GetProductByIdV2(string pCode)
		{
			var response = await _productService.GetProductPriceByCode(pCode);
			if(!string.IsNullOrWhiteSpace(response))
			{
				response += " from v2";
			}
			return Ok(response);
		}
	}
}
