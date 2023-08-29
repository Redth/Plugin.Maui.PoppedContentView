<!-- 
Everything in here is of course optional. If you want to add/remove something, absolutely do so as you see fit.
This example README has some dummy APIs you'll need to replace and only acts as a placeholder for some inspiration that you can fill in with your own functionalities.
-->
![](nuget.png)
# Plugin.Maui.PoppedContentView

`Plugin.Maui.PoppedContentView` is a simple control / service for presenting popup content in .NET MAUI apps.

## Install Plugin

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.PoppedContentView.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.PoppedContentView/)

Available on [NuGet](http://www.nuget.org/packages/Plugin.Maui.PoppedContentView).

Install with the dotnet CLI: `dotnet add package Plugin.Maui.PoppedContentView`, or through the NuGet Package Manager in Visual Studio.

## Setup

In your _MauiProgram.cs_ file, call the `builder.UsePopped()` method on your builder to configure the plugin.

In your app you can inject the `IPoppedNavigationService` (or get it from the service provider manually, eg: `this.Handler.MauiContext.Services.GetService<IPoppedNavigationService>();`).


## Usage

Once you have an instance of the service, you can show popups:

```csharp
await poppedNavService.ShowAsync(this.Window, new SimplePoppedView());
```

The first argument is an `IWindow` which should be the window you would like to present the popup within.  The second argument is an `IPoppedContentView`.  The easiest way to create this is to make a new control which subclasses the `PoppedContentView` implementation, eg:

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<popped:PoppedContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:popped="clr-namespace:Maui.Popped;assembly=Plugin.Maui.PoppedContentView"
    x:Class="Sample.SimplePoppedView">
    <Grid VerticalOptions="End">

        <Border VerticalOptions="End" Background="Red" StrokeShape="{RoundRectangle CornerRadius=18}" Padding="20" Margin="20">
            <Label 
                Text="Welcome to .NET MAUI!"
                VerticalOptions="Center" 
                HorizontalOptions="Center" />
        </Border>
    </Grid>
</popped:PoppedContentView>
```
