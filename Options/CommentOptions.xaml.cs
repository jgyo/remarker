using System.Windows.Controls;

namespace YoderZone.Extensions.OptionsDialog
{
using System.Reflection;

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
