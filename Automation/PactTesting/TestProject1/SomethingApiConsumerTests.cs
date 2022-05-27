using System;
using Xunit;

namespace TestProject1
{
    public class SomethingApiConsumerTests
    {
        private readonly IPactBuilderV3 _pactBuilder;

        public SomethingApiConsumerTests(ITestOutputHelper output)
        {
            // Use default pact directory ..\..\pacts and default log
            // directory ..\..\logs
            var pact = Pact.V3("Something API Consumer", "Something API");

            // or specify custom configuration such as pact file directory and serializer settings
            pact = Pact.V3("Something API Consumer", "Something API", new PactConfig
            {
                PactDir = @"..\pacts",
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            });

            // Initialize Rust backend
            _pactBuilder = pact.UsingNativeBackend();
        }

        [Fact]
        public async Task GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
        {
            // Arrange
            _pactBuilder
                .UponReceiving("A GET request to retrieve the something")
                    .Given("There is a something with id 'tester'")
                    .WithRequest(HttpMethod.Get, "/somethings/tester")
                    .WithHeader("Accept", "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(new
                    {
                    // NOTE: These properties are case sensitive!
                    id = "tester",
                        firstName = "Totally",
                        lastName = "Awesome"
                    });

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new SomethingApiClient(ctx.MockServerUri);
                var something = await client.GetSomething("tester");

                // Assert
                Assert.Equal("tester", something.Id);
            });
        }
    }
}
