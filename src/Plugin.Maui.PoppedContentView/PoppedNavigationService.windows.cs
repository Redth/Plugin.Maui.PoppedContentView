using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Maui.Popped;

public class PoppedNavigationService : IPoppedNavigationService
{
	Dictionary<IPoppedContentView, (Popup Popup, Microsoft.UI.Xaml.Window XamlWindow, Windows.Foundation.TypedEventHandler<object, WindowSizeChangedEventArgs> WindowSizeHandler)> popped = new();

	public async Task HideAsync(IPoppedContentView content)
	{
		if (popped.TryGetValue(content, out var info) && content is not null)
		{
			await content.PoppingOutAsync();

			info.Popup.Child = null;
			info.Popup.IsOpen = false;

			popped.Remove(content);

			info.XamlWindow.SizeChanged -= info.WindowSizeHandler;

			content.Handler?.DisconnectHandler();
			content.Handler = null;
		}
	}

	public async Task ShowAsync(IWindow fromWindow, IPoppedContentView content)
	{
		var window = fromWindow.Handler?.PlatformView as Microsoft.UI.Xaml.Window;

		if (window is not null)
		{
			if (fromWindow?.Handler?.MauiContext is not null)
			{
				var platformView = content.ToPlatform(fromWindow.Handler.MauiContext);

				window.SizeChanged += Window_SizeChanged;

				void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
				{
					platformView.Width = window.Bounds.Width;
					platformView.Height = window.Bounds.Height;
				}

				platformView.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
				platformView.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch;
				platformView.Width = window.Bounds.Width;
				platformView.Height = window.Bounds.Height;

				var popup = new Popup();
				popup.Child = platformView;
				popup.XamlRoot = window.Content.XamlRoot;

				popup.IsOpen = true;
				
				platformView.InvalidateMeasure();
				popup.InvalidateMeasure();

				await content.PoppingInAsync();

				popped.Add(content, (popup, window, Window_SizeChanged));

				if (fromWindow is Element windowElement && content is Element contentElement)
				{
					windowElement.AddLogicalChild(contentElement);
				}
			}

		}
	}
}
