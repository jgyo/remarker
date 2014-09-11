// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace Options.Itboy
{
using System;

/// <summary>
/// Validation type, it can be one or more elements combination
/// </summary>
[Flags]
public enum ValidationType
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0x00,
    /// <summary>
    /// Required validation.
    /// </summary>
    Required = 0x01,
    /// <summary>
    /// Regular expression validation.
    /// </summary>
    RegularExpression = 0x02,
    /// <summary>
    /// Compare validation.
    /// </summary>
    Compare = 0x04,
    /// <summary>
    /// Range validation.
    /// </summary>
    Range = 0x08,
    /// <summary>
    /// Custom validation.
    /// </summary>
    Custom = 0x10
}
}
