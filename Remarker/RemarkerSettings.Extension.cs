// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerSettings.Extension.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 09, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage
{
#region Imports

using System.Collections.Generic;
using System.Windows.Media;

using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;

#endregion

internal partial class RemarkerSettings
{
    #region Fields

    private TaskNameIndex nameIndex;

    private TaskColor taskColor;

    private TaskWeight taskWeight;

    private TaskFont fontFamilyIndex;

    #endregion

    #region Public Properties

    public TaskColor ColorIndex
    {
        get
        {
            return this.taskColor ?? (this.taskColor = new TaskColor());
        }
    }

    public TaskWeight IsBoldIndex
    {
        get
        {
            return this.taskWeight ?? (this.taskWeight = new TaskWeight());
        }
    }

    public TaskFont FontFamilyIndex
    {
        get
        {
            return fontFamilyIndex ?? (this.fontFamilyIndex = new TaskFont());
        }
    }

    public float EmSize { get; set; }

    public float GiaganticFontSize
    {
        get
        {
            return this.EmSize * this.GiantFactor;
        }
    }

    public float HugeFontSize
    {
        get
        {
            return this.EmSize * this.HugeFactor;
        }
    }

    public float LargeFontSize
    {
        get
        {
            return this.EmSize * this.LargeFactor;
        }
    }

    public float MicroFontSize
    {
        get
        {
            return this.EmSize * this.MicroFactor;
        }
    }

    public TaskNameIndex NameIndex
    {
        get
        {
            return this.nameIndex ?? (this.nameIndex = new TaskNameIndex());
        }
    }

    public float SmallFontSize
    {
        get
        {
            return this.EmSize * this.SmallFactor;
        }
    }

    public IEnumerable<string> TaskNames
    {
        get
        {
            yield return this.Task01;
            yield return this.Task02;
            yield return this.Task03;
            yield return this.Task04;
            yield return this.Task05;
            yield return this.Task06;
            yield return this.Task07;
            yield return this.Task08;
            yield return this.Task09;
            yield return this.Task10;
        }
    }

    public float TinyFontSize
    {
        get
        {
            return this.EmSize * this.TinyFactor;
        }
    }

    #endregion

    public class TaskFont
    {
        private readonly RemarkerSettings settings = Default;

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1:
                        return settings.TaskTypeface01;
                    case 2:
                        return settings.TaskTypeface02;
                    case 3:
                        return settings.TaskTypeface03;
                    case 4:
                        return settings.TaskTypeface04;
                    case 5:
                        return settings.TaskTypeface05;
                    case 6:
                        return settings.TaskTypeface06;
                    case 7:
                        return settings.TaskTypeface07;
                    case 8:
                        return settings.TaskTypeface08;
                    case 9:
                        return settings.TaskTypeface09;
                    case 10:
                        return settings.TaskTypeface10;
                }

                return "";
            }
        }

        public string this[string typeName]
        {
            get
            {
                return this[this.settings.NameIndex[typeName]];
            }
        }

    }

    public class TaskWeight
    {
        private readonly RemarkerSettings settings = Default;

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1:
                        return  settings.TaskBold01;
                    case 2:
                        return  settings.TaskBold02;
                    case 3:
                        return  settings.TaskBold03;
                    case 4:
                        return  settings.TaskBold04;
                    case 5:
                        return  settings.TaskBold05;
                    case 6:
                        return  settings.TaskBold06;
                    case 7:
                        return  settings.TaskBold07;
                    case 8:
                        return  settings.TaskBold08;
                    case 9:
                        return  settings.TaskBold09;
                    case 10:
                        return settings.TaskBold10;
                }

                return true;
            }
        }

        public bool this[string typeName]
        {
            get
            {
                return this[this.settings.NameIndex[typeName]];
            }
        }
    }

    public class TaskColor
    {
        #region Fields

        private readonly RemarkerSettings settings = Default;

        #endregion

        #region Public Indexers

        public Color this[int index]
        {
            get
            {
                string color;
                switch (index)
                {
                    case 1:
                        color = this.settings.TaskColor01;
                        break;
                    case 2:
                        color = this.settings.TaskColor02;
                        break;
                    case 3:
                        color = this.settings.TaskColor03;
                        break;
                    case 4:
                        color = this.settings.TaskColor04;
                        break;
                    case 5:
                        color = this.settings.TaskColor05;
                        break;
                    case 6:
                        color = this.settings.TaskColor06;
                        break;
                    case 7:
                        color = this.settings.TaskColor07;
                        break;
                    case 8:
                        color = this.settings.TaskColor08;
                        break;
                    case 9:
                        color = this.settings.TaskColor09;
                        break;
                    case 10:
                        color = this.settings.TaskColor10;
                        break;
                    default:
                        return Colors.Black;
                }

                return color.ConvertStringToWpfColor();
            }
        }

        public Color this[string taskName]
        {
            get
            {
                return this[this.settings.NameIndex[taskName]];
            }
        }

        #endregion
    }

    public class TaskNameIndex
    {
        #region Fields

        private readonly RemarkerSettings settings = Default;

        #endregion

        #region Public Indexers

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1:
                        return this.settings.Task01;
                    case 2:
                        return this.settings.Task02;
                    case 3:
                        return this.settings.Task03;
                    case 4:
                        return this.settings.Task04;
                    case 5:
                        return this.settings.Task05;
                    case 6:
                        return this.settings.Task06;
                    case 7:
                        return this.settings.Task07;
                    case 8:
                        return this.settings.Task08;
                    case 9:
                        return this.settings.Task09;
                    case 10:
                        return this.settings.Task10;
                    default:
                        return "";
                }
            }
        }

        public int this[string name]
        {
            get
            {
                name = name.ToLower();
                for (int i = 1; i <= 10; i++)
                {
                    switch (i)
                    {
                        case 1:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 2:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 3:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 4:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 5:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 6:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 7:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 8:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 9:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                        case 10:
                            if (this.settings.Task01 == name)
                            {
                                return i;
                            }
                            break;
                    }
                }

                return -1;
            }
        }

        #endregion
    }
}
}