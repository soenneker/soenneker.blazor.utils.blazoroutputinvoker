using System.Threading.Tasks;

namespace Soenneker.Blazor.Utils.BlazorOutputInvoker.Abstract;

/// <summary>
/// Wraps a value-task callback with input and output in an instance method that can be invoked through Blazor JavaScript interop.
/// </summary>
public interface IBlazorOutputInvoker<in TInput, TOutput>
{
    /// <summary>
    /// Invokes the configured callback and returns its result to the caller.
    /// </summary>
    /// <param name="args">The input argument.</param>
    /// <returns>A <see cref="ValueTask{TOutput}"/> representing the asynchronous operation and containing the output result.</returns>
    ValueTask<TOutput> InvokeWithOutput(TInput args);
}
