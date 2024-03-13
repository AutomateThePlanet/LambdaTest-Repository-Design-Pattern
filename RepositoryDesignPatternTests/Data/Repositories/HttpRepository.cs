using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories;


public abstract class HttpRepository<TEntity> where TEntity : new()
{
    protected RestClient client;
    protected string entityEndpoint;

    public HttpRepository(string baseUrl, string entityEndpoint)
    {
        this.client = new RestClient(baseUrl);
        this.entityEndpoint = entityEndpoint;
    }

    public TEntity GetById(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Get);
        var response = client.Execute<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching entity by ID: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public List<TEntity> GetAll()
    {
        var request = new RestRequest(entityEndpoint, Method.Get);
        var response = client.Execute<List<TEntity>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching entities: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public TEntity Create(TEntity entity)
    {
        var request = new RestRequest(entityEndpoint, Method.Post);
        request.AddBody(entity, ContentType.Json);
        var response = client.Execute<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error creating entity: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public TEntity Update(int id, TEntity entity)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Put);
        request.AddBody(entity, ContentType.Json);
        var response = client.Execute<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error updating entity: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public void Delete(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Delete);
        var response = client.Execute(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error deleting entity: {response.ErrorMessage}");
        }
    }

    // Implement other necessary methods (e.g., aggregate queries) as needed
}

