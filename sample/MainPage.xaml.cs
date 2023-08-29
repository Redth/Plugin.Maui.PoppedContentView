namespace Sample;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

		PoppedNavigation = MauiProgram.Services.GetService<Maui.Popped.IPoppedNavigationService>();

	}

	readonly Maui.Popped.IPoppedNavigationService PoppedNavigation;


    private async void OnCounterClicked(object sender, EventArgs e)
	{
		await PoppedNavigation.ShowAsync(this.Window, new SimplePoppedView(this.Window));
	}
}


