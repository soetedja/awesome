namespace Awesome.Test.Infrastructures.Db
{
    [CollectionDefinition("TransactionalTests")]
    public class TransactionalTestsCollection : ICollectionFixture<TransactionalTestDatabaseFixture>
    {
    }
}
