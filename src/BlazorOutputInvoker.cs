using Soenneker.Blazor.Utils.BlazorOutputInvoker.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System;
using Microsoft.JSInterop;
using System.Threading;

namespace Soenneker.Blazor.Utils.BlazorOutputInvoker;

///<inheritdoc cref="IBlazorOutputInvoker{TInput,TOutput}"/>
public class BlazorOutputInvoker<TInput, TOutput> : IBlazorOutputInvoker<TInput, TOutput>
{
    private readonly Func<TInput, CancellationToken, ValueTask<TOutput>> _func;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorOutputInvoker{TInput,TOutput}"/> class.
    /// </summary>
    /// <param name="invoker">The invoker function.</param>
    [DynamicDependency(nameof(InvokeWithOutput))]
    public BlazorOutputInvoker(Func<TInput, CancellationToken, ValueTask<TOutput>> invoker)
    {
        _func = invoker;
    }

    [JSInvokable(nameof(InvokeWithOutput))]
    public ValueTask<TOutput> InvokeWithOutput(TInput args, CancellationToken cancellationToken = default)
    {
        return _func(args, cancellationToken);
    }
}