namespace YoderZone.Extensions.Remarker.Remarker.Service.Indexes
{
public class FontFamilyIndex
{
    private readonly RemarkerService service;

    public FontFamilyIndex(RemarkerService service)
    {
        this.service = service;
    }

    public string this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.service.TaskTypeface01;
                case 1:
                    return this.service.TaskTypeface02;
                case 2:
                    return this.service.TaskTypeface03;
                case 3:
                    return this.service.TaskTypeface04;
                case 4:
                    return this.service.TaskTypeface05;
                case 5:
                    return this.service.TaskTypeface06;
                case 6:
                    return this.service.TaskTypeface07;
                case 7:
                    return this.service.TaskTypeface08;
                case 8:
                    return this.service.TaskTypeface09;
                case 9:
                    return this.service.TaskTypeface10;
            }

            return null;
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.service.TaskTypeface01 = value;
                    break;
                case 1:
                    this.service.TaskTypeface02 = value;
                    break;
                case 2:
                    this.service.TaskTypeface03 = value;
                    break;
                case 3:
                    this.service.TaskTypeface04 = value;
                    break;
                case 4:
                    this.service.TaskTypeface05 = value;
                    break;
                case 5:
                    this.service.TaskTypeface06 = value;
                    break;
                case 6:
                    this.service.TaskTypeface07 = value;
                    break;
                case 7:
                    this.service.TaskTypeface08 = value;
                    break;
                case 8:
                    this.service.TaskTypeface09 = value;
                    break;
                case 9:
                    this.service.TaskTypeface10 = value;
                    break;
            }
        }
    }
}
}