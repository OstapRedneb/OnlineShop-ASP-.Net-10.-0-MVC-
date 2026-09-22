using System;
using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices;

public class ProductService : IProductService
{
    private const string _path = "products.json";

    public List<ProductViewModel> GetAll()
    {
        string blob = GetProductsBlob();

        return JsonConvert.DeserializeObject<List<ProductViewModel>>(blob)?.Where(product => !product.IsDeleted)?.ToList() ?? new List<ProductViewModel>();
    }
    public List<ProductViewModel> GetAllWithDeleted()
    {
        string blob = GetProductsBlob();

        return JsonConvert.DeserializeObject<List<ProductViewModel>>(blob) ?? new List<ProductViewModel>();
    }
    public ProductViewModel? GetById(Guid id) 
    {
        return GetAllWithDeleted().FirstOrDefault(product => product.Id == id);
    }
    public bool Add(ProductViewModel product)
    {
        bool answer = false;

        List<ProductViewModel> products = GetAll();

        if (product != null && !products.Any(productFromMemory => productFromMemory.Id == product.Id))
        {
            products.Add(product);
            WriteIntoMemory(products);
            answer = true;
        }

        return answer;
    }
    public void AddRange(params List<ProductViewModel> products)
    {
        List<ProductViewModel> memoryProducts = GetAll();
        List<ProductViewModel> productsToAdd = memoryProducts
            .Union(
                products.Where(product => product != null), 
                new ProductIdEqualityComparer()
            )
            .ToList();

        WriteIntoMemory(productsToAdd);
    }
    public bool Update(ProductViewModel product) 
    {
        if (product is null)
            return false;

        List<ProductViewModel> products = GetAll();

        bool wasFound = false;
        for (int i = 0; i < products.Count; i++) 
        {
            if (products[i].Id == product.Id) 
            {
                products[i] = product;
                wasFound = true;
                break;
            }
        }

        if (!wasFound)
            Add(product);
        else
            WriteIntoMemory(products);

        return true;
    }
    public void Clear()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
    private void WriteIntoMemory(List<ProductViewModel>  products)
    {
        string blob = JsonConvert.SerializeObject(products);

        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.Write(blob);
        }
    }
    private string GetProductsBlob()
    {
        if (File.Exists(_path))
            using (StreamReader reader = new StreamReader(_path, false))
                return reader.ReadToEnd();
        return "";
    }
}
