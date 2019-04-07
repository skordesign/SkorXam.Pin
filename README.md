# SkorXam.Pin
PinView for Xamarin.Forms
### Installation
Nuget: [SkorXam.Pin](https://www.nuget.org/packages/SkorXam.Pin/)
### Usage
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage ...
             xmlns:pin="clr-namespace:SkorXam.Pin;assembly=SkorXam.Pin"
             ...>
    ...
    
    <pin:PinView VerticalOptions="FillAndExpand"
                 HorizontalOptions="Fill"
                 ButtonColor="Azure"
                 PinChanged="PinView_PinChanged"
                 PinSubmitCommand="{Binding PinSubmit}"
                 Pin="{Binding Pin}"
                 ButtonTextColor="Red"/>
</ContentPage>

```

#### Reference 
It dependent to: [Plugin.CrossPlatformTintedImage](https://github.com/shrutinambiar/xamarin-forms-tinted-image)

#### Demo
![Demo](Demo.png)

##### Note: If you have any issue with this package, feel free to create issue on this repository.