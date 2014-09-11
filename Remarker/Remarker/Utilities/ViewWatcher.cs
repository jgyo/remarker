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

namespace YoderZone.Extensions.OptionsPackage.Remarker.Utilities
{
#region Imports

using System.Collections.Generic;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

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

    private List<IWpfTextView> textViews = new List<IWpfTextView>();

    #endregion

    #region Public Methods and Operators

    public static ViewWatcher Default { get; private set; }

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
        if (Default == null)
        {
            Default = this;
        }

        textViews.Add(textView);
        textView.Closed += textView_Closed;

        textView.Properties.GetOrCreateSingletonProperty(
            () =>
            new FormatMapService(
            textView,
            this.formatMapService.GetClassificationFormatMap(textView),
            this.typeRegistry));
    }

    void textView_Closed(object sender, System.EventArgs e)
    {
        var wpfTextView = sender as IWpfTextView;
        if (wpfTextView == null)
        {
            return;
        }

        textViews.Remove(wpfTextView);
        wpfTextView.Closed -= textView_Closed;
    }

    #endregion
}
}