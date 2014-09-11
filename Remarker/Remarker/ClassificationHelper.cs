namespace YoderZone.Extensions.Remarker
{
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.Utilities;

/// <summary>
/// A classification helper.
/// </summary>
public static class ClassificationHelper
{
    #region Static Fields

    /// <summary>
    /// List of types of the htmls.
    /// </summary>
    private static List<string> htmlTypes = new List<string> { "html", "htmlx" };

    /// <summary>
    /// List of types of the markups.
    /// </summary>
    private static List<string> markupTypes;

    /// <summary>
    /// List of types of the xmls.
    /// </summary>
    private static List<string> xmlTypes = new List<string> { "XML", "XAML" };

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes static members of the YoderZone.Extensions.ClassificationHelper class.
    /// </summary>
    static ClassificationHelper()
    {
        markupTypes = new List<string>(xmlTypes);
        markupTypes.AddRange(htmlTypes);
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// An IContentType extension method that query if 'target' is HTML.
    /// </summary>
    /// <param name="target" type="this IContentType">
    /// The target to act on.
    /// </param>
    /// <returns>
    /// true if html, false if not.
    /// </returns>
    public static bool IsHtml(this IContentType target)
    {
        return htmlTypes.Contains(target.TypeName);
    }

    /// <summary>
    /// An IContentType extension method that query if 'target' is markup.
    /// </summary>
    /// <param name="target" type="this IContentType">
    /// The target to act on.
    /// </param>
    /// <returns>
    /// true if markup, false if not.
    /// </returns>
    public static bool IsMarkup(this IContentType target)
    {
        return markupTypes.Contains(target.TypeName);
    }

    /// <summary>
    /// An IContentType extension method that query if 'target' is XML.
    /// </summary>
    /// <param name="target" type="this IContentType">
    /// The target to act on.
    /// </param>
    /// <returns>
    /// true if xml, false if not.
    /// </returns>
    public static bool IsXml(this IContentType target)
    {
        return xmlTypes.Contains(target.TypeName);
    }

    /// <summary>
    /// A string extension method that starts with which.
    /// </summary>
    /// <param name="target" type="this string">
    /// The target to act on.
    /// </param>
    /// <param name="validStrings" type="string[]">
    /// The valid strings.
    /// </param>
    /// <returns>
    /// A string.
    /// </returns>
    public static string StartsWithWhich(this string target,
                                         string[] validStrings)
    {
        if (target == null || validStrings == null)
        {
            return null;
        }

        foreach (var validString in validStrings.Where(target.StartsWith))
        {
            return validString;
        }
    }

    #endregion
}
}