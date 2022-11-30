using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Utilities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IntlTelInputBlazor
{
    public partial class MudIntlTelInput<T> : MudDebouncedInput<T>
    {
        protected string Classname =>
           new CssBuilder("mud-input-input-control")
           .AddClass(Class)
           .AddClass("mud-input-iti")
           .Build();

        public MudInput<string> InputReference { get; private set; }

        public MudIntlTelInput() : base()
        {
            //Validation = new Func<T, bool>(ValidateInput);

            Converter = new()
            {
                SetFunc = value => value as string,
                GetFunc = text => (T)(object)(IntlTel)text,
            };
        }

        protected bool ValidateInput(T value)
        {
            return typeof(T) == typeof(IntlTel) && ((IntlTel)(object)value)?.IsValid != false;
        }

        /// <summary>
        /// Type of the input element. It should be a valid HTML5 input type.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Behavior)]
        public InputType InputType { get; set; } = InputType.Telephone;

        [CascadingParameter(Name = "SubscribeToParentForm")]
        internal bool SubscribeToParentFormEx { get; set; } = true;

        private string GetCounterText() => Counter == null ? string.Empty : (Counter == 0 ? (string.IsNullOrEmpty(Text) ? "0" : $"{Text.Length}") : ((string.IsNullOrEmpty(Text) ? "0" : $"{Text.Length}") + $" / {Counter}"));

        /// <summary>
        /// Show clear button.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Behavior)]
        public bool Clearable { get; set; } = false;

        /// <summary>
        /// Button click event for clear button. Called after text and value has been cleared.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClearButtonClick { get; set; }

        public override ValueTask FocusAsync()
        {
            return InputReference.FocusAsync();
        }

        public override ValueTask BlurAsync()
        {
            return InputReference.BlurAsync();
        }

        public override ValueTask SelectAsync()
        {
            return InputReference.SelectAsync();
        }

        public override ValueTask SelectRangeAsync(int pos1, int pos2)
        {
            return InputReference.SelectRangeAsync(pos1, pos2);
        }

        protected override void ResetValue()
        {
            InputReference.Reset();

            base.ResetValue();
        }

        /// <summary>
        /// Clear the text field, set Value to default(T) and Text to null
        /// </summary>
        /// <returns></returns>
        public Task Clear()
        {
            return InputReference.SetText(null);
        }

        /// <summary>
        /// Sets the input text from outside programmatically
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task SetText(string text)
        {
            if (InputReference != null)
                await InputReference.SetText(text);
            return;
        }
        protected override Task SetTextAsync(string text, bool updateValue = true)
        {
            return Task.CompletedTask;
        }

        [Parameter]
        public bool AllowDropDown { get; set; } = true;

        [Parameter]
        public bool AutoHideDialCode { get; set; } = true;

        [Parameter]
        public string AutoPlaceholder { get; set; } = "polite";

        [Parameter]
        public string CustomContainer { get; set; }

        [Parameter]
        public IEnumerable<string> ExcludeCountries { get; set; } = Enumerable.Empty<string>();
        [Parameter]
        public bool FormatOnDisplay { get; set; } = true;

        [Parameter]
        public string InitialCountry { get; set; }

        [Parameter]
        public Dictionary<string, string> LocalizedCountries { get; set; }

        [Parameter]
        public bool NationalMode { get; set; } = true;

        [Parameter]
        public IEnumerable<string> OnlyCountries { get; set; } = Enumerable.Empty<string>();

        [Parameter]
        public string PlaceholderNumberType { get; set; } = "MOBILE";

        [Parameter]
        public IEnumerable<string> PreferredCountries { get; set; } = new[] { RegionInfo.CurrentRegion.TwoLetterISORegionName };

        [Parameter]
        public bool SeparateDialCode { get; set; }

        [Parameter]
        public string UtilsScript { get; set; } = "./_content/IntlTelInputBlazor/js/utils.js";

        private int _inputIndex;

        private DotNetObjectReference<MudIntlTelInput<IntlTel>> dotNetHelper;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetHelper = DotNetObjectReference.Create(this) as DotNetObjectReference<MudIntlTelInput<IntlTel>>;

                _inputIndex = await _intlTelInputJsInterop.Init2(InputReference.ElementReference, dotNetHelper, new
                {
                    AllowDropDown,
                    AutoHideDialCode,
                    AutoPlaceholder,
                    CustomContainer,
                    ExcludeCountries,
                    FormatOnDisplay,
                    InitialCountry,
                    LocalizedCountries,
                    NationalMode,
                    OnlyCountries,
                    PlaceholderNumberType,
                    PreferredCountries,
                    SeparateDialCode,
                    UtilsScript
                });

                if (Value is not null)
                {
                    await _intlTelInputJsInterop.SetNumber(_inputIndex, Value.ToString());
                }
            }
        }

        [JSInvokable]
        public async Task Update()
        {
            var value = (T)(object)await _intlTelInputJsInterop.GetData(_inputIndex);

            if (value is not null)
            {
                await _intlTelInputJsInterop.SetNumber(_inputIndex, value.ToString());
            }

            await SetValueAsync(value, true);
        }

        private async Task OnInput(ChangeEventArgs e)
        {
            await Update();
        }

        protected override void Dispose(bool disposing)
        {
            dotNetHelper?.Dispose();

            base.Dispose(disposing);
        }
    }
}