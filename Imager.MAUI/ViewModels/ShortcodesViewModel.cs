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
        CopyShortcodeCommand = new Command<string>(async (string code) =>
        {
            await Clipboard.SetTextAsync(code);
        });
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

    private string _youtubeTitle;
    public string YouTubeTitle
    {
        get => _youtubeTitle;
        set
        {
            if (value != null)
            {
                _youtubeTitle = value;
                NotifyPropertyChanged("YouTubeTitle");
                NotifyPropertyChanged("YouTubeShortcode");
            }
        }
    }


    private string _youtubeUrl;
    public string YouTubeUrl
    {
        get => _youtubeUrl;
        set
        {
            if (value != null)
            {
                _youtubeUrl = value;
                NotifyPropertyChanged("YouTubeUrl");
                NotifyPropertyChanged("YouTubeShortcode");
            }
        }
    }

    private bool _isAutoPlay;
    public bool IsAutoPlay
    {
        get => _isAutoPlay;
        set
        {
            _isAutoPlay = value;
            NotifyPropertyChanged("IsAutoPlay");
            NotifyPropertyChanged("YouTubeShortcode");
        }
    }

    public string TweetShortcode
    {
        get
        {
            if (string.IsNullOrWhiteSpace(TweetUrl)) return "{{< tweet user=\"\" id=\"\" >}}";

            try
            {
                string pattern = @".com\/(\w+)\/\w+\/(\d+)";
                var regex = new Regex(pattern).Match(TweetUrl);
                string user = regex.Groups[1].Value;
                string id = regex.Groups[2].Value;
                return "{{< tweet user=\"" + regex.Groups[1] + "\" id=\"" + regex.Groups[2] + "\" >}}";
            }
            catch (RegexParseException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public string YouTubeShortcode
    {
        get
        {
            if (string.IsNullOrWhiteSpace(YouTubeUrl)) return "{{< youtube >}}";

            string pattern = @"[v=|/](\w+)$";
            var regex = new Regex(pattern).Match(YouTubeUrl);
            string videoId = regex.Groups[1].Value;
            return IsAutoPlay
                ? string.IsNullOrWhiteSpace(YouTubeTitle) 
                    ? "{{< youtube id=\"" + videoId + "\" autoplay=\"true\" >}}"
                    : "{{< youtube id=\"" + videoId + "\" title=\"" + YouTubeTitle + "\" autoplay=\"true\" >}}"
                : string.IsNullOrWhiteSpace(YouTubeTitle) 
                    ? "{{< youtube " + videoId + " >}}"
                    : "{{< youtube id=\"" + videoId + "\" title=\"" + YouTubeTitle  + "\" >}}";
        }
    }


    // COMMANDS
    public ICommand CopyShortcodeCommand { get; private set; }


    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}



