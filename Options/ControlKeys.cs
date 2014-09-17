namespace YoderZone.Extensions.OptionsDialog
{
    using System;

    [Flags]
    public enum ControlKeys
    {
        None = 0,
        LeftCtrl = 1,
        RightCtrl = 2,
        LeftShift = 4,
        RightShift = 8,
        LeftAlt = 16,
        RightAlt = 32,
        Enter = 64,
        Escape = 128,
        All = LeftAlt | RightAlt | LeftCtrl | RightCtrl | LeftShift | RightShift | Enter | Escape
    }
}