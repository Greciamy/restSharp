using FluentAssertions;
using GraphQLProductApp.Data;
using RestSharp;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RestSharpTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetWithQuerySegmentTest()
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
            // Rest client initialization
            var client = new RestClient(restClientOptions);
            // Rest request
            var request = new RestRequest("Product/GetProductById/{id}");
            request.AddUrlSegment("id", 2);
            // Perform SET operation
            var response = await client.GetAsync<Product>(request);
            // Assert
            response.Should().NotBeNull();
            response?.Price.Should().Be(400);

            testOutputHelper.WriteLine("Executing first test");
        }

        public async Task GetWithQueryParameterTest()
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
            // Rest client initialization
            var client = new RestClient(restClientOptions);
            // Rest request
            var request = new RestRequest("/Product/GetProductByIdAndName");
            request.AddQueryParameter("id", 2); ;
            request.AddQueryParameter("name", "Monitor");
            // Perform SET operation
            var response = await client.GetAsync<Product>(request);
            // Assert
            response.Should().NotBeNull();
            response?.Price.Should().Be(400);

            testOutputHelper.WriteLine("Executing first test");
        }

        public async Task PostProductTest()
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
            // Rest client initialization
            var client = new RestClient(restClientOptions);
            // Rest request
            var request = new RestRequest("/Product/Create");
            request.AddJsonBody(new Product
            {
                Name = "Cabinet",
                Description = "Gaming cabinet",
                Price = 300,
                ProductType = ProductType.PERIPHARALS
            });
            // Perform POST operation
            var response = await client.PostAsync<Product>(request);
            // Assert
            response.Should().NotBeNull();
            response?.Price.Should().Be(300);

            testOutputHelper.WriteLine("Executing first test");
        }
    }
}