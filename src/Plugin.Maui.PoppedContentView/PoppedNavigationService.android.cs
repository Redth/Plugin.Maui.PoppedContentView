using System.Threading.Tasks;
using Microsoft.Maui.Platform;

namespace Maui.Popped;

public class PoppedNavigationService : IPoppedNavigationService
{
    public async Task HideAsync(IPoppedContentView content)
    {
        var platformView = content?.Handler?.PlatformView as Android.Views.View;

        if (content is not null)
        {
            await content.PoppingOutAsync();
        }

        platformView?.RemoveFromParent();

        if (content?.Handler is not null)
        {
            content.Handler.DisconnectHandler();
            content.Handler = null;
        }
    }

    public async Task ShowAsync(IWindow fromWindow, IPoppedContentView content)
    {
        var activity = fromWindow.Handler?.PlatformView as Android.App.Activity;

        if (activity?.Window?.DecorView is Android.Widget.FrameLayout frameLayout)
        {
            if (fromWindow?.Handler?.MauiContext is not null)
            {
                var platformView = content.ToPlatform(fromWindow.Handler.MauiContext);

                frameLayout.AddView(platformView);

				await content.PoppingInAsync();

				if (fromWindow is Element windowElement && content is Element contentElement)
				{
					windowElement.AddLogicalChild(contentElement);
				}
			}

        }
    }
}
