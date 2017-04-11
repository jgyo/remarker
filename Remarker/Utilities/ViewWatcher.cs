// ***********************************************************************
// Assembly         : Remarker
// Solution         : YoderZone.Com.Extensions
// File name        : ViewWatcher.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 03, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Utilities
{
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Utilities;
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;
    using YoderZone.Extensions.Remarker.Service;

    /// <summary>
    ///     A view watcher.
    /// </summary>
    /// <seealso
    ///     cref="T:Microsoft.VisualStudio.Text.Editor.IWpfTextViewCreationListener" />
    [TextViewRole("DOCUMENT")]
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("code")]
    internal sealed class ViewWatcher : IWpfTextViewCreationListener
    {
#pragma warning disable 0649

        private FormatMapService _formater;

        /// <summary>
        ///     The format map service.
        /// </summary>
        [Import]
        private IClassificationFormatMapService _formatMapService;

#pragma warning disable 0649

        private RemarkerService _service;

        private IWpfTextView _textView;

        /// <summary>
        ///     The type registry.
        /// </summary>
        [Import]
        private IClassificationTypeRegistryService _typeRegistry;

        //private List<IWpfTextView> textViews = new List<IWpfTextView>();

        /// <summary>
        ///     Called when a text view having matching roles is created over a text data
        ///     model having a matching content type.
        /// </summary>
        /// <param name="view">
        ///     The newly created text view.
        /// </param>
        /// <seealso
        ///     cref="M:Microsoft.VisualStudio.Text.Editor.IWpfTextViewCreationListener.TextViewCreated(IWpfTextView)" />
        public void TextViewCreated(IWpfTextView view)
        {
            this._service = Package.GetGlobalService(typeof(IRemarkerService)) as
                           RemarkerService;
            if (this._service == null)
            {
                return;
            }

            this._service.SettingsChanged += this.ServiceSettingsChanged;
            this._textView = view;

            //textViews.Add(view);
            this._textView.Closed += this.TextView_Closed;

            this.UpdateFormatSettings();
        }

        private void ServiceSettingsChanged(object sender, EventArgs args) => this._formater?.UpdateFormatDefinitions();

        private void TextView_Closed(object sender, EventArgs e)
        {
            Contract.Requires(sender != null);

            var wpfTextView = sender as IWpfTextView;
            if (wpfTextView == null)
            {
                return;
            }

            this._service.SettingsChanged -= this.ServiceSettingsChanged;
            wpfTextView.Closed -= this.TextView_Closed;
        }

        private void UpdateFormatSettings()
        {
            this._formater = this._textView.Properties.GetOrCreateSingletonProperty(
                                () =>
                                new FormatMapService(
                this._textView,
                this._service,
                this._formatMapService.GetClassificationFormatMap(this._textView),
                this._typeRegistry));

            if (this._formater == null)
            {
            }
        }
    }
}