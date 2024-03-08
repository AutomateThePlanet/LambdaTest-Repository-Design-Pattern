using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using RepositoryDesignPatternTests.Data.Factories;
using RepositoryDesignPatternTests.Data.Repositories;
using WebDriverManager.DriverConfigs.Impl;

namespace RepositoryDesignPatternTests;

[TestFixture]
public class MusicShopTests
{
    private IWebDriver _driver;
    private CustomerRepository _customerRepository;

    [SetUp]
    public void TestInit()
    {
        _customerRepository = new CustomerRepository(Urls.BASE_API_URL);

        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();

        _driver.Navigate().GoToUrl(Urls.BASE_URL);
        _driver.Manage().Cookies.DeleteAllCookies();
    }

    [TearDown]
    public void TestCleanup()
    {
        _driver.Quit();
    }

    [Test]
    public void RightCustomersDisplayed_When_SearchViaUI()
    {
        // Arrange
        var customer1 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "john.doe@example.com"));
        var customer2 = _customerRepository.Create(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "jane.doe@example.net"));

        var customersTab = _driver.FindElement(By.XPath("//a[text()='Customers']"));
        customersTab.Click();

        var customersSearchInput = _driver.FindElement(By.Id("searchCustomerQuery"));
        customersSearchInput.Clear();
        customersSearchInput.SendKeys("LastName:Doe;AND;Email:.com");


        var searchButton = _driver.FindElement(By.XPath("//button[text()='Search']"));
        searchButton.Click();

        var allHeaders = _driver.FindElements(By.XPath("//tbody[@id='customerList']/preceding-sibling::thead/tr/th")).Select(x => x.Text).ToList();
        int indexOfFirstName = allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("First Name")) + 1;
        int indexOfLastName = allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("Last Name")) + 1;
        int indexOfEmail= allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("Email")) + 1;

        var allLastNames = _driver.FindElements(By.XPath($"//tbody[@id='customerList']/tr/td[{indexOfLastName}]"));
        allLastNames.ToList().ForEach(s => Debug.WriteLine(s.Text));
        var allEmails = _driver.FindElements(By.XPath($"//tbody[@id='customerList']/tr/td[{indexOfEmail}]"));

        Assert.IsTrue(allLastNames.Any(c => c.Text.Contains("Doe")));
        Assert.IsTrue(allEmails.Any(c => c.Text.Contains(".com")));

        // Cleanup
        _customerRepository.Delete(customer1.CustomerId);
        _customerRepository.Delete(customer2.CustomerId);
    }
}
