using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class ColorChanger : IColorChanger
    {
        public string LightenBy(string colorHex, int percent)
        {
            Color color = GetColorFromHex(colorHex);
            var adjustedColor = ChangeColorBrightness(color, (float)(percent / 100.0));
            return GetHexFromColor(adjustedColor);
        }

        public string DarkenBy(string colorHex, int percent)
        {
            Color color = GetColorFromHex(colorHex);
            var adjustedColor = ChangeColorBrightness(color, (float)(-1 * percent / 100.0));
            return GetHexFromColor(adjustedColor);
        }

        private static Color GetColorFromHex(string colorHex)
        {
            return ColorTranslator.FromHtml(colorHex); ;
        }

        private static string GetHexFromColor(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        private static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
