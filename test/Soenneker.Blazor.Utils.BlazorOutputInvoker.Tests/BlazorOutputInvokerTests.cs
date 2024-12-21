using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.Utils.BlazorOutputInvoker.Tests;

[Collection("Collection")]
public class BlazorOutputInvokerTests : FixturedUnitTest
{
    public BlazorOutputInvokerTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
