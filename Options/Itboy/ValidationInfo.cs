// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace YoderZone.Extensions.Options.Itboy
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    partial class Validator
    {
        /// <summary>
        /// A ValidationInfo class represents a piece of validation unit for a control.
        /// </summary>
        private sealed partial class ValidationInfo
        {
            private Control _comparedControl;
            private string _compareMessage;
            private ValidationCompareOperator _compareOperator;
            private Control _control;
            private string _customMessage;
            private ValidationDataType _dataType;
            private bool _error;
            private ErrorProvider _errorProvider;
            private string _rangeMessage;
            private string _rangeOfLowerBound;
            private string _rangeOfUpperBound;
            private Regex _regex;
            private string _regularExpressionMessage;
            private string _requiredMessage;
            private ValidationType _type;

            /// <summary>
            /// Custom validation event. In face, it chain raise outside
            /// where register Validator CustomValidation.
            /// </summary>
            public event EventHandler<ValidationEventArgs> CustomValidation;

            /// <summary>
            /// Gets or sets compared control with current control.
            /// </summary>
            public Control ComparedControl
            {
                get => this._comparedControl;
                set => this._comparedControl = value;
            }

            /// <summary>
            /// Gets or sets a message when control's data(text value) violate compare rule.
            /// </summary>
            public string CompareMessage
            {
                get => this._compareMessage;
                set => this._compareMessage = value;
            }

            /// <summary>
            /// Gets or sets a value indicate how to compare to another control's data(text value).
            /// </summary>
            public ValidationCompareOperator CompareOperator
            {
                get => this._compareOperator;
                set => this._compareOperator = value;
            }

            /// <summary>
            /// Gets or sets a message when control's data(text value) is not pass custom test.
            /// </summary>
            public string CustomMessage
            {
                get => this._customMessage;
                set => this._customMessage = value;
            }

            /// <summary>
            /// Gets or sets a value indicating data type of control's text.
            /// </summary>
            public ValidationDataType DataType
            {
                get => this._dataType;
                set => this._dataType = value;
            }

            /// <summary>
            /// Gets a value indicating current control whether has exist error.
            /// </summary>
            public bool HasError => this._error;

            /// <summary>
            /// Gets or sets a message when control's data(text value) is not in range.
            /// </summary>
            public string RangeMessage
            {
                get => this._rangeMessage;
                set => this._rangeMessage = value;
            }

            /// <summary>
            /// Gets or sets lower bound for control's data(text value).
            /// </summary>
            public string RangeOfLowerBound
            {
                get => this._rangeOfLowerBound;
                set => this._rangeOfLowerBound = value;
            }

            /// <summary>
            /// Gets or sets upper bound for control's data(text value).
            /// </summary>
            public string RangeOfUpperBound
            {
                get => this._rangeOfUpperBound;
                set => this._rangeOfUpperBound = value;
            }

            /// <summary>
            /// Gets or sets regex text for regular expression validation.
            /// </summary>
            public string RegularExpression
            {
                get
                {
                    if (this._regex == null)
                    {
                        return null;
                    }
                    else
                    {
                        return this._regex.ToString();
                    }
                }
                set
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this._regex = null;
                    }
                    else
                    {
                        if (this._regex == null)
                        {
                            this._regex = new Regex(value.Trim());
                        }
                        else
                        {
                            RegexOptions options = this._regex.Options;
                            this._regex = new Regex(value.Trim(), options);
                        }
                    }
                }
            }

            /// <summary>
            /// Gets or sets a message when control's text is not match specified regex.
            /// </summary>
            public string RegularExpressionMessage
            {
                get => this._regularExpressionMessage;
                set => this._regularExpressionMessage = value;
            }

            /// <summary>
            /// Gets or sets regex options for regular expression validation.
            /// </summary>
            public RegexOptions RegularExpressOptions
            {
                get
                {
                    if (this._regex == null)
                    {
                        return RegexOptions.None;
                    }
                    else
                    {
                        return this._regex.Options;
                    }
                }
                set
                {
                    if (this._regex != null)
                    {
                        string text = this._regex.ToString();
                        this._regex = new Regex(text, value);
                    }
                }
            }

            /// <summary>
            /// Gets or sets a message when control's text is null or empty.
            /// </summary>
            public string RequiredMessage
            {
                get => this._requiredMessage;
                set => this._requiredMessage = value;
            }

            /// <summary>
            /// Gets or sets validation type for current control, it can be one or more validation type.
            /// </summary>
            public ValidationType Type
            {
                get => this._type;
                set => this._type = value;
            }
        }

        partial class ValidationInfo
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="control">Validated control.</param>
            /// <param name="errorProvider">ErrorProvider instance.</param>
            public ValidationInfo(Control control, ErrorProvider errorProvider)
            {
                this._control = control;
                this._errorProvider = errorProvider;
            }
        }

        partial class ValidationInfo
        {
            /// <summary>
            /// Validate current control's text.
            /// </summary>
            public void Validate()
            {
                // Prior level: Require, RegularExpression, Compare, range, custom
                // Required.
                if ((this._type & ValidationType.Required) == ValidationType.Required)
                {
                    if (this.HasRequiredError())
                    {
                        return;
                    }
                }

                // RegularExpression
                if ((this._type & ValidationType.RegularExpression) ==
                        ValidationType.RegularExpression)
                {
                    if (this.HasRegularExpressionError())
                    {
                        return;
                    }
                }

                // Compare
                if ((this._type & ValidationType.Compare) == ValidationType.Compare)
                {
                    if (this.HasCompareError())
                    {
                        return;
                    }
                }

                // Compare
                if ((this._type & ValidationType.Range) == ValidationType.Range)
                {
                    if (this.HasRangeError())
                    {
                        return;
                    }
                }

                // Custom
                if ((this._type & ValidationType.Custom) == ValidationType.Custom)
                {
                    if (this.HasCustomError())
                    {
                        return;
                    }
                }

                // No error.
                this.SetError(ValidationType.None);
            }

            private static bool EvaluateIsValid(string original, string destnation,
                                                ValidationCompareOperator op)
            {
                bool valid = false;
                int result = string.Compare(original, destnation);
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (result == 0);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (result > 0);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (result >= 0);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (result < 0);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (result <= 0);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (result != 0);
                        break;
                }
                return valid;
            }

            private static bool EvaluateIsValid(int original, int destnation,
                                                ValidationCompareOperator op)
            {
                bool valid = false;
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (original == destnation);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (original > destnation);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (original >= destnation);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (original < destnation);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (original <= destnation);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (original != destnation);
                        break;
                }
                return valid;
            }

            private static bool EvaluateIsValid(double original, double destnation,
                                                ValidationCompareOperator op)
            {
                bool valid = false;
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (original == destnation);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (original > destnation);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (original >= destnation);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (original < destnation);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (original <= destnation);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (original != destnation);
                        break;
                }
                return valid;
            }

            private static bool EvaluateIsValid(float original, float destnation,
                                                ValidationCompareOperator op)
            {
                bool valid = false;
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (original == destnation);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (original > destnation);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (original >= destnation);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (original < destnation);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (original <= destnation);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (original != destnation);
                        break;
                }
                return valid;
            }

            private static bool EvaluateIsValid(DateTime original,
                                                DateTime destnation, ValidationCompareOperator op)
            {
                bool valid = false;
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (original == destnation);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (original > destnation);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (original >= destnation);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (original < destnation);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (original <= destnation);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (original != destnation);
                        break;
                }
                return valid;
            }

            private static bool EvaluateIsValid(decimal original, decimal destnation,
                                                ValidationCompareOperator op)
            {
                bool valid = false;
                switch (op)
                {
                    case ValidationCompareOperator.Equal:
                        valid = (original == destnation);
                        break;

                    case ValidationCompareOperator.GreaterThan:
                        valid = (original > destnation);
                        break;

                    case ValidationCompareOperator.GreaterThanEqual:
                        valid = (original >= destnation);
                        break;

                    case ValidationCompareOperator.LessThan:
                        valid = (original < destnation);
                        break;

                    case ValidationCompareOperator.LessThanEqual:
                        valid = (original <= destnation);
                        break;

                    case ValidationCompareOperator.NotEqual:
                        valid = (original != destnation);
                        break;
                }
                return valid;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability",
                                                                                            "CA1502:AvoidExcessiveComplexity")]
            private bool HasCompareError()
            {
                if (string.IsNullOrEmpty(this._control.Text))
                {
                    return false;
                }

                if (this._compareOperator == ValidationCompareOperator.DataTypeCheck)
                {
                    // Self test.
                    switch (this._dataType)
                    {
                        case ValidationDataType.Integer:
                            {
                                if (!int.TryParse(this._control.Text, out var value))
                                {
                                    this.SetError(ValidationType.Compare);
                                    return true;
                                }
                            }
                            break;

                        case ValidationDataType.Double:
                            {
                                if (!double.TryParse(this._control.Text, out var value))
                                {
                                    this.SetError(ValidationType.Compare);
                                    return true;
                                }
                            }
                            break;

                        case ValidationDataType.Float:
                            {
                                if (!float.TryParse(this._control.Text, out var value))
                                {
                                    this.SetError(ValidationType.Compare);
                                    return true;
                                }
                            }
                            break;

                        case ValidationDataType.Date:
                            {
                                if (!DateTime.TryParse(this._control.Text, out var value))
                                {
                                    this.SetError(ValidationType.Compare);
                                    return true;
                                }
                            }
                            break;

                        case ValidationDataType.Currency:
                            {
                                if (!DateTime.TryParse(this._control.Text, out var value))
                                {
                                    this.SetError(ValidationType.Compare);
                                    return true;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (this._comparedControl == null)
                    {
                        this.SetError(ValidationType.Compare);
                        return true;
                    }
                    else
                    {
                        // Compare.
                        switch (this._dataType)
                        {
                            case ValidationDataType.String:
                                {
                                    string original = this._control.Text;
                                    string destination = this._comparedControl.Text;
                                    if (!EvaluateIsValid(original, destination, this._compareOperator))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;

                            case ValidationDataType.Integer:
                                {
                                    if ((!int.TryParse(this._control.Text, out var original))
|| (!int.TryParse(this._comparedControl.Text, out var destination))
|| (!EvaluateIsValid(original, destination, this._compareOperator)))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;

                            case ValidationDataType.Double:
                                {
                                    if ((!double.TryParse(this._control.Text, out var original))
|| (!double.TryParse(this._comparedControl.Text, out var destination))
|| (!EvaluateIsValid(original, destination, this._compareOperator)))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;

                            case ValidationDataType.Float:
                                {
                                    if ((!float.TryParse(this._control.Text, out var original))
|| (!float.TryParse(this._comparedControl.Text, out var destination))
|| (!EvaluateIsValid(original, destination, this._compareOperator)))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;

                            case ValidationDataType.Date:
                                {
                                    if ((!DateTime.TryParse(this._control.Text, out var original))
|| (!DateTime.TryParse(this._comparedControl.Text, out var destination))
|| (!EvaluateIsValid(original, destination, this._compareOperator)))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;

                            case ValidationDataType.Currency:
                                {
                                    if ((!decimal.TryParse(this._control.Text, out var original))
|| (!decimal.TryParse(this._comparedControl.Text, out var destination))
|| (!EvaluateIsValid(original, destination, this._compareOperator)))
                                    {
                                        this.SetError(ValidationType.Compare);
                                        return true;
                                    }
                                }
                                break;
                        }
                    }
                }
                return false;
            }

            private bool HasCustomError()
            {
                if (string.IsNullOrEmpty(this._control.Text))
                {
                    return false;
                }

                if (this.CustomValidation != null)
                {
                    var args = new ValidationEventArgs(this._control);
                    this.CustomValidation(this._control, args);
                    if (!args.Valid)
                    {
                        this.SetError(ValidationType.Custom);
                        return true;
                    }
                }

                return false;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability",
                    "CA1502:AvoidExcessiveComplexity")]
            private bool HasRangeError()
            {
                if (string.IsNullOrEmpty(this._control.Text))
                {
                    return false;
                }

                switch (this._dataType)
                {
                    case ValidationDataType.String:
                        {
                            string lower = this._rangeOfLowerBound;
                            string value = this._control.Text;
                            string upper = this._rangeOfUpperBound;
                            if ((!EvaluateIsValid(value, lower,
                                                  ValidationCompareOperator.GreaterThanEqual))
                                    || (!EvaluateIsValid(value, upper,
                                                         ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;

                    case ValidationDataType.Integer:
                        {
                            if ((!int.TryParse(this._rangeOfLowerBound, out var lower))
|| (!int.TryParse(this._control.Text, out var value))
|| (!int.TryParse(this._rangeOfUpperBound, out var upper))
|| (!EvaluateIsValid(value, lower,
ValidationCompareOperator.GreaterThanEqual))
|| (!EvaluateIsValid(value, upper,
ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;

                    case ValidationDataType.Double:
                        {
                            if ((!double.TryParse(this._rangeOfLowerBound, out var lower))
|| (!double.TryParse(this._control.Text, out var value))
|| (!double.TryParse(this._rangeOfUpperBound, out var upper))
|| (!EvaluateIsValid(value, lower,
ValidationCompareOperator.GreaterThanEqual))
|| (!EvaluateIsValid(value, upper,
ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;

                    case ValidationDataType.Float:
                        {
                            if ((!float.TryParse(this._rangeOfLowerBound, out var lower))
|| (!float.TryParse(this._control.Text, out var value))
|| (!float.TryParse(this._rangeOfUpperBound, out var upper))
|| (!EvaluateIsValid(value, lower,
ValidationCompareOperator.GreaterThanEqual))
|| (!EvaluateIsValid(value, upper,
ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;

                    case ValidationDataType.Date:
                        {
                            if ((!DateTime.TryParse(this._rangeOfLowerBound, out var lower))
|| (!DateTime.TryParse(this._control.Text, out var value))
|| (!DateTime.TryParse(this._rangeOfUpperBound, out var upper))
|| (!EvaluateIsValid(value, lower,
ValidationCompareOperator.GreaterThanEqual))
|| (!EvaluateIsValid(value, upper,
ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;

                    case ValidationDataType.Currency:
                        {
                            if ((!decimal.TryParse(this._rangeOfLowerBound, out var lower))
|| (!decimal.TryParse(this._control.Text, out var value))
|| (!decimal.TryParse(this._rangeOfUpperBound, out var upper))
|| (!EvaluateIsValid(value, lower,
ValidationCompareOperator.GreaterThanEqual))
|| (!EvaluateIsValid(value, upper,
ValidationCompareOperator.LessThanEqual)))
                            {
                                this.SetError(ValidationType.Range);
                                return true;
                            }
                        }
                        break;
                }

                return false;
            }

            private bool HasRegularExpressionError()
            {
                if (string.IsNullOrEmpty(this._control.Text))
                {
                    return false;
                }

                if (this._regex == null)
                {
                    this.SetError(ValidationType.RegularExpression);
                    return true;
                }
                else
                {
                    if (!this._regex.IsMatch(this._control.Text))
                    {
                        this.SetError(ValidationType.RegularExpression);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            private bool HasRequiredError()
            {
                if (string.IsNullOrEmpty(this._control.Text.Trim()))
                {
                    this.SetError(ValidationType.Required);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private void SetError(ValidationType errorType)
            {
                string message = null;
                switch (errorType)
                {
                    case ValidationType.None:
                        message = null;
                        break;

                    case ValidationType.Required:
                        if (!string.IsNullOrEmpty(this.RequiredMessage))
                        {
                            message = this._requiredMessage;
                        }
                        else
                        {
                            message = Resources.VALIDATOR_DEFAULT_REQUIREMESSAGE;
                        }
                        break;

                    case ValidationType.RegularExpression:
                        if (!string.IsNullOrEmpty(this._regularExpressionMessage))
                        {
                            message = this._regularExpressionMessage;
                        }
                        else
                        {
                            message = Resources.VALIDATOR_DEFAULT_REGULAREXPRESSIONMESSAGE;
                        }
                        break;

                    case ValidationType.Compare:
                        if (!string.IsNullOrEmpty(this._compareMessage))
                        {
                            message = this._compareMessage;
                            message = message.Replace("{$CompareOperator}",
                                                      this.CompareOperator.ToString());
                            message = message.Replace("{$DataType}", this.DataType.ToString());
                        }
                        else
                        {
                            message = Resources.VALIDATOR_DEFAULT_COMPAREMESSAGE;
                            message = message.Replace("{$CompareOperator}",
                                                      this.CompareOperator.ToString());
                            message = message.Replace("{$DataType}", this.DataType.ToString());
                        }
                        break;

                    case ValidationType.Range:
                        if (!string.IsNullOrEmpty(this._rangeMessage))
                        {
                            message = this._rangeMessage;
                            message = message.Replace("{$RangeOfLowerBound}", this.RangeOfLowerBound);
                            message = message.Replace("{$RangeOfUpperBound}", this.RangeOfUpperBound);
                        }
                        else
                        {
                            message = Resources.VALIDATOR_DEFAULT_RANGEMESSAGE;
                            message = message.Replace("{$RangeOfLowerBound}", this.RangeOfLowerBound);
                            message = message.Replace("{$RangeOfUpperBound}", this.RangeOfUpperBound);
                        }
                        break;

                    case ValidationType.Custom:
                        if (!string.IsNullOrEmpty(this._customMessage))
                        {
                            message = this._customMessage;
                        }
                        else
                        {
                            message = Resources.VALIDATOR_DEFAULT_CUSTOMMESSAGE;
                        }
                        break;

                    default:
                        message = null;
                        break;
                }
                this._errorProvider.SetError(this._control, message);

                if (message == null)
                {
                    this._error = false;
                }
                else
                {
                    this._error = true;
                }
            }
        }
    }
}