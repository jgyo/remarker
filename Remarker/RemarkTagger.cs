// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkTagger.cs
// Author           : Gil Yoder
// Created          : 09 11,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 20, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker
{
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

using global::NLog;

using YoderZone.Extensions.NLog;
using YoderZone.Extensions.Remarker.FormatDefinitions;
using YoderZone.Extensions.Remarker.Service;
using YoderZone.Extensions.Remarker.Utilities;

#endregion

/// <summary>
///     A remark tagger.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Tagging.ITagger{Microsoft.VisualStudio.Text.Tagging.ClassificationTag}" />
internal class RemarkTagger : ITagger<ClassificationTag>
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    #region Fields

    /// <summary>
    ///     List of empty tags.
    /// </summary>
    private readonly List<ITagSpan<ClassificationTag>> EmptyTagList =
        new List<ITagSpan<ClassificationTag>>();

    /// <summary>
    ///     The aggregator.
    /// </summary>
    private readonly ITagAggregator<IClassificationTag> aggregator;

    /// <summary>
    ///     The regular expression to match comments.
    /// </summary>
    private readonly Regex commentRegex = new Regex(
        Definitions.RegexPatternComment,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    ///     The regular expression to match important comments.
    /// </summary>
    private readonly Regex importantRegex = new Regex(
        Definitions.RegexPatternImportant,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    ///     The regular expression to match comments with a question.
    /// </summary>
    private readonly Regex questionRegex = new Regex(
        Definitions.RegexPatternQuestion,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    ///     Dictionary of comment tags.
    /// </summary>
    private readonly Dictionary<string, ClassificationTag> remarkerTags;

    /// <summary>
    ///     The service.
    /// </summary>
    private readonly RemarkerService service;

    /// <summary>
    ///     The regular expression to match comments that have been deleted.
    /// </summary>
    private readonly Regex strikeoutRegex = new Regex(
        Definitions.RegexPatternStrikeout,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.Remarker.Remarker.RemarkTagger class.
    /// </summary>
    /// <param name="registry">
    ///     The classification registry service.
    /// </param>
    /// <param name="tagAggregator">
    ///     The tag aggregator.
    /// </param>
    /// <param name="service">
    ///     The service.
    /// </param>
    public RemarkTagger(
        IClassificationTypeRegistryService registry,
        ITagAggregator<IClassificationTag> tagAggregator,
        RemarkerService service)
    {
        Contract.Requires<ArgumentNullException>(registry != null);
        Contract.Requires<ArgumentNullException>(tagAggregator != null);
        Contract.Requires<ArgumentNullException>(service != null);
        logger.Debug("Entered constructor.");

        this.service = service;

        this.remarkerTags = new Dictionary<string, ClassificationTag>();
        foreach (var typeString in Definitions.ClassificationTypeStrings)
        {
            this.remarkerTags.Add(
                typeString,
                new ClassificationTag(registry.GetClassificationType(typeString)));
        }

        this.aggregator = tagAggregator;
    }

    #endregion

    #region Public Events

    /// <summary>
    ///     Occurs when Tags Changed.
    /// </summary>
    public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

    protected virtual void OnTagsChanged(SnapshotSpanEventArgs e)
    {
        var handler = this.TagsChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Gets all the tags that intersect the specified spans.
    /// </summary>
    /// <param name="spans">
    ///     The spans to visit.
    /// </param>
    /// <returns>
    ///     A <see cref="T:Microsoft.VisualStudio.Text.Tagging.TagSpan`1" /> for
    ///     each tag.
    /// </returns>
    public IEnumerable<ITagSpan<ClassificationTag>> GetTags(
        NormalizedSnapshotSpanCollection spans)
    {
        Contract.Requires<ArgumentNullException>(spans != null);
        Contract.Ensures(
            Contract.Result<IEnumerable<ITagSpan<ClassificationTag>>>() != null);

        logger.Debug("Entered method.");

        // ######## SECTION ONE
        if (spans.Count == 0)
        {
            return this.EmptyTagList;
        }

        // ######## SECTION TWO

        // Get the first span in order to get it's snapshotSpan.
        ITextSnapshot snapshot = spans[0].Snapshot;
        IContentType contentType = snapshot.TextBuffer.ContentType;
        logger.Trace("contentType: {0}", contentType);

        // Remarker does not currently work for SQL documents
        if (contentType.DisplayName.Contains("SQL"))
        {
            return this.EmptyTagList;
        }

        // ######## SECTION THREE
        if (!contentType.IsOfType("code"))
        {
            return this.EmptyTagList;
        }

        // ######## SECTION FOUR
        var resultClassificationTags = new List<ITagSpan<ClassificationTag>>();

        // ######## SECTION FIVE
        // Iterate through all of the IClassificationTags
        foreach (var currentTagSpan in this.aggregator.GetTags(spans))
        {
            logger.Trace("currentTagSpan.Tag.ClassificationType: {0}",
                         currentTagSpan.Tag.ClassificationType);

            // ######## SECTION SIX
            if (!currentTagSpan.Tag.ClassificationType.Classification.ToLower()
                    .Contains("comment"))
            {
                logger.Debug("Not a comment. Looping back.");
                continue;
            }

            // ######## SECTION SEVEN
            NormalizedSnapshotSpanCollection normalizedSpans =
                currentTagSpan.Span.GetSpans(snapshot);

            // ######## SECTION EIGHT
            if (normalizedSpans.Count == 0)
            {
                continue;
            }

            // ######## SECTION NINE
            SnapshotSpan snapshotSpan = normalizedSpans[0];
            string workingText = snapshotSpan.GetText();
            logger.Trace("workingText: {0}", workingText);

            // ######## SECTION TEN
            if (string.IsNullOrWhiteSpace(workingText))
            {
                continue;
            }

            // named collection groups
            GroupCollection commentPartGroups;

            // comment type
            Classification commentClass;

            // ######## SECTION ELEVEN
            if (this.questionRegex.IsMatch(workingText))
            {
                commentClass = Classification.Question;
                commentPartGroups = this.questionRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.strikeoutRegex.IsMatch(workingText))
            {
                commentClass = Classification.Strikeout;
                commentPartGroups = this.strikeoutRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.importantRegex.IsMatch(workingText))
            {
                commentClass = Classification.Important;
                commentPartGroups = this.importantRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.commentRegex.IsMatch(workingText))
            {
                commentClass = Classification.Comment;
                commentPartGroups = this.commentRegex.Match(workingText)
                                    .Groups;
            }
            else
            {
                logger.Trace("No match found for {0}.", workingText);
                continue;
            }

            logger.Trace("Match found, commentClass: {0}", commentClass);

            // Determine where comment start and end,
            // and the amount of emphasis.
            Group commentPart = commentPartGroups["comment"];
            Group emphasisPart = commentPartGroups["emphasis"];

            // This should never happen, since we have already determined
            // that the text is a comment.
            if (commentPart.Success == false)
            {
                logger.Error("No reachable code reached in GetTags().");
                continue;
            }

            int start = commentPart.Index;
            int length = commentPart.Length;

            int emphasisAmount = 0;
            if (emphasisPart.Success)
            {
                logger.Trace("Emphasis found in comment.");
                string emphasis = emphasisPart.Value;
                bool isPlus = !string.IsNullOrEmpty(emphasis) && emphasis[0] == '+';
                emphasisAmount = emphasis.Length * (isPlus ? 1 : -1);
            }

            // ######## SECTION TWELVE
            // ######## DIFFERENCIATION LOGIC #######
            // Determine the type of comment we have.
            // If there is no match, continue loop.

            // Get a tag name.
            string commentTagName = this.DetermineTagName(commentClass,
                                    emphasisAmount);

            // ######## SECTION THIRTEEN
            // Get a comment tag.
            ClassificationTag commentTag = this.remarkerTags[commentTagName];
            logger.Trace("commentTag: {0}", commentTag);

            // ######## SECTION FOURTEEN
            // Create a tagSpan with the current values.
            var span = new SnapshotSpan(snapshotSpan.Snapshot,
                                        snapshotSpan.Start + start,
                                        length);
            var commentTagSpan =
                new TagSpan<ClassificationTag>(
                span,
                commentTag);

            resultClassificationTags.Add(commentTagSpan);
            //OnTagsChanged(new SnapshotSpanEventArgs(span));

            //++ TaskName section

            string taskClassName = null;

            foreach (var name in this.service.ValueNames)
            {
                if (!name.StartsWith("Task0") && !name.StartsWith("Task10"))
                {
                    continue;
                }

                string value = this.service.ReadValue(name);

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                const string taskNamePattern =
                    @"^\s*(\*?|<!--|//)\s*[!?x]?(\+{{0,3}}|-{{0,3}})\s+(?<taskName>{0}\b)";

                bool isMatch = Regex.IsMatch(
                                   workingText,
                                   string.Format(
                                       taskNamePattern,
                                       Regex.Escape(value)),
                                   RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
                                   | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

                if (!isMatch)
                {
                    continue;
                }

                logger.Trace("Match found for task named {0}.", name);

                Match matches = Regex.Match(
                                    workingText,
                                    string.Format(
                                        taskNamePattern,
                                        Regex.Escape(value)),
                                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
                                    | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

                taskClassName = name;

                Group taskNameGroup = matches.Groups["taskName"];
                start = taskNameGroup.Index;
                length = taskNameGroup.Length;

                break;
            }

            // Check for Tasks
            if (taskClassName == null)
            {
                continue;
            }

            // ######### PROCESS FOUND TASKS

            ClassificationTag taskTag = this.remarkerTags[taskClassName];

            span = new SnapshotSpan(snapshotSpan.Snapshot, snapshotSpan.Start + start,
                                    length);
            var taskTagSpan =
                new TagSpan<ClassificationTag>(
                span,
                taskTag);

            resultClassificationTags.Add(taskTagSpan);
            //OnTagsChanged(new SnapshotSpanEventArgs(span));
        }

        logger.Trace("Count of classification tags to return: {0}",
                     resultClassificationTags.Count);

        return resultClassificationTags;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Determine tag name.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="classification">
    ///     The classification.
    /// </param>
    /// <param name="emphasis">
    ///     The emphasis.
    /// </param>
    /// <returns>
    ///     A string.
    /// </returns>
    private string DetermineTagName(Classification classification,
                                    int emphasis)
    {
        Contract.Requires<ArgumentException>(Enum.IsDefined(typeof(
                Classification), classification));
        Contract.Ensures(Contract.Result<string>() != null);
        logger.Debug("Entered method.");

        string result = string.Format("{0} - ", classification);

        switch (emphasis)
        {
            case -3:
                result += "04em";
                break;
            case -2:
                result += "06em";
                break;
            case -1:
                result += "08em";
                break;
            case 0:
                result += "10em";
                break;
            case 1:
                result += "15em";
                break;
            case 2:
                result += "20em";
                break;
            case 3:
                result += "25em";
                break;
            default:
                throw new ArgumentOutOfRangeException("emphasis");
        }

        return result;
    }

    #endregion
}
}