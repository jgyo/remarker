// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : RemarkTaggerProvider.cs
// Author           : Gil Yoder
// Created          : 08 26,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker
{
#region Imports

    using System.ComponentModel.Composition;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    using YoderZone.Extensions.Remarker.Service;

    #endregion

/// <summary>
///     A remark tagger provider.
/// </summary>
/// <seealso cref="T:Microsoft.VisualStudio.Text.Tagging.IViewTaggerProvider" />
[Export(typeof(IViewTaggerProvider))]
[ContentType("code")]
[TagType(typeof(ClassificationTag))]
public class RemarkTaggerProvider : IViewTaggerProvider
{
    #region Fields

#pragma warning disable 0649

    /// <summary>
    ///     The buffer tag aggregator factory service.
    /// </summary>
    [Import]
    internal IBufferTagAggregatorFactoryService
    AggregatorFactory;

    /// <summary>
    ///     The classification registry service.
    /// </summary>
    [Import]
    internal IClassificationTypeRegistryService RegistryService;

    [Import]
    internal IClassificationTypeRegistryService classificationTypeService;

    [Import]
    internal IEditorFormatMapService FormatMapService { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Creates a tagger.
    /// </summary>
    /// <tparam name="T">
    ///     Generic type parameter.
    /// </tparam>
    /// <param name="textView" type="ITextView">
    ///     The text view.
    /// </param>
    /// <param name="buffer" type="ITextBuffer">
    ///     The buffer.
    /// </param>
    /// <returns>
    ///     The new tagger.
    /// </returns>
    public ITagger<T> CreateTagger<T>(ITextView textView,
                                      ITextBuffer buffer) where T : ITag
    {

        var service = Package.GetGlobalService(typeof(IRemarkerService)) as
                      RemarkerService;
        ITagAggregator<IClassificationTag> tagAggregator =
            this.AggregatorFactory.CreateTagAggregator<IClassificationTag>(buffer);

        var tagger = (ITagger<T>)new RemarkTagger(
                         this.RegistryService,
                         tagAggregator, service);
        return tagger;
    }

    #endregion
}
}