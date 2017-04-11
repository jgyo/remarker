// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace YoderZone.Extensions.Options.Itboy
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Provider data for custom validation - CustomValidation event.
    /// </summary>
    public sealed class ValidationEventArgs : EventArgs
    {
        private Control _control;
        private bool _valid = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="control">Validated control instance.</param>
        public ValidationEventArgs(Control control) => this._control = control;

        /// <summary>
        /// Gets control which when raise CustomValidation event.
        /// </summary>
        public Control Control => this._control;

        /// <summary>
        /// Gets or Sets a value indicating whether control text pass custom code test.
        /// </summary>
        public bool Valid
        {
            get => this._valid;
            set => this._valid = value;
        }
    }
}