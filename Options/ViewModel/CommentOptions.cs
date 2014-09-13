// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : CommentOptions.cs
// Author           : Gil Yoder
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 13, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsDialog.ViewModel
{
#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text;

using Options;

#endregion

/// <summary>
///     Comment options view model.
/// </summary>
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
/// <seealso cref="T:System.ComponentModel.IDataErrorInfo" />
/// <seealso cref="T:System.ComponentModel.IEditableObject" />
public class CommentOptions : INotifyPropertyChanged, IDataErrorInfo,
    IEditableObject
{
    #region Static Fields

    /// <summary>
    ///     The default options.
    /// </summary>
    private static CommentOptions defaultOptions;

    #endregion

    #region Fields

    /// <summary>
    ///     Information describing the data error.
    /// </summary>
    private readonly Dictionary<string, string> dataErrorInfo = new
    Dictionary<string, string>();

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     boldface.
    /// </summary>
    private bool hasBoldface;

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has fixed
    ///     pitch.
    /// </summary>
    private bool hasFixedPitch;

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     italics style.
    /// </summary>
    private bool hasItalicsStyle;

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     regular style.
    /// </summary>
    private bool hasRegularStyle;

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     strikeout style.
    /// </summary>
    private bool hasStrikeoutStyle;

    /// <summary>
    ///     The first less.
    /// </summary>
    private float less1 = -1f;

    /// <summary>
    ///     The less 1 text.
    /// </summary>
    private string less1Text;

    /// <summary>
    ///     The second less.
    /// </summary>
    private float less2 = -1f;

    /// <summary>
    ///     The less 2 text.
    /// </summary>
    private string less2Text;

    /// <summary>
    ///     The third less.
    /// </summary>
    private float less3 = -1f;

    /// <summary>
    ///     The less 3 text.
    /// </summary>
    private string less3Text;

    /// <summary>
    ///     The first plus.
    /// </summary>
    private float plus1 = -1f;

    /// <summary>
    ///     The plus 1 text.
    /// </summary>
    private string plus1Text;

    /// <summary>
    ///     The second plus.
    /// </summary>
    private float plus2 = -1f;

    /// <summary>
    ///     The plus 2 text.
    /// </summary>
    private string plus2Text;

    /// <summary>
    ///     The third plus.
    /// </summary>
    private float plus3 = -1f;

    /// <summary>
    ///     The plus 3 text.
    /// </summary>
    private string plus3Text;

    /// <summary>
    ///     The selected font family.
    /// </summary>
    private FontAttributes selectedFontFamily;

    /// <summary>
    ///     The selected font family name.
    /// </summary>
    private string selectedFontFamilyName;

    private CommentOptions memberwiseClone;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions class.
    /// </summary>
    private CommentOptions()
    {
        this.FontFamilies = new List<FontAttributes>();
        var installedFontCollection = new InstalledFontCollection();
        FontFamily[] fontFamilyArray = installedFontCollection.Families;

        FontAttributes select = null;
        var bitmap = new Bitmap(100, 100);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            foreach (var fontFamily in fontFamilyArray)
            {
                var fontAttributes = new FontAttributes(fontFamily, graphics);
                this.FontFamilies.Add(fontAttributes);
                if (fontAttributes.Name.Contains("Verdana"))
                {
                    select = fontAttributes;
                }
            }

            graphics.Dispose();
        }

        this.SelectedFontFamily = select;
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
    public static CommentOptions DefaultOptions
    {
        get
        {
            return defaultOptions ?? (defaultOptions = new CommentOptions());
        }
    }

    /// <summary>
    ///     Gets an error message indicating what is wrong with this object.
    /// </summary>
    /// <seealso cref="P:System.ComponentModel.IDataErrorInfo.Error" />
    public string Error
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var error in this.dataErrorInfo.Values)
            {
                sb.AppendLine(error);
            }

            return sb.ToString();
        }
    }

    /// <summary>
    ///     Gets or sets the font families.
    /// </summary>
    /// <value>
    ///     The font families.
    /// </value>
    public List<FontAttributes> FontFamilies { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     boldface.
    /// </summary>
    /// <value>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     boldface, false if not.
    /// </value>
    public bool HasBoldface
    {
        get
        {
            return this.hasBoldface;
        }
        set
        {
            this.hasBoldface = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has fixed
    ///     pitch.
    /// </summary>
    /// <value>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has fixed
    ///     pitch, false if not.
    /// </value>
    public bool HasFixedPitch
    {
        get
        {
            return this.hasFixedPitch;
        }
        set
        {
            this.hasFixedPitch = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     italics style.
    /// </summary>
    /// <value>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     italics style, false if not.
    /// </value>
    public bool HasItalicsStyle
    {
        get
        {
            return this.hasItalicsStyle;
        }
        set
        {
            this.hasItalicsStyle = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     regular style.
    /// </summary>
    /// <value>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     regular style, false if not.
    /// </value>
    public bool HasRegularStyle
    {
        get
        {
            return this.hasRegularStyle;
        }
        set
        {
            this.hasRegularStyle = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     strikeout style.
    /// </summary>
    /// <value>
    ///     true if this
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has
    ///     strikeout style, false if not.
    /// </value>
    public bool HasStrikeoutStyle
    {
        get
        {
            return this.hasStrikeoutStyle;
        }
        set
        {
            this.hasStrikeoutStyle = value;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the less 1.
    /// </summary>
    /// <value>
    ///     The less 1.
    /// </value>
    public float Less1
    {
        get
        {
            return this.less1;
        }
        set
        {
            if (Math.Abs(this.less1 - value) < 0.00001)
            {
                return;
            }

            this.less1 = value;
            this.OnPropertyChanged();
            this.Less1Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Less1Size");
        }
    }

    /// <summary>
    ///     Gets the size of the less 1.
    /// </summary>
    /// <value>
    ///     The size of the less 1.
    /// </value>
    public float Less1Size
    {
        get
        {
            return this.Less1 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the less 1 text.
    /// </summary>
    /// <value>
    ///     The less 1 text.
    /// </value>
    public string Less1Text
    {
        get
        {
            return this.less1Text;
        }
        set
        {
            // Less1Text setter guard
            if (this.less1Text == value)
            {
                return;
            }

            this.less1Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "-"))
            {
                return;
            }

            this.Less1 = floatValue;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the less 2.
    /// </summary>
    /// <value>
    ///     The less 2.
    /// </value>
    public float Less2
    {
        get
        {
            return this.less2;
        }
        set
        {
            if (Math.Abs(this.less2 - value) < 0.00001)
            {
                return;
            }

            this.less2 = value;
            this.OnPropertyChanged();
            this.Less2Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Less2Size");
        }
    }

    /// <summary>
    ///     Gets the size of the less 2.
    /// </summary>
    /// <value>
    ///     The size of the less 2.
    /// </value>
    public float Less2Size
    {
        get
        {
            return this.Less2 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the less 2 text.
    /// </summary>
    /// <value>
    ///     The less 2 text.
    /// </value>
    public string Less2Text
    {
        get
        {
            return this.less2Text;
        }
        set
        {
            // Less2Text setter guard
            if (this.less2Text == value)
            {
                return;
            }

            this.less2Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "--"))
            {
                return;
            }

            this.Less2 = floatValue;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the less 3.
    /// </summary>
    /// <value>
    ///     The less 3.
    /// </value>
    public float Less3
    {
        get
        {
            return this.less3;
        }
        set
        {
            if (Math.Abs(this.less3 - value) < 0.00001)
            {
                return;
            }

            this.less3 = value;
            this.OnPropertyChanged();
            this.Less3Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Less3Size");
        }
    }

    /// <summary>
    ///     Gets the size of the less 3.
    /// </summary>
    /// <value>
    ///     The size of the less 3.
    /// </value>
    public float Less3Size
    {
        get
        {
            return this.Less3 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the less 3 text.
    /// </summary>
    /// <value>
    ///     The less 3 text.
    /// </value>
    public string Less3Text
    {
        get
        {
            return this.less3Text;
        }
        set
        {
            // Less3Text setter guard
            if (this.less3Text == value)
            {
                return;
            }

            this.less3Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "---"))
            {
                return;
            }

            this.Less3 = floatValue;
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the plus 1.
    /// </summary>
    /// <value>
    ///     The plus 1.
    /// </value>
    public float Plus1
    {
        get
        {
            return this.plus1;
        }
        set
        {
            if (Math.Abs(this.plus1 - value) < 0.00001)
            {
                return;
            }

            this.plus1 = value;
            this.OnPropertyChanged();
            this.Plus1Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Plus1Size");
        }
    }

    /// <summary>
    ///     Gets the size of the plus 1.
    /// </summary>
    /// <value>
    ///     The size of the plus 1.
    /// </value>
    public float Plus1Size
    {
        get
        {
            return this.Plus1 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the plus 1 text.
    /// </summary>
    /// <value>
    ///     The plus 1 text.
    /// </value>
    public string Plus1Text
    {
        get
        {
            return this.plus1Text;
        }
        set
        {
            // Plus1Text setter guard
            if (this.plus1Text == value)
            {
                return;
            }

            this.plus1Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "+"))
            {
                return;
            }

            this.OnPropertyChanged();
            this.Plus1 = floatValue;
        }
    }

    /// <summary>
    ///     Gets or sets the plus 2.
    /// </summary>
    /// <value>
    ///     The plus 2.
    /// </value>
    public float Plus2
    {
        get
        {
            return this.plus2;
        }
        set
        {
            if (Math.Abs(this.plus2 - value) < 0.00001)
            {
                return;
            }

            this.plus2 = value;
            this.OnPropertyChanged();
            this.Plus2Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Plus2Size");
        }
    }

    /// <summary>
    ///     Gets the size of the plus 2.
    /// </summary>
    /// <value>
    ///     The size of the plus 2.
    /// </value>
    public float Plus2Size
    {
        get
        {
            return this.Plus2 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the plus 2 text.
    /// </summary>
    /// <value>
    ///     The plus 2 text.
    /// </value>
    public string Plus2Text
    {
        get
        {
            return this.plus2Text;
        }
        set
        {
            // Plus2Text setter guard
            if (this.plus2Text == value)
            {
                return;
            }

            this.plus2Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "++"))
            {
                return;
            }

            this.OnPropertyChanged();
            this.Plus2 = floatValue;
        }
    }

    /// <summary>
    ///     Gets or sets the plus 3.
    /// </summary>
    /// <value>
    ///     The plus 3.
    /// </value>
    public float Plus3
    {
        get
        {
            return this.plus3;
        }
        set
        {
            if (Math.Abs(this.plus3 - value) < 0.00001)
            {
                return;
            }

            this.plus3 = value;
            this.OnPropertyChanged();
            this.Plus3Text = value.ToString(".###");
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Plus3Size");
        }
    }

    /// <summary>
    ///     Gets the size of the plus 3.
    /// </summary>
    /// <value>
    ///     The size of the plus 3.
    /// </value>
    public float Plus3Size
    {
        get
        {
            return this.Plus3 * this.EmSize;
        }
    }

    /// <summary>
    ///     Gets or sets the plus 3 text.
    /// </summary>
    /// <value>
    ///     The plus 3 text.
    /// </value>
    public string Plus3Text
    {
        get
        {
            return this.plus3Text;
        }
        set
        {
            // Plus3Text setter guard
            if (this.plus3Text == value)
            {
                return;
            }

            this.plus3Text = value;

            float floatValue;
            if (!float.TryParse(value, out floatValue))
            {
                this.RegisterError("Invalid entry.");
            }

            if (!this.CheckRange(floatValue, 0.1f, true, 5f, true, "+++"))
            {
                return;
            }

            this.OnPropertyChanged();
            this.Plus3 = floatValue;
        }
    }

    /// <summary>
    ///     Gets or sets the selected font family.
    /// </summary>
    /// <value>
    ///     The selected font family.
    /// </value>
    public FontAttributes SelectedFontFamily
    {
        get
        {
            return this.selectedFontFamily;
        }
        set
        {
            this.selectedFontFamily = value;
            this.SelectedFontFamilyName = value.Name;
            this.OnSelectedFontChanged();
            this.OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Gets or sets the selected font family name.
    /// </summary>
    /// <value>
    ///     The name of the selected font family.
    /// </value>
    public string SelectedFontFamilyName
    {
        get
        {
            return this.selectedFontFamilyName;
        }
        set
        {
            if (this.selectedFontFamilyName == value)
            {
                return;
            }

            this.selectedFontFamilyName = value;
            this.OnPropertyChanged();

            this.SelectedFontFamily = this.FindSelectedFontFamily(value);
        }
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Gets the size of the em.
    /// </summary>
    /// <value>
    ///     The size of the em.
    /// </value>
    private float EmSize
    {
        get
        {
            return 12f;
        }
    }

    #endregion

    #region Public Indexers

    /// <summary>
    ///     Gets the error message for the property with the given name.
    /// </summary>
    /// <param name="columnName">
    ///     The name of the property whose error message to get.
    /// </param>
    /// <returns>
    ///     The error message for the property. The default is an empty string
    ///     ("").
    /// </returns>
    public string this[string columnName]
    {
        get
        {
            if (!this.dataErrorInfo.ContainsKey(columnName))
            {
                return "";
            }

            return this.dataErrorInfo[columnName];
        }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Begins an edit on an object.
    /// </summary>
    /// <seealso cref="M:System.ComponentModel.IEditableObject.BeginEdit()" />
    public void BeginEdit()
    {
        memberwiseClone = this.MemberwiseClone() as CommentOptions;
    }

    /// <summary>
    ///     Discards changes since the last
    ///     <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.
    /// </summary>
    /// <seealso cref="M:System.ComponentModel.IEditableObject.CancelEdit()" />
    public void CancelEdit()
    {
        this.SelectedFontFamily = memberwiseClone.SelectedFontFamily;
        this.Less1 = memberwiseClone.Less1;
        this.Less2 = memberwiseClone.Less2;
        this.Less3 = memberwiseClone.Less3;
        this.Plus1 = memberwiseClone.Plus1;
        this.Plus2 = memberwiseClone.Plus2;
        this.Plus3 = memberwiseClone.Plus3;
    }

    /// <summary>
    ///     Pushes changes since the last
    ///     <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or
    ///     <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into
    ///     the underlying object.
    /// </summary>
    /// <seealso cref="M:System.ComponentModel.IEditableObject.EndEdit()" />
    public void EndEdit()
    {
        memberwiseClone = null;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Executes the property changed action.
    /// </summary>
    /// <param name="propertyName">
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

    /// <summary>
    ///     Checks the size of value to determine whether it lies within the
    ///     specified lowExtent and hightExtent.
    /// </summary>
    /// <param name="value">
    ///     The value.
    /// </param>
    /// <param name="lowExtent">
    ///     The low extent.
    /// </param>
    /// <param name="inclusiveLow">
    ///     Whether the low extent is included within the range.
    /// </param>
    /// <param name="high">
    ///     The high extent.
    /// </param>
    /// <param name="inclusiveHigh">
    ///     Whether the high extent is included within the range.
    /// </param>
    /// <param name="displayName">
    ///     The display name of the property being checked.
    /// </param>
    /// <param name="propertyName">
    ///     Name of the property.
    /// </param>
    /// <returns>
    ///     true if it succeeds, false if it fails.
    /// </returns>
    private bool CheckRange(
        float value,
        float lowExtent,
        bool inclusiveLow,
        float high,
        bool inclusiveHigh,
        string displayName,
        [CallerMemberName] string propertyName = null)
    {
        StringBuilder sb = null;
        Debug.Assert(propertyName != null, "propertyName != null");

        if (value < lowExtent || (Math.Abs(value - lowExtent) < 0.00001 &&
                                  !inclusiveLow))
        {
            sb =
                new StringBuilder(
                string.Format(
                    "{0:F4} is too low for the \"{1}\" field. ",
                    value,
                    displayName));
        }
        else if (value > high || (Math.Abs(value - high) < 0.00001 &&
                                  !inclusiveHigh))
        {
            sb =
                new StringBuilder(
                string.Format(
                    "{0:F4} is to high for the \"{1}\" field. ",
                    value,
                    displayName));
        }

        if (sb == null)
        {
            if (!this.dataErrorInfo.ContainsKey(propertyName))
            {
                return true;
            }

            this.dataErrorInfo.Remove(propertyName);
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("Error");
            return true;
        }

        sb.AppendFormat(
            "Valid values are {0}above {1:F4} and below {2}{3:F4}.",
            inclusiveLow ? "equal to or " : "",
            lowExtent,
            inclusiveHigh ? "or equal to " : "",
            high);
        if (this.dataErrorInfo.ContainsKey(propertyName))
        {
            this.dataErrorInfo[propertyName] = sb.ToString();
        }
        else
        {
            this.dataErrorInfo.Add(propertyName, sb.ToString());
        }

        // ReSharper disable once ExplicitCallerInfoArgument
        this.OnPropertyChanged("Error");
        return false;
    }

    /// <summary>
    ///     Searches for the first selected font family.
    /// </summary>
    /// <param name="value">
    ///     The value.
    /// </param>
    /// <param name="low">
    ///     The high.
    /// </param>
    /// <param name="high">
    ///     The high.
    /// </param>
    /// <returns>
    ///     The found selected font family.
    /// </returns>
    private FontAttributes FindSelectedFontFamily(string value, int low = -1,
            int high = -1)
    {
        if (value == null)
        {
            return null;
        }

        if (low == -1)
        {
            low = 0;
            high = this.FontFamilies.Count - 1;
        }

        int middle = (high - low) / 2 + low;
        int compare = string.Compare(
                          value,
                          this.FontFamilies[middle].Name,
                          StringComparison.InvariantCulture);

        if (compare == 0)
        {
            return this.FontFamilies[middle];
        }

        if (middle != low && compare < 0)
        {
            if (middle - low == 1)
            {
                return this.FontFamilies[low].Name == value ? this.FontFamilies[low] :
                       null;
            }

            //! Recursive call
            return this.FindSelectedFontFamily(value, low, middle);
        }

        if (middle != high)
        {
            if (high - middle == 1)
            {
                return this.FontFamilies[high].Name == value ? this.FontFamilies[high] :
                       null;
            }

            //! Recursive call
            return this.FindSelectedFontFamily(value, middle, high);
        }

        return null;
    }

    /// <summary>
    ///     Executes the selected font changed action.
    /// </summary>
    private void OnSelectedFontChanged()
    {
        if (this.SelectedFontFamily == null)
        {
            return;
        }

        this.HasBoldface = this.SelectedFontFamily.HasBoldStyle;
        this.HasFixedPitch = this.SelectedFontFamily.IsFixedWidth ?? false;
        this.HasItalicsStyle = this.SelectedFontFamily.HasItalicsStyle;
        this.HasRegularStyle = this.SelectedFontFamily.HasRegularStyle;
        this.HasStrikeoutStyle = this.SelectedFontFamily.HasStrikeoutStyle;
    }

    /// <summary>
    ///     Registers the error.
    /// </summary>
    /// <param name="errorMessage">
    ///     Message describing the error.
    /// </param>
    /// <param name="member">
    ///     The member.
    /// </param>
    private void RegisterError(
        string errorMessage = "",
        [CallerMemberName] string member = null)
    {
        Debug.Assert(member != null, "member != null");

        if (errorMessage == "")
        {
            if (this.dataErrorInfo.ContainsKey(member))
            {
                this.dataErrorInfo.Remove(member);
            }

            return;
        }

        if (this.dataErrorInfo.ContainsKey(member))
        {
            this.dataErrorInfo[member] = errorMessage;
        }
        else
        {
            this.dataErrorInfo.Add(member, errorMessage);
        }
    }

    #endregion
}
}