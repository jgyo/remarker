// ***********************************************************************
// Assembly         : CommentPlus
// Solution         : CommentPlus
// File name        : Components.cs
// Author           : Gil Yoder
// Created          : 08 27,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 27, 2014
// ***********************************************************************
namespace YoderZone.Extensions.OptionsPackage.Remarker
{
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

internal sealed class Components
{
    [Export]
    [Name("LargestComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition largestCommentTypeDefinition;

    [Export]
    [Name("LargerComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition largerCommentTypeDefinition;

    [Export]
    [Name("LargeComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition largeCommentTypeDefinition;

    [Export]
    [Name("SmallComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition smallCommentTypeDefinition;

    [Export]
    [Name("TinyComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition tinyCommentTypeDefinition;

    [Export]
    [Name("MicroComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition microCommentTypeDefinition;

    [Export]
    [Name("ImportantComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition
    importantCommentTypeDefinition;

    [Export]
    [Name("QuestionComment")]
    [BaseDefinition("text")]
    internal static ClassificationTypeDefinition
    questionCommentTypeDefinition;

}

[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        "importantCommentTypeDefinition")]
[Name("ImportantComment")]
[DisplayName("CommentPlus.Important")]
[UserVisible(true)]
[Order(After = Priority.Default, Before = Priority.High)]
internal sealed class ImportantCommentFormat :
    ClassificationFormatDefinition
{
}

[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        "questionCommentTypeDefinition")]
[Name("QuestionComment")]
[DisplayName("CommentPlus.Question")]
[UserVisible(true)]
[Order(After = Priority.Default, Before = Priority.High)]
internal sealed class QuestionCommentFormat :
    ClassificationFormatDefinition
{
}
}