﻿namespace YoderZone.Extensions.Remarker.FormatDefinitions
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
///     Task10 Format
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.Task10)]
[UserVisible(false)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames = Definitions.Task10)]
public class Task10Format : ClassificationFormatDefinition
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
    public Task10Format()
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

            this.DisplayName = "Remarker Task10";
            this.ForegroundColor =
                service.TaskColor10.ConvertStringToWpfColor();
            this.FontTypeface = new Typeface(service.TaskTypeface10);
            this.IsBold = service.TaskBold10;
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