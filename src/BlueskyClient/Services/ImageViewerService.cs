using Bluesky.NET.Models;
using System;
using System.Collections.Generic;

namespace BlueskyClient.Services;

public sealed class ImageViewerService : IImageViewerService
{
    public event EventHandler<ImageViewerArgs>? ImageViewerRequested;

    public void RequestImageViewer(IReadOnlyList<ImageEmbed> images, int launchIndex = 0)
    {
        if (images.Count == 0)
        {
            return;
        }

        ImageViewerRequested?.Invoke(this, new ImageViewerArgs(images, launchIndex));
    }
}
