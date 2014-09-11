// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace Options.Itboy
{
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using YoderZone.Extensions.OptionsDialog.Itboy;

#region Extend Property
/// <summary>
/// This component validate controls text in represention.
/// </summary>
[ProvideProperty("Type", typeof(Control))]
[ProvideProperty("DataType", typeof(Control))]
[ProvideProperty("RequiredMessage", typeof(Control))]
[ProvideProperty("RegularExpression", typeof(Control))]
[ProvideProperty("RegularExpressionOptions", typeof(Control))]
[ProvideProperty("RegularExpressionMessage", typeof(Control))]
[ProvideProperty("ComparedControl", typeof(Control))]
[ProvideProperty("CompareOperator", typeof(Control))]
[ProvideProperty("CompareMessage", typeof(Control))]
[ProvideProperty("RangeOfLowerBound", typeof(Control))]
[ProvideProperty("RangeOfUpperBound", typeof(Control))]
[ProvideProperty("RangeMessage", typeof(Control))]
[ProvideProperty("CustomMessage", typeof(Control))]
[ToolboxBitmap(typeof(Validator), "Validator.Toolbox.ico")]
[ToolboxItemFilter("System.Windows.Forms")]
public partial class Validator : Component, IExtenderProvider
{
    #region Type
    [Localizable(true)]
    public ValidationType GetType(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.Type;
        }
        else
        {
            return ValidationType.None;
        }
    }
    public void SetType(Control control, ValidationType type)
    {
        if (this.infos.ContainsKey(control))
        {
            // Especially, in design mode and type is none, ask whether remove validation information.
            if ((this.DesignMode)
                    && (type == ValidationType.None))
            {
                // Set right to left message box options.
                MessageBoxOptions options = (MessageBoxOptions)0;
                if (this.RightToLeft)
                {
                    options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
                }

                if (MessageBox.Show(
                            Resources.VALIDATOR_REMOVEWARNING,
                            Resources.VALIDATOR_NAME,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1,
                            options) == DialogResult.Yes)
                {
                    this.RemoveValidationInfo(control);
                }
            }
            else
            {
                this.infos[control].Type = type;
            }
        }
        else
        {
            this.AddValidationInfo(control);
            ValidationInfo info = this.infos[control];
            info.Type = type;
        }
    }

    [DefaultValue(ValidationDataType.String)]
    [Localizable(true)]
    public ValidationDataType GetDataType(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.DataType;
        }
        else
        {
            return ValidationDataType.String;
        }
    }
    public void SetDataType(Control control, ValidationDataType dataType)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.DataType = dataType;
    }
    #endregion Type

    #region Required
    [DefaultValue(null)]
    [Localizable(true)]
    public string GetRequiredMessage(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RequiredMessage;
        }
        else
        {
            return null;
        }
    }
    public void SetRequiredMessage(Control control, string message)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RequiredMessage = message;
    }
    #endregion Required

    #region Regular Expression
    [DefaultValue((string)null)]
    [Localizable(true)]
    public string GetRegularExpression(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RegularExpression;
        }
        else
        {
            return null;
        }
    }
    public void SetRegularExpression(Control control,
                                     string regularExpression)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RegularExpression = regularExpression;
    }

    [DefaultValue(RegexOptions.None)]
    [Localizable(true)]
    public RegexOptions GetRegularExpressionOptions(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RegularExpressOptions;
        }
        else
        {
            return RegexOptions.None;
        }
    }
    public void SetRegularExpressionOptions(Control control,
                                            RegexOptions options)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RegularExpressOptions = options;
    }


    [DefaultValue(null)]
    [Localizable(true)]
    public string GetRegularExpressionMessage(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RegularExpressionMessage;
        }
        else
        {
            return null;
        }
    }
    public void SetRegularExpressionMessage(Control control, string message)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RegularExpressionMessage = message;
    }
    #endregion Regular expression

    #region Compare
    [DefaultValue(null)]
    [Localizable(true)]
    public Control GetComparedControl(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.ComparedControl;
        }
        else
        {
            return null;
        }
    }
    public void SetComparedControl(Control control, Control comparedControl)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.ComparedControl = comparedControl;
    }

    [DefaultValue(ValidationCompareOperator.DataTypeCheck)]
    [Localizable(true)]
    public ValidationCompareOperator GetCompareOperator(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.CompareOperator;
        }
        else
        {
            return ValidationCompareOperator.DataTypeCheck;
        }
    }
    public void SetCompareOperator(Control control,
                                   ValidationCompareOperator compareOperator)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.CompareOperator = compareOperator;
    }

    [DefaultValue(null)]
    [Localizable(true)]
    public string GetCompareMessage(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.CompareMessage;
        }
        else
        {
            return null;
        }
    }
    public void SetCompareMessage(Control control, string message)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.CompareMessage = message;
    }
    #endregion Compare

    #region Range
    [DefaultValue(null)]
    [Localizable(true)]
    public string GetRangeOfLowerBound(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RangeOfLowerBound;
        }
        else
        {
            return null;
        }
    }
    public void SetRangeOfLowerBound(Control control, string value)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RangeOfLowerBound = value;
    }

    [DefaultValue(null)]
    [Localizable(true)]
    public string GetRangeOfUpperBound(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RangeOfUpperBound;
        }
        else
        {
            return null;
        }
    }
    public void SetRangeOfUpperBound(Control control, string value)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RangeOfUpperBound = value;
    }

    [DefaultValue(null)]
    [Localizable(true)]
    public string GetRangeMessage(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.RangeMessage;
        }
        else
        {
            return null;
        }
    }
    public void SetRangeMessage(Control control, string message)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.RangeMessage = message;
    }
    #endregion Range

    #region Custom
    [DefaultValue(null)]
    [Localizable(true)]
    public string GetCustomMessage(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            ValidationInfo info = this.infos[control];
            return info.CustomMessage;
        }
        else
        {
            return null;
        }
    }
    public void SetCustomMessage(Control control, string message)
    {
        if (!this.infos.ContainsKey(control))
        {
            this.AddValidationInfo(control);
        }

        ValidationInfo info = this.infos[control];
        info.CustomMessage = message;
    }
    #endregion Custom
}
#endregion Extend Property

