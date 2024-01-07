using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IntlTelInputBlazor
{
    public class IntlTelInputJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private IJSObjectReference _module;

        public IntlTelInputJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/IntlTelInputBlazor/js/intlTelInputInterop.js").AsTask());
        }

        public async ValueTask<int> Init(ElementReference reference, DotNetObjectReference<IntlTelInput> dotNetHelper, object options)
        {
            _module = await _moduleTask.Value;
            return await _module.InvokeAsync<int>("init", reference, dotNetHelper, options);
        }

        public async ValueTask<int> Init2(ElementReference reference, DotNetObjectReference<MudIntlTelInput<IntlTel>> dotNetHelper, object options)
        {
            _module = await _moduleTask.Value;
            return await _module.InvokeAsync<int>("init", reference, dotNetHelper, options);
        }

        public async ValueTask<IntlTel> GetData(int inputIndex)
        {
            return await _module.InvokeAsync<IntlTel>("get", inputIndex);
        }

        public async ValueTask SetNumber(int id, string number)
        {
            await _module.InvokeVoidAsync("setNumber", id, number);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}