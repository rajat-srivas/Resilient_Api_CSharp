namespace ResilientWebApiDemo.Services
{

	public interface IProductService
	{
		Task<string> GetProductPriceByCode(string pCode);
	}
}
