// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerService.cs
// Author           : Gil Yoder
// Created          : 09 15,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 16, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Service
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    using global::Options;

    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.Win32;

    using YoderZone.Extensions.Options;
    using YoderZone.Extensions.Options.ViewModel;
    using YoderZone.Extensions.Remarker.Service.Indexes;

    #endregion

    public class RemarkerService : IRemarkerService,
                                   IRemarkerSettings,
                                   IDisposable
    {
        #region Fields

        private bool disposedValue = false; // To detect redundant calls

        private readonly ProfileManager profile;

        #endregion

        #region Events

        public event EventHandler SettingsChanged;

        #endregion

        #region Constructors and Destructors

        public RemarkerService(RemarkerPackage package)
        {
            try
            {
                this.Package = package;
                this.profile = new ProfileManager();
                this.ColorIndex = new ColorIndex(this);
                this.FontFamilyIndex = new FontFamilyIndex(this);
                this.IsBoldIndex = new IsBoldIndex(this);

                this.Load();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Properties

        public ColorIndex ColorIndex { get; }

        public float EmSize { get; set; }

        public string FontFamily { get; set; }

        public FontFamilyIndex FontFamilyIndex { get; }

        public float GiganticFactor { get; set; }

        public float GiganticFontSize => this.EmSize * this.GiganticFactor;

        public float HugeFactor { get; set; }

        public float HugeFontSize => this.EmSize * this.HugeFactor;

        public IsBoldIndex IsBoldIndex { get; }

        public bool IsDebugEnabled { get; set; }

        public bool IsErrorEnabled { get; set; }

        public bool IsFatalEnabled { get; set; }

        public bool IsInfoEnabled { get; set; }

        public bool IsLoggingEnabled { get; set; }

        public bool IsNLogConfigLoggingEnabled { get; set; }

        public bool IsTraceEnabled { get; set; }

        public bool IsWarnEnabled { get; set; }

        public float LargeFactor { get; set; }

        public float LargeFontSize => this.EmSize * this.LargeFactor;

        public float MicroFactor { get; set; }

        public float MicroFontSize => this.EmSize * this.MicroFactor;

        public RemarkerPackage Package { get; private set; }

        public ProfileManager Profile
        {
            get
            {
                return this.profile;
            }
        }

        public float SmallFactor { get; set; }

        public float SmallFontSize => this.EmSize * this.SmallFactor;

        public string Task01 { get; set; }

        public string Task02 { get; set; }

        public string Task03 { get; set; }

        public string Task04 { get; set; }

        public string Task05 { get; set; }

        public string Task06 { get; set; }

        public string Task07 { get; set; }

        public string Task08 { get; set; }

        public string Task09 { get; set; }

        public string Task10 { get; set; }

        public bool TaskBold01 { get; set; }

        public bool TaskBold02 { get; set; }

        public bool TaskBold03 { get; set; }

        public bool TaskBold04 { get; set; }

        public bool TaskBold05 { get; set; }

        public bool TaskBold06 { get; set; }

        public bool TaskBold07 { get; set; }

        public bool TaskBold08 { get; set; }

        public bool TaskBold09 { get; set; }

        public bool TaskBold10 { get; set; }

        public string TaskColor01 { get; set; }

        public string TaskColor02 { get; set; }

        public string TaskColor03 { get; set; }

        public string TaskColor04 { get; set; }

        public string TaskColor05 { get; set; }

        public string TaskColor06 { get; set; }

        public string TaskColor07 { get; set; }

        public string TaskColor08 { get; set; }

        public string TaskColor09 { get; set; }

        public string TaskColor10 { get; set; }

        public FontAttributes TaskTypeface01 { get; set; }

        public FontAttributes TaskTypeface02 { get; set; }

        public FontAttributes TaskTypeface03 { get; set; }

        public FontAttributes TaskTypeface04 { get; set; }

        public FontAttributes TaskTypeface05 { get; set; }

        public FontAttributes TaskTypeface06 { get; set; }

        public FontAttributes TaskTypeface07 { get; set; }

        public FontAttributes TaskTypeface08 { get; set; }

        public FontAttributes TaskTypeface09 { get; set; }

        public FontAttributes TaskTypeface10 { get; set; }

        public float TinyFactor { get; set; }

        public float TinyFontSize => this.EmSize * this.TinyFactor;

        public IEnumerable<string> ValueNames
        {
            get
            {
                yield return "IsLoggingEnabled";
                yield return "IsNLogConfigLoggingEnabled";
                yield return "IsTraceEnabled";
                yield return "IsDebugEnabled";
                yield return "IsInfoEnabled";
                yield return "IsWarnEnabled";
                yield return "IsFatalEnabled";
                yield return "IsErrorEnabled";
                yield return "FontFamily";
                yield return "GiganticFactor";
                yield return "HugeFactor";
                yield return "LargeFactor";
                yield return "SmallFactor";
                yield return "TinyFactor";
                yield return "MicroFactor";
                yield return "Task01";
                yield return "Task02";
                yield return "Task03";
                yield return "Task04";
                yield return "Task05";
                yield return "Task06";
                yield return "Task07";
                yield return "Task08";
                yield return "Task09";
                yield return "Task10";
                yield return "TaskBold01";
                yield return "TaskBold02";
                yield return "TaskBold03";
                yield return "TaskBold04";
                yield return "TaskBold05";
                yield return "TaskBold06";
                yield return "TaskBold07";
                yield return "TaskBold08";
                yield return "TaskBold09";
                yield return "TaskBold10";
                yield return "TaskColor01";
                yield return "TaskColor02";
                yield return "TaskColor03";
                yield return "TaskColor04";
                yield return "TaskColor05";
                yield return "TaskColor06";
                yield return "TaskColor07";
                yield return "TaskColor08";
                yield return "TaskColor09";
                yield return "TaskColor10";
                yield return "TaskTypeface01";
                yield return "TaskTypeface02";
                yield return "TaskTypeface03";
                yield return "TaskTypeface04";
                yield return "TaskTypeface05";
                yield return "TaskTypeface06";
                yield return "TaskTypeface07";
                yield return "TaskTypeface08";
                yield return "TaskTypeface09";
                yield return "TaskTypeface10";
            }
        }

        public int Version => RemarkerPackage.Version;

        #endregion

        #region Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.profile.Dispose();
                }

                disposedValue = true;
            }
        }

        // Override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RemarkerService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        public void Load()
        {
            this.Profile.LoadSettingsFromStorage(this);
        }

        public void Load(RegistryKey key, string valueName)
        {
            string value = key?.GetValue(valueName) as string;

            this.Load(value, valueName);
        }

        public void Load(string value, string valueName)
        {
            float floatResult;
            bool boolResult;
            switch (valueName)
            {
                case "IsLoggingEnabled":
                    this.IsLoggingEnabled = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "IsNLogConfigLoggingEnabled":
                    this.IsNLogConfigLoggingEnabled = bool.TryParse(value, out boolResult) &&
                                                      boolResult;
                    break;
                case "IsTraceEnabled":
                    this.IsTraceEnabled = bool.TryParse(value, out boolResult) && boolResult;
                    break;
                case "IsDebugEnabled":
                    this.IsDebugEnabled = bool.TryParse(value, out boolResult) && boolResult;
                    break;
                case "IsInfoEnabled":
                    this.IsInfoEnabled = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "IsWarnEnabled":
                    this.IsWarnEnabled = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "IsFatalEnabled":
                    this.IsFatalEnabled = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "IsErrorEnabled":
                    this.IsErrorEnabled = !bool.TryParse(value, out boolResult) || boolResult;
                    break;

                case "FontFamily":
                    this.FontFamily = value ?? "Verdana";
                    break;
                case "GiganticFactor":
                    this.GiganticFactor = float.TryParse(value, out floatResult) ? floatResult : 2f;
                    break;
                case "HugeFactor":
                    this.HugeFactor = float.TryParse(value, out floatResult) ? floatResult : 1.6667f;
                    break;
                case "LargeFactor":
                    this.LargeFactor = float.TryParse(value, out floatResult)
                                           ? floatResult
                                           : 1.3333f;
                    break;
                case "SmallFactor":
                    this.SmallFactor = float.TryParse(value, out floatResult) ? floatResult : .85f;
                    break;
                case "TinyFactor":
                    this.TinyFactor = float.TryParse(value, out floatResult) ? floatResult : .70f;
                    break;
                case "MicroFactor":
                    this.MicroFactor = float.TryParse(value, out floatResult) ? floatResult : .55f;
                    break;
                case "Task01":
                    this.Task01 = value ?? "Todo";
                    break;
                case "Task02":
                    this.Task02 = value ?? "Hack";
                    break;
                case "Task03":
                    this.Task03 = value ?? "Question";
                    break;
                case "Task04":
                    this.Task04 = value ?? "Bug";
                    break;
                case "Task05":
                    this.Task05 = value ?? "Note";
                    break;
                case "Task06":
                    this.Task06 = value ?? "";
                    break;
                case "Task07":
                    this.Task07 = value ?? "";
                    break;
                case "Task08":
                    this.Task08 = value ?? "";
                    break;
                case "Task09":
                    this.Task09 = value ?? "";
                    break;
                case "Task10":
                    this.Task10 = value ?? "";
                    break;
                case "TaskBold01":
                    this.TaskBold01 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold02":
                    this.TaskBold02 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold03":
                    this.TaskBold03 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold04":
                    this.TaskBold04 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold05":
                    this.TaskBold05 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold06":
                    this.TaskBold06 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold07":
                    this.TaskBold07 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold08":
                    this.TaskBold08 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold09":
                    this.TaskBold09 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskBold10":
                    this.TaskBold10 = !bool.TryParse(value, out boolResult) || boolResult;
                    break;
                case "TaskColor01":
                    this.TaskColor01 = value ?? "0000ff";
                    break;
                case "TaskColor02":
                    this.TaskColor02 = value ?? "008000";
                    break;
                case "TaskColor03":
                    this.TaskColor03 = value ?? "ff00d6";
                    break;
                case "TaskColor04":
                    this.TaskColor04 = value ?? "ff0000";
                    break;
                case "TaskColor05":
                    this.TaskColor05 = value ?? "8b0807";
                    break;
                case "TaskColor06":
                    this.TaskColor06 = value ?? "000000";
                    break;
                case "TaskColor07":
                    this.TaskColor07 = value ?? "000000";
                    break;
                case "TaskColor08":
                    this.TaskColor08 = value ?? "000000";
                    break;
                case "TaskColor09":
                    this.TaskColor09 = value ?? "000000";
                    break;
                case "TaskColor10":
                    this.TaskColor10 = value ?? "000000";
                    break;
                case "TaskTypeface01":
                    this.TaskTypeface01 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface02":
                    this.TaskTypeface02 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface03":
                    this.TaskTypeface03 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface04":
                    this.TaskTypeface04 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface05":
                    this.TaskTypeface05 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface06":
                    this.TaskTypeface06 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface07":
                    this.TaskTypeface07 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface08":
                    this.TaskTypeface08 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface09":
                    this.TaskTypeface09 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
                case "TaskTypeface10":
                    this.TaskTypeface10 = FontHelper.GetTypeFace(value ?? "Verdana");
                    break;
            }
        }

        public void Load(IVsSettingsReader reader, string valueName)
        {
            string value;
            reader.ReadSettingString(valueName, out value);
            this.Load(value, valueName);
        }

        public void OnSettingsChanged()
        {
            this.Profile.SaveSettingsToStorage(this);
            this.SettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        public string ReadValue(string valueName)
        {
            Contract.Requires(!string.IsNullOrEmpty(valueName));

            switch (valueName)
            {
                case "IsLoggingEnabled":
                    return this.IsLoggingEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsNLogConfigLoggingEnabled":
                    return this.IsNLogConfigLoggingEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsTraceEnabled":
                    return this.IsTraceEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsDebugEnabled":
                    return this.IsDebugEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsInfoEnabled":
                    return this.IsInfoEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsWarnEnabled":
                    return this.IsWarnEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsFatalEnabled":
                    return this.IsFatalEnabled.ToString(CultureInfo.InvariantCulture);
                case "IsErrorEnabled":
                    return this.IsErrorEnabled.ToString(CultureInfo.InvariantCulture);
                case "FontFamily":
                    return this.FontFamily.ToString(CultureInfo.InvariantCulture);
                case "GiganticFactor":
                    return this.GiganticFactor.ToString(CultureInfo.InvariantCulture);
                case "HugeFactor":
                    return this.HugeFactor.ToString(CultureInfo.InvariantCulture);
                case "LargeFactor":
                    return this.LargeFactor.ToString(CultureInfo.InvariantCulture);
                case "SmallFactor":
                    return this.SmallFactor.ToString(CultureInfo.InvariantCulture);
                case "TinyFactor":
                    return this.TinyFactor.ToString(CultureInfo.InvariantCulture);
                case "MicroFactor":
                    return this.MicroFactor.ToString(CultureInfo.InvariantCulture);
                case "Task01":
                    return this.Task01.ToString(CultureInfo.InvariantCulture);
                case "Task02":
                    return this.Task02.ToString(CultureInfo.InvariantCulture);
                case "Task03":
                    return this.Task03.ToString(CultureInfo.InvariantCulture);
                case "Task04":
                    return this.Task04.ToString(CultureInfo.InvariantCulture);
                case "Task05":
                    return this.Task05.ToString(CultureInfo.InvariantCulture);
                case "Task06":
                    return this.Task06.ToString(CultureInfo.InvariantCulture);
                case "Task07":
                    return this.Task07.ToString(CultureInfo.InvariantCulture);
                case "Task08":
                    return this.Task08.ToString(CultureInfo.InvariantCulture);
                case "Task09":
                    return this.Task09.ToString(CultureInfo.InvariantCulture);
                case "Task10":
                    return this.Task10.ToString(CultureInfo.InvariantCulture);
                case "TaskBold01":
                    return this.TaskBold01.ToString(CultureInfo.InvariantCulture);
                case "TaskBold02":
                    return this.TaskBold02.ToString(CultureInfo.InvariantCulture);
                case "TaskBold03":
                    return this.TaskBold03.ToString(CultureInfo.InvariantCulture);
                case "TaskBold04":
                    return this.TaskBold04.ToString(CultureInfo.InvariantCulture);
                case "TaskBold05":
                    return this.TaskBold05.ToString(CultureInfo.InvariantCulture);
                case "TaskBold06":
                    return this.TaskBold06.ToString(CultureInfo.InvariantCulture);
                case "TaskBold07":
                    return this.TaskBold07.ToString(CultureInfo.InvariantCulture);
                case "TaskBold08":
                    return this.TaskBold08.ToString(CultureInfo.InvariantCulture);
                case "TaskBold09":
                    return this.TaskBold09.ToString(CultureInfo.InvariantCulture);
                case "TaskBold10":
                    return this.TaskBold10.ToString(CultureInfo.InvariantCulture);
                case "TaskColor01":
                    return this.TaskColor01.ToString(CultureInfo.InvariantCulture);
                case "TaskColor02":
                    return this.TaskColor02.ToString(CultureInfo.InvariantCulture);
                case "TaskColor03":
                    return this.TaskColor03.ToString(CultureInfo.InvariantCulture);
                case "TaskColor04":
                    return this.TaskColor04.ToString(CultureInfo.InvariantCulture);
                case "TaskColor05":
                    return this.TaskColor05.ToString(CultureInfo.InvariantCulture);
                case "TaskColor06":
                    return this.TaskColor06.ToString(CultureInfo.InvariantCulture);
                case "TaskColor07":
                    return this.TaskColor07.ToString(CultureInfo.InvariantCulture);
                case "TaskColor08":
                    return this.TaskColor08.ToString(CultureInfo.InvariantCulture);
                case "TaskColor09":
                    return this.TaskColor09.ToString(CultureInfo.InvariantCulture);
                case "TaskColor10":
                    return this.TaskColor10.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface01":
                    return this.TaskTypeface01.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface02":
                    return this.TaskTypeface02.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface03":
                    return this.TaskTypeface03.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface04":
                    return this.TaskTypeface04.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface05":
                    return this.TaskTypeface05.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface06":
                    return this.TaskTypeface06.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface07":
                    return this.TaskTypeface07.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface08":
                    return this.TaskTypeface08.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface09":
                    return this.TaskTypeface09.Name.ToString(CultureInfo.InvariantCulture);
                case "TaskTypeface10":
                    return this.TaskTypeface10.Name.ToString(CultureInfo.InvariantCulture);
            }

            return null;
        }

        public void ResetSettings()
        {
            this.Profile.ResetSettings();
        }

        public void Save(RegistryKey key, string valueName)
        {
            string value = this.ReadValue(valueName);

            key.SetValue(valueName, value);
        }

        public void Save(IVsSettingsWriter writer, string valueName)
        {
            string value = this.ReadValue(valueName);
            writer.WriteSettingString(valueName, value);
        }

        #endregion
    }
}