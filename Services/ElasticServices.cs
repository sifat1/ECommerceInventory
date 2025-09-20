using ECommerceInventory.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace ECommerceInventory.Services;

public class ElasticServices
{
    private readonly ElasticsearchClient _es;
    private const string IndexName = "products";

    public ElasticServices(ElasticsearchClient es)
    {
        _es = es;
    }
    
    public async Task CreateProductAsync(Product product)
    {
        var response = await _es.IndexAsync(product, i => i
            .Index(IndexName)
            .Id(product.Id)
        );

        if (!response.IsValidResponse)
            throw new Exception($"Failed to index product {product.Id}: {response.DebugInformation}");
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        var response = await _es.IndexAsync(product, i => i
            .Index(IndexName)
            .Id(product.Id)
        );

        if (!response.IsValidResponse)
            throw new Exception($"Failed to update product {product.Id}: {response.DebugInformation}");
    }
    
    public async Task DeleteProductAsync(int productId)
    {
        var response = await _es.DeleteAsync<Product>(productId, d => d.Index(IndexName));

        if (!response.IsValidResponse)
            throw new Exception($"Failed to delete product {productId}: {response.DebugInformation}");
    }
    
    public async Task<IReadOnlyCollection<Product>> SearchProductsAsync(
        string keyword,
        int from = 0,
        int size = 10)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Array.Empty<Product>();

        var response = await _es.SearchAsync<Product>(s => new SearchRequest<Product>(IndexName)
        {
            From = from,
            Size = size,
            Query = new MultiMatchQuery
            {
                Query = keyword,
                Fields = Infer.Fields<Product>(p => p.Name, p => p.Description)
            }
        });

        if (!response.IsValidResponse)
            throw new Exception($"Elasticsearch search failed: {response.DebugInformation}");

        return response.Documents;
    }
}
