namespace YoderZone.Extensions.Remarker.Service.Indexes
{
using System;
using System.Diagnostics.Contracts;

using global::NLog;

using YoderZone.Extensions.NLog;

public class IsBoldIndex
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        SettingsHelper.CreateLogger();

    private readonly RemarkerService service;

    public IsBoldIndex(RemarkerService service)
    {
        Contract.Requires<ArgumentNullException>(service != null);
        logger.Debug("Entered constructor.");

        this.service = service;
    }

    public bool this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.service.TaskBold01;
                case 1:
                    return this.service.TaskBold02;
                case 2:
                    return this.service.TaskBold03;
                case 3:
                    return this.service.TaskBold04;
                case 4:
                    return this.service.TaskBold05;
                case 5:
                    return this.service.TaskBold06;
                case 6:
                    return this.service.TaskBold07;
                case 7:
                    return this.service.TaskBold08;
                case 8:
                    return this.service.TaskBold09;
                case 9:
                    return this.service.TaskBold10;
            }

            return true;
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.service.TaskBold01 = value;
                    break;
                case 1:
                    this.service.TaskBold02 = value;
                    break;
                case 2:
                    this.service.TaskBold03 = value;
                    break;
                case 3:
                    this.service.TaskBold04 = value;
                    break;
                case 4:
                    this.service.TaskBold05 = value;
                    break;
                case 5:
                    this.service.TaskBold06 = value;
                    break;
                case 6:
                    this.service.TaskBold07 = value;
                    break;
                case 7:
                    this.service.TaskBold08 = value;
                    break;
                case 8:
                    this.service.TaskBold09 = value;
                    break;
                case 9:
                    this.service.TaskBold10 = value;
                    break;
            }
        }
    }
}
}