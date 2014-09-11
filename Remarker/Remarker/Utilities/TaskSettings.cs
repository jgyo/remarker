namespace YoderZone.Extensions.OptionsPackage.Remarker.Utilities
{
using System.Security.RightsManagement;
using System.Windows;

using Microsoft.VisualStudio.Shell.Interop;

internal class TaskSettings
{
    public string Name { get; set; }
    public string Color { get; set; }
    public double Size { get; set; }
    public FontWeight FontWeight
    {
        get;
        set;
    }
}
}