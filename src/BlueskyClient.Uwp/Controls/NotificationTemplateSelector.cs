using Bluesky.NET.Constants;
using BlueskyClient.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient.Controls;

public sealed class NotificationTemplateSelector : DataTemplateSelector
{
    public DataTemplate? Fallback { get; set; }
    public DataTemplate? Follow { get; set; }
    public DataTemplate? Reply { get; set; }
    public DataTemplate? LikeRepost { get; set; }

    protected override DataTemplate? SelectTemplateCore(object item)
    {
        if (item is not NotificationViewModel vm)
        {
            return Fallback;
        }

        return vm.Reason switch
        {
            ReasonConstants.Like => LikeRepost,
            ReasonConstants.Repost => LikeRepost,
            ReasonConstants.Follow => Follow,
            ReasonConstants.Reply => Reply,
            _ => Fallback,
        } ?? Fallback;
    }
}
