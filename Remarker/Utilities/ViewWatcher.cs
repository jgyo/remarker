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
#region Imports

    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Utilities;

    using YoderZone.Extensions.Remarker.Service;

    #endregion

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
    #region Fields

#pragma warning disable 0649

    /// <summary>
    ///     The format map service.
    /// </summary>
    [Import]
    private IClassificationFormatMapService formatMapService;

#pragma warning disable 0649

    /// <summary>
    ///     The type registry.
    /// </summary>
    [Import]
    private IClassificationTypeRegistryService typeRegistry;

    private RemarkerService service;

    private IWpfTextView textView;

    private FormatMapService formater;

    //private List<IWpfTextView> textViews = new List<IWpfTextView>();

    #endregion

    #region Public Methods and Operators

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

        this.service = Package.GetGlobalService(typeof(IRemarkerService)) as
                       RemarkerService;
        if (this.service == null)
        {
            return;
        }

        this.service.SettingsChanged += this.ServiceSettingsChanged;
        this.textView = view;

        //textViews.Add(view);
        this.textView.Closed += this.textView_Closed;

        this.UpdateFormatSettings();
    }

    private void ServiceSettingsChanged(object sender, EventArgs args)
    {
        this.formater?.UpdateFormatDefinitions();
    }

    private void UpdateFormatSettings()
    {

        this.formater = this.textView.Properties.GetOrCreateSingletonProperty(
                            () =>
                            new FormatMapService(
            this.textView,
            this.service,
            this.formatMapService.GetClassificationFormatMap(this.textView),
            this.typeRegistry));

        if (this.formater == null)
        {
        }
    }

    void textView_Closed(object sender, EventArgs e)
    {
        Contract.Requires(sender != null);

        var wpfTextView = sender as IWpfTextView;
        if (wpfTextView == null)
        {
            return;
        }

        this.service.SettingsChanged -= this.ServiceSettingsChanged;
        wpfTextView.Closed -= this.textView_Closed;
    }

    #endregion
}
}