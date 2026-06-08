using System;
using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services;

public static class ProductService
{
    private const string _path = "products.json";

    public static List<Product> GetAll()
    {
        string blob = GetProductsBlob();

        return JsonConvert.DeserializeObject<List<Product>>(blob) ?? new List<Product>();
    }
    public static Product? GetById(Guid id) 
    {
        return GetAll().FirstOrDefault(product => product.Id == id);
    }
    public static bool Add(Product product)
    {
        bool answer = false;

        List<Product> products = GetAll();

        if (product != null && !products.Any(productFromMemory => productFromMemory.Id == product.Id))
        {
            products.Add(product);
            WriteIntoMemory(products);
            answer = true;
        }

        return answer;
    }
    public static void AddRange(params List<Product> products)
    {
        List<Product> memoryProducts = GetAll();
        List<Product> productsToAdd = memoryProducts
            .Union(
                products.Where(product => product != null), 
                new ProductIdEqualityComparer()
            )
            .ToList();

        WriteIntoMemory(productsToAdd);
    }
    public static void Clear()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
    private static void WriteIntoMemory(List<Product>  products)
    {
        string blob = JsonConvert.SerializeObject(products);

        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.Write(blob);
        }
    }
    private static string GetProductsBlob()
    {
        if (File.Exists(_path))
            using (StreamReader reader = new StreamReader(_path, false))
                return reader.ReadToEnd();
        return "";
    }
}
