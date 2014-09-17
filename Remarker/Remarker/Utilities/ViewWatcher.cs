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

namespace YoderZone.Extensions.Remarker.Remarker.Utilities
{
#region Imports

using System;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.Service;
using YoderZone.Extensions.Remarker.Remarker.Service;

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

    /// <summary>
    ///     The format map service.
    /// </summary>
    [Import]
    private IClassificationFormatMapService formatMapService;

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
    ///     model having a
    ///     matching content type.
    /// </summary>
    /// <param name="textView">
    ///     The newly created text view.
    /// </param>
    /// <seealso
    ///     cref="M:Microsoft.VisualStudio.Text.Editor.IWpfTextViewCreationListener.TextViewCreated(IWpfTextView)" />
    public void TextViewCreated(IWpfTextView textView)
    {
        service = Package.GetGlobalService(typeof(IRemarkerService)) as
                  RemarkerService;
        service.FormatSettingsChanged += service_FormatSettingsChanged;
        this.textView = textView;

        //textViews.Add(textView);
        this.textView.Closed += this.textView_Closed;

        this.UpdateFormatSettings();
    }

    private void service_FormatSettingsChanged(object sender, EventArgs args)
    {
        if (this.formater == null)
        {
            return;
        }

        formater.UpdateFormatDefinitions();
    }

    private void UpdateFormatSettings()
    {
        formater = this.textView.Properties.GetOrCreateSingletonProperty(
                       () =>
                       new FormatMapService(
            this.textView,
            this.service,
            this.formatMapService.GetClassificationFormatMap(this.textView),
            this.typeRegistry));
    }

    void textView_Closed(object sender, System.EventArgs e)
    {
        var wpfTextView = sender as IWpfTextView;
        if (wpfTextView == null)
        {
            return;
        }

        service.FormatSettingsChanged -= service_FormatSettingsChanged;
        wpfTextView.Closed -= this.textView_Closed;
    }

    #endregion
}
}