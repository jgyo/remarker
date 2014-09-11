// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : RemarkTagger.cs
// Author           : Gil Yoder
// Created          : 08 28,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Remarker
{
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions;
using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;

#endregion

/// <summary>
/// A remark tagger.
/// </summary>
/// <seealso cref="T:Microsoft.VisualStudio.Text.Tagging.ITagger{Microsoft.VisualStudio.Text.Tagging.ClassificationTag}"/>
internal class RemarkTagger : ITagger<ClassificationTag>
{
    #region Fields

    /// <summary>
    /// The aggregator.
    /// </summary>
    private readonly ITagAggregator<IClassificationTag> aggregator;

    /// <summary>
    /// The regular expression to match comments.
    /// </summary>
    private readonly Regex commentRegex = new Regex(
        Definitions.RegexPatternComment,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    /// The regular expression to match important comments.
    /// </summary>
    private readonly Regex importantRegex = new Regex(
        Definitions.RegexPatternImportant,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    /// The regular expression to match comments with a question.
    /// </summary>
    private readonly Regex questionRegex = new Regex(
        Definitions.RegexPatternQuestion,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    /// The regular expression to match comments that have been deleted.
    /// </summary>
    private readonly Regex strikeoutRegex = new Regex(
        Definitions.RegexPatternStrikeout,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
        RegexOptions.Singleline
        | RegexOptions.ExplicitCapture);

    /// <summary>
    /// Dictionary of comment tags.
    /// </summary>
    private readonly Dictionary<string, ClassificationTag> remarkerTags;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the YoderZone.Extensions.RemarkTagger class.
    /// </summary>
    /// <param name="classificationRegistryService">
    /// .
    /// </param>
    /// <param name="tagAggregator" type="ITagAggregator<IClassificationTag>">
    /// The tag aggregator.
    /// </param>
    public RemarkTagger(
        IClassificationTypeRegistryService classificationRegistryService,
        ITagAggregator<IClassificationTag> tagAggregator)
    {

        IClassificationTypeRegistryService registry =
            classificationRegistryService;

        this.remarkerTags = new Dictionary<string, ClassificationTag>();
        foreach (var typeString in Definitions.ClassificationTypeStrings)
        {
            this.remarkerTags.Add(typeString,
                                  new ClassificationTag(registry.GetClassificationType(typeString)));
        }

        this.aggregator = tagAggregator;
    }

    #endregion

    #region Public Events

    /// <summary>
    /// Occurs when Tags Changed.
    /// </summary>
    public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

    #endregion

    /// <summary>
    /// List of empty tags.
    /// </summary>
    private readonly List<ITagSpan<ClassificationTag>> EmptyTagList = new
    List<ITagSpan<ClassificationTag>>();

    #region Public Methods and Operators

    /// <summary>
    /// Gets all the tags that intersect the specified spans.
    /// </summary>
    /// <param name="spans">
    /// The spans to visit.
    /// </param>
    /// <returns>
    /// A <see cref="T:Microsoft.VisualStudio.Text.Tagging.TagSpan`1" /> for each tag.
    /// </returns>
    public IEnumerable<ITagSpan<ClassificationTag>> GetTags(
        NormalizedSnapshotSpanCollection spans)
    {

        //Debug.WriteLine("");
        //Debug.WriteLine("");

        // ######## SECTION ONE
        if (spans.Count == 0)
        {
            return this.EmptyTagList;
        }

        // ######## SECTION TWO

        // Get the first span in order to get it's snapshotSpan.
        ITextSnapshot snapshot = spans[0].Snapshot;
        IContentType contentType = snapshot.TextBuffer.ContentType;

        // Debug.WriteLine("SECTION TWO: IContentType = {0}", contentType);

        // ######## SECTION THREE
        if (!contentType.IsOfType("code"))
        {
            // Debug.WriteLine("SECTION THREE: IContentType is not of type \"code\"");
            return this.EmptyTagList;
        }

        // ######## SECTION FOUR
        var resultClassificationTags = new List<ITagSpan<ClassificationTag>>();

        // Debug.WriteLine("SECTION FOUR: Iterating through content code.");
        // ######## SECTION FIVE
        // Iterate through all of the IClassificationTags
        foreach (var currentTagSpan in this.aggregator.GetTags(spans))
        {
            // ######## SECTION SIX
            if (!currentTagSpan.Tag.ClassificationType.Classification.ToLower()
                    .Contains("comment"))
            {
                // Debug.WriteLine("SECTION SIX: {0} is not a comment.", currentTagSpan.Tag.ClassificationType.Classification);
                continue;
            }

            // Debug.WriteLine("SECTION SIX: Working on {0}", currentTagSpan.Tag.ClassificationType.Classification);

            // ######## SECTION SEVEN
            NormalizedSnapshotSpanCollection normalizedSpans =
                currentTagSpan.Span.GetSpans(
                    snapshot);

            // ######## SECTION EIGHT
            if (normalizedSpans.Count == 0)
            {
                continue;
            }

            // ######## SECTION NINE
            SnapshotSpan snapshotSpan = normalizedSpans[0];
            string workingText = snapshotSpan.GetText();

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("SECTION NINE: Working text: {0}", workingText);

            // ######## SECTION TEN
            if (string.IsNullOrWhiteSpace(workingText))
            {
                Debug.WriteLine("SECTION TEN: Ignoring null text.");
                continue;
            }

            // named collection groups
            GroupCollection commentPartGroups;

            // comment type
            Classification commentClass;

            // ######## SECTION ELEVEN
            if (this.questionRegex.IsMatch(workingText))
            {
                Debug.WriteLine("SECTION ELEVEN: Found a question.");
                commentClass = Classification.Question;
                commentPartGroups = this.questionRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.strikeoutRegex.IsMatch(workingText))
            {
                Debug.WriteLine("SECTION ELEVEN: Found a strikeout.");
                commentClass = Classification.Strikeout;
                commentPartGroups = this.strikeoutRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.importantRegex.IsMatch(workingText))
            {
                Debug.WriteLine("SECTION ELEVEN: Found a bang.");
                commentClass = Classification.Important;
                commentPartGroups = this.importantRegex.Match(workingText)
                                    .Groups;
            }
            else if (this.commentRegex.IsMatch(workingText))
            {
                Debug.WriteLine("SECTION ELEVEN: Found a plan ol' comment.");
                commentClass = Classification.Comment;
                commentPartGroups = this.commentRegex.Match(workingText)
                                    .Groups;
            }
            else
            {
                Debug.WriteLine("SECTION ELEVEN: No match.");
                continue;
            }

            // Determine where comment start and end,
            // and the amount of emphasis.
            Group commentPart = commentPartGroups["comment"];
            Group emphasisPart = commentPartGroups["emphasis"];

            // This should never happen, since we have already determined
            // that the text is a comment.
            if (commentPart.Success == false)
            {
                continue;
            }

            int start = commentPart.Index;
            int length = commentPart.Length;

            Debug.WriteLine("SECTION ELEVEN: Start:{0} Length:{1}", start, length);

            int emphasisAmount = 0;
            if (emphasisPart.Success)
            {
                Debug.WriteLine("SECTION ELEVEN: Found emphasis.");
                string emphasis = emphasisPart.Value;
                bool isPlus = !string.IsNullOrEmpty(emphasis) && emphasis[0] == '+';
                Debug.WriteLine("SECTION ELEVEN: Is Plus? {0}", isPlus);
                emphasisAmount = emphasis.Length * (isPlus ? 1 : -1);
                Debug.WriteLine("SECTION ELEVEN: Emphasis amount: {0}", emphasisAmount);
            }

            // ######## SECTION TWELVE
            // ######## DIFFERENCIATION LOGIC #######
            // Determine the type of comment we have.
            // If there is no match, continue loop.

            // Get a tag name.
            string commentTagName = this.DetermineTagName(commentClass,
                                    emphasisAmount);

            Debug.WriteLine("SECTION TWELVE: Comment tag name: {0}", commentTagName);

            // ######## SECTION THIRTEEN
            // Get a comment tag.
            ClassificationTag commentTag = this.remarkerTags[commentTagName];

            // ######## SECTION FOURTEEN
            // Create a tagSpan with the current values.
            var commentTagSpan =
                new TagSpan<ClassificationTag>(
                new SnapshotSpan(snapshotSpan.Snapshot, snapshotSpan.Start + start,
                                 length),
                commentTag);

            Debug.WriteLine("SECTION FOURTEEN: Tag span: {0}", commentTagSpan);

            Debug.WriteLine("SECTION FIFTEEN: Adding tag span {0}", commentTagSpan);
            resultClassificationTags.Add(commentTagSpan);

            //++ TaskName section

            string taskClassName = null;
            int taskIndex = 1;

            Debug.WriteLine("SECTION FIFTEEN: Iterating TaskNames.");
            foreach (var name in RemarkerSettings.Default.TaskNames)
            {
                Debug.WriteLine("SECTION FIFTEEN: Name = {0}", name);
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                var isMatch = Regex.IsMatch(
                                  workingText,
                                  string.Format(@"^\s*/*\s*[!?x]?(\+{{0,3}}|-{{0,3}})\s+(?<taskName>{0}\b)",
                                                Regex.Escape(name)),
                                  RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
                                  | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

                if (!isMatch)
                {
                    taskIndex++;
                    continue;
                }

                var matches = Regex.Match(
                                  workingText,
                                  string.Format(@"^\s*/*\s*[!?x]?(\+{{0,3}}|-{{0,3}})\s+(?<taskName>{0}\b)",
                                                Regex.Escape(name)),
                                  RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
                                  | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

                taskClassName = string.Format("Task{0:00}", taskIndex);

                var taskNameGroup = matches.Groups["taskName"];
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

            var taskTagSpan =
                new TagSpan<ClassificationTag>(
                new SnapshotSpan(snapshotSpan.Snapshot, snapshotSpan.Start + start,
                                 length),
                taskTag);

            resultClassificationTags.Add(taskTagSpan);
        }

        return resultClassificationTags ?? this.EmptyTagList;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Determine tag name.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="classification" type="Classification">
    /// The classification.
    /// </param>
    /// <param name="emphasis" type="int">
    /// The emphasis.
    /// </param>
    /// <returns>
    /// A string.
    /// </returns>
    private string DetermineTagName(Classification classification,
                                    int emphasis)
    {

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