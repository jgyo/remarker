// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : ViewWatcher.cs
// Author           : Gil Yoder
// Created          : 08 29,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************
namespace YoderZone.Extensions.Remarker
{
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

/// <summary>
/// A view watcher.
/// </summary>
/// <seealso cref="T:Microsoft.VisualStudio.Text.Editor.IWpfTextViewCreationListener"/>
[TextViewRole("DOCUMENT")]
[Export(typeof(IWpfTextViewCreationListener))]
[ContentType("code")]
internal sealed class ViewWatcher : IWpfTextViewCreationListener
{

    /// <summary>
    /// The map service.
    /// </summary>
    [Import]
    private IClassificationFormatMapService formatMapService;

    /// <summary>
    /// The registry service.
    /// </summary>
    [Import]
    private IClassificationTypeRegistryService typeRegistry;

    /// <summary>
    /// Called when a text view having matching roles is created over a text data model having a
    /// matching content type.
    /// </summary>
    /// <param name="textView" type="IWpfTextView">
    /// The newly created text view.
    /// </param>
    /// <seealso cref="M:Microsoft.VisualStudio.Text.Editor.IWpfTextViewCreationListener.TextViewCreated(IWpfTextView)"/>
    public void TextViewCreated(IWpfTextView textView)
    {
        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Entering ViewWatcher.TextViewCreated.",
                        DateTime.Now);

        textView.Properties.GetOrCreateSingletonProperty(
            () =>
            new FormatMapWatcher(
            textView,
            this.formatMapService.GetClassificationFormatMap(textView),
            this.typeRegistry));

        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Exiting ViewWatcher.TextViewCreated.",
                        DateTime.Now);
    }
}
}