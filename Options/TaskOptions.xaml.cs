using System.Windows.Controls;
using System.Xaml;

namespace YoderZone.Extensions.OptionsDialog
{
using System;
using System.Windows.Input;

/// <summary>
/// Interaction logic for TaskOptions.xaml
/// </summary>
public partial class TaskOptions : UserControl
{
    //! Constructor
    public TaskOptions()
    {
        InitializeComponent();
    }



    private void DataGrid_BeginningEdit(object sender,
                                        DataGridBeginningEditEventArgs e)
    {
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
        if (e.Column == this.colorColumn)
        {
            e.Column.Width = 39;
        }
    }

    public bool Confirm()
    {
        if (ViewModel.TaskOptions.DefaultOptions.IsInEditMode)
        {
            ViewModel.TaskOptions.DefaultOptions.SelectedTask.EndEdit();
        }

        return true;
    }
}

[Flags]
public enum ControlKeys
{
    None = 0,
    LeftCtrl = 1,
    RightCtrl = 2,
    LeftShift = 4,
    RightShift = 8,
    LeftAlt = 16,
    RightAlt = 32,
    Enter = 64,
    Escape = 128,
    All = LeftAlt | RightAlt | LeftCtrl | RightCtrl | LeftShift | RightShift | Enter | Escape
}
}
