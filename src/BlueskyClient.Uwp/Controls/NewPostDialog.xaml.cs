using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class NewPostDialog : ContentDialog
{
    public NewPostDialog()
    {
        this.InitializeComponent();
    }

    public void Initialize()
    {
        _ = NewPostControl.ViewModel.InitializeAsync().ConfigureAwait(false);
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var d = args.GetDeferral();
        await NewPostControl.ViewModel.SubmitCommand.ExecuteAsync(null);
        d.Complete();
    }
}
