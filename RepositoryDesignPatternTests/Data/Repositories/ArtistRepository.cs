namespace RepositoryDesignPatternTests.Data.Repositories;

using RepositoryDesignPatternTests.Models;
using RestSharp;
using System.Collections.Generic;

public class ArtistRepository : HttpRepository<Artist, List<Artist>>
{
    public ArtistRepository(string baseUrl)
        : base(baseUrl, "artists")
    {
    }

    public List<Artist> SearchByName(string name)
    {
        var request = new RestRequest($"{entityEndpoint}/search/{name}", Method.Get);
        var response = client.Execute<List<Artist>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error searching artists by name: {response.ErrorMessage}");
        }

        return response.Data;
    }
}

