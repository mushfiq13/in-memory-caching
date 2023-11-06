using InMemoryCaching.Console.Lib;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Console.Services;

public class ProductService
{
	private readonly IMemoryCache _memoryCache;

	public ProductService(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
	}

	public async Task<List<Product>> GetAllAsync()
	{
		List<Product> products = new()
				{
						new Product{ Id=1, Title="ACADEMY ACADEMY", Actor="PENELOPE GUINESS", Price=25.99 },
						new Product{ Id=2, Title="ACADEMY ACE", Actor="EWAN RICKMAN", Price=28.99 },
						new Product{ Id=3, Title="ACADEMY ADAPTATION", Actor="VIVIEN KAHN", Price=25.99 },
						new Product{ Id=4, Title="ACADEMY AFFAIR", Actor="ALAN MARX", Price=14.99 },
				};

		// Act like we are really htting heavy things!
		await Task.Delay(TimeSpan.FromSeconds(5));

		return products;
	}

	public async Task<List<Product>> GetProductsCacheAsync()
	{
		_memoryCache.TryGetValue("products", out List<Product> products);

		if (products is null) {
			products = await GetAllAsync();

			_memoryCache.Set<List<Product>>(
				key: "products",
				value: products,
				absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(1));
		}

		return products;
	}
}
