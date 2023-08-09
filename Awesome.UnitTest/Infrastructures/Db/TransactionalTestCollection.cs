namespace Awesome.UnitTest.Infrastructures.Db
{
    [CollectionDefinition("TransactionalTests")]
    public class TransactionalTestsCollection : ICollectionFixture<TransactionalTestDatabaseFixture>
    {
    }
}
