using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoderZone.Extensions.Options
{
using global::NLog;

using YoderZone.Extensions.NLog;
using YoderZone.Extensions.Options.ViewModel;

public partial class LoggingOptionsWindow : UserControl
{
    private Logger logger = SettingsHelper.CreateLogger();

    public ILoggingOptionsModel Model { get; set; }

    public LoggingOptionsWindow(ILoggingOptionsModel model)
    {
        logger.Debug("Entered constructor.");

        this.Model = model;
        InitializeComponent();
        this.loggingOptions1.DataContext = this.Model;
    }

    public bool CanDeactivate()
    {
        logger.Debug("Entered method.");

        return true;
    }
}
}
