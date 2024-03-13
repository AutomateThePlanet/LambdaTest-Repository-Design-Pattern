using RepositoryDesignPatternTests.Models;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories.NoBase;
public class ArtistRepository
{
    private RestClient client;
    private string entityEndpoint = "artists";

    public ArtistRepository(string baseUrl)
    {
        this.client = new RestClient(baseUrl);
    }

    public List<Artist> GetAll()
    {
        var request = new RestRequest(entityEndpoint, Method.Get);
        var response = client.Execute<List<Artist>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching customers: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Artist GetById(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Get);
        var response = client.Execute<Artist>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching customer by ID: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Artist Create(Artist artist)
    {
        var request = new RestRequest(entityEndpoint, Method.Post);
        request.AddJsonBody(artist);
        var response = client.Execute<Artist>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error creating artist: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Artist Update(int id, Artist artist)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Put);
        request.AddJsonBody(artist);
        var response = client.Execute<Artist>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error updating artist: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public void Delete(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Delete);
        var response = client.Execute(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error deleting artist: {response.ErrorMessage}");
        }
    }
}
