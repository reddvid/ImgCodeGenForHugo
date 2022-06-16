using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Imager.MAUI.ViewModels;

public class ShortcodesViewModel : INotifyPropertyChanged
{
    public ShortcodesViewModel()
    {

    }

    private string _tweetUrl;
    public String TweetUrl
    {
        get => _tweetUrl;
        set
        {
            if (value != null)
            {
                _tweetUrl = value;
                NotifyPropertyChanged("TweetUrl");
                NotifyPropertyChanged("TweetShortcode");
            }
        }
    }

    public string TweetShortcode => GetTwitterUserAndId(TweetUrl);

    private string GetTwitterUserAndId(string url)
    {
        string pattern = @".com\/(\w+)\/\w+\/(\d+)";
        var regex = new Regex.Match(pattern, url);
        return "{{< tweet user=\"" + regex[0] + "\" id=\"" + regex[1] + "\" >}}";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}



