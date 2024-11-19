using Bluesky.NET.Models;
using BlueskyClient.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed partial class EmbeddedPost : UserControl
{
    public static readonly DependencyProperty RecordProperty = DependencyProperty.Register(
        nameof(Record), 
        typeof(FeedRecord), 
        typeof(EmbeddedPost), 
        new PropertyMetadata(null, OnRecordChanged));

    private static void OnRecordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EmbeddedPost post)
        {
            post.Bindings.Update();
        }
    }

    public EmbeddedPost()
    {
        this.InitializeComponent();
    }

    public FeedRecord? Record
    {
        get => (FeedRecord?)GetValue(RecordProperty);
        set => SetValue(RecordProperty, value);
    }

    public string Avatar => Record.SafeAvatarUrl();

    public string Handle => Record?.Author?.AtHandle ?? string.Empty;

    public string DisplayName => Record?.Author?.DisplayName ?? string.Empty;

    public string PostText => Record?.Value?.Text ?? string.Empty;

    public PostEmbed? PostEmbed => Record?.Embeds is [PostEmbed first, ..] ? first : null;
}
