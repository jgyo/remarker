// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : TaskOptionsControl.cs
// Author           : Gil Yoder
// Created          : 09 07,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 07, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Options.ViewModel
{
    using global::Options;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    /// <summary>
    ///     A task's options.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
    public class TaskOptions : INotifyPropertyChanged
    {
        /// <summary>
        ///     The tasks.
        /// </summary>
        private readonly ObservableCollection<TaskAttributes> _tasks;

        /// <summary>
        ///     The selected color.
        /// </summary>
        private Color _selectedColor = Colors.Beige;

        private FontAttributes _selectedFontFamily;

        /// <summary>
        ///     true to selected font weight.
        /// </summary>
        private bool _selectedFontWeight;

        /// <summary>
        ///     The selected name.
        /// </summary>
        private string _selectedName;

        private TaskAttributes _selectedTask;
        // private string selectedFont;

        /// <summary>
        ///     Prevents a default instance of the
        ///     YoderZone.Extensions.OptionsDialog.ViewModel.TaskOptionsControl class from being
        ///     created.
        /// </summary>
        public TaskOptions()
        {
            this._tasks = new ObservableCollection<TaskAttributes>();
            this.FontFamilies = FontHelper.FontFamilies;
            this.SelectedFontFamily = FontHelper.Verdana;
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public List<FontAttributes> FontFamilies { get; private set; }

        /// <summary>
        ///     Gets or sets the selected color.
        /// </summary>
        /// <value>
        ///     The color of the selected.
        /// </value>
        public Color SelectedColor
        {
            get => this._selectedColor;
            set
            {
                this._selectedColor = value;
                this.OnPropertyChanged();
            }
        }

        public FontAttributes SelectedFontFamily
        {
            get => this._selectedFontFamily;
            private set
            {
                if (value == this._selectedFontFamily)
                {
                    return;
                }
                this._selectedFontFamily = value;
                OnPropertyChanged();
            }
        }

        // public List<string> FontNames { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether the selected font weight.
        /// </summary>
        /// <value>
        ///     true if selected font weight, false if not.
        /// </value>
        public bool SelectedFontWeight
        {
            get => this._selectedFontWeight;
            set
            {
                this._selectedFontWeight = value;
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
            get => this._selectedName;
            set
            {
                this._selectedName = string.IsNullOrWhiteSpace(value) ? null :
                                    value.Trim();
                this.OnPropertyChanged();
            }
        }

        public TaskAttributes SelectedTask
        {
            get => this._selectedTask;
            set
            {
                if (this._selectedTask != null)
                {
                    this._selectedTask.PropertyChanged -= this.SelectedTask_PropertyChanged;
                }

                this._selectedTask = value;

                if (this._selectedTask != null)
                {
                    this._selectedTask.PropertyChanged += this.SelectedTask_PropertyChanged;
                }
                this.OnPropertyChanged();
                if (value == null)
                {
                    return;
                }

                this.SelectedColor = value.Color;
                this.SelectedFontWeight = value.IsBold;
                this.SelectedName = value.Name;
                this.SelectedFontFamily = value.Typeface;
            }
        }

        /// <summary>
        ///     Gets the tasks.
        /// </summary>
        /// <value>
        ///     The tasks.
        /// </value>
        public ObservableCollection<TaskAttributes> Tasks => this._tasks;

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

        /// <summary>
        ///     Executes the property changed action.
        /// </summary>
        /// <param name="propertyName" type="string">
        ///     Name of the property.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SelectedTask_PropertyChanged(object sender,
                            PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Typeface":
                    this.SelectedFontFamily = this._selectedTask.Typeface;
                    break;

                case "Name":
                    this.SelectedName = this._selectedTask.Name;
                    break;

                case "IsBold":
                    this.SelectedFontWeight = this._selectedTask.IsBold;
                    break;

                case "Color":
                    this.SelectedColor = this._selectedTask.Color;
                    break;
            }
        }
    }
}