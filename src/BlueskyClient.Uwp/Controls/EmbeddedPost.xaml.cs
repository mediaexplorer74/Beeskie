using Bluesky.NET.Models;
using BlueskyClient.Extensions;
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
}
