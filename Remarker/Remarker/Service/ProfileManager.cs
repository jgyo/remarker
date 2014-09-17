// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : ProfileManager.cs
// Author           : Gil Yoder
// Created          : 09 15,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 16, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Remarker.Service
{
#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

using YoderZone.Extensions.OptionsPackage.Options;
using YoderZone.Extensions.OptionsPackage.Remarker.Service;

#endregion

[Guid(Guids.ProfileManagerGuid)]
public class ProfileManager : Component, IProfileManager
{
    #region Constants

    private const string SUBKEY_NAME = "Remarker";

    #endregion

    #region Static Fields

    private static readonly object dataLock = new object();

    private static int backupCount;

    private static Dictionary<string, string> backupSettings;

    #endregion

    #region Public Properties

    public RemarkerService Service
    {
        get
        {
            if (this.Site != null)
            {
                return (RemarkerService)this.Site.GetService(typeof(IRemarkerService));
            }

            return
                (RemarkerService)
                ServiceProvider.GlobalProvider.GetService(typeof(IRemarkerService));
        }
    }

    #endregion

    #region Public Methods and Operators

    public bool CommitSettings()
    {
        lock (dataLock)
        {
            if (backupCount == 0)
            {
                return false;
            }

            if (backupCount > 1)
            {
                backupCount--;
                return false;
            }

            backupSettings = null;
            backupCount = 0;
            RemarkerService service = this.Service;
            if (service == null)
            {
                return false;
            }

            service.OnSettingsChanged();
            return true;
        }
    }

    public void LoadSettingsFromStorage()
    {
        this.LoadSettingsFromStorage(this.Service);
    }

    public void LoadSettingsFromStorage(RemarkerService service)
    {
        lock (dataLock)
        {
            if (backupSettings != null)
            {
                return;
            }
        }

        RegistryKey key = null;
        try
        {
            key = service.Package.UserRegistryRoot.OpenSubKey(SUBKEY_NAME);

            foreach (var valueName in service.ValueNames)
            {
                service.Load(key, valueName);
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine(string.Format("Remarker::LoadSettingsFromStorage: {0}",
                                          ex));
        }
        finally
        {
            if (key != null)
            {
                key.Close();
            }
        }
    }

    public void LoadSettingsFromXml(IVsSettingsReader reader)
    {
        string valueKeysString;
        reader.ReadSettingString("ValueKeys", out valueKeysString);
        RemarkerService service = this.Service;

        foreach (var valueKey in valueKeysString.Split(';'))
        {
            if (string.IsNullOrWhiteSpace(valueKey))
            {
                continue;
            }

            try
            {
                service.Load(reader, valueKey);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("ProfileManager::LoadSettingsFromXml: {0}",
                                              ex));
            }
        }
    }

    public bool ProtectSettings()
    {
        lock (dataLock)
        {
            if (backupSettings != null)
            {
                backupCount++;
                return false;
            }

            RemarkerService service = this.Service;

            backupSettings = new Dictionary<string, string>();
            foreach (var valueName in service.ValueNames)
            {
                backupSettings.Add(valueName, service.ReadValue(valueName));
            }
            backupCount = 1;
            return true;
        }
    }

    public void ResetSettings()
    {
        this.ResetSettings(this.Service);
    }

    public void ResetSettings(RemarkerService service)
    {
        service.Package.UserRegistryRoot.DeleteSubKey(SUBKEY_NAME, false);
        this.LoadSettingsFromStorage(this.Service);
    }

    public bool RollBackSettings()
    {
        lock (dataLock)
        {
            if (backupCount == 0)
            {
                return false;
            }

            if (backupSettings == null)
            {
                backupCount--;
                return false;
            }

            RemarkerService service = this.Service;
            foreach (var backupSetting in backupSettings)
            {
                // Restore the original setting
                service.Load(backupSetting.Value, backupSetting.Key);
            }

            return true;
        }
    }

    public void SaveSettingsToStorage()
    {
        this.SaveSettingsToStorage(this.Service);
    }

    public void SaveSettingsToStorage(RemarkerService service)
    {
        lock (dataLock)
        {
            if (backupSettings != null)
            {
                return;
            }
        }

        RegistryKey key = null;
        try
        {
            key = service.Package.UserRegistryRoot.CreateSubKey(SUBKEY_NAME);
            Debug.Assert(key != null, "key != null");
            key.SetValue("Version", service.Version);

            foreach (var valueName in service.ValueNames)
            {
                service.Save(key, valueName);
            }
        }
        finally
        {
            if (key != null)
            {
                key.Close();
            }
        }
    }

    public void SaveSettingsToXml(IVsSettingsWriter writer)
    {
        RemarkerService service = this.Service;

        var stringBuilder = new StringBuilder();
        foreach (var valueName in service.ValueNames)
        {
            stringBuilder.Append(valueName);
            stringBuilder.Append(";");
            service.Save(writer, valueName);
        }

        writer.WriteSettingLong("Version", service.Version);
        writer.WriteSettingString("ValueKeys", stringBuilder.ToString());
    }

    #endregion
}
}