using BlueskyClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class NewPostControl : UserControl
{
    public NewPostControl()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<NewPostViewModel>();
    }

    public NewPostViewModel ViewModel { get; }
}
