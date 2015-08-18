// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : TinyImportantFormat.cs
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
///     A tiny important format.
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.CommentImportantTinyType)]
[UserVisible(true)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames =
                        Definitions.CommentImportantTinyType)]
public class TinyImportantFormat : ClassificationFormatDefinition
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.TinyImportantFormat
    ///     class.
    /// </summary>
    public TinyImportantFormat()
    {
        this.DisplayName = "Remarker Important - Tiny (--)";
        this.IsBold = false;
        this.ForegroundColor = Resources.NormalImportantColor;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = false;
    }

    #endregion
}
}