using BlueskyClient.Controls;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services.Uwp;

public sealed class DialogService : IDialogService
{
    private bool _dialogOpen;

    public async Task OpenPostDialogAsync()
    {
        if (_dialogOpen)
        {
            return;
        }

        _dialogOpen = true;

        var dialog = new NewPostDialog();
        dialog.Initialize();
        await dialog.ShowAsync();

        _dialogOpen = false;
    }
}
