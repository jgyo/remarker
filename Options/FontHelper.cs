namespace YoderZone.Extensions.Options
{
    using global::Options;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Linq;

    public static class FontHelper
    {
        #region Fields

        private static FontAttributes _consolas;

        private static List<FontAttributes> _fontFamilies;

        private static FontAttributes _verdana;

        #endregion Fields

        #region Constructors and Destructors

        static FontHelper()
        {
            FontHelper._fontFamilies = new List<FontAttributes>();
            var installedFontCollection = new InstalledFontCollection();
            FontFamily[] fontFamilyArray = installedFontCollection.Families;

            var bitmap = new Bitmap(100, 100);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var fontFamily in fontFamilyArray)
                {
                    try
                    {
                        var fontAttributes = new FontAttributes(fontFamily, graphics);
                        FontHelper.FontFamilies.Add(fontAttributes);
                        if (fontAttributes.Name.Contains("Verdana"))
                        {
                            FontHelper._verdana = fontAttributes;
                        }

                        if (fontAttributes.Name.Contains("Consolas"))
                        {
                            FontHelper._consolas = fontAttributes;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }
        }

        #endregion Constructors and Destructors

        #region Properties

        public static FontAttributes Consolas => FontHelper._consolas;

        public static List<FontAttributes> FontFamilies => FontHelper._fontFamilies;

        public static FontAttributes Verdana => FontHelper._verdana;

        #endregion Properties

        public static FontAttributes GetTypeFace(string name) => FontHelper.FontFamilies.FirstOrDefault(family => family.Name == name);
    }
}