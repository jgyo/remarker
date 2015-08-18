// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : NormalStrikeoutFormat.cs
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

    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    using YoderZone.Extensions.Remarker.Utilities;

    #endregion

/// <summary>
///     Strikeout Formats.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentStrikeoutType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentStrikeoutType)]
public class StrikeoutFormat : ClassificationFormatDefinition
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.NormalStrikeoutFormat class.
    /// </summary>
    public StrikeoutFormat()
    {
        this.DisplayName = "Remarker Strikeout - Normal";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalStrikeoutColor;
        this.TextDecorations = System.Windows.TextDecorations.Strikethrough;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}