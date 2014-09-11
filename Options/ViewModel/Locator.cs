using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoderZone.Extensions.OptionsDialog.ViewModel
{
public class Locator
{
    public CommentOptions CommentOptions
    {
        get
        {
            return CommentOptions.DefaultOptions;
        }
    }

    public TaskOptions TaskOptions
    {
        get
        {
            return TaskOptions.DefaultOptions;
        }
    }
}
}
