// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : GiganticQuestionFormat.cs
// Author           : Gil Yoder
// Created          : 08 29,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;

#endregion

/// <summary>
///     A gigantic question format.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentQuestionGiganticType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentQuestionGiganticType)]
public class GiganticQuestionFormat : ClassificationFormatDefinition
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.GiganticQuestionFormat class.
    /// </summary>
    public GiganticQuestionFormat()
    {
        this.DisplayName = "Remarker Question - Gigantic (+++)";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalQuestionColor;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}