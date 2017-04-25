namespace YoderZone.Extensions.Options
{
using System.Reflection;
using System.Windows.Controls;

using YoderZone.Extensions.OptionsDialog;

/// <summary>
/// Interaction logic for CommentOptionsControl.xaml
/// </summary>
public partial class CommentOptionsControl : UserControl
{
    public CommentOptionsControl()
    {
        InitializeComponent();
    }



    public string Version
    {
        get
        {
            // Extract the version
            var assembly = typeof(CommentOptionsControl).Assembly;
            string fullName = assembly.FullName;
            string[] strings = fullName.Split(',');
            var version = strings[1];
            strings = version.Split('=');
            version = strings[1];

            return $"Copyright © Gil Yoder 2014 - Version {version}";
        }
    }
}
}
