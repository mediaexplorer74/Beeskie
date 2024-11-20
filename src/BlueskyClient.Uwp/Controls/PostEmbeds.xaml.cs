using Bluesky.NET.Models;
using BlueskyClient.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class PostEmbeds : UserControl
{
    private readonly IImageViewerService _imageViewerService;

    public static readonly DependencyProperty EmbedProperty = DependencyProperty.Register(
        nameof(Embed),
        typeof(PostEmbed),
        typeof(PostEmbeds),
        new PropertyMetadata(null, OnEmbedChanged));

    private static void OnEmbedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PostEmbeds p)
        {
            p.UpdateBindings();
        }
    }

    public PostEmbeds()
    {
        this.InitializeComponent();
        _imageViewerService = App.Services.GetRequiredService<IImageViewerService>();
    }

    public PostEmbed? Embed
    {
        get => (PostEmbed?)GetValue(EmbedProperty);
        set => SetValue(EmbedProperty, value);
    }

    private bool IsExternalUrl => Embed?.External?.Uri is not null;

    private string ExternalThumb => Embed?.External?.Thumb ?? "http://localhost";

    private string ExternalTitle => Embed?.External?.Title ?? string.Empty;

    private string ExternalDescription => Embed?.External?.Description ?? string.Empty;

    private bool IsSingleImageEmbed => Embed?.Images?.Length == 1;

    private bool IsMultiImageEmbed => Embed?.Images?.Length > 1;

    public IReadOnlyList<ImageEmbed> MultiImages => Embed?.Images ?? [];

    private ImageEmbed? SingleImage => Embed?.Images is [ImageEmbed image, ..]
        ? image
        : null;

    private double SingleImageMaxWidth =>
        SingleImage?.AspectRatio is { Height: double height, Width: double width } && height > width
        ? 300
        : double.PositiveInfinity;

    private void UpdateBindings()
    {
        this.Bindings.Update();
    }

    private async void OnExternalUrlClicked(object sender, RoutedEventArgs e)
    {
        if (Embed?.External?.Uri is string { Length: > 0 } uri &&
            Uri.TryCreate(uri, UriKind.Absolute, out Uri result))
        {
            await Launcher.LaunchUriAsync(result);
        }
    }

    private void OnGridViewImageClicked(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is not ImageEmbed imageClicked)
        {
            return;
        }

        var index = 0;

        foreach (var image in MultiImages)
        {
            if (image == imageClicked)
            {
                break;
            }

            index++;
        }

        if (index < MultiImages.Count)
        {
            _imageViewerService.RequestImageViewer(MultiImages, launchIndex: index);
        }
    }

    private void OnSingleImageClicked(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        if (SingleImage is { } image)
        {
            _imageViewerService.RequestImageViewer([image]);
        }
    }
}
