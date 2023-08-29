using Microsoft.Maui.Handlers;

namespace Maui.Popped;

public class PoppedContent : ContentView, IPoppedContentView
{
    public virtual Task PoppingInAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task PoppingOutAsync()
    {
        return Task.CompletedTask;
    }
}
