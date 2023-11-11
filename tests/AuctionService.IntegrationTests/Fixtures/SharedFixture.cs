namespace AuctionService.IntegrationTests;
//to share CustomWebAppFactory across tests class so it wont create multiple containers
[CollectionDefinition("Shared Collection")]
public class SharedFixture : ICollectionFixture<CustomWebAppFactory>
{

}
