using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    public class SomethingApiFixture : IDisposable
    {
        private readonly IHost server;
        public Uri ServerUri { get; }

        public SomethingApiFixture()
        {
            ServerUri = new Uri("http://localhost:9222");
            server = Host.CreateDefaultBuilder()
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseUrls(ServerUri.ToString());
                            webBuilder.UseStartup<TestStartup>();
                        })
                        .Build();
            server.Start();
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }

    public class SomethingApiTests : IClassFixture<SomethingApiFixture>
    {
        private readonly SomethingApiFixture fixture;
        private readonly ITestOutputHelper output;

        public SomethingApiTests(SomethingApiFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.output = output;
        }

        [Fact]
        public void EnsureSomethingApiHonoursPactWithConsumer()
        {
            //Arrange
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
            {
                // NOTE: PactNet defaults to a ConsoleOutput, however
                // xUnit 2 does not capture the console output, so this
                // sample creates a custom xUnit outputter. You will
                // have to do the same in xUnit projects.
                new XUnitOutput(output),
            },
            };

            string pactPath = Path.Combine("..",
                                            "..",
                                            "path",
                                            "to",
                                            "pacts",
                                            "Something API Consumer-Something API.json");

            // Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("Something API", fixture.ServerUri)
                .WithFileSource(new FileInfo(pactPath))
                .WithProviderStateUrl(new Uri(fixture.ServerUri, "/provider-states"))
                .Verify();
        }
    }
}
