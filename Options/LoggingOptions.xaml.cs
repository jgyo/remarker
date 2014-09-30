using System.Windows;
using System.Windows.Controls;

namespace YoderZone.Extensions.Options
{
using System;
using System.Diagnostics;
using System.Reflection;

using global::NLog;

using YoderZone.Extensions.NLog;

/// <summary>
/// Interaction logic for LoggingOptions.xaml
/// </summary>
public partial class LoggingOptions : UserControl
{
    private Logger logger = SettingsHelper.CreateLogger();

    public LoggingOptions()
    {
        logger.Debug("Entered constructor.");

        InitializeComponent();
    }

    private void OpenFolderButtonClicked(object sender, RoutedEventArgs e)
    {
        logger.Debug("Entered method.");
        try
        {
            var button = sender as Button;
            if (button == null)
            {
                logger.Warn("sender is not a Button.");
                return;
            }

            var loggingFolder = (string)button.Tag;
            logger.Trace("loggingFolder: {0}", loggingFolder);

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
        catch (Exception ex)
        {
            logger.Error(ex.Message, ex);
            throw;
        }
    }

    public string Version
    {
        get
        {
            logger.Debug("Entered property get.");

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
