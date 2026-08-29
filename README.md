[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.blazoroutputinvoker/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.blazoroutputinvoker/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.blazoroutputinvoker.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.blazoroutputinvoker/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.blazoroutputinvoker/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.blazoroutputinvoker/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.BlazorOutputInvoker

A generic invoker to simplify JavaScript to C# interaction that allows for an input and output, providing two-way communication with invocations.

## Install

```bash
dotnet add package Soenneker.Blazor.Utils.BlazorOutputInvoker
```

## Quick start

```csharp
using Soenneker.Blazor.Utils.BlazorOutputInvoker.Abstract;

IBlazorOutputInvoker<TInput, TOutput> blazorOutputInvoker = /* resolve from DI */;
var result = await blazorOutputInvoker.InvokeWithOutput(/* supply args */ default!);
```

Invokes the Blazor invoker.

## What you get

- `IBlazorOutputInvoker<TInput, TOutput>` — A generic invoker to simplify JavaScript to C# interaction that allows for an input and output, providing two-way communication with invocations.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IBlazorOutputInvoker<TInput, TOutput>.InvokeWithOutput(args)` | Invokes the Blazor invoker. | A `ValueTask{TOutput}` representing the asynchronous operation and containing the output result. |
