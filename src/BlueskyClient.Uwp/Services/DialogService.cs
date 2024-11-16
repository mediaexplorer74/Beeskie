using Bluesky.NET.Models;
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

    public async Task OpenReplyDialogAsync(FeedPost target)
    {
        if (_dialogOpen)
        {
            return;
        }

        _dialogOpen = true;

        var dialog = new NewPostDialog();
        dialog.Initialize(target);
        await dialog.ShowAsync();

        _dialogOpen = false;
    }
}
