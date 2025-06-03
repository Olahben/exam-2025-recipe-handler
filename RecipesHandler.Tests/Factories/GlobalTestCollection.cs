using Xunit;

namespace RecipesHandler.Tests.Factories;

[CollectionDefinition("Global Collection")]
public class GlobalTestCollection : ICollectionFixture<TestFixture>
{
    // This class has no code and is never instantiated. 
    // Its purpose is to be the anchor for CollectionDefinition and ICollectionFixture.
}
