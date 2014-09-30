namespace YoderZone.Extensions.Remarker.FormatDefinitions
{
#region Imports

using System;
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
///     Task04 Format
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.Task04)]
[UserVisible(false)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames = Definitions.Task04)]
public class Task04Format : ClassificationFormatDefinition
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
    public Task04Format()
    {
        logger.Debug("Entered constructor.");
        try
        {
            var service = Package.GetGlobalService(typeof(IRemarkerService)) as
                          RemarkerService;
            if (service == null)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("service");
            }

            this.DisplayName = "Remarker Task04";
            this.ForegroundColor =
                service.TaskColor04.ConvertStringToWpfColor();
            this.FontTypeface = new Typeface(service.TaskTypeface04);
            this.IsBold = service.TaskBold04;
            this.ForegroundCustomizable = true;
            this.BackgroundCustomizable = true;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message, ex);
            throw;
        }
    }

    #endregion
}
}