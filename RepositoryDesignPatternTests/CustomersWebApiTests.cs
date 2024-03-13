using RepositoryDesignPatternTests.Data.Factories;
using RepositoryDesignPatternTests.Data.Repositories;

namespace RepositoryDesignPatternTests;

[TestFixture]
public class CustomersWebApiTests
{
    private CustomerRepository _customerRepository;

    [SetUp]
    public void Setup()
    {
        _customerRepository = new CustomerRepository(Urls.BASE_API_URL);
    }

    [Test]
    public void CreateCustomer_ShouldReturnValidCustomer()
    {
        // Arrange
        var fakeCustomer = CustomerFactory.GenerateCustomer();

        // Act
        var createdCustomer = _customerRepository.Create(fakeCustomer);

        // Assert
        Assert.IsNotNull(createdCustomer);
        Assert.That(createdCustomer.FirstName, Is.EqualTo(fakeCustomer.FirstName));
        // Add more assertions as needed

        // Cleanup
        _customerRepository.Delete(createdCustomer.CustomerId);
    }

    [Test]
    public void GetCustomerById_ShouldReturnCustomer()
    {
        // Arrange
        var fakeCustomer = CustomerFactory.GenerateCustomer();
        var createdCustomer = _customerRepository.Create(fakeCustomer);

        // Act
        var fetchedCustomer = _customerRepository.GetById(createdCustomer.CustomerId);

        // Assert
        Assert.IsNotNull(fetchedCustomer);
        Assert.That(fetchedCustomer.FirstName, Is.EqualTo(createdCustomer.FirstName));

        // Cleanup
        _customerRepository.Delete(createdCustomer.CustomerId);
    }

    // Implement UpdateCustomer and DeleteCustomer tests similarly

    [Test]
    public void SearchCustomers_WithANDQuery_ShouldReturnCorrectResults()
    {
        // Arrange
        var customer1 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "john.doe@example.com"));
        var customer2 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "jane.doe@example.net"));

        // Act
        var results = _customerRepository.Search("LastName:Doe AND Email:.com");

        // Assert
        Assert.IsTrue(results.Any(c => c.Email.EndsWith(".com") && c.LastName == "Doe"));

        // Cleanup
        _customerRepository.Delete(customer1.CustomerId);
        _customerRepository.Delete(customer2.CustomerId);
    }

    [Test]
    public void SearchCustomers_WithORQuery_ShouldReturnCorrectResults()
    {
        // Arrange
        var customer1 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName:"Doe", email: "example@example.com"));
        var customer2 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName: "Smith", email: "example@example.com"));

        // Act
        var results = _customerRepository.Search("LastName:Doe;OR;LastName:Smith");

        // Assert
        Assert.IsTrue(results.Count >= 2); // Assuming these are the only Doe or Smith in DB

        // Cleanup
        _customerRepository.Delete(customer1.CustomerId);
        _customerRepository.Delete(customer2.CustomerId);
    }

    [Test]
    public void SearchCustomers_WithNORQuery_ShouldReturnCorrectResults()
    {
        // Arrange - Ensure test data that does not match NOR condition exists

        // Act
        var results = _customerRepository.Search("NOR;LastName:Doe,LastName:Smith");

        // Assert
        Assert.IsFalse(results.Any(c => c.LastName == "Doe" || c.LastName == "Smith"));

        // No specific cleanup needed if no new data was created for this test
    }

    [Test]
    public void SearchCustomers_WithBoundaryValue_ShouldBehaveAsExpected()
    {
        // Arrange - Consider creating a customer with minimal attributes to test boundary values

        // Act
        var resultsWithEmptySearch = _customerRepository.Search("");

        // Assert
        Assert.IsNotEmpty(resultsWithEmptySearch); // Assuming an empty query returns all customers

        // No specific cleanup needed if no new data was created for this test
    }



}
