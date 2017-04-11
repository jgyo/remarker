// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : FormatMapService.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 10, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Utilities
{
    #region Imports

    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using System;
    using System.Diagnostics.Contracts;
    using System.Windows.Media;
    using YoderZone.Extensions.Remarker.FormatDefinitions;
    using YoderZone.Extensions.Remarker.Service;

    #endregion Imports

    /// <summary>
    ///     A format map service.
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    internal sealed class FormatMapService : IDisposable
    {
        #region Fields

        /// <summary>
        ///     The format map.
        /// </summary>
        private IClassificationFormatMap _formatMap;

        /// <summary>
        ///     The text view.
        /// </summary>
        private ITextView _textView;

        /// <summary>
        ///     The type registry.
        /// </summary>
        private IClassificationTypeRegistryService _typeRegistry;

        private readonly RemarkerService _service;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     YoderZone.Extensions.Utilities.FormatMapService
        ///     class.
        /// </summary>
        /// <param name="textView" type="ITextView">
        ///     The text view.
        /// </param>
        /// <param name="service"></param>
        /// <param name="formatMap" type="IClassificationFormatMap">
        ///     The format map.
        /// </param>
        /// <param name="typeRegistry" type="IClassificationTypeRegistryService">
        ///     The type registry.
        /// </param>
        public FormatMapService(ITextView textView, RemarkerService service,
                                IClassificationFormatMap formatMap,
                                IClassificationTypeRegistryService typeRegistry)
        {
            Contract.Requires(textView != null);
            Contract.Requires(service != null);
            Contract.Requires(formatMap != null);
            Contract.Requires(typeRegistry != null);

            //+ FormatMapService Constructor

            this._textView = textView;
            this._formatMap = formatMap;
            this._service = service;
            this._typeRegistry = typeRegistry;

            this.UpdateFormatDefinitions();
            textView.GotAggregateFocus += this.OnViewGotAggregateFocus;
            this._formatMap.ClassificationFormatMappingChanged +=
                this.FormatMapChanged;
            textView.Closed += this.OnViewClosed;
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or
        ///     resetting
        ///     unmanaged resources.
        /// </summary>
        /// <seealso cref="M:System.IDisposable.Dispose()" />
        public void Dispose()
        {
            if (this._textView != null)
            {
                this._textView.Closed -= this.OnViewClosed;
                this._textView.GotAggregateFocus -= this.OnViewGotAggregateFocus;
                this._textView = null;
            }
            if (this._formatMap != null)
            {
                this._formatMap.ClassificationFormatMappingChanged -=
                    this.FormatMapChanged;
                this._formatMap = null;
            }
            this._typeRegistry = null;
        }

        #endregion Public Methods and Operators

        #region Methods

        internal void UpdateFormatDefinitions()
        {
            //! This method is called by the constructor, and two event handlers:
            //! FormatMapChanged, and OnViewGotAggregateFocus. The latter
            //! makes the call only once.

            if (this._formatMap == null || this._textView != null &&
                    this._textView.IsClosed)
            {
                return;
            }

            bool isReentry = this._formatMap.IsInBatchUpdate;

            try
            {
                if (!isReentry)
                {
                    this._formatMap.BeginBatchUpdate();
                }

                foreach (var typeString in Definitions.ClassificationTypeStrings)
                {
                    //+ Handle Task classifications
                    if (typeString.StartsWith("Task", StringComparison.Ordinal))
                    {
                        IClassificationType taskType =
                            this._typeRegistry.GetClassificationType(typeString);

                        int index = 0;
                        switch (typeString)
                        {
                            case Definitions.Task01:
                                index = 0;
                                break;

                            case Definitions.Task02:
                                index = 1;
                                break;

                            case Definitions.Task03:
                                index = 2;
                                break;

                            case Definitions.Task04:
                                index = 3;
                                break;

                            case Definitions.Task05:
                                index = 4;
                                break;

                            case Definitions.Task06:
                                index = 5;
                                break;

                            case Definitions.Task07:
                                index = 6;
                                break;

                            case Definitions.Task08:
                                index = 7;
                                break;

                            case Definitions.Task09:
                                index = 8;
                                break;

                            case Definitions.Task10:
                                index = 9;
                                break;
                        }

                        this.UpdateTaskAppearance(
                            taskType,
                            this._service.ColorIndex[index],
                            this._service.FontFamilyIndex[index],
                            this._service.IsBoldIndex[index]);

                        continue;
                    }

                    var remarkSize = RemarkSize.Regular;

                    if (typeString.EndsWith("04em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Micro; /*emSizeFactor = 0.55d;*/
                    }
                    else if (typeString.EndsWith("06em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Tiny; /*emSizeFactor = 0.70d;*/
                    }
                    else if (typeString.EndsWith("08em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Small; /*emSizeFactor = 0.85d;*/
                    }
                    else if (typeString.EndsWith("15em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Large; /*emSizeFactor = 1.25d;*/
                    }
                    else if (typeString.EndsWith("20em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Huge; /*emSizeFactor = 1.5d;*/
                    }
                    else if (typeString.EndsWith("25em", StringComparison.OrdinalIgnoreCase))
                    {
                        remarkSize = RemarkSize.Gigantic; /*emSizeFactor = 2.0d;*/
                    }

                    IClassificationType classificationType =
                        this._typeRegistry.GetClassificationType(typeString);

                    this.UpdateCommentFontSize(classificationType, remarkSize);
                }
            }
            finally
            {
                if (!isReentry && this._formatMap.IsInBatchUpdate)
                {
                    this._formatMap.EndBatchUpdate();
                }
            }
        }

        /// <summary>
        ///     Format map changed.
        /// </summary>
        /// <param name="sender" type="object">
        ///     Source of the event.
        /// </param>
        /// <param name="e" type="EventArgs">
        ///     Event information.
        /// </param>
        private void FormatMapChanged(object sender, EventArgs e) => this.UpdateFormatDefinitions();

        /// <summary>
        ///     Event handler. Called by view for closed events.
        /// </summary>
        /// <param name="sender" type="object">
        ///     Source of the event.
        /// </param>
        /// <param name="e" type="EventArgs">
        ///     Event information.
        /// </param>
        private void OnViewClosed(object sender, EventArgs e)
        {
            if (sender is ITextView view)
            {
                view.Closed -= this.OnViewClosed;
            }

            this.Dispose();
        }

        private void OnViewGotAggregateFocus(object sender, EventArgs e)
        {
            if (sender is ITextView view)
            {
                view.GotAggregateFocus -= this.OnViewGotAggregateFocus;
            }

            this.UpdateFormatDefinitions();
        }

        //! Updates a comment's configuration definition.

        private void UpdateCommentFontSize(IClassificationType formatType,
                                           RemarkSize remarkSize)
        {
            Contract.Requires(formatType != null);
            Contract.Requires(Enum.IsDefined(typeof(RemarkSize),
                                                 remarkSize));

            // Get the default text classification.
            var textType = this._typeRegistry.GetClassificationType("text");
            var textProperties = this._formatMap.GetTextProperties(textType);

            // We must have the em size. Otherwise we may as well give up.
            if (textProperties.FontRenderingEmSizeEmpty)
            {
                // No hope.
                return;
            }

            // Calculate the font size based on the em size times the factor.
            var emSize = (float)textProperties.FontRenderingEmSize;
            this._service.EmSize = emSize;
            float fontSize;
            switch (remarkSize)
            {
                case RemarkSize.Micro:
                    fontSize = this._service.MicroFontSize;
                    break;

                case RemarkSize.Tiny:
                    fontSize = this._service.TinyFontSize;
                    break;

                case RemarkSize.Small:
                    fontSize = this._service.SmallFontSize;
                    break;

                case RemarkSize.Large:
                    fontSize = this._service.LargeFontSize;
                    break;

                case RemarkSize.Huge:
                    fontSize = this._service.HugeFontSize;
                    break;

                case RemarkSize.Gigantic:
                    fontSize = this._service.GiganticFontSize;
                    break;

                case RemarkSize.Regular:
                    fontSize = emSize;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(remarkSize));
            }

            var typeProperties = this._formatMap.GetTextProperties(
                                     formatType);

            // Update the font size.
            typeProperties = typeProperties.SetFontRenderingEmSize(fontSize);
            string fontFamilyName = this._service.FontFamily;

            if (fontFamilyName != null)
            {
                // Update the font type.
                typeProperties = typeProperties.SetTypeface(new Typeface(fontFamilyName));
            }

            // Save the changes to the classification type.
            this._formatMap.SetTextProperties(formatType, typeProperties);
        }

        //! Updates a Task configuration definition.

        private void UpdateTaskAppearance(
            IClassificationType taskType,
            string color,
            string font,
            bool isBold)
        {
            Contract.Requires(taskType != null);
            Contract.Requires(!string.IsNullOrEmpty(color));
            Contract.Requires(!string.IsNullOrEmpty(font));

            // Get the task's current settings for a basis (mainly the size).
            var textProperties = this._formatMap.GetTextProperties(taskType);

            // Update the task's typeface property.
            textProperties = textProperties.SetTypeface(new Typeface(font));

            // Update the task's color property.
            textProperties = textProperties.SetForegroundBrush(new SolidColorBrush(
                                 color.ConvertStringToWpfColor()));

            // Update the task's weight property.
            textProperties = textProperties.SetBold(isBold);
            this._formatMap.SetTextProperties(taskType, textProperties);
        }

        #endregion Methods
    }

    internal enum RemarkSize
    {
        Regular = 0,

        Micro,

        Tiny,

        Small,

        Large,

        Huge,

        Gigantic
    }
}