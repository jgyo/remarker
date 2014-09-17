namespace YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;
using System.Windows.Media;

using EnvDTE;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.Service;
using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;
using YoderZone.Extensions.Remarker.Remarker.Service;

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
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.TodoWorkItemFormat
    ///     class.
    /// </summary>
    public Task10Format()
    {
        var service = Package.GetGlobalService(typeof(IRemarkerService)) as
                      RemarkerService;
        this.DisplayName = "Remarker Task10";
        this.ForegroundColor =
            service.TaskColor10.ConvertStringToWpfColor();
        this.FontTypeface = new Typeface(service.TaskTypeface10);
        this.IsBold = service.TaskBold10;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = true;
    }

    #endregion
}
}