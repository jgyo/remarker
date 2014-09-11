namespace YoderZone.Extensions.Remarker
{
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;

using YoderZone.Extensions.Remarker.FormatDefinitions;

/// <summary>
/// A format map watcher.
/// </summary>
/// <seealso cref="T:System.IDisposable"/>
internal class FormatMapWatcher : IDisposable
{
    private readonly List<string> RemarkTypes = new List<string>()
    {
        Definitions.MicroCommentType,
        Definitions.TinyCommentType,
        Definitions.SmallCommentType,
        Definitions.NormalCommentType,
        Definitions.LargeCommentType,
        Definitions.HugeCommentType,
        Definitions.GiaganticCommentType,

        Definitions.MicroImportantType,
        Definitions.TinyImportantType,
        Definitions.SmallImportantType,
        Definitions.NormalImportantType,
        Definitions.LargeImportantType,
        Definitions.HugeCommentType,
        Definitions.GiaganticImportantType,

        Definitions.MicroQuestionType,
        Definitions.TinyQuestionType,
        Definitions.SmallQuestionType,
        Definitions.NormalQuestionType,
        Definitions.LargeQuestionType,
        Definitions.HugeQuestionType,
        Definitions.GiaganticQuestionType,

        Definitions.MicroStrikeoutType,
        Definitions.TinyStrikeoutType,
        Definitions.SmallStrikeoutType,
        Definitions.NormalStrikeoutType,
        Definitions.LargeStrikeoutType,
        Definitions.HugeStrikeoutType,
        Definitions.GiaganticStrikeoutType,

        Definitions.TaskBugType,
        Definitions.TaskHackType,
        Definitions.TaskNoteType,
        Definitions.TaskQuestionType,
        Definitions.TaskTodoType
    };

    /// <summary>
    /// The view.
    /// </summary>
    private IWpfTextView view;

    /// <summary>
    /// The map.
    /// </summary>
    private IClassificationFormatMap map;

    /// <summary>
    /// The registry.
    /// </summary>
    private IClassificationTypeRegistryService registry;

    /// <summary>
    /// true if view closed has delegate.
    /// </summary>
    private bool viewClosedHasDelegate;

    /// <summary>
    /// true if view got aggregate focus has delegate.
    /// </summary>
    private bool viewGotAggregateFocusHasDelegate;

    /// <summary>
    /// Initializes a new instance of the YoderZone.Extensions.FormatMapWatcher class.
    /// </summary>
    /// <param name="textView" type="IWpfTextView">
    /// The text view.
    /// </param>
    /// <param name="classificationFormatMap" type="IClassificationFormatMap">
    /// The classification format map.
    /// </param>
    /// <param name="typeRegistry" type="IClassificationTypeRegistryService">
    /// The type registry.
    /// </param>
    public FormatMapWatcher(IWpfTextView textView,
                            IClassificationFormatMap classificationFormatMap,
                            IClassificationTypeRegistryService typeRegistry)
    {
        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Entering FormatMapWatcher.FormatMapWatcher.",
                        DateTime.Now);

        this.view = textView;
        this.map = classificationFormatMap;
        this.registry = typeRegistry;

        this.map.ClassificationFormatMappingChanged += this.FormatMappingChanged;
        this.viewGotAggregateFocusHasDelegate = true;
        this.view.GotAggregateFocus += this.view_GotAggregateFocus;
        this.viewClosedHasDelegate = true;
        this.view.Closed += this.view_Closed;

        this.DoFormatting();
        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Exiting FormatMapWatcher.FormatMapWatcher.",
                        DateTime.Now);
    }

    /// <summary>
    /// Format comment spans.
    /// </summary>
    private void DoFormatting()
    {
        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Entering FormatMapWatcher.DoFormatting.",
                        DateTime.Now);

        if (this.map == null || this.view == null ||
                this.view == null
                || this.view.IsClosed)
        {
            Debug.WriteLine("{0:hh:mm:ss.ffffff}: Exiting FormatMapWatcher.DoFormatting.",
                            DateTime.Now);
            return;
        }

        bool isReentrantCall = this.map.IsInBatchUpdate;
        try
        {
            if (!isReentrantCall)
            {
                this.map.BeginBatchUpdate();
            }

            foreach (var classificationType in from typeName in this.RemarkTypes
                     select this.registry.GetClassificationType(typeName))
            {
                this.FormatClassification(classificationType);
            }
        }
        finally
        {
            if(this.map.IsInBatchUpdate && !isReentrantCall)
            { this.map.EndBatchUpdate(); }

            Debug.WriteLine("{0:hh:mm:ss.ffffff}: Exiting FormatMapWatcher.DoFormatting.",
                            DateTime.Now);
        }
    }

    private void FormatClassification(IClassificationType classification)
    {
        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Entering FormatMapWatcher.FormatClassification.",
                        DateTime.Now);

        var defaultProperties = this.map.DefaultTextProperties;
        var defaultEmSize = defaultProperties.FontRenderingEmSize;
        var emSize = defaultEmSize;

        var textFormattingRunProperties = this.map.GetTextProperties(
                                              classification);

        switch (classification.Classification)
        {
            case Definitions.GiaganticCommentType:
            case Definitions.GiaganticImportantType:
            case Definitions.GiaganticQuestionType:
            case Definitions.GiaganticStrikeoutType:
                emSize = 2.5 * defaultEmSize;
                break;
            case Definitions.HugeCommentType:
            case Definitions.HugeImportantType:
            case Definitions.HugeQuestionType:
            case Definitions.HugeStrikeoutType:
                emSize = 2.0 * defaultEmSize;
                break;
            case Definitions.LargeCommentType:
            case Definitions.LargeImportantType:
            case Definitions.LargeQuestionType:
            case Definitions.LargeStrikeoutType:
                emSize = 1.5 * defaultEmSize;
                break;
            case Definitions.MicroCommentType:
            case Definitions.MicroImportantType:
            case Definitions.MicroQuestionType:
            case Definitions.MicroStrikeoutType:
                emSize = 0.4 * defaultEmSize;
                break;
            case Definitions.NormalCommentType:
            case Definitions.NormalImportantType:
            case Definitions.NormalQuestionType:
            case Definitions.NormalStrikeoutType:
                break;
            case Definitions.SmallCommentType:
            case Definitions.SmallImportantType:
            case Definitions.SmallQuestionType:
            case Definitions.SmallStrikeoutType:
                emSize = 0.8 * defaultEmSize;
                break;
            case Definitions.TaskBugType:
            case Definitions.TaskHackType:
            case Definitions.TaskNoteType:
            case Definitions.TaskQuestionType:
            case Definitions.TaskTodoType:
                break;
            case Definitions.TinyCommentType:
            case Definitions.TinyImportantType:
            case Definitions.TinyQuestionType:
            case Definitions.TinyStrikeoutType:
                emSize = 0.6 * defaultEmSize;
                break;
        }

        var formattingRunProperties =
            textFormattingRunProperties.SetFontRenderingEmSize(emSize);
        this.map.SetTextProperties(classification, formattingRunProperties);

        Debug.WriteLine("{0:hh:mm:ss.ffffff}: Exiting FormatMapWatcher.FormatClassification.",
                        DateTime.Now);
    }

    /// <summary>
    /// Event handler. Called by view for closed events.
    /// </summary>
    /// <param name="sender" type="object">
    /// Source of the event.
    /// </param>
    /// <param name="e" type="System.EventArgs">
    /// Event information.
    /// </param>
    private void view_Closed(object sender, System.EventArgs e)
    {
        if (this.view != null && this.viewClosedHasDelegate)
        {
            this.view.Closed -= this.view_Closed;
            this.viewClosedHasDelegate = false;
        }
        this.Dispose();
    }

    /// <summary>
    /// Event handler. Called by view for got aggregate focus events.
    /// </summary>
    /// <param name="sender" type="object">
    /// Source of the event.
    /// </param>
    /// <param name="e" type="System.EventArgs">
    /// Event information.
    /// </param>
    private void view_GotAggregateFocus(object sender, System.EventArgs e)
    {
        var textView = sender as ITextView;
        if (this.view == textView && this.view != null &&
                this.viewGotAggregateFocusHasDelegate)
        {
            // Let this run for ITextView only once.
            // Question: Does this ever run from other contexts?
            this.view.GotAggregateFocus -= this.view_GotAggregateFocus;
            this.viewGotAggregateFocusHasDelegate = false;
        }

        this.DoFormatting();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources.
    /// </summary>
    /// <seealso cref="M:System.IDisposable.Dispose()"/>
    public void Dispose()
    {
        if (this.view != null)
        {
            if(this.viewClosedHasDelegate)
            { this.view.Closed -= this.view_Closed; }
            this.viewClosedHasDelegate = false;
            if(this.viewGotAggregateFocusHasDelegate)
            { this.view.GotAggregateFocus -= this.view_GotAggregateFocus; }
            this.viewGotAggregateFocusHasDelegate = false;
            this.view = null;
        }

        if (this.map != null)
        {
            this.map.ClassificationFormatMappingChanged -= this.FormatMappingChanged;
            this.map = null;
        }

        this.registry = null;
    }

    /// <summary>
    /// Format mapping changed.
    /// </summary>
    /// <param name="sender" type="object">
    /// Source of the event.
    /// </param>
    /// <param name="eventArgs" type="EventArgs">
    /// Event information.
    /// </param>
    private void FormatMappingChanged(object sender, EventArgs eventArgs)
    {
        this.DoFormatting();
    }
}
}