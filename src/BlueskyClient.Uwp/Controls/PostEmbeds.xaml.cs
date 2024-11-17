using Bluesky.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
}
