// ---------------------------------------------------------------------------
// <copyright file="RemarkTagger.cs" company="Gil Yoder">
//   Copyright 2014 by Gil Yoder
//   3731 Crescent Drive
//   Pearland, TX 77584
//
//   Gil.Yoder@outlook.com
// </copyright>
// <summary>
//
// </summary>
// ---------------------------------------------------------------------------

namespace YoderZone.Extensions.Remarker
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;

public class CommentTagger<T> where T : RemarkerTag, ITagger<T>
{
    #region Fields

    /// <summary>
    /// The update lock.
    /// </summary>
    private readonly object updateLock = new object();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the HighlightWordSample.RemarkTagger class.
    /// </summary>
    /// <param name="view" type="ITextView">
    /// The view.
    /// </param>
    /// <param name="sourceBuffer" type="ITextBuffer">
    /// Buffer for source data.
    /// </param>
    /// <param name="textSearchService" type="ITextSearchService">
    /// The text search service.
    /// </param>
    /// <param name="textStructureNavigator" type="ITextStructureNavigator">
    /// The text structure navigator.
    /// </param>
    public CommentTagger(
        ITextView view,
        ITextBuffer sourceBuffer,
        ITextSearchService textSearchService,
        ITextStructureNavigator textStructureNavigator)
    {
        this.View = view;

        this.SourceBuffer = sourceBuffer;
        this.TextSearchService = textSearchService;
        this.TextStructureNavigator = textStructureNavigator;

        this.CommentSpans = new NormalizedSnapshotSpanCollection();

        this.View.LayoutChanged += this.ViewLayoutChanged;
    }

    #endregion

    #region Public Events

    /// <summary>
    /// Occurs when Tags Changed.
    /// </summary>
    public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the buffer for source data.
    /// </summary>
    /// <value>
    /// A Buffer for source data.
    /// </value>
    private ITextBuffer SourceBuffer { get; set; }

    /// <summary>
    /// Gets or sets the text search service.
    /// </summary>
    /// <value>
    /// The text search service.
    /// </value>
    private ITextSearchService TextSearchService { get; set; }

    /// <summary>
    /// Gets or sets the text structure navigator.
    /// </summary>
    /// <value>
    /// The text structure navigator.
    /// </value>
    private ITextStructureNavigator TextStructureNavigator { get; set; }

    /// <summary>
    /// Gets or sets the view.
    /// </summary>
    /// <value>
    /// The view.
    /// </value>
    private ITextView View { get; set; }

    /// <summary>
    /// The current set of words to highlight.
    /// </summary>
    /// <value>
    /// The word spans.
    /// </value>
    private NormalizedSnapshotSpanCollection CommentSpans { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Gets the tags in this collection.
    /// </summary>
    /// <param name="spans" type="NormalizedSnapshotSpanCollection">
    /// The spans.
    /// </param>
    /// <returns>
    /// An enumerator that allows foreach to be used to process the tags in this collection.
    /// </returns>
    public IEnumerable<ITagSpan<RemarkerTag>> GetTags(
        NormalizedSnapshotSpanCollection spans)
    {
        // Hold on to a "snapshot" of the comment spans, so that we maintain the same
        // collection throughout
        NormalizedSnapshotSpanCollection commentSpans = this.CommentSpans;

        if (spans.Count == 0 || this.CommentSpans.Count == 0)
        {
            yield break;
        }

        // If the requested snapshot isn't the same as the one our comments are on, translate our spans
        // to the expected snapshot
        if (spans[0].Snapshot != commentSpans[0].Snapshot)
        {
            commentSpans =
                new NormalizedSnapshotSpanCollection(
                commentSpans.Select(
                    span =>
                    span.TranslateTo(spans[0].Snapshot, SpanTrackingMode.EdgeExclusive)));
        }

        // Second, yield all the spans in the file.
        foreach (SnapshotSpan span in NormalizedSnapshotSpanCollection.Overlap(
                     spans, commentSpans)
                )
        {
            yield return new TagSpan<RemarkerTag>(span, new RemarkerTag());
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Perform a synchronous update, in case multiple background threads are running.
    /// </summary>
    /// <param name="newSpans" type="NormalizedSnapshotSpanCollection">
    /// The new spans.
    /// </param>
    private void SynchronousUpdate(NormalizedSnapshotSpanCollection newSpans)
    {
        lock (this.updateLock)
        {
            this.CommentSpans = newSpans;

            var h = this.TagsChanged;
            if (h == null)
            {
                return;
            }

            h(
                this,
                new SnapshotSpanEventArgs(
                    new SnapshotSpan(
                        this.SourceBuffer.CurrentSnapshot,
                        0,
                        this.SourceBuffer.CurrentSnapshot.Length)));
        }
    }

    /// <summary>
    /// Updates at caret position described by caretPosition.
    /// </summary>
    /// <param name="sequence" type="int">
    /// Update sequence number.
    /// </param>
    private void QueueUpdate(int sequence)
    {
        ThreadPool.QueueUserWorkItem(this.UpdateCommentAdornments, sequence);
    }

    /// <summary>
    /// Updates the word adornments described by threadContext.
    /// </summary>
    /// <param name="state" type="object">
    /// Context for the thread.
    /// </param>
    private void UpdateCommentAdornments(object state)
    {
        var sequence = state is int ? (int)state : 0;
        var commentSpans = new List<SnapshotSpan>();

        // FindData(String, ITextSnapShot)
        // FindData(String, ITextSnapShot, FindOptions, ITextStructorNavigator);

        const string microComment = @"";
        const string tinyComment = @"";
        const string smallComment = @"";
        const string largeComment = @"";
        const string largerComment = @"";
        const string largestComment = @"";
        const string importantComment = @"";
        const string questionComment = @"";


        // Find the new spans
        var findData = new FindData(microComment, this.View.TextSnapshot);
        findData.FindOptions = FindOptions.SingleLine |
                               FindOptions.UseRegularExpressions;

        commentSpans.AddRange(this.TextSearchService.FindAll(findData));

        // if the update sequence has changed, skip the update this round.
        if (this.updateSequence != sequence)
        {
            return;
        }

        this.SynchronousUpdate(new NormalizedSnapshotSpanCollection(
                                   commentSpans));
    }

    private int updateSequence;

    /// <summary>
    /// View layout changed.
    /// </summary>
    /// <param name="sender" type="object">
    /// Source of the event.
    /// </param>
    /// <param name="e" type="TextViewLayoutChangedEventArgs">
    /// Text view layout changed event information.
    /// </param>
    private void ViewLayoutChanged(object sender,
                                   TextViewLayoutChangedEventArgs e)
    {
        // If a new snapshot wasn't generated, then skip this layout
        if (e.NewViewState.EditSnapshot == e.OldViewState.EditSnapshot)
        {
            return;
        }

        this.updateSequence++;
        if (this.updateSequence == int.MaxValue)
        {
            this.updateSequence = int.MinValue;
        }

        this.QueueUpdate(this.updateSequence);
    }

    #endregion
}
}