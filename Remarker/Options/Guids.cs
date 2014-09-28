// Guids.cs
// MUST match guids.h

namespace YoderZone.Extensions.Remarker.Options
{
using System;

static class Guids
{
    public const string guidRemarkerPkgString =
        "81ce9f0c-4ff5-4279-9099-892a71549176";

    public const string guidRemarkerCmdSetString =
        "cb2fe36d-d618-4b8d-aa30-81e6b5f8da2d";

    public const string RemarkerServiceGuid =
        "C9E0EFB6-17B8-439D-AEE9-1D2A86F5334A";

    public static readonly Guid guidRemarkerCmdSet = new Guid(
        guidRemarkerCmdSetString);

    public const string ProfileManagerGuid =
        "7C44AF57-067D-40DA-BC56-3A2679C64008";
};
}