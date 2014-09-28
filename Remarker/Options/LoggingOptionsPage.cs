// <copyright file="LoggingOptionsPage.cs" company="YoderZone.com">
// Copyright (c) 2014 Gil Yoder. All rights reserved.
// </copyright>
// <author>Gil Yoder</author>
// <date>9/27/2014</date>
// <summary>Implements the logging options page class</summary>
// <remarks>
// Licensed under the Microsoft Public License (Ms-PL); you may not
// use this file except in compliance with the License. You may obtain a copy
// of the License at
//
// https://remarker.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.
// </remarks>

namespace YoderZone.Extensions.Remarker.Options
{
#region Imports

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using global::NLog;
using global::NLog.Config;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.NLog;
using YoderZone.Extensions.Options;
using YoderZone.Extensions.Options.ViewModel;
using YoderZone.Extensions.Remarker.Service;

#endregion

[ClassInterface(ClassInterfaceType.AutoDual)]
[ToolboxItem(false)]
[CLSCompliant(false)]
[ComVisible(true)]
public sealed class LoggingOptionsPage : DialogPage, ILoggingOptionsModel
{
    #region Fields

    private readonly Logger logger = SettingsHelper.CreateLogger();

    private readonly ProfileManager profileManager;

    private readonly RemarkerService service;

    private LoggingOptionsWindow control;

    private bool isActivated;

    private bool isDebugEnabled;

    private bool isErrorEnabled;

    private bool isFatalEnabled;

    private bool isInfoEnabled;

    private bool isLoggingEnabled;

    private bool isTraceEnabled;

    private bool isWarnEnabled;

    private LoggingRule loggingRule1;

    private LoggingRule loggingRule2;

    private SettingsHelper settingsHelper;

    private bool shouldSave;

    private ILoggingOptionsModel model;

    #endregion

    #region Public Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Public Properties

