using UIKit;
using Microsoft.Maui.Platform;

namespace Maui.Popped;

class PoppedContentViewController : UIViewController
{
    public PoppedContentViewController(IMauiContext context, IPoppedContentView content)
        : base()
    {
        Content = content;

        this.View = content.ToPlatform(context);
    }

    public readonly IPoppedContentView Content;

    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);

        _ = Content.PoppingInAsync();
    }
}

