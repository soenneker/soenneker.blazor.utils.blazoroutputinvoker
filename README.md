[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.blazoroutputinvoker/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.blazoroutputinvoker/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.blazoroutputinvoker/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.blazoroutputinvoker/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.BlazorOutputInvoker

An adapter that exposes a `Func<TInput, ValueTask<TOutput>>` as an instance `[JSInvokable]` method and returns the serialized result to JavaScript.

## Installation

```bash
dotnet add package Soenneker.Blazor.Utils.BlazorOutputInvoker
```

There is no service registration. Construct the callback, retain a `DotNetObjectReference`, and pass it to the JavaScript code that owns the call site.

## Usage

```csharp
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.BlazorOutputInvoker;

var invoker = new BlazorOutputInvoker<NormalizeRequest, NormalizeResult>(request =>
    ValueTask.FromResult(new NormalizeResult(request.Value.Trim(), request.Value.Length)));

DotNetObjectReference<BlazorOutputInvoker<NormalizeRequest, NormalizeResult>> reference =
    DotNetObjectReference.Create(invoker);

await module.InvokeVoidAsync("registerNormalizeCallback", reference);

public sealed record NormalizeRequest(string Value);
public sealed record NormalizeResult(string Value, int OriginalLength);
```

JavaScript awaits `InvokeWithOutput` and receives a normal object:

```javascript
let normalizeCallback;

export function registerNormalizeCallback(reference) {
    normalizeCallback = reference;
}

export function unregisterNormalizeCallback() {
    normalizeCallback = null;
}

export async function normalize(value) {
    if (!normalizeCallback)
        throw new Error("The normalize callback has not been registered.");

    const result = await normalizeCallback.invokeMethodAsync("InvokeWithOutput", { value });
    return result;
}
```

When the owner is disposed, unregister the browser callback before disposing the object reference:

```csharp
await module.InvokeVoidAsync("unregisterNormalizeCallback");
reference.Dispose();
```

## Contract and failure behavior

- `TInput` and `TOutput` pass through Blazor's System.Text.Json-based interop serialization. Use serializable DTOs with predictable JSON names; delegates, streams, cyclic graphs, and arbitrary runtime objects are not suitable return values.
- A nullable output is serialized as JavaScript `null`. Model expected failures explicitly in the output DTO when JavaScript should handle them as normal results.
- If the delegate throws, JavaScript's `invokeMethodAsync` promise rejects. Await it and handle the error; do not expose internal exception details directly to end users.
- The wrapper does not marshal onto a component renderer. A delegate that reads or changes component state should use the owning component's `InvokeAsync`.
- JavaScript cannot pass a .NET `CancellationToken` to this method. Capture an appropriate lifetime token in the delegate if the work must stop during teardown.
- The wrapper does not own the `DotNetObjectReference`. Failing to unregister and dispose it keeps the delegate and captured services or component state alive.
- Treat `TInput` as untrusted browser input. Revalidate identity, authorization, identifiers, limits, and business rules before returning sensitive data or performing work.

This adapter is intended for a single request/response callback shape. Components with several distinct operations are usually clearer with named `[JSInvokable]` instance methods.
