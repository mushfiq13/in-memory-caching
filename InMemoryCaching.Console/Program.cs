using InMemoryCaching.Console.Services;
using Microsoft.Extensions.Caching.Memory;

var memorySingletonInstance = new MemoryCache(new MemoryCacheOptions());
var service = new ProductService(memorySingletonInstance);

// Loop over 10 times to test cache data
for (var i = 0; i < 10; ++i) {
	Console.Write("Press enter to read the data...");
	Console.ReadLine();

	var data = await service.GetProductsCacheAsync();

	foreach (var product in data) {
		Console.WriteLine(
			 $"Id: {product.Id}, Title: {product.Title}, Actor: {product.Actor}, Price: {product.Price}");
	}

	Console.WriteLine($"Count: {data.Count}");
	Console.WriteLine();
}