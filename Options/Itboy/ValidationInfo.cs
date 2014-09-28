// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace YoderZone.Extensions.Options.Itboy
{
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

partial class Validator
{
    #region Field & Property
    /// <summary>
    /// A ValidationInfo class represents a piece of validation unit for a control.
    /// </summary>
    private sealed partial class ValidationInfo
    {
        private ErrorProvider errorProvider;
        private Control control;

        private bool error;
        /// <summary>
        /// Gets a value indicating current control whether has exist error.
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.error;
            }
        }

        private ValidationType type;
        /// <summary>
        /// Gets or sets validation type for current control, it can be one or more validation type.
        /// </summary>
        public ValidationType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }


        private ValidationDataType dataType;
        /// <summary>
        /// Gets or sets a value indicating data type of control's text.
        /// </summary>
        public ValidationDataType DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = value;
            }
        }

        #region Required
        private string requiredMessage;
        /// <summary>
        /// Gets or sets a message when control's text is null or empty.
        /// </summary>
        public string RequiredMessage
        {
            get
            {
                return this.requiredMessage;
            }
            set
            {
                this.requiredMessage = value;
            }
        }
        #endregion Required

        #region RegularExpression
        private Regex regex;
        /// <summary>
        /// Gets or sets regex text for regular expression validation.
        /// </summary>
        public string RegularExpression
        {
            get
            {
                if (this.regex == null)
                {
                    return null;
                }
                else
                {
                    return this.regex.ToString();
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.regex = null;
                }
                else
                {
                    if (this.regex == null)
                    {
                        this.regex = new Regex(value.Trim());
                    }
                    else
                    {
                        RegexOptions options = this.regex.Options;
                        this.regex = new Regex(value.Trim(), options);
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets regex options for regular expression validation.
        /// </summary>
        public RegexOptions RegularExpressOptions
        {
            get
            {
                if (this.regex == null)
                {
                    return RegexOptions.None;
                }
                else
                {
                    return this.regex.Options;
                }
            }
            set
            {
                if (this.regex != null)
                {
                    string text = this.regex.ToString();
                    this.regex = new Regex(text, value);
                }
            }
        }

        private string regularExpressionMessage;
        /// <summary>
        /// Gets or sets a message when control's text is not match specified regex.
        /// </summary>
        public string RegularExpressionMessage
        {
            get
            {
                return this.regularExpressionMessage;
            }
            set
            {
                this.regularExpressionMessage = value;
            }
        }
        #endregion RegularExpression

        #region Compare
        private Control comparedControl;
        /// <summary>
        /// Gets or sets compared control with current control.
        /// </summary>
        public Control ComparedControl
        {
            get
            {
                return this.comparedControl;
            }
            set
            {
                this.comparedControl = value;
            }
        }

        private ValidationCompareOperator compareOperator;
        /// <summary>
        /// Gets or sets a value indicate how to compare to another control's data(text value).
        /// </summary>
        public ValidationCompareOperator CompareOperator
        {
            get
            {
                return this.compareOperator;
            }
            set
            {
                this.compareOperator = value;
            }
        }

        private string compareMessage;
        /// <summary>
        /// Gets or sets a message when control's data(text value) violate compare rule.
        /// </summary>
        public string CompareMessage
        {
            get
            {
                return this.compareMessage;
            }
            set
            {
                this.compareMessage = value;
            }
        }
        #endregion Compare

        #region Range
        private string rangeOfLowerBound;
        /// <summary>
        /// Gets or sets lower bound for control's data(text value).
        /// </summary>
        public string RangeOfLowerBound
        {
            get
            {
                return this.rangeOfLowerBound;
            }
            set
            {
                this.rangeOfLowerBound = value;
            }
        }

        private string rangeOfUpperBound;
        /// <summary>
        /// Gets or sets upper bound for control's data(text value).
        /// </summary>
        public string RangeOfUpperBound
        {
            get
            {
                return this.rangeOfUpperBound;
            }
            set
            {
                this.rangeOfUpperBound = value;
            }
        }

        private string rangeMessage;
        /// <summary>
        /// Gets or sets a message when control's data(text value) is not in range.
        /// </summary>
        public string RangeMessage
        {
            get
            {
                return this.rangeMessage;
            }
            set
            {
                this.rangeMessage = value;
            }
        }
        #endregion Range

        #region Custom
        /// <summary>
        /// Custom validation event. In face, it chain raise outside
        /// where register Validator CustomValidation.
        /// </summary>
        public event EventHandler<ValidationEventArgs> CustomValidation;

        private string customMessage;
        /// <summary>
        /// Gets or sets a message when control's data(text value) is not pass custom test.
        /// </summary>
        public string CustomMessage
        {
            get
            {
                return this.customMessage;
            }
            set
            {
                this.customMessage = value;
            }
        }
        #endregion Custom
    }
    #endregion Field & Property

    #region Constructor
    partial class ValidationInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="control">Validated control.</param>
        /// <param name="errorProvider">ErrorProvider instance.</param>
        public ValidationInfo(Control control, ErrorProvider errorProvider)
        {
            this.control = control;
            this.errorProvider = errorProvider;
        }
    }
    #endregion Constructor

    #region Method
    partial class ValidationInfo
    {
        #region Validate
        /// <summary>
        /// Validate current control's text.
        /// </summary>
        public void Validate()
        {
            // Prior level: Require, RegularExpression, Compare, range, custom
            // Required.
            if ((this.type & ValidationType.Required) == ValidationType.Required)
            {
                if (this.HasRequiredError())
                {
                    return;
                }
            }

            // RegularExpression
            if ((this.type & ValidationType.RegularExpression) ==
                    ValidationType.RegularExpression)
            {
                if (this.HasRegularExpressionError())
                {
                    return;
                }
            }

            // Compare
            if ((this.type & ValidationType.Compare) == ValidationType.Compare)
            {
                if (this.HasCompareError())
                {
                    return;
                }
            }

            // Compare
            if ((this.type & ValidationType.Range) == ValidationType.Range)
            {
                if (this.HasRangeError())
                {
                    return;
                }
            }

            // Custom
            if ((this.type & ValidationType.Custom) == ValidationType.Custom)
            {
                if (this.HasCustomError())
                {
                    return;
                }
            }

            // No error.
            this.SetError(ValidationType.None);
        }
        private bool HasRequiredError()
        {
            if (string.IsNullOrEmpty(this.control.Text.Trim()))
            {
                this.SetError(ValidationType.Required);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool HasRegularExpressionError()
        {
            if (string.IsNullOrEmpty(this.control.Text))
            {
                return false;
            }

            if (this.regex == null)
            {
                this.SetError(ValidationType.RegularExpression);
                return true;
            }
            else
            {
                if (!this.regex.IsMatch(this.control.Text))
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability",
                "CA1502:AvoidExcessiveComplexity")]
        private bool HasCompareError()
        {
            if (string.IsNullOrEmpty(this.control.Text))
            {
                return false;
            }

            if (this.compareOperator == ValidationCompareOperator.DataTypeCheck)
            {
                // Self test.
                switch (this.dataType)
                {
                    case ValidationDataType.Integer:
                    {
                        int value;
                        if (!int.TryParse(this.control.Text, out value))
                        {
                            this.SetError(ValidationType.Compare);
                            return true;
                        }
                    }
                    break;
                    case ValidationDataType.Double:
                    {
                        double value;
                        if (!double.TryParse(this.control.Text, out value))
                        {
                            this.SetError(ValidationType.Compare);
                            return true;
                        }
                    }
                    break;
                    case ValidationDataType.Float:
                    {
                        float value;
                        if (!float.TryParse(this.control.Text, out value))
                        {
                            this.SetError(ValidationType.Compare);
                            return true;
                        }
                    }
                    break;
                    case ValidationDataType.Date:
                    {
                        DateTime value;
                        if (!DateTime.TryParse(this.control.Text, out value))
                        {
                            this.SetError(ValidationType.Compare);
                            return true;
                        }
                    }
                    break;
                    case ValidationDataType.Currency:
                    {
                        DateTime value;
                        if (!DateTime.TryParse(this.control.Text, out value))
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
                if (this.comparedControl == null)
                {
                    this.SetError(ValidationType.Compare);
                    return true;
                }
                else
                {
                    // Compare.
                    switch (this.dataType)
                    {
                        case ValidationDataType.String:
                        {
                            string original = this.control.Text;
                            string destination = this.comparedControl.Text;
                            if (!EvaluateIsValid(original, destination, this.compareOperator))
                            {
                                this.SetError(ValidationType.Compare);
                                return true;
                            }
                        }
                        break;
                        case ValidationDataType.Integer:
                        {
                            int original;
                            int destination;
                            if ((!int.TryParse(this.control.Text, out original))
                                    || (!int.TryParse(this.comparedControl.Text, out destination))
                                    || (!EvaluateIsValid(original, destination, this.compareOperator)))
                            {
                                this.SetError(ValidationType.Compare);
                                return true;
                            }
                        }
                        break;
                        case ValidationDataType.Double:
                        {
                            double original;
                            double destination;
                            if ((!double.TryParse(this.control.Text, out original))
                                    || (!double.TryParse(this.comparedControl.Text, out destination))
                                    || (!EvaluateIsValid(original, destination, this.compareOperator)))
                            {
                                this.SetError(ValidationType.Compare);
                                return true;
                            }
                        }
                        break;
                        case ValidationDataType.Float:
                        {
                            float original;
                            float destination;
                            if ((!float.TryParse(this.control.Text, out original))
                                    || (!float.TryParse(this.comparedControl.Text, out destination))
                                    || (!EvaluateIsValid(original, destination, this.compareOperator)))
                            {
                                this.SetError(ValidationType.Compare);
                                return true;
                            }
                        }
                        break;
                        case ValidationDataType.Date:
                        {
                            DateTime original;
                            DateTime destination;
                            if ((!DateTime.TryParse(this.control.Text, out original))
                                    || (!DateTime.TryParse(this.comparedControl.Text, out destination))
                                    || (!EvaluateIsValid(original, destination, this.compareOperator)))
                            {
                                this.SetError(ValidationType.Compare);
                                return true;
                            }
                        }
                        break;
                        case ValidationDataType.Currency:
                        {
                            decimal original;
                            decimal destination;
                            if ((!decimal.TryParse(this.control.Text, out original))
                                    || (!decimal.TryParse(this.comparedControl.Text, out destination))
                                    || (!EvaluateIsValid(original, destination, this.compareOperator)))
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability",
                "CA1502:AvoidExcessiveComplexity")]
        private bool HasRangeError()
        {
            if (string.IsNullOrEmpty(this.control.Text))
            {
                return false;
            }

            switch (this.dataType)
            {
                case ValidationDataType.String:
                {
                    string lower = this.rangeOfLowerBound;
                    string value = this.control.Text;
                    string upper = this.rangeOfUpperBound;
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
                    int lower;
                    int upper;
                    int value;
                    if ((!int.TryParse(this.rangeOfLowerBound, out lower))
                            || (!int.TryParse(this.control.Text, out value))
                            || (!int.TryParse(this.rangeOfUpperBound, out upper))
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
                    double lower;
                    double upper;
                    double value;
                    if ((!double.TryParse(this.rangeOfLowerBound, out lower))
                            || (!double.TryParse(this.control.Text, out value))
                            || (!double.TryParse(this.rangeOfUpperBound, out upper))
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
                    float lower;
                    float upper;
                    float value;
                    if ((!float.TryParse(this.rangeOfLowerBound, out lower))
                            || (!float.TryParse(this.control.Text, out value))
                            || (!float.TryParse(this.rangeOfUpperBound, out upper))
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
                    DateTime lower;
                    DateTime upper;
                    DateTime value;
                    if ((!DateTime.TryParse(this.rangeOfLowerBound, out lower))
                            || (!DateTime.TryParse(this.control.Text, out value))
                            || (!DateTime.TryParse(this.rangeOfUpperBound, out upper))
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
                    decimal lower;
                    decimal upper;
                    decimal value;
                    if ((!decimal.TryParse(this.rangeOfLowerBound, out lower))
                            || (!decimal.TryParse(this.control.Text, out value))
                            || (!decimal.TryParse(this.rangeOfUpperBound, out upper))
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
        private bool HasCustomError()
        {
            if (string.IsNullOrEmpty(this.control.Text))
            {
                return false;
            }

            if (this.CustomValidation != null)
            {
                ValidationEventArgs args = new ValidationEventArgs(this.control);
                this.CustomValidation(this.control, args);
                if (!args.Valid)
                {
                    this.SetError(ValidationType.Custom);
                    return true;
                }
            }

            return false;
        }
        #endregion Validate

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
                        message = this.requiredMessage;
                    }
                    else
                    {
                        message = Resources.VALIDATOR_DEFAULT_REQUIREMESSAGE;
                    }
                    break;
                case ValidationType.RegularExpression:
                    if (!string.IsNullOrEmpty(this.regularExpressionMessage))
                    {
                        message = this.regularExpressionMessage;
                    }
                    else
                    {
                        message = Resources.VALIDATOR_DEFAULT_REGULAREXPRESSIONMESSAGE;
                    }
                    break;
                case ValidationType.Compare:
                    if (!string.IsNullOrEmpty(this.compareMessage))
                    {
                        message = this.compareMessage;
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
                    if (!string.IsNullOrEmpty(this.rangeMessage))
                    {
                        message = this.rangeMessage;
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
                    if (!string.IsNullOrEmpty(this.customMessage))
                    {
                        message = this.customMessage;
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
            this.errorProvider.SetError(this.control, message);

            if (message == null)
            {
                this.error = false;
            }
            else
            {
                this.error = true;
            }
        }

        #region Compare
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
        #endregion Compare
    }
    #endregion Method
}
}
