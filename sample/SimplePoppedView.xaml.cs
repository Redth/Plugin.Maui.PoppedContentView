using Maui.Popped;
using Microsoft.Maui.Controls;

namespace Sample;

public partial class SimplePoppedView : Maui.Popped.PoppedContentView
{
	public SimplePoppedView(IWindow window)
	{
		InitializeComponent();

        TheWindow = window;
	}

    public readonly IWindow TheWindow;

    public override Task PoppingInAsync()
    {
        var contentSize = this.Content.Measure(TheWindow.Width, TheWindow.Height, MeasureFlags.IncludeMargins);

        var contentHeight = contentSize.Request.Height; // (this.TheWindow?.Height ?? 0);

        this.Content.TranslationY = contentHeight;
        
        this.Animate("BG", v =>
        {
            this.Background = new SolidColorBrush(Colors.Black.WithAlpha((float)v));

        }, 0d, 0.7d, 32, 350, easing: Easing.CubicOut, finished: (v, k) =>
        {
            this.Background = new SolidColorBrush(Colors.Black.WithAlpha(0.7f));
        });

        this.Animate("TL", v =>
        {
            var t = (contentHeight - v);
            this.Content.TranslationY = (int)t;
        }, 0, contentHeight, length: 500, easing: Easing.CubicInOut, finished: (v, k) =>
        {
            this.Content.TranslationY = 0;
        });


        return Task.CompletedTask;
    }

    public override Task PoppingOutAsync()
    {
        var done = new TaskCompletionSource();

        var contentSize = this.Content.Measure(TheWindow.Width, TheWindow.Height, MeasureFlags.IncludeMargins);

        var windowHeight = contentSize.Request.Height; // (this.TheWindow?.Height ?? 0);

        this.Animate("BG", v =>
        {
            this.Background = new SolidColorBrush(Colors.Black.WithAlpha((float)v));

        }, 0.7d, 0d, 32, 350, easing: Easing.CubicIn, finished: (v, k) =>
        {
            this.Background = new SolidColorBrush(Colors.Black.WithAlpha(0.0f));
        });

        this.Animate("TL", v =>
        {
            var t = (windowHeight - v);
            Console.WriteLine("TranslationY : " + t);

            this.Content.TranslationY = (int)t;
        }, windowHeight, 0, length: 500, easing: Easing.CubicInOut, finished: (v, k) =>
        {
            this.Content.TranslationY = windowHeight;
            done.TrySetResult();
        });

        return done.Task;
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        var nav = this.Handler.MauiContext.Services.GetRequiredService<IPoppedNavigationService>();

        nav.HideAsync(this);
    }
}
