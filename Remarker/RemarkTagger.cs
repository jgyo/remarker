//= ***********************************************************************
//= Assembly         : Options.Package
//= Solution         : YoderZone.Com.Extensions
//= File name        : RemarkTagger.cs
//= Author           : Gil Yoder
//= Created          : 09 11,  2014
//
//= Last Modified By : Gil Yoder
//= Last Modified On : 09 20, 2014
//= ***********************************************************************

namespace YoderZone.Extensions.Remarker
{
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text.RegularExpressions;
    using YoderZone.Extensions.Remarker.FormatDefinitions;
    using YoderZone.Extensions.Remarker.Service;
    using YoderZone.Extensions.Remarker.Utilities;

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

        /// <summary>
        ///     The aggregator.
        /// </summary>
        private readonly ITagAggregator<IClassificationTag> _aggregator;

        /// <summary>
        ///     The regular expression to match comments.
        /// </summary>
        private readonly Regex _commentRegex = new Regex(
            Definitions.RegexPatternComment,
            RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Singleline
            | RegexOptions.ExplicitCapture);

        /// <summary>
        ///     List of empty tags.
        /// </summary>
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<ITagSpan<ClassificationTag>> _emptyTagList =
        new List<ITagSpan<ClassificationTag>>();

        /// <summary>
        ///     The regular expression to match important comments.
        /// </summary>
        private readonly Regex _importantRegex = new Regex(
            Definitions.RegexPatternImportant,
            RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Singleline
            | RegexOptions.ExplicitCapture);

        /// <summary>
        ///     The regular expression to match comments with a question.
        /// </summary>
        private readonly Regex _questionRegex = new Regex(
            Definitions.RegexPatternQuestion,
            RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Singleline
            | RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Dictionary of comment tags.
        /// </summary>
        private readonly Dictionary<string, ClassificationTag> _remarkerTags;

        /// <summary>
        ///     The service.
        /// </summary>
        private readonly RemarkerService _service;

        /// <summary>
        ///     The regular expression to match comments that have been deleted.
        /// </summary>
        private readonly Regex _strikeoutRegex = new Regex(
            Definitions.RegexPatternStrikeout,
            RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.Singleline
            | RegexOptions.ExplicitCapture);

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
            Contract.Requires(registry != null);
            Contract.Requires(tagAggregator != null);
            Contract.Requires(service != null);

            this._service = service;

            this._remarkerTags = new Dictionary<string, ClassificationTag>();
            foreach (var typeString in Definitions.ClassificationTypeStrings)
            {
                this._remarkerTags.Add(
                    typeString,
                    new ClassificationTag(registry.GetClassificationType(typeString)));
            }

            this._aggregator = tagAggregator;
        }

        /// <summary>
        ///     Occurs when Tags Changed.
        /// </summary>
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

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
            Contract.Requires(spans != null);
            Contract.Ensures(
                Contract.Result<IEnumerable<ITagSpan<ClassificationTag>>>() != null);

            // ######## SECTION ONE
            if (spans.Count == 0)
            {
                return this._emptyTagList;
            }

            // ######## SECTION TWO

            // Get the first span in order to get it's snapshotSpan.
            ITextSnapshot snapshot = spans[0].Snapshot;
            IContentType contentType = snapshot.TextBuffer.ContentType;

            // Remarker does not currently work for SQL documents
            if (contentType.DisplayName.Contains("SQL"))
            {
                return this._emptyTagList;
            }

            // ######## SECTION THREE
            if (!contentType.IsOfType("code"))
            {
                return this._emptyTagList;
            }

            // ######## SECTION FOUR
            var resultClassificationTags = new List<ITagSpan<ClassificationTag>>();

            // ######## SECTION FIVE
            // Iterate through all of the IClassificationTags
            foreach (var currentTagSpan in this._aggregator.GetTags(spans))
            {
                // ######## SECTION SIX
                if (!currentTagSpan.Tag.ClassificationType.Classification.ToLower()
                        .Contains("comment"))
                {
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
                if (this._questionRegex.IsMatch(workingText))
                {
                    commentClass = Classification.Question;
                    commentPartGroups = this._questionRegex.Match(workingText)
                                        .Groups;
                }
                else if (this._strikeoutRegex.IsMatch(workingText))
                {
                    commentClass = Classification.Strikeout;
                    commentPartGroups = this._strikeoutRegex.Match(workingText)
                                        .Groups;
                }
                else if (this._importantRegex.IsMatch(workingText))
                {
                    commentClass = Classification.Important;
                    commentPartGroups = this._importantRegex.Match(workingText)
                                        .Groups;
                }
                else if (this._commentRegex.IsMatch(workingText))
                {
                    commentClass = Classification.Comment;
                    commentPartGroups = this._commentRegex.Match(workingText)
                                        .Groups;
                }
                else
                {
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

                int emphasisAmount = 0;
                if (emphasisPart.Success)
                {
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
                ClassificationTag commentTag = this._remarkerTags[commentTagName];

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

                foreach (var name in this._service.ValueNames)
                {
                    if (!name.StartsWith("Task0") && !name.StartsWith("Task10"))
                    {
                        continue;
                    }

                    string value = this._service.ReadValue(name);

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

                ClassificationTag taskTag = this._remarkerTags[taskClassName];

                span = new SnapshotSpan(snapshotSpan.Snapshot, snapshotSpan.Start + start,
                                        length);
                var taskTagSpan =
                    new TagSpan<ClassificationTag>(
                    span,
                    taskTag);

                resultClassificationTags.Add(taskTagSpan);
                //OnTagsChanged(new SnapshotSpanEventArgs(span));
            }

            return resultClassificationTags;
        }

        protected virtual void OnTagsChanged(SnapshotSpanEventArgs e) => this.TagsChanged?.Invoke(this, e);

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
            Contract.Requires(Enum.IsDefined(typeof(
                    Classification), classification));
            Contract.Ensures(Contract.Result<string>() != null);

            string result = $"{classification} - ";

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
                    throw new ArgumentOutOfRangeException(nameof(emphasis));
            }

            return result;
        }
    }
}