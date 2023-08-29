namespace Maui.Popped;

public interface IPoppedNavigationService
{
    Task ShowAsync(IWindow fromWindow, IPoppedContentView content);
    Task HideAsync(IPoppedContentView content);
}
