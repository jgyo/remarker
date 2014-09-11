// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms
//
namespace Options.Itboy
{
using System;

/// <summary>
/// ValidationMode, can use FocusChange and Submit combination.
/// </summary>
[Flags]
public enum ValidationMode
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0x0000,
    /// <summary>
    /// Allow focus change to next control
    /// </summary>
    FocusChange = 0x0001,
    /// <summary>
    /// Validate controls when user click "OK" or "Yes" button.
    /// </summary>
    Submit = 0x0002,
}
}
