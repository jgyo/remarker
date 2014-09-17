namespace YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions
{
#region Imports

using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using YoderZone.Extensions.OptionsPackage.Remarker.Service;
using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;
using YoderZone.Extensions.Remarker.Remarker.Service;

#endregion

/// <summary>
///     Task05 Format
/// </summary>
/// <seealso
///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
[Name(Definitions.Task05)]
[UserVisible(false)]
[Order(After = Priority.High)]
[Export(typeof(EditorFormatDefinition))]
[ClassificationType(ClassificationTypeNames = Definitions.Task05)]
public class Task05Format : ClassificationFormatDefinition
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the YoderZone.Extensions.TodoWorkItemFormat
    ///     class.
    /// </summary>
    public Task05Format()
    {
        var service = Package.GetGlobalService(typeof(IRemarkerService)) as
                      RemarkerService;
        this.DisplayName = "Remarker Task05";
        this.ForegroundColor =
            service.TaskColor05.ConvertStringToWpfColor();
        this.FontTypeface = new Typeface(service.TaskTypeface05);
        this.IsBold = service.TaskBold05;
        this.ForegroundCustomizable = true;
        this.BackgroundCustomizable = true;
    }

    #endregion
}
}