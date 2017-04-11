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

namespace YoderZone.Extensions.Options.ViewModel
{
    #region Imports

    using global::Options;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    #endregion Imports

    /// <summary>
    ///     A task attributes.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
    public class TaskAttributes : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        ///     The color.
        /// </summary>
        private Color _color;

        /// <summary>
        ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.TaskAttributes is
        ///     bold.
        /// </summary>
        private bool _isBold;

        /// <summary>
        ///     The name.
        /// </summary>
        private string _name;

        /// <summary>
        ///     The type face.
        /// </summary>
        private FontAttributes _typeFace;

        #endregion Fields

        #region Public Events

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public Color Color
        {
            get => this._color;
            set
            {
                this._color = value;
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
            get => this._isBold;
            set
            {
                this._isBold = value;
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
            get => this._name;
            set
            {
                this._name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the typeface.
        /// </summary>
        /// <value>
        ///     The typeface.
        /// </value>
        public FontAttributes Typeface
        {
            get => this._typeFace;
            set
            {
                this._typeFace = value;
                this.OnPropertyChanged();
            }
        }

        #endregion Public Properties

        #region Methods

        /// <summary>
        ///     Executes the property changed action.
        /// </summary>
        /// <param name="propertyName" type="string">
        ///     Name of the property.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion Methods
    }
}