// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : TaskOptions.cs
// Author           : Gil Yoder
// Created          : 09 07,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 07, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsDialog.ViewModel
{
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;

#endregion

/// <summary>
///     A task options.
/// </summary>
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
public class TaskOptions : INotifyPropertyChanged
{
    #region Static Fields

    /// <summary>
    ///     The default task options.
    /// </summary>
    private static TaskOptions defaultTaskOptions;

    #endregion

    #region Fields

    /// <summary>
    ///     The tasks.
    /// </summary>
    private readonly ObservableCollection<TaskAttributes> tasks;

    /// <summary>
    ///     The selected color.
    /// </summary>
    private Color selectedColor = Colors.Beige;

    /// <summary>
    ///     Size of the selected font.
    /// </summary>
    private float selectedFontSize;

    /// <summary>
    ///     true to selected font weight.
    /// </summary>
    private bool selectedFontWeight;

    /// <summary>
    ///     The selected name.
    /// </summary>
    private string selectedName;

    private TaskAttributes selectedTask;

    private string selectedFont;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Prevents a default instance of the
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.TaskOptions class from being
    ///     created.
    /// </summary>
    private TaskOptions()
    {
        this.tasks = new ObservableCollection<TaskAttributes>();
    }

    #endregion

    #region Public Events

    /// <summary>
    ///     Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets the default options.
    /// </summary>
    /// <value>
    ///     The default options.
    /// </value>
    public static TaskOptions DefaultOptions
    {
        get
        {
            return defaultTaskOptions ?? (defaultTaskOptions = new TaskOptions());
        }
    }

    public List<string> FontNames { get; set; }

    /// <summary>
    ///     Gets or sets the selected color.
    /// </summary>
    /// <value>
    ///     The color of the selected.
    /// </value>
    public Color SelectedColor
    {
        get
        {
            return this.selectedColor;
        }
        set
        {
            this.selectedColor = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsInEditMode
    {
        get
        {
            return Tasks.Any(a => a.IsInEditMode);
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the selected font weight.
    /// </summary>
    /// <value>
    ///     true if selected font weight, false if not.
    /// </value>
    public bool SelectedFontWeight
    {
        get
        {
            return this.selectedFontWeight;
        }
        set
        {
            this.selectedFontWeight = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the selected name.
    /// </summary>
    /// <value>
    ///     The name of the selected.
    /// </value>
    public string SelectedName
    {
        get
        {
            return this.selectedName;
        }
        set
        {
            this.selectedName = string.IsNullOrWhiteSpace(value) ? null :
                                value.Trim();
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets the tasks.
    /// </summary>
    /// <value>
    ///     The tasks.
    /// </value>
    public ObservableCollection<TaskAttributes> Tasks
    {
        get
        {
            return this.tasks;
        }
    }

    public TaskAttributes SelectedTask
    {
        get
        {
            return this.selectedTask;
        }
        set
        {
            if (this.selectedTask != null)
            {
                this.selectedTask.PropertyChanged -= selectedTask_PropertyChanged;
            }

            this.selectedTask = value;

            if (this.selectedTask != null)
            {
                this.selectedTask.PropertyChanged += selectedTask_PropertyChanged;
            }
            OnPropertyChanged();
            if(value == null)
            {
                return;
            }

            this.SelectedColor = value.Color;
            this.SelectedFontWeight = value.IsBold;
            this.SelectedName = value.Name;
            this.SelectedFont = value.Typeface;
        }
    }

    private void selectedTask_PropertyChanged(object sender,
            PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "Typeface":
                this.SelectedFont = this.selectedTask.Typeface;
                break;
            case "Name":
                this.SelectedName = this.selectedTask.Name;
                break;
            case "IsBold":
                this.SelectedFontWeight = this.selectedTask.IsBold;
                break;
            case "Color":
                this.SelectedColor = this.selectedTask.Color;
                break;
        }
    }

    public string SelectedFont
    {
        get
        {
            return selectedFont;
        }
        set
        {
            selectedFont = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Loads the tasks.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="taskInstances" type="IEnumerable <TaskAttributes>">
    ///     The task instances.
    ///     </param>
    public void LoadTasks(IEnumerable<TaskAttributes> taskInstances)
    {
        this.Tasks.Clear();
        foreach (var taskInstance in taskInstances)
        {
            this.Tasks.Add(taskInstance);
        }

        if (this.Tasks.Count != 10)
        {
            throw new ArgumentOutOfRangeException("taskInstances");
        }
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Executes the property changed action.
    /// </summary>
    /// <param name="propertyName" type="string">
    ///     Name of the property.
    /// </param>
    protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null)
    {
        PropertyChangedEventHandler handler = this.PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion
}
}