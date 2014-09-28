﻿namespace YoderZone.Extensions.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;
using System.Windows.Media;

using global::NLog;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.Service;
using YoderZone.Extensions.Remarker.Service;
using YoderZone.Extensions.Remarker.Utilities;

#endregion

/// <summary>
///     Task02 Format
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.Task02)]
[UserVisible(false)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames = Definitions.Task02)]
public class Task02Format : ClassificationFormatDefinition
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        NLog.SettingsHelper.CreateLogger();

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.TodoWorkItemFormat
    ///     class.
    /// </summary>
    public Task02Format()
    {

        logger.Trace("Entered Task02Format()");
        var service = Package.GetGlobalService(typeof(IRemarkerService)) as
                      RemarkerService;
        this.DisplayName = "Remarker Task02";
        this.ForegroundColor =
            service.TaskColor02.ConvertStringToWpfColor();
        this.FontTypeface = new Typeface(service.TaskTypeface02);
        this.IsBold = service.TaskBold02;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = true;
    }

    #endregion
}
}