// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : LargeQuestionFormat.cs
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
///     A large question format.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentQuestionLargeType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentQuestionLargeType)]
public class LargeQuestionFormat : ClassificationFormatDefinition
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        NLog.SettingsHelper.CreateLogger();

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.LargeQuestionFormat
    ///     class.
    /// </summary>
    public LargeQuestionFormat()
    {

        logger.Debug("Entered constructor.");
        this.DisplayName = "Remarker Question - Large (+)";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalQuestionColor;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}