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
using YoderZone.Extensions.Options.ViewModel;

public partial class LoggingOptionsWindow : UserControl
{
    public ILoggingOptionsModel Model { get; set; }

    public LoggingOptionsWindow(ILoggingOptionsModel model)
    {
        this.Model = model;
        InitializeComponent();
        this.loggingOptions1.DataContext = this.Model;
    }

    public bool CanDeactivate()
    {
        return true;
    }
}
}
