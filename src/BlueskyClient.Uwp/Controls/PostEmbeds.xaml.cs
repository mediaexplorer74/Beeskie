using Bluesky.NET.Models;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class PostEmbeds : UserControl
{
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

    private ImageEmbed? SingleImage => Embed?.Images is [ImageEmbed image, ..]
        ? image
        : null;

    private double SingleImageMaxWidth =>
        SingleImage?.AspectRatio is { Height: double height, Width: double width } && height > width
        ? 300
        : double.PositiveInfinity;

    private bool IsMultiImageEmbed => Embed?.Images?.Length > 1;

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
}
