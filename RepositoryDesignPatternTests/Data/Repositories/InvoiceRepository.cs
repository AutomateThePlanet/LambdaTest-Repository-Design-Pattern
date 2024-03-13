using RepositoryDesignPatternTests.Models;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class InvoiceRepository : HttpRepository<Invoice>
{
    public InvoiceRepository(string baseUrl)
        : base(baseUrl, "invoices")
    {
    }

    public List<InvoiceItem> GetInvoiceItems(int invoiceId)
    {
        var request = new RestRequest($"{entityEndpoint}/{invoiceId}/invoiceitems", Method.Get);
        var response = client.Execute<List<InvoiceItem>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching invoice items: {response.ErrorMessage}");
        }

        return response.Data;
    }
}
