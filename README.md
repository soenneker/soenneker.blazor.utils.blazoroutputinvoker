[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.blazoroutputinvoker/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.blazoroutputinvoker/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Utils.BlazorOutputInvoker
### A generic invoker to simplify JavaScript to C# interaction that allows for an input and output, providing two-way communication with invocations.

## Installation

```
dotnet add package Soenneker.Blazor.Utils.BlazorOutputInvoker
```

## Usage

### C#
```csharp
async ValueTask<int> YourInvokerMethod(string input)
{
    Console.Log(input); // 'Hello there'
    return 42;
}

var blazorOutputInvoker = new BlazorOutputInvoker<string, int>(YourInvokerMethod);

```

### JS

```javascript
dotnetObject.invokeMethodAsync('InvokeWithOutput', 'Hello there').then((data) => {
    console.log(data); // 42
});
```