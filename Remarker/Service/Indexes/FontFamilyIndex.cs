namespace YoderZone.Extensions.Remarker.Service.Indexes
{
    using YoderZone.Extensions.Options;

    public class FontFamilyIndex
{
    private readonly RemarkerService service;

        public FontFamilyIndex(RemarkerService service) => this.service = service;

        public string this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.service.TaskTypeface01.Name;
                case 1:
                    return this.service.TaskTypeface02.Name;
                case 2:
                    return this.service.TaskTypeface03.Name;
                case 3:
                    return this.service.TaskTypeface04.Name;
                case 4:
                    return this.service.TaskTypeface05.Name;
                case 5:
                    return this.service.TaskTypeface06.Name;
                case 6:
                    return this.service.TaskTypeface07.Name;
                case 7:
                    return this.service.TaskTypeface08.Name;
                case 8:
                    return this.service.TaskTypeface09.Name;
                case 9:
                    return this.service.TaskTypeface10.Name;
            }

            return null;
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.service.TaskTypeface01 = FontHelper.GetTypeFace(value);
                    break;
                case 1:
                    this.service.TaskTypeface02 = FontHelper.GetTypeFace(value);
                    break;
                case 2:
                    this.service.TaskTypeface03 = FontHelper.GetTypeFace(value);
                    break;
                case 3:
                    this.service.TaskTypeface04 = FontHelper.GetTypeFace(value);
                    break;
                case 4:
                    this.service.TaskTypeface05 = FontHelper.GetTypeFace(value);
                    break;
                case 5:
                    this.service.TaskTypeface06 = FontHelper.GetTypeFace(value);
                    break;
                case 6:
                    this.service.TaskTypeface07 = FontHelper.GetTypeFace(value);
                    break;
                case 7:
                    this.service.TaskTypeface08 = FontHelper.GetTypeFace(value);
                    break;
                case 8:
                    this.service.TaskTypeface09 = FontHelper.GetTypeFace(value);
                    break;
                case 9:
                    this.service.TaskTypeface10 = FontHelper.GetTypeFace(value);
                    break;
            }
        }
    }
}
}