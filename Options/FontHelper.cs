namespace YoderZone.Extensions.Options
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Linq;

    using global::Options;

    public static class FontHelper
    {
        #region Fields

        private static FontAttributes consolas;

        private static List<FontAttributes> fontFamilies;

        private static FontAttributes verdana;

        #endregion

        #region Constructors and Destructors

        static FontHelper()
        {
            FontHelper.fontFamilies = new List<FontAttributes>();
            var installedFontCollection = new InstalledFontCollection();
            FontFamily[] fontFamilyArray = installedFontCollection.Families;

            var bitmap = new Bitmap(100, 100);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                foreach (var fontFamily in fontFamilyArray)
                {
                    try
                    {
                        var fontAttributes = new FontAttributes(fontFamily, graphics);
                        FontHelper.FontFamilies.Add(fontAttributes);
                        if (fontAttributes.Name.Contains("Verdana"))
                        {
                            FontHelper.verdana = fontAttributes;
                        }

                        if (fontAttributes.Name.Contains("Consolas"))
                        {
                            FontHelper.consolas = fontAttributes;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Properties

        public static FontAttributes Consolas
        {
            get
            {
                return FontHelper.consolas;
            }
        }

        public static List<FontAttributes> FontFamilies
        {
            get
            {
                return FontHelper.fontFamilies;
            }
        }

        public static FontAttributes Verdana
        {
            get
            {
                return FontHelper.verdana;
            }
        }

        #endregion

        public static FontAttributes GetTypeFace(string name)
        {
            return FontHelper.FontFamilies.FirstOrDefault(family => family.Name == name);
        }
    }
}