    public bool IsDebugEnabled
    {
        get
        {
            return this.isDebugEnabled;
        }
        set
        {
            // IsDebugEnabled setter guard
            if (this.isDebugEnabled == value)
            {
                return;
            }

            this.isDebugEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsErrorEnabled
    {
        get
        {
            return this.isErrorEnabled;
        }
        set
        {
            // IsErrorEnabled setter guard
            if (this.isErrorEnabled == value)
            {
                return;
            }

            this.isErrorEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsFatalEnabled
    {
        get
        {
            return this.isFatalEnabled;
        }
        set
        {
            // IsFatalEnabled setter guard
            if (this.isFatalEnabled == value)
            {
                return;
            }

            this.isFatalEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsInfoEnabled
    {
        get
        {
            return this.isInfoEnabled;
        }
        set
        {
            // IsInfoEnabled setter guard
            if (this.isInfoEnabled == value)
            {
                return;
            }

            this.isInfoEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsLoggingEnabled
    {
        get
        {
            return this.isLoggingEnabled;
        }
        set
        {
            // IsLoggingEnabled setter guard
            if (this.isLoggingEnabled == value)
            {
                return;
            }

            this.isLoggingEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsTraceEnabled
    {
        get
        {
            return this.isTraceEnabled;
        }
        set
        {
            // IsTraceEnabled setter guard
            if (this.isTraceEnabled == value)
            {
                return;
            }

            this.isTraceEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public bool IsWarnEnabled
    {
        get
        {
            return this.isWarnEnabled;
        }
        set
        {
            // IsWarnEnabled setter guard
            if (this.isWarnEnabled == value)
            {
                return;
            }

            this.isWarnEnabled = value;
            this.OnPropertyChanged();
        }
    }

    public string LogFilesPath { get; private set; }

    #endregion

    public LoggingOptionsPage()
    {
        logger.Trace("Entered cstor.");

        this.settingsHelper = SettingsHelper.InstanceList["Remarker"];
        this.LogFilesPath = this.settingsHelper.LogFilesPath;
        this.loggingRule1 = this.settingsHelper.GetRule("rule1");
        this.loggingRule2 = this.settingsHelper.GetRule("rule2");

        this.profileManager = new ProfileManager();
        this.service = this.profileManager.Service;
        this.model = this;
        this.isActivated = false;
    }

    #region Properties

    [Browsable(false)]
    protected override IWin32Window Window
    {
        get
        {
            return this.control ?? (this.control = new LoggingOptionsWindow(
                this.model));
        }
    }

    #endregion

    #region Methods

    protected override void Dispose(bool disposing)
    {
        this.logger.Trace("Entered method.");
        LoggingOptionsWindow page = this.control;
        base.Dispose(disposing);
    }

    protected override void OnActivate(CancelEventArgs e)
    {
        this.logger.Trace("Entered method.");

        if (!this.isActivated)
        {
            this.profileManager.ProtectSettings();
            this.isActivated = true;
            this.shouldSave = false;
            this.SetValues();
        }

        base.OnActivate(e);
    }

    /// <summary>
    /// Handles Apply messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnApply(PageApplyEventArgs e)
    {
        logger.Trace("Entered method.");
        base.OnApply(e);
        this.shouldSave = true;
    }

    /// <summary>
    /// Handles Close messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnClosed(EventArgs e)
    {
        logger.Trace("Entered method.");
        if (!this.isActivated)
        {
            // noop
        }
        else if (this.shouldSave)
        {
            this.profileManager.CommitSettings();
        }
        else
        {
            this.profileManager.RollBackSettings();
        }

        this.isActivated = false;
        base.OnClosed(e);
    }

    /// <summary>
    /// Handles Deactive messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnDeactivate(CancelEventArgs e)
    {
        logger.Trace("Entered method.");
        base.OnDeactivate(e);
        if (e.Cancel || this.control == null)
        {
            return;
        }

        e.Cancel = !this.control.CanDeactivate();
        if (e.Cancel)
        {
            return;
        }

        this.ApplyChanges();
    }

    private void ApplyChanges()
    {
        logger.Trace("Entered method.");

        this.service.IsLoggingEnabled = this.IsLoggingEnabled;

        this.service.IsDebugEnabled = this.IsDebugEnabled;
        this.service.IsErrorEnabled = this.IsErrorEnabled;
        this.service.IsFatalEnabled = this.IsFatalEnabled;
        this.service.IsInfoEnabled = this.IsInfoEnabled;
        this.service.IsTraceEnabled = this.IsTraceEnabled;
        this.service.IsWarnEnabled = this.IsWarnEnabled;

        this.ApplyLogConfiguration();
    }

    public void ApplyLogConfiguration()
    {
        if (this.IsDebugEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Debug);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Debug);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Debug);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Debug);
        }

        if (this.IsErrorEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Error);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Error);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Error);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Error);
        }

        if (this.IsFatalEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Fatal);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Fatal);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Fatal);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Fatal);
        }

        if (this.IsInfoEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Info);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Info);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Info);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Info);
        }

        if (this.IsTraceEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Trace);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Trace);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Trace);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Trace);
        }

        if (this.IsWarnEnabled && this.IsLoggingEnabled)
        {
            this.loggingRule1.EnableLoggingForLevel(LogLevel.Warn);
            this.loggingRule2.EnableLoggingForLevel(LogLevel.Warn);
        }
        else
        {
            this.loggingRule1.DisableLoggingForLevel(LogLevel.Warn);
            this.loggingRule2.DisableLoggingForLevel(LogLevel.Warn);
        }
    }

    private void GetCurrentSettings()
    {


        this.IsDebugEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                  LogLevel.Debug);
        this.IsErrorEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                  LogLevel.Error);
        this.IsFatalEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                  LogLevel.Fatal);
        this.IsInfoEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                 LogLevel.Info);
        this.IsTraceEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                  LogLevel.Trace);
        this.IsWarnEnabled = this.loggingRule1.IsLoggingEnabledForLevel(
                                 LogLevel.Warn);
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName =
                                       null)
    {
        PropertyChangedEventHandler handler = this.PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    private void SetValues()
    {
        this.logger.Trace("Entered method.");

        this.IsLoggingEnabled = this.service.IsLoggingEnabled;

        this.IsDebugEnabled = this.service.IsDebugEnabled;
        this.IsErrorEnabled = this.service.IsErrorEnabled;
        this.IsFatalEnabled = this.service.IsFatalEnabled;
        this.IsInfoEnabled = this.service.IsInfoEnabled;
        this.IsTraceEnabled = this.service.IsTraceEnabled;
        this.IsWarnEnabled = this.service.IsWarnEnabled;
    }

    #endregion
}
}