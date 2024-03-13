using RepositoryDesignPatternTests.Models;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class CustomerRepository : HttpRepository<Customer>
{
    public CustomerRepository(string baseUrl)
        : base(baseUrl, "customers")
    {
    }

    // Method for searching customers by name
    public List<Customer> Search(string searchTerm)
    {
        var request = new RestRequest($"{entityEndpoint}/search/{searchTerm}", Method.Get);
        var response = client.Execute<List<Customer>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error searching customers by name: {response.ErrorMessage}");
        }

        return response.Data;
    }
}
