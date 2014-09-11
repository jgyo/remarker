// ---------------------------------------------------------------------------
// <copyright file="RemarkerTag.cs" company="Gil Yoder">
//   Copyright 2014 by Gil Yoder
//   3731 Crescent Drive
//   Pearland, TX 77584
//
//   Gil.Yoder@outlook.com
// </copyright>
// <summary>
//
// </summary>
// ---------------------------------------------------------------------------

namespace YoderZone.Extensions.Remarker
{
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

using FontStyle = System.Drawing.FontStyle;

public abstract class RemarkerTag : ITag
{
    public abstract string RegExPattern { get; }

    public FontStyle? FontStyle { get; set; }

    public FontWeight? FontWeight { get; set; }

    public FontFamily FontFamily { get; set; }

    public Color? Color { get; set; }

    public Color? BackgroundColor { get; set; }
}

[Export(typeof(ITaggerProvider))]
[ContentType("text")]
[TagType(typeof(RemarkerTag))]
internal class CommentTaggerProvider : ITaggerProvider
{
    /// <summary>
    /// Creates a tag provider for the specified buffer.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Microsoft.VisualStudio.Text.Tagging.ITagger`1"/>.
    /// </returns>
    /// <param name="buffer">The <see cref="T:Microsoft.VisualStudio.Text.ITextBuffer"/>.</param><typeparam name="T">The type of the tag.</typeparam>
    public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
    {
        return null;
    }
}

}