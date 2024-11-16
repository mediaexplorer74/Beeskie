using Bluesky.NET.Models;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class NewPostViewModel : ObservableObject
{
    private readonly IProfileService _profileService;
    private readonly IPostSubmissionService _postSubmissionService;

    public NewPostViewModel(
        IProfileService profileService,
        IPostSubmissionService postSubmissionService)
    {
        _profileService = profileService;
        _postSubmissionService = postSubmissionService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Avatar))]
    [NotifyPropertyChangedFor(nameof(Handle))]
    private Author? _author;

    public string Avatar => Author?.Avatar is string { Length: > 0 } avatarUri && Uri.IsWellFormedUriString(avatarUri, UriKind.Absolute)
        ? avatarUri
        : "http://localhost";

    public string Handle => Author?.AtHandle ?? string.Empty;

    [ObservableProperty]
    private string _inputText = string.Empty;

    public async Task InitializeAsync()
    {
        Author = await _profileService.GetCurrentUserAsync();
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        var input = InputText.Trim();
        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        await _postSubmissionService.SubmitPostAsync(input).ConfigureAwait(false);
    }
}
