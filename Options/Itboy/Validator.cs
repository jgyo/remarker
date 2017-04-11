// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace YoderZone.Extensions.Options.Itboy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

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
        [DefaultValue(null)]
        [Localizable(true)]
        public Control GetComparedControl(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.ComparedControl;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetCompareMessage(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.CompareMessage;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(ValidationCompareOperator.DataTypeCheck)]
        [Localizable(true)]
        public ValidationCompareOperator GetCompareOperator(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.CompareOperator;
            }
            else
            {
                return ValidationCompareOperator.DataTypeCheck;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetCustomMessage(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.CustomMessage;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(ValidationDataType.String)]
        [Localizable(true)]
        public ValidationDataType GetDataType(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.DataType;
            }
            else
            {
                return ValidationDataType.String;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetRangeMessage(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RangeMessage;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetRangeOfLowerBound(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RangeOfLowerBound;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetRangeOfUpperBound(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RangeOfUpperBound;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue((string)null)]
        [Localizable(true)]
        public string GetRegularExpression(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RegularExpression;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetRegularExpressionMessage(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RegularExpressionMessage;
            }
            else
            {
                return null;
            }
        }

        [DefaultValue(RegexOptions.None)]
        [Localizable(true)]
        public RegexOptions GetRegularExpressionOptions(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RegularExpressOptions;
            }
            else
            {
                return RegexOptions.None;
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public string GetRequiredMessage(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.RequiredMessage;
            }
            else
            {
                return null;
            }
        }

        [Localizable(true)]
        public ValidationType GetType(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                ValidationInfo info = this._infos[control];
                return info.Type;
            }
            else
            {
                return ValidationType.None;
            }
        }

        public void SetComparedControl(Control control, Control comparedControl)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.ComparedControl = comparedControl;
        }

        public void SetCompareMessage(Control control, string message)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.CompareMessage = message;
        }

        public void SetCompareOperator(Control control,
                                       ValidationCompareOperator compareOperator)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.CompareOperator = compareOperator;
        }

        public void SetCustomMessage(Control control, string message)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.CustomMessage = message;
        }

        public void SetDataType(Control control, ValidationDataType dataType)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.DataType = dataType;
        }

        public void SetRangeMessage(Control control, string message)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RangeMessage = message;
        }

        public void SetRangeOfLowerBound(Control control, string value)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RangeOfLowerBound = value;
        }

        public void SetRangeOfUpperBound(Control control, string value)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RangeOfUpperBound = value;
        }

        public void SetRegularExpression(Control control,
                                         string regularExpression)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RegularExpression = regularExpression;
        }

        public void SetRegularExpressionMessage(Control control, string message)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RegularExpressionMessage = message;
        }

        public void SetRegularExpressionOptions(Control control,
                                                RegexOptions options)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RegularExpressOptions = options;
        }

        public void SetRequiredMessage(Control control, string message)
        {
            if (!this._infos.ContainsKey(control))
            {
                this.AddValidationInfo(control);
            }

            ValidationInfo info = this._infos[control];
            info.RequiredMessage = message;
        }

        public void SetType(Control control, ValidationType type)
        {
            if (this._infos.ContainsKey(control))
            {
                // Especially, in design mode and type is none, ask whether remove validation information.
                if ((this.DesignMode)
                        && (type == ValidationType.None))
                {
                    // Set right to left message box options.
                    var options = (MessageBoxOptions)0;
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
                    this._infos[control].Type = type;
                }
            }
            else
            {
                this.AddValidationInfo(control);
                ValidationInfo info = this._infos[control];
                info.Type = type;
            }
        }
    }

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

    partial class Validator
    {
        private ErrorProvider _errorProvider;

        private Form _form;

        private Dictionary<Control, ValidationInfo> _infos;

        private ValidationMode _mode = ValidationMode.FocusChange |
                                              ValidationMode.Submit;

        /// <summary>
        /// Custom validation, it will raise when set "Custom" flag of validation type.
        /// Set "Valid" to true in ValidationEventArgs if pass custom test, or else, false.
        /// </summary>
        public event EventHandler<ValidationEventArgs> CustomValidation;

        /// <summary>
        /// Gets or sets the rate at which the error icon flashes.
        /// </summary>
        [DefaultValue(250)]
        [Localizable(true)]
        public int BlinkRate
        {
            get => this._errorProvider.BlinkRate;
            set => this._errorProvider.BlinkRate = value;
        }

        /// <summary>
        /// Gets or sets a value indicating when the error icon flashes.
        /// </summary>
        [DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
        [Localizable(true)]
        public ErrorBlinkStyle BlinkStyle
        {
            get => this._errorProvider.BlinkStyle;
            set => this._errorProvider.BlinkStyle = value;
        }

        [DefaultValue(null)]
        [Localizable(true)]
        public Form Form
        {
            get => this._form;
            set
            {
                if (this._form != value)
                {
                    // Unregister previous form and register current form.
                    if (this._form != null)
                    {
                        this._form.FormClosing -= new FormClosingEventHandler(
                                                     this.ValidationForm_FormClosing);
                    }

                    this._form = value;

                    if (this._form != null)
                    {
                        this._form.FormClosing += new FormClosingEventHandler(
                                                     this.ValidationForm_FormClosing);
                    }
                }
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
            get => this._errorProvider.Icon;
            set => this._errorProvider.Icon = value ?? throw new ArgumentNullException("value");
        }

        /// <summary>
        /// Gets or sets validation mode for specified form.
        /// </summary>
        [DefaultValue(ValidationMode.FocusChange | ValidationMode.Submit)]
        [Localizable(true)]
        public ValidationMode Mode
        {
            get => this._mode;
            set => this._mode = value;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the component is
        /// used in a locale that supports right-to-left fonts.
        /// </summary>
        [DefaultValue(false)]
        [Localizable(true)]
        public bool RightToLeft
        {
            get => this._errorProvider.RightToLeft;
            set => this._errorProvider.RightToLeft = value;
        }

        public bool HasError(Control control)
        {
            if (this._infos == null || this._infos.ContainsKey(control) == false)
            {
                return false;
            }

            return this._infos[control].HasError;
        }
    }

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
            this._infos = new Dictionary<Control, ValidationInfo>();
            this._errorProvider = new ErrorProvider(this.components);
        }
    }

    partial class Validator
    {
        private void Control_Validating(object sender, CancelEventArgs e)
        {
            var control = sender as Control;
            ValidationInfo info = this._infos[control];

            info.Validate();

            // Decide whether can lost foucs.
            if (((this._mode & ValidationMode.FocusChange) !=
                    ValidationMode.FocusChange)
                    && (info.HasError))
            {
                e.Cancel = true;
            }
        }

        private void Info_CustomValidation(object sender, ValidationEventArgs e) => this.CustomValidation?.Invoke(this, e);

        private void ValidationForm_FormClosing(object sender,
                                                                FormClosingEventArgs e)
        {
            if ((this._mode & ValidationMode.Submit) != ValidationMode.Submit)
            {
                return;
            }

            DialogResult dr = this._form.DialogResult;
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
    }

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
            if (!this._infos.ContainsKey(control))
            {
                control.Validating += new CancelEventHandler(this.Control_Validating);
                // Register validation info CustomValidation event.
                var info = new ValidationInfo(control, this._errorProvider);
                info.CustomValidation += new EventHandler<ValidationEventArgs>
                                         (this.Info_CustomValidation);
                this._infos.Add(control, info);
            }
        }

        /// <summary>
        /// Clear all validation info.
        /// </summary>
        public void ClearValidationInfos()
        {
            // Set right to left message box options.
            var options = (MessageBoxOptions)0;
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
                Control[] controls = new Control[this._infos.Keys.Count];
                int i = 0;
                // Do not directly remove validation infomation in the loop.
                // because, when remove a item, it should be change index.
                foreach (Control control in this._infos.Keys)
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
        /// Remove specified control validation info.
        /// </summary>
        /// <param name="control"></param>
        public void RemoveValidationInfo(Control control)
        {
            if (this._infos.ContainsKey(control))
            {
                this._infos.Remove(control);
                control.Validating -= new CancelEventHandler(this.Control_Validating);
            }
        }

        /// <summary>
        /// Validate all controls.
        /// </summary>
        /// <returns>If all control validated successful return true, or else false.</returns>
        public bool Validate()
        {
            if ((this._mode & ValidationMode.FocusChange) ==
                    ValidationMode.FocusChange)
            {
                // Focus change mode.
                bool fail = false;
                foreach (Control control in this._infos.Keys)
                {
                    ValidationInfo info = this._infos[control];
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
                foreach (Control control in this._infos.Keys)
                {
                    ValidationInfo info = this._infos[control];
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
                    var errorProvider = new ErrorProvider();
                    Icon icon = errorProvider.Icon;
                    errorProvider.Dispose();
                    return icon;
                }
            }
        }
    }
}