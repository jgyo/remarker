namespace YoderZone.Extensions.Options
{
using System.Reflection;
using System.Windows.Controls;

using YoderZone.Extensions.Options.ViewModel;

/// <summary>
/// Interaction logic for TaskOptionsControl.xaml
/// </summary>
public partial class TaskOptionsControl : UserControl
{
    private bool canDeactivate;

    private bool isEditing;

    //! Constructor
    public TaskOptionsControl()
    {
        InitializeComponent();
    }

    public bool CanDeactivate
    {
        get
        {
            if (this.isEditing)
            {
                this.dataGrid.CommitEdit();
            }

            return !this.isEditing;
        }
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

    private void DataGrid_BeginningEdit(object sender,
                                        DataGridBeginningEditEventArgs e)
    {
        this.isEditing = true;
        if (e.Column == this.colorColumn)
        {
            e.Column.Width = DataGridLength.Auto;
            return;
        }
        if (e.Column == this.boldColumn)
        {
            var taskAttributes = e.Row.Item as TaskAttributes;
            if (taskAttributes == null)
            {
                return;
            }

            taskAttributes.IsBold = !taskAttributes.IsBold;
        }
    }

    private void DataGrid_CellEditEnding(object sender,
                                         DataGridCellEditEndingEventArgs e)
    {
        this.isEditing = false;
        if (e.Column == this.colorColumn)
        {
            e.Column.Width = 39;
        }
    }
}
}
