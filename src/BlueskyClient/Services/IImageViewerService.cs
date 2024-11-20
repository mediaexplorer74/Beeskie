using Bluesky.NET.Models;
using System;
using System.Collections.Generic;

namespace BlueskyClient.Services;

public interface IImageViewerService
{
    event EventHandler<ImageViewerArgs>? ImageViewerRequested;

    void RequestImageViewer(IReadOnlyList<ImageEmbed> images, int launchIndex = 0);
}

public class ImageViewerArgs(IReadOnlyList<ImageEmbed> images, int launchIndex = 0) : EventArgs
{
    public IReadOnlyList<ImageEmbed> Images { get; } = images;

    public int LaunchIndex { get; } = launchIndex;
}