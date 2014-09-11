// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : CommentOptions.cs
// Author           : Gil Yoder
// Created          : 09 06,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 06, 2014
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
public class CommentOptions : INotifyPropertyChanged, IDataErrorInfo,
    IEditableObject
{
    #region Fields

    /// <summary>
    ///     Information describing the data error.
    /// </summary>
    private readonly Dictionary<string, string> dataErrorInfo = new
    Dictionary<string, string>();

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has boldface.
    /// </summary>
    private bool hasBoldface;

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has fixed pitch.
    /// </summary>
    private bool hasFixedPitch;

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has italics
    ///     style.
    /// </summary>
    private bool hasItalicsStyle;

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has regular
    ///     style.
    /// </summary>
    private bool hasRegularStyle;

    /// <summary>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has strikeout
    ///     style.
    /// </summary>
    private bool hasStrikeoutStyle;

    /// <summary>
    ///     The first less.
    /// </summary>
    private float less1 = 0.85f;

    /// <summary>
    ///     The second less.
    /// </summary>
    private float less2 = 0.70f;

    /// <summary>
    ///     The third less.
    /// </summary>
    private float less3 = 0.55f;

    /// <summary>
    ///     The first plus.
    /// </summary>
    private float plus1 = 1.3333f;

    /// <summary>
    ///     The second plus.
    /// </summary>
    private float plus2 = 1.6667f;

    /// <summary>
    ///     The third plus.
    /// </summary>
    private float plus3 = 2.0f;

    /// <summary>
    ///     The selected font family.
    /// </summary>
    private FontAttributes selectedFontFamily;

    /// <summary>
    ///     The selected font family name.
    /// </summary>
    private string selectedFontFamilyName;

    private static CommentOptions defaultOptions;

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
    ///     Gets an error message indicating what is wrong with this object.
    /// </summary>
    /// <value>
    ///     An error message indicating what is wrong with this object. The default is
    ///     an empty
    ///     string ("").
    /// </value>
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
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has boldface.
    /// </summary>
    /// <value>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has boldface,
    ///     false if not.
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
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has fixed pitch,
    ///     false if not.
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
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has italics
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has italics
    ///     style, false if not.
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
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has regular
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has regular
    ///     style, false if not.
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
    ///     YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions has strikeout
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions
    ///     has strikeout
    ///     style, false if not.
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
            if (Math.Abs(this.less1 - value) < 0.00001 ||
                    !this.CheckRange(value, 0.1f, true, 1.0f, false, "\"-\""))
            {
                return;
            }

            this.less1 = value;
            this.OnPropertyChanged();
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
            if (Math.Abs(this.less2 - value) < 0.00001 ||
                    !this.CheckRange(value, 0.1f, true, 1.0f, false, "\"--\""))
            {
                return;
            }

            this.less2 = value;
            this.OnPropertyChanged();
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
            if (Math.Abs(this.less3 - value) < 0.00001 ||
                    !this.CheckRange(value, 0.1f, true, 1.0f, false, "\"---\""))
            {
                return;
            }

            this.less3 = value;
            this.OnPropertyChanged();
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
            if (Math.Abs(this.plus1 - value) < 0.00001 ||
                    !this.CheckRange(value, 1.0f, false, 5.0f, true, "\"+\""))
            {
                return;
            }

            this.plus1 = value;
            this.OnPropertyChanged();
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged("Plus1Size");
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
            if (Math.Abs(this.plus2 - value) < 0.00001 ||
                    !this.CheckRange(value, 1.0f, false, 5.0f, true, "\"++\""))
            {
                return;
            }

            this.plus2 = value;
            this.OnPropertyChanged();
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
            if (Math.Abs(this.plus3 - value) < 0.00001 ||
                    !this.CheckRange(value, 1.0f, false, 5.0f, true, "\"+++\""))
            {
                return;
            }

            this.plus3 = value;
            this.OnPropertyChanged();
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

    public static CommentOptions DefaultOptions
    {
        get
        {
            return defaultOptions ?? (defaultOptions = new CommentOptions());
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
    ///     The error message for the property. The default is an empty string ("").
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

    /// <summary>
    ///     Check range.
    /// </summary>
    /// <param name="value" type="float">
    ///     The value.
    /// </param>
    /// <param name="lowExtent" type="float">
    ///     Extent of the low.
    /// </param>
    /// <param name="inclusiveLow" type="bool">
    ///     true to inclusive low.
    /// </param>
    /// <param name="high" type="float">
    ///     The high.
    /// </param>
    /// <param name="inclusiveHigh" type="bool">
    ///     true to inclusive high.
    /// </param>
    /// <param name="displayName" type="string">
    ///     Name of the display.
    /// </param>
    /// <param name="propertyName" type="string">
    ///     Name of the property.
    /// </param>
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
                string.Format("{0:F4} is too low for the {1} field. ", value,
                              displayName));
        }
        else if (value > high || (Math.Abs(value - high) < 0.00001 &&
                                  !inclusiveHigh))
        {
            sb =
                new StringBuilder(
                string.Format("{0:F4} is to high for the {1} field. ", value,
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
    /// <param name="value" type="string">
    ///     The value.
    /// </param>
    /// <param name="low" type="int">
    ///     The high.
    /// </param>
    /// <param name="high" type="int">
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

            return this.FindSelectedFontFamily(value, low, middle);
        }

        if (middle != high)
        {
            if (high - middle == 1)
            {
                return this.FontFamilies[high].Name == value ? this.FontFamilies[high] :
                       null;
            }

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

    #endregion

    /// <summary>
    /// Begins an edit on an object.
    /// </summary>
    public void BeginEdit()
    {
    }

    /// <summary>
    /// Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> or <see cref="M:System.ComponentModel.IBindingList.AddNew"/> call into the underlying object.
    /// </summary>
    public void EndEdit()
    {
    }

    /// <summary>
    /// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> call.
    /// </summary>
    public void CancelEdit()
    {
    }
}
}