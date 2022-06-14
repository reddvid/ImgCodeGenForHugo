using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Imager.MAUI;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        Width = 85D;

        CopyFigureCommand = new Command(async () =>
        {
            await Clipboard.SetTextAsync(Figure);
        });

        CopyMarkdownCommand = new Command(async () =>
        {
            await Clipboard.SetTextAsync(Markdown);
        });

        CopyShortcodeCommand = new Command(async () =>
        {
            await Clipboard.SetTextAsync(Shortcode);
        });

        PreviewHtmlCommand = new Command(() =>
        {
            HtmlPreview = @"<HTML><BODY>" + Figure + "</BODY></HTML>";
        });
    }

    private string _src;
    public string Src
    {
        get => _src;
        set
        {
            if (value != null)
            {
                _src = value;
                NotifyPropertyChanged("Src");
                NotifyPropertyChanged("Figure");
                NotifyPropertyChanged("Markdown");
                NotifyPropertyChanged("Shortcode");
            }
        }
    }

    private string _alt;
    public string Alt
    {
        get => _alt;
        set
        {
            if (value != null)
            {
                _alt = value;
                NotifyPropertyChanged("Alt");
                NotifyPropertyChanged("Figure");
                NotifyPropertyChanged("Markdown");
                NotifyPropertyChanged("Shortcode");
            }
        }
    }

    private string _caption;
    public string Caption
    {
        get => _caption;
        set
        {
            if (value != null)
            {
                _caption = value;
                NotifyPropertyChanged("Caption");
                NotifyPropertyChanged("Figure");
                NotifyPropertyChanged("Markdown");
                NotifyPropertyChanged("Shortcode");
            }
        }
    }

    private double _width;
    public double Width
    {
        get => _width;
        set
        {
            if (value != _width)
            {
                _width = value;
                NotifyPropertyChanged("Width");
                NotifyPropertyChanged("Figure");
                NotifyPropertyChanged("Markdown");
                NotifyPropertyChanged("Shortcode");
            }
        }
    }

    private string _htmlPreview;
    public string HtmlPreview
    {
        get => _htmlPreview;
        set
        {
            if (value != null)
            {
                _htmlPreview = value;
                NotifyPropertyChanged("HtmlPreview");
            }
        }
    }

    public string Figure => string.IsNullOrWhiteSpace(Caption)
        ? $"<figure class=\"image\">\n\t<img src=\"{Src}\" alt=\"{Alt}\" width=\"{Width:N0}%\">\n</figure>"
        : $"<figure class=\"image\">\n\t<img src=\"{Src}\" alt=\"{Alt}\" width=\"{Width:N0}%\">\n\t<figcaption>{Caption}</figcaption>\n</figure>";


    public string Markdown => $"![{Alt}]({Src})";

    public string Shortcode => string.IsNullOrWhiteSpace(Caption)
        ? "{{< figure src=\"" + Src + "\" alt=\"" + Alt + "\" width=\"" + Width + "%\" >}}"
        : "{{< figure src=\"" + Src + "\" caption=\"" + Caption + "\" alt=\"" + Alt +"\" width=\"" + Width + "%\" >}}";

    public int WidthPercent => Convert.ToInt32(Width);

    // COMMANDS //
    public ICommand CopyFigureCommand { get; private set; }
    public ICommand CopyMarkdownCommand { get; private set; }
    public ICommand CopyShortcodeCommand { get; private set; }
    public ICommand PreviewHtmlCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}