#region Interface method
partial class Validator
{
    public bool CanExtend(object extendee)
    {
        // Only accept textboxbase driver class(TextBox and RichTextBox) and ComboBox
        if (extendee is TextBoxBase || extendee is ComboBox)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
#endregion Interface method

#region Field & Property
partial class Validator
{
    /// <summary>
    /// Custom validation, it will raise when set "Custom" flag of validation type.
    /// Set "Valid" to true in ValidationEventArgs if pass custom test, or else, false.
    /// </summary>
    public event EventHandler<ValidationEventArgs> CustomValidation;

    private Dictionary<Control, ValidationInfo> infos;
    private ErrorProvider errorProvider;

    public bool HasError(Control control)
    {
        if (this.infos == null || this.infos.ContainsKey(control)==false)
        {
            return false;
        }

        return this.infos[control].HasError;
    }

    private Form form;
    [DefaultValue(null)]
    [Localizable(true)]
    public Form Form
    {
        get
        {
            return this.form;
        }
        set
        {
            if (this.form != value)
            {
                // Unregister previous form and register current form.
                if (this.form != null)
                {
                    this.form.FormClosing -= new FormClosingEventHandler(
                                                 this.validationForm_FormClosing);
                }

                this.form = value;

                if (this.form != null)
                {
                    this.form.FormClosing += new FormClosingEventHandler(
                                                 this.validationForm_FormClosing);
                }
            }
        }
    }

    private ValidationMode mode = ValidationMode.FocusChange |
                                  ValidationMode.Submit;
    /// <summary>
    /// Gets or sets validation mode for specified form.
    /// </summary>
    [DefaultValue(ValidationMode.FocusChange | ValidationMode.Submit)]
    [Localizable(true)]
    public ValidationMode Mode
    {
        get
        {
            return this.mode;
        }
        set
        {
            this.mode = value;
        }
    }

    /// <summary>
    /// Gets or sets the Icon that is displayed next to a control
    /// when an error description string has been set for the control.
    /// </summary>
    [ValidatorDefaultIcon(null)]
    [Localizable(true)]
    public Icon Icon
    {
        get
        {
            return this.errorProvider.Icon;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.errorProvider.Icon = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the component is
    /// used in a locale that supports right-to-left fonts.
    /// </summary>
    [DefaultValue(false)]
    [Localizable(true)]
    public bool RightToLeft
    {
        get
        {
            return this.errorProvider.RightToLeft;
        }
        set
        {
            this.errorProvider.RightToLeft = value;
        }
    }

    /// <summary>
    /// Gets or sets the rate at which the error icon flashes.
    /// </summary>
    [DefaultValue(250)]
    [Localizable(true)]
    public int BlinkRate
    {
        get
        {
            return this.errorProvider.BlinkRate;
        }
        set
        {
            this.errorProvider.BlinkRate = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating when the error icon flashes.
    /// </summary>
    [DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
    [Localizable(true)]
    public ErrorBlinkStyle BlinkStyle
    {
        get
        {
            return this.errorProvider.BlinkStyle;
        }
        set
        {
            this.errorProvider.BlinkStyle = value;
        }
    }
}
#endregion Feild & Property

#region Constructor & Destructor
partial class Validator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public Validator()
    {
        this.InitializeComponent();
        this.Initialize();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="container">Container for current components</param>
    public Validator(IContainer container)
    : this()
    {
        if (container == null)
        {
            throw new ArgumentNullException("container");
        }

        container.Add(this);
    }

    private void Initialize()
    {
        this.infos = new Dictionary<Control, ValidationInfo>();
        this.errorProvider = new ErrorProvider(this.components);
    }
}
#endregion Constructor & Destructor

#region Event
partial class Validator
{
    private void validationForm_FormClosing(object sender,
                                            FormClosingEventArgs e)
    {
        if ((this.mode & ValidationMode.Submit) != ValidationMode.Submit)
        {
            return;
        }

        DialogResult dr = this.form.DialogResult;
        // Hook form closing event when verify input. Usually, we use "Yes" or "OK" button
        // to confirm user input.
        if ((dr == DialogResult.Yes)
                || (dr == DialogResult.OK))
        {
            if ((e.CloseReason == CloseReason.None)
                    && (!this.Validate()))
            {
                e.Cancel = true;
            }
        }
    }

    private void control_Validating(object sender, CancelEventArgs e)
    {
        Control control = sender as Control;
        ValidationInfo info = this.infos[control];

        info.Validate();

        // Decide whether can lost foucs.
        if (((this.mode & ValidationMode.FocusChange) !=
                ValidationMode.FocusChange)
                && (info.HasError))
        {
            e.Cancel = true;
        }
    }


    private void info_CustomValidation(object sender, ValidationEventArgs e)
    {
        if (this.CustomValidation != null)
        {
            this.CustomValidation(this, e);
        }
    }
}
#endregion Event

#region Method
/// <summary>
/// Method
/// </summary>
partial class Validator
{
    /// <summary>
    /// Add a new validation info.
    /// </summary>
    /// <param name="control">Control for added.</param>
    public void AddValidationInfo(Control control)
    {
        if (!this.infos.ContainsKey(control))
        {
            control.Validating += new CancelEventHandler(this.control_Validating);
            // Register validation info CustomValidation event.
            ValidationInfo info = new ValidationInfo(control, this.errorProvider);
            info.CustomValidation += new EventHandler<ValidationEventArgs>
                                     (this.info_CustomValidation);
            this.infos.Add(control, info);
        }
    }

    /// <summary>
    /// Remove specified control validation info.
    /// </summary>
    /// <param name="control"></param>
    public void RemoveValidationInfo(Control control)
    {
        if (this.infos.ContainsKey(control))
        {
            this.infos.Remove(control);
            control.Validating -= new CancelEventHandler(this.control_Validating);
        }
    }

    /// <summary>
    /// Clear all validation info.
    /// </summary>
    public void ClearValidationInfos()
    {
        // Set right to left message box options.
        MessageBoxOptions options = (MessageBoxOptions)0;
        if (this.RightToLeft)
        {
            options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
        }

        if (MessageBox.Show(
                    Resources.VALIDATOR_CLEARWARNING,
                    Resources.VALIDATOR_NAME,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    options) == DialogResult.Yes)
        {
            Control[] controls = new Control[this.infos.Keys.Count];
            int i = 0;
            // Do not directly remove validation infomation in the loop.
            // because, when remove a item, it should be change index.
            foreach (Control control in this.infos.Keys)
            {
                controls[i++] = control;
            }

            foreach (Control control in controls)
            {
                this.RemoveValidationInfo(control);
            }
        }
    }

    /// <summary>
    /// Validate all controls.
    /// </summary>
    /// <returns>If all control validated successful return true, or else false.</returns>
    public bool Validate()
    {
        if ((this.mode & ValidationMode.FocusChange) ==
                ValidationMode.FocusChange)
        {
            // Focus change mode.
            bool fail = false;
            foreach (Control control in this.infos.Keys)
            {
                ValidationInfo info = this.infos[control];
                info.Validate();
                if (!fail)
                {
                    fail = info.HasError;
                }
            }

            return !fail;
        }
        else
        {
            // Not allow focus change.
            foreach (Control control in this.infos.Keys)
            {
                ValidationInfo info = this.infos[control];
                info.Validate();
                if (info.HasError)
                {
                    control.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
#endregion Method

#region Add on
partial class Validator
{
    /// <summary>
    /// No exposed.
    /// </summary>
    private sealed class ValidatorDefaultIconAttribute : DefaultValueAttribute
    {
        public ValidatorDefaultIconAttribute(object icon)
        : base(icon)
        {

        }

        public override object Value
        {
            get
            {
                ErrorProvider errorProvider = new ErrorProvider();
                Icon icon = errorProvider.Icon;
                errorProvider.Dispose();
                return icon;
            }
        }
    }
}
#endregion Add on
}
