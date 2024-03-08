using RepositoryDesignPatternTests.Models;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories.NoBase;
public class CustomerRepository
{
    private RestClient client;
    private string entityEndpoint = "customers";

    public CustomerRepository(string baseUrl)
    {
        this.client = new RestClient(baseUrl);
    }

    public List<Customer> GetAll()
    {
        var request = new RestRequest(entityEndpoint, Method.Get);
        var response = client.Execute<List<Customer>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching customers: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Customer GetById(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Get);
        var response = client.Execute<Customer>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching customer by ID: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Customer Create(Customer customer)
    {
        var request = new RestRequest(entityEndpoint, Method.Post);
        request.AddJsonBody(customer);
        var response = client.Execute<Customer>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error creating customer: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public Customer Update(int id, Customer customer)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Put);
        request.AddJsonBody(customer);
        var response = client.Execute<Customer>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error updating customer: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public void Delete(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Delete);
        var response = client.Execute(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error deleting customer: {response.ErrorMessage}");
        }
    }
}
