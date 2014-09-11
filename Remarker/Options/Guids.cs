// Guids.cs
// MUST match guids.h

namespace YoderZone.Extensions.OptionsPackage.Options
{
using System;

static class GuidList
{
    public const string guidRemarkerPkgString =
        "81ce9f0c-4ff5-4279-9099-892a71549176";
    public const string guidRemarkerCmdSetString =
        "cb2fe36d-d618-4b8d-aa30-81e6b5f8da2d";

    public static readonly Guid guidRemarkerCmdSet = new Guid(
        guidRemarkerCmdSetString);
};
}