// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : TaskAttributes.cs
// Author           : Gil Yoder
// Created          : 09 07,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 07, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsDialog.ViewModel
{
#region Imports

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

#endregion

/// <summary>
///     A task attributes.
/// </summary>
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
public class TaskAttributes : INotifyPropertyChanged, IEditableObject
{
    #region Fields

    /// <summary>
    ///     The color.
    /// </summary>
    private Color color;

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.TaskAttributes is
    ///     bold.
    /// </summary>
    private bool isBold;

    /// <summary>
    ///     The name.
    /// </summary>
    private string name;

    /// <summary>
    ///     The type face.
    /// </summary>
    private string typeFace;

    private TaskAttributes taskCopy;

    #endregion

    #region Public Events

    /// <summary>
    ///     Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets or sets the color.
    /// </summary>
    /// <value>
    ///     The color.
    /// </value>
    public Color Color
    {
        get
        {
            return this.color;
        }
        set
        {
            this.color = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.TaskAttributes is bold.
    /// </summary>
    /// <value>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.TaskAttributes is
    ///     bold, false
    ///     if not.
    /// </value>
    public bool IsBold
    {
        get
        {
            return this.isBold;
        }
        set
        {
            this.isBold = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            this.name = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the typeface.
    /// </summary>
    /// <value>
    ///     The typeface.
    /// </value>
    public string Typeface
    {
        get
        {
            return this.typeFace;
        }
        set
        {
            this.typeFace = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsInEditMode
    {
        get
        {
            return taskCopy != null;
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

    /// <summary>
    /// Begins an edit on an object.
    /// </summary>
    public void BeginEdit()
    {
        taskCopy = this.MemberwiseClone() as TaskAttributes;
    }

    /// <summary>
    /// Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> or <see cref="M:System.ComponentModel.IBindingList.AddNew"/> call into the underlying object.
    /// </summary>
    public void EndEdit()
    {
        taskCopy = null;
    }

    /// <summary>
    /// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> call.
    /// </summary>
    public void CancelEdit()
    {
        this.Color = taskCopy.Color;
        this.IsBold = taskCopy.IsBold;
        this.Name = taskCopy.Name;
        this.Typeface = taskCopy.Typeface;
    }
}
}