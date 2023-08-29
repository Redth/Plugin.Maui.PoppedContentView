namespace Maui.Popped;

public interface IPoppedContentView : IView
{
    Task PoppingInAsync();

    Task PoppingOutAsync();
}
