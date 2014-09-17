using System.Windows.Controls;

namespace YoderZone.Extensions.OptionsDialog
{
using YoderZone.Extensions.OptionsDialog.ViewModel;

/// <summary>
/// Interaction logic for TaskOptionsControl.xaml
/// </summary>
public partial class TaskOptionsControl : UserControl
{
    private bool canDeactivate;

    private bool isEditing;

    private TaskOptions model;

    //! Constructor
    public TaskOptionsControl()
    {
        InitializeComponent();
        this.DataContextChanged += TaskOptionsControl_DataContextChanged;
    }

    void TaskOptionsControl_DataContextChanged(object sender,
            System.Windows.DependencyPropertyChangedEventArgs e)
    {
        model = this.DataContext as TaskOptions;
    }

    public bool CanDeactivate
    {
        get
        {
            return !isEditing;
        }
    }

    private void DataGrid_BeginningEdit(object sender,
                                        DataGridBeginningEditEventArgs e)
    {
        isEditing = true;
        if (e.Column == colorColumn)
        {
            e.Column.Width = DataGridLength.Auto;
            return;
        }
        if (e.Column == boldColumn)
        {
            var taskAttributes = e.Row.Item as ViewModel.TaskAttributes;
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
        isEditing = false;
        if (e.Column == this.colorColumn)
        {
            e.Column.Width = 39;
        }
    }
}
}
