// ***********************************************************************
// Assembly         : Remarker
// Solution         : RemarkerExtension
// File name        : Definitions.cs
// Author           : Gil Yoder
// Created          : 08 31,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 31, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;

using global::NLog;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

#endregion

/// <summary>
/// A definitions.
/// </summary>
public static class Definitions
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        NLog.SettingsHelper.CreateLogger();

    #region Constants

    /// <summary>
    /// The first task.
    /// </summary>
    public const string Task01 = "Task01";

    /// <summary>
    /// The second task.
    /// </summary>
    public const string Task02 = "Task02";

    /// <summary>
    /// The third task.
    /// </summary>
    public const string Task03 = "Task03";

    /// <summary>
    /// The fourth task.
    /// </summary>
    public const string Task04 = "Task04";

    /// <summary>
    /// The fifth task.
    /// </summary>
    public const string Task05 = "Task05";

    /// <summary>
    /// The task6.
    /// </summary>
    public const string Task06 = "Task06";

    /// <summary>
    /// The task7.
    /// </summary>
    public const string Task07 = "Task07";

    /// <summary>
    /// The task8.
    /// </summary>
    public const string Task08 = "Task08";

    /// <summary>
    /// The task9.
    /// </summary>
    public const string Task09 = "Task09";

    /// <summary>
    /// The task 10.
    /// </summary>
    public const string Task10 = "Task10";

    /// <summary>
    /// Type of the gigantic important.
    /// </summary>
    public const string CommentImportantGiganticType = "Important - 25em";

    /// <summary>
    /// Type of the huge important.
    /// </summary>
    public const string CommentImportantHugeType = "Important - 20em";

    /// <summary>
    /// Type of the large important.
    /// </summary>
    public const string CommentImportantLargeType = "Important - 15em";

    /// <summary>
    /// Type of the micro important.
    /// </summary>
    public const string CommentImportantMicroType = "Important - 04em";

    /// <summary>
    /// Type of the small important.
    /// </summary>
    public const string CommentImportantSmallType = "Important - 08em";

    /// <summary>
    /// Type of the tiny important.
    /// </summary>
    public const string CommentImportantTinyType = "Important - 06em";

    /// <summary>
    /// Type of the normal important.
    /// </summary>
    public const string CommentImportantType = "Important - 10em";

    /// <summary>
    /// Type of the gigantic comment.
    /// </summary>
    public const string CommentNormalGiganticType = "Comment - 25em";

    /// <summary>
    /// Type of the huge comment.
    /// </summary>
    public const string CommentNormalHugeType = "Comment - 20em";

    /// <summary>
    /// Type of the large comment.
    /// </summary>
    public const string CommentNormalLargeType = "Comment - 15em";

    /// <summary>
    /// Type of the micro comment.
    /// </summary>
    public const string CommentNormalMicroType = "Comment - 04em";

    /// <summary>
    /// Type of the small comment.
    /// </summary>
    public const string CommentNormalSmallType = "Comment - 08em";

    /// <summary>
    /// Type of the tiny comment.
    /// </summary>
    public const string CommentNormalTinyType = "Comment - 06em";

    /// <summary>
    /// Type of the normal comment.
    /// </summary>
    public const string CommentNormalType = "Comment - 10em";

    /// <summary>
    /// Type of the gigantic question.
    /// </summary>
    public const string CommentQuestionGiganticType = "Question - 25em";

    /// <summary>
    /// Type of the huge question.
    /// </summary>
    public const string CommentQuestionHugeType = "Question - 20em";

    /// <summary>
    /// Type of the large question.
    /// </summary>
    public const string CommentQuestionLargeType = "Question - 15em";

    /// <summary>
    /// Type of the micro question.
    /// </summary>
    public const string CommentQuestionMicroType = "Question - 04em";

    /// <summary>
    /// Type of the small question.
    /// </summary>
    public const string CommentQuestionSmallType = "Question - 08em";

    /// <summary>
    /// Type of the tiny question.
    /// </summary>
    public const string CommentQuestionTinyType = "Question - 06em";

    /// <summary>
    /// Type of the normal question.
    /// </summary>
    public const string CommentQuestionType = "Question - 10em";

    /// <summary>
    /// Type of the normal strikeout.
    /// </summary>
    public const string CommentStrikeoutType = "Strikeout - 10em";

    /// <summary>
    /// The comment pattern.
    /// </summary>
    internal const string RegexPatternComment =
        @"(^\s*(//|'|<!--)\s*(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*(-->)?)$
        |
       ^\s*(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*$";

    /// <summary>
    /// The important pattern.
    /// </summary>
    internal const string RegexPatternImportant =
        @"(^\s*(//|'|<!--)\s*!(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*(-->)?)$
        |
        ^\s*!(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*$";

    /// <summary>
    /// The question pattern.
    /// </summary>
    internal const string RegexPatternQuestion =
        @"(^\s*(//|'|<!--)\s*\?(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*(-->)?)$
        |
        ^\s*\?(?<emphasis>-{0,3}|\+{0,3})?\s+(?<comment>.*?)\s*$";

    /// <summary>
    /// The strikeout pattern.
    /// </summary>
    internal const string RegexPatternStrikeout =
        @"(^\s*(//|'|<!--)\s*x\s+(?<comment>.*?)\s*(-->)?)$
        |
        ^\s*x\s+(?<comment>.*?)\s*$";

    #endregion

    #region Static Fields

    /// <summary>
    /// The classification type strings.
    /// </summary>
    public static string[] ClassificationTypeStrings =
    {
        CommentImportantGiganticType,
        CommentImportantHugeType,
        CommentImportantLargeType,
        CommentImportantMicroType,
        CommentImportantSmallType,
        CommentImportantTinyType,
        CommentImportantType,
        CommentNormalGiganticType,
        CommentNormalHugeType,
        CommentNormalLargeType,
        CommentNormalMicroType,
        CommentNormalSmallType,
        CommentNormalTinyType,
        CommentNormalType,
        CommentQuestionGiganticType,
        CommentQuestionHugeType,
        CommentQuestionLargeType,
        CommentQuestionMicroType,
        CommentQuestionSmallType,
        CommentQuestionTinyType,
        CommentQuestionType,
        CommentStrikeoutType,
        Task01,
        Task02,
        Task03,
        Task04,
        Task05,
        Task06,
        Task07,
        Task08,
        Task09,
        Task10
    };

    /// <summary>
    /// The gigantic important classification type definition.
    /// </summary>
    [Name(CommentImportantGiganticType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantGiganticTypeDefinition;

    /// <summary>
    /// The huge important classification type definition.
    /// </summary>
    [Name(CommentImportantHugeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantHugeTypeDefinition;

    /// <summary>
    /// The large important classification type definition.
    /// </summary>
    [Name(CommentImportantLargeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantLargeTypeDefinition;

    /// <summary>
    /// The micro important classification type definition.
    /// </summary>
    [Name(CommentImportantMicroType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantMicroTypeDefinition;

    /// <summary>
    /// The small important classification type definition.
    /// </summary>
    [Name(CommentImportantSmallType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantSmallTypeDefinition;

    /// <summary>
    /// The tiny important classification type definition.
    /// </summary>
    [Name(CommentImportantTinyType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentImportantTinyTypeDefinition;

    /// <summary>
    /// The gigantic comment classification type definition.
    /// </summary>
    [Name(CommentNormalGiganticType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalGiganticTypeDefinition;

    /// <summary>
    /// The huge comment classification type definition.
    /// </summary>
    [Name(CommentNormalHugeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalHugeTypeDefinition;

    /// <summary>
    /// The large comment classification type definition.
    /// </summary>
    [Name(CommentNormalLargeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalLargeTypeDefinition;

    /// <summary>
    /// The micro comment classification type definition.
    /// </summary>
    [Name(CommentNormalMicroType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalMicroTypeDefinition;

    /// <summary>
    /// The small comment classification type definition.
    /// </summary>
    [Name(CommentNormalSmallType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalSmallTypeDefinition;

    /// <summary>
    /// The tiny comment classification type definition.
    /// </summary>
    [Name(CommentNormalTinyType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentNormalTinyTypeDefinition;

    /// <summary>
    /// The comment classification type definition.
    /// </summary>
    [Name(CommentNormalType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition CommentNormalTypeDefinition;

    /// <summary>
    /// The gigantic question classification type definition.
    /// </summary>
    [Name(CommentQuestionGiganticType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionGiganticTypeDefinition;

    /// <summary>
    /// The huge question classification type definition.
    /// </summary>
    [Name(CommentQuestionHugeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionHugeTypeDefinition;

    /// <summary>
    /// The large question classification type definition.
    /// </summary>
    [Name(CommentQuestionLargeType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionLargeTypeDefinition;

    /// <summary>
    /// The micro question classification type definition.
    /// </summary>
    [Name(CommentQuestionMicroType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionMicroTypeDefinition;

    /// <summary>
    /// The small question classification type definition.
    /// </summary>
    [Name(CommentQuestionSmallType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionSmallTypeDefinition;

    /// <summary>
    /// The tiny question classification type definition.
    /// </summary>
    [Name(CommentQuestionTinyType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    CommentQuestionTinyTypeDefinition;

    /// <summary>
    /// The important classification type definition.
    /// </summary>
    [Name(CommentImportantType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    ImportantClassificationTypeDefinition;

    /// <summary>
    /// The question classification type definition.
    /// </summary>
    [Name(CommentQuestionType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    QuestionClassificationTypeDefinition;

    /// <summary>
    /// The strikeout classification type definition.
    /// </summary>
    [Name(CommentStrikeoutType)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    StrikeoutClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task01)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task01ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task02)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task02ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task03)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task03ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task04)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task04ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task05)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task05ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task06)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task06ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task07)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task07ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task08)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task08ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task09)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task09ClassificationTypeDefinition;

    /// <summary>
    /// The first task classification type definition.
    /// </summary>
    [Name(Task10)]
    [BaseDefinition("Comment")]
    [Export(typeof(ClassificationTypeDefinition))]
    internal static ClassificationTypeDefinition
    Task10ClassificationTypeDefinition;


    #endregion
}
}