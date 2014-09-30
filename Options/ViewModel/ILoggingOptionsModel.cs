namespace YoderZone.Extensions.Options.ViewModel
{
using System.ComponentModel;

public interface ILoggingOptionsModel : INotifyPropertyChanged
{
    bool IsDebugEnabled { get; set; }

    bool IsErrorEnabled { get; set; }

    bool IsFatalEnabled { get; set; }

    bool IsInfoEnabled { get; set; }

    bool IsLoggingEnabled { get; set; }

    bool IsTraceEnabled { get; set; }

    bool IsWarnEnabled { get; set; }

    string LogFilesPath { get; }

    string NLogConfigLogFilePath { get; }

    bool NLogConfigLogFilePathExists { get; }

    bool LogFilesPathExists { get; }
}
}