using System.Threading.Tasks;
using Microsoft.Maui.Platform;

namespace Maui.Popped;

#if !IOS && !MACCATALYST && !ANDROID && !WINDOWS
public class PoppedNavigationService : IPoppedNavigationService
{
	public Task HideAsync(IPoppedContentView content)
		=> Task.CompletedTask;

	public async Task ShowAsync(IWindow fromWindow, IPoppedContentView content)
		=> Task.CompletedTask;
}
#endif