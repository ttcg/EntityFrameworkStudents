namespace Students.IntegrationTests
{
    [CollectionDefinition(TestCollections.SqlIntegration)]
    public class SqlCollection : ICollectionFixture<IntegrationTestFactory> { }

    public static class TestCollections
    {
        public const string SqlIntegration = "SQL Integration Collection";
    }
}
