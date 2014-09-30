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
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using global::NLog;

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

    private bool shouldSave;

    private readonly ILoggingOptionsModel model;

    private bool isNLogConfigLoggingEnabled;

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

    public bool IsNLogConfigLoggingEnabled
    {
        get
        {
            return this.isNLogConfigLoggingEnabled;
        }
        set
        {
            // IsNLogConfigLoggingEnabled setter guard
            if (this.isNLogConfigLoggingEnabled == value)
            {
                return;
            }

            this.isNLogConfigLoggingEnabled = value;
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

    public string LogFilesPath
    {
        get
        {
            SettingsHelper value;
            return !SettingsHelper.InstanceList.TryGetValue("Remarker",
                    out value) ? null : value.LogFilesPath;
        }
    }

    public string NLogConfigLogFilePath
    {
        get
        {
            SettingsHelper value;
            return !SettingsHelper.InstanceList.TryGetValue("NLogConfig",
                    out value) ? null : value.LogFilesPath;
        }
    }

    public bool NLogConfigLogFilePathExists
    {
        get
        {
            return this.NLogConfigLogFilePath != null &&
                   Directory.Exists(this.NLogConfigLogFilePath);
        }
    }

    public bool LogFilesPathExists
    {
        get
        {
            return this.LogFilesPath != null &&
                   Directory.Exists(this.LogFilesPath);
        }
    }

    #endregion

    public LoggingOptionsPage()
    {
        logger.Debug("Entered constructor.");

        try
        {
            this.profileManager = new ProfileManager();
            this.service = this.profileManager.Service;
            this.model = this;
            this.isActivated = false;
        }
        catch (Exception ex)
        {
            logger.Fatal(ex.Message, ex);
            throw;
        }
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
        this.logger.Debug("Entered method.");
        LoggingOptionsWindow page = this.control;
        if (page != null && page.IsDisposed == false)
        {
            page.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnActivate(CancelEventArgs e)
    {
        this.logger.Debug("Entered method.");

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
        logger.Debug("Entered method.");
        base.OnApply(e);
        this.shouldSave = true;
    }

    /// <summary>
    /// Handles Close messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnClosed(EventArgs e)
    {
        logger.Debug("Entered method.");
        if (!this.isActivated)
        {
            // no op
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
    /// Handles Deactivate events from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnDeactivate(CancelEventArgs e)
    {
        logger.Debug("Entered method.");
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
        logger.Debug("Entered method.");

        this.service.IsLoggingEnabled = this.IsLoggingEnabled;

        this.service.IsNLogConfigLoggingEnabled = this.IsNLogConfigLoggingEnabled;
        this.service.IsDebugEnabled = this.IsDebugEnabled;
        this.service.IsErrorEnabled = this.IsErrorEnabled;
        this.service.IsFatalEnabled = this.IsFatalEnabled;
        this.service.IsInfoEnabled = this.IsInfoEnabled;
        this.service.IsTraceEnabled = this.IsTraceEnabled;
        this.service.IsWarnEnabled = this.IsWarnEnabled;

        this.service.UpdateLoggingSettings();
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
        this.logger.Debug("Entered method.");

        this.IsLoggingEnabled = this.service.IsLoggingEnabled;
        this.IsNLogConfigLoggingEnabled = this.service.IsNLogConfigLoggingEnabled;

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