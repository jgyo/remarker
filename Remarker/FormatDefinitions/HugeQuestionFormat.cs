// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : HugeQuestionFormat.cs
// Author           : Gil Yoder
// Created          : 08 29,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;

using global::NLog;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.Remarker.Utilities;

#endregion

/// <summary>
///     A huge question format.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentQuestionHugeType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentQuestionHugeType)]
public class HugeQuestionFormat : ClassificationFormatDefinition
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        NLog.SettingsHelper.CreateLogger();

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.HugeQuestionFormat
    ///     class.
    /// </summary>
    public HugeQuestionFormat()
    {

        logger.Debug("Entered constructor.");
        this.DisplayName = "Remarker Question - Huge (++)";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalQuestionColor;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}