using BlueskyClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class ProfileControl : UserControl
{
    public ProfileControl()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<ProfileControlViewModel>();
    }
    
    public ProfileControlViewModel ViewModel { get; }
}
