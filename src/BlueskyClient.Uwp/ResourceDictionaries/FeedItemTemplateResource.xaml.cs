// Required to support xbind
// in resource dictionaries.
// Ref: https://docs.microsoft.com/en-us/windows/uwp/data-binding/data-binding-in-depth#resource-dictionaries-with-xbind

#nullable enable

namespace BlueskyClient.ResourceDictionaries;

public sealed partial class FeedItemTemplateResource
{
    public FeedItemTemplateResource()
    {
        this.InitializeComponent();
    }
}
