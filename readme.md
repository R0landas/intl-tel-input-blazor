# International Telephone Input Blazor wrapper
[International Telephone Input](https://github.com/jackocnr/intl-tel-input) js library wrapper for [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) serverside and clientside. Most of the features are supported, but there are some that I have not implemented! If you would like to add them feel free to do so.


### Not supported features:
* Custom dropdownContainer
* CustomPlaceholder
* GeoIpLookup
* hiddenInput
* Setters for properties that are configured during initialization
* And some more advanced features such as events


## Installation
1. Add the nuget package
1. Add JS (just copy this)
   ```html
    <script src="./_content/IntlTelInputBlazor/js/intlTelInput.js"></script>
   ```
1. Link CSS (just copy this)
   ```html
    <link rel="stylesheet" href="./_content/IntlTelInputBlazor/css/intlTelInput.css">
    ```
1. Register dependencies using IServiceCollection.RegisterIntlTelInput();
   ```c#
   builder.Services.RegisterIntlTelInput();
   ```
1. Profit $$$

## Validation
A custom validation attribute is included, see the example below. It is important to note that the included validation attribute works by calling the International Telephone Input library. More specifically isValidNumber method.

## Example
![img.png](img.png)

```html
<EditForm EditContext="_editContext" OnValidSubmit="OnValidSubmit">
<DataAnnotationsValidator/>
<ValidationSummary/>

    <label>Nr. 1:</label>
    <IntPhoneNumberInput @bind-Value="_model.IntTelNumber"/>
    
    <label>Nr. 2:</label>
    <IntPhoneNumberInput @bind-Value="_model.IntTelNumber2"/>
    <button class="btn-primary">Submit</button>
</EditForm>
```
```c#
@code
{
   NumberModel _model = new NumberModel();
   EditContext _editContext;
   
   protected override void OnInitialized()
   {
      _editContext = new EditContext(_model);
   }
   
   private void OnValidSubmit()
   {
      var tel1 = _model.IntTelNumber.Number;
      var tel2 = _model.IntTelNumber2.Number;
      Console.WriteLine($"Number 1: {tel1}; Number 2: {tel2}");
   }
}

public class NumberModel
{
   [IntlTelephone(ErrorMessage = "Tel. 1 incorrect format")]
   public IntlTel IntTelNumber { get; set; }
   
   [IntlTelephone(ErrorMessage = "Tel. 2 incorrect format")]
   public IntlTel IntTelNumber2 { get; set; }
}
```