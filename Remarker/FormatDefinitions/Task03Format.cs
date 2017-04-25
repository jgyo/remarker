namespace YoderZone.Extensions.Remarker.FormatDefinitions
{
    #region Imports

    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    using YoderZone.Extensions.Remarker.Service;
    using YoderZone.Extensions.Remarker.Utilities;

    #endregion

    /// <summary>
    ///     Task03 Format
    /// </summary>
    /// <seealso
    ///     cref="T:Microsoft.VisualStudio.Text.Classification.ClassificationFormatDefinition" />
    [Name(Definitions.Task03)]
    [UserVisible(false)]
    [Order(After = Priority.High)]
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Definitions.Task03)]
    public class Task03Format : ClassificationFormatDefinition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the YoderZone.Extensions.TodoWorkItemFormat
        ///     class.
        /// </summary>
        public Task03Format()
        {
            try
            {
                var service = Package.GetGlobalService(typeof(IRemarkerService)) as RemarkerService;
                if (service == null)
                {
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentNullException("service");
                }

                this.DisplayName = "Remarker Task03";
                this.ForegroundColor = service.TaskColor03.ConvertStringToWpfColor();
                this.FontTypeface = new Typeface(service.TaskTypeface03.Name);
                this.IsBold = service.TaskBold03;
                this.ForegroundCustomizable = true;
                this.BackgroundCustomizable = true;
            }
                // ReSharper disable once RedundantCatchClause
                // ReSharper disable once UnusedVariable
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}