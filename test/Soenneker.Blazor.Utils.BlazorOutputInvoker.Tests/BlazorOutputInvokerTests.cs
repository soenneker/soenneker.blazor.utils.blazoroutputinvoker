using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Utils.BlazorOutputInvoker.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BlazorOutputInvokerTests : HostedUnitTest
{
    public BlazorOutputInvokerTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
