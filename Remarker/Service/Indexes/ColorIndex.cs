namespace YoderZone.Extensions.Remarker.Service.Indexes
{
    public class ColorIndex
{
    private readonly RemarkerService service;

        public ColorIndex(RemarkerService service) => this.service = service;

        public string this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.service.TaskColor01;
                case 1:
                    return this.service.TaskColor02;
                case 2:
                    return this.service.TaskColor03;
                case 3:
                    return this.service.TaskColor04;
                case 4:
                    return this.service.TaskColor05;
                case 5:
                    return this.service.TaskColor06;
                case 6:
                    return this.service.TaskColor07;
                case 7:
                    return this.service.TaskColor08;
                case 8:
                    return this.service.TaskColor09;
                case 9:
                    return this.service.TaskColor10;
            }
            return null;
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.service.TaskColor01 = value;
                    break;
                case 1:
                    this.service.TaskColor02 = value;
                    break;
                case 2:
                    this.service.TaskColor03 = value;
                    break;
                case 3:
                    this.service.TaskColor04 = value;
                    break;
                case 4:
                    this.service.TaskColor05 = value;
                    break;
                case 5:
                    this.service.TaskColor06 = value;
                    break;
                case 6:
                    this.service.TaskColor07 = value;
                    break;
                case 7:
                    this.service.TaskColor08 = value;
                    break;
                case 8:
                    this.service.TaskColor09 = value;
                    break;
                case 9:
                    this.service.TaskColor10 = value;
                    break;
            }
        }
    }
}
}