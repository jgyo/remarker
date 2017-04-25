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
            try
            {
                var service = Package.GetGlobalService(typeof(IRemarkerService)) as RemarkerService;
                if (service == null)
                {
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentNullException("service");
                }

                this.DisplayName = "Remarker Task10";
                this.ForegroundColor = service.TaskColor10.ConvertStringToWpfColor();
                this.FontTypeface = new Typeface(service.TaskTypeface10.Name);
                this.IsBold = service.TaskBold10;
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