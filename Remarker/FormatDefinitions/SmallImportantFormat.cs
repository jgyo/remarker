// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : SmallImportantFormat.cs
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
///     A small important format.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentImportantSmallType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentImportantSmallType)]
public class SmallImportantFormat : ClassificationFormatDefinition
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.SmallImportantFormat
    ///     class.
    /// </summary>
    public SmallImportantFormat()
    {
        this.DisplayName = "Remarker Important - Small (-)";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalImportantColor;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}