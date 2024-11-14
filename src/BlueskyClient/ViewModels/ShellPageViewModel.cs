using CommunityToolkit.Mvvm.ComponentModel;
using JeniusApps.Common.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueskyClient.ViewModels;

public partial class ShellPageViewModel : ObservableObject
{
    private readonly INavigator _contentNavigator;

    public ShellPageViewModel(INavigator contentNavigator)
    {
        _contentNavigator = contentNavigator;
    }
}
