namespace YoderZone.Extensions.Options
{
    using System.Windows.Controls;

    using YoderZone.Extensions.Options.ViewModel;

    /// <summary>
    /// Interaction logic for TaskOptionsControl.xaml
    /// </summary>
    public partial class TaskOptionsControl : UserControl
    {
        #region Fields

        private bool isEditing;

        #endregion

        #region Constructors and Destructors

        //! Constructor
        public TaskOptionsControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public bool CanDeactivate
        {
            get
            {
                if (this.isEditing)
                {
                    this.dataGrid.CommitEdit();
                }

                return !this.isEditing;
            }
        }

        public string Version
        {
            get
            {
                // Extract the version
                var assembly = typeof(TaskOptionsControl).Assembly;
                string fullName = assembly.FullName;
                string[] strings = fullName.Split(',');
                var version = strings[1];
                strings = version.Split('=');
                version = strings[1];

                return $"Copyright © Gil Yoder 2014 - Version {version}";
            }
        }

        #endregion

        #region Methods

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            this.isEditing = true;
            if (e.Column == this.colorColumn)
            {
                e.Column.Width = DataGridLength.Auto;
                return;
            }
            if (e.Column == this.boldColumn)
            {
                var taskAttributes = e.Row.Item as TaskAttributes;
                if (taskAttributes == null)
                {
                    return;
                }

                taskAttributes.IsBold = !taskAttributes.IsBold;
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            this.isEditing = false;
            if (e.Column == this.colorColumn)
            {
                e.Column.Width = 39;
            }
        }

        #endregion
    }
}