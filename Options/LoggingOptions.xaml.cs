using System.Windows;
using System.Windows.Controls;

namespace YoderZone.Extensions.Options
{
using System.Diagnostics;
using System.Reflection;

using YoderZone.Extensions.NLog;

/// <summary>
/// Interaction logic for LoggingOptions.xaml
/// </summary>
public partial class LoggingOptions : UserControl
{
    public LoggingOptions()
    {
        InitializeComponent();
    }

    private void OpenFolderButtonClicked(object sender, RoutedEventArgs e)
    {
        var loggingFolder =
            string.Format("file://{0}\\YoderZone\\XamlHelpmeet\\log",
                          SettingsHelper.ApplicationDataFolder);
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = loggingFolder,
                UseShellExecute = true
            }
        };
        process.Start();
    }

    public string Version
    {
        get
        {
            // Extract the version
            var assembly = typeof(TaskOptionsControl).Assembly;
            var attribs = assembly.GetCustomAttributes(typeof(
                              System.Reflection.AssemblyFileVersionAttribute), false);
            string version;
            if (attribs.Length == 0) { version = "1.2"; }
            else
            {
                var attrib = (AssemblyFileVersionAttribute)attribs[0];
                version = attrib.Version;
            }

            return string.Format("Copyright © Gil Yoder 2014 - Version {0}",
                                 version);
        }
    }
}
}
