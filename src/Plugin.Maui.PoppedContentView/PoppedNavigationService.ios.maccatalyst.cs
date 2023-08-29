using CoreGraphics;
using UIKit;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Maui.Popped;

public class PoppedNavigationService : IPoppedNavigationService
{
    Dictionary<IPoppedContentView, PoppedContentViewController> popped = new();

    public async Task HideAsync(IPoppedContentView content)
    {
        if (popped.TryGetValue(content, out var vc) && content is not null)
        {
            await content.PoppingOutAsync();

            await vc.DismissViewControllerAsync(false);
            popped.Remove(content);

            vc.Dispose();
            content.Handler?.DisconnectHandler();
            content.Handler = null;
        }
    }

    public async Task ShowAsync(IWindow fromWindow, IPoppedContentView content)
    {
        var uiWindow = fromWindow?.Handler?.PlatformView as UIWindow;

        if (fromWindow?.Handler?.MauiContext is not null && uiWindow is not null)
        {
            var vc = new PoppedContentViewController(fromWindow.Handler.MauiContext, content);
            vc.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;

            if (uiWindow.RootViewController is not null)
			{
				await uiWindow.RootViewController.PresentViewControllerAsync(vc, false);
			}

			popped.Add(content, vc);

            if (fromWindow is Element windowElement && content is Element contentElement)
            {
                windowElement.AddLogicalChild(contentElement);
            }
        }
    }
}

