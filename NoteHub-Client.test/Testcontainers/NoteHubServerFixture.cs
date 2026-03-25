using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace NoteHub_Client.test.Testcontainers
{
    public class NoteHubServerFixture : IAsyncLifetime
    {
        const string DATABASE_NAME = "notehubDB";
        const string DATABASE_USER = "notehub";
        const string DATABASE_PASSWORD = "password";

        private readonly INetwork network;
        private readonly PostgreSqlContainer db;
        private readonly IContainer api;

        public NoteHubServerFixture()
        {
            network = new NetworkBuilder()
                .WithName("notehub_testing")
                .WithCleanUp(true)
                .Build();

            db = new PostgreSqlBuilder("postgres:latest")
                .WithAutoRemove(true)
                .WithNetwork(network)
                .WithDatabase(DATABASE_NAME)
                .WithUsername(DATABASE_USER)
                .WithPassword(DATABASE_PASSWORD)
                .Build();

            api = new ContainerBuilder("ghcr.io/maui-si-ios/notehub-server:latest")
                .WithImagePullPolicy(_ => true)
                .WithAutoRemove(true)
                .WithNetwork(network)
                .WithEnvironment("POSTGRES_HOST", db.Hostname)
                .WithEnvironment("POSTGRES_DB", DATABASE_NAME)
                .WithEnvironment("POSTGRES_USER", DATABASE_USER)
                .WithEnvironment("POSTGRES_PASSWORD", DATABASE_PASSWORD)
                .WithPortBinding(8080, true)
                .DependsOn(db)
                .Build();
        }

        public async ValueTask DisposeAsync()
        {
            await api.DisposeAsync();
            await db.DisposeAsync();
            await network.DisposeAsync();
        }

        public async ValueTask InitializeAsync()
        {
            await network.CreateAsync();
            await db.StartAsync();
            await api.StartAsync();
        }

        public string ApiBaseUrl => $"http://localhost:{api.GetMappedPublicPort(8080)}";
    }
}
