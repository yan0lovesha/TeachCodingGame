using System;
using System.Drawing;

namespace GameCourse.Common
{
    public static class ColorUtility
    {
        public static Color FromColorNameToColor(string colorName)
        {
            return Color.FromName(colorName);
        }

        public static string FromColorToHex(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static string FromColorNameToHex(string colorName)
        {
            return FromColorNameToColor(colorName).FromColorToHex();
        }

        public static Color ChangeBrightness(this Color color, double brightness)
        {
            double R = color.R;
            double G = color.G;
            double B = color.B;

            R *= brightness;
            G *= brightness;
            B *= brightness;

            // Ensure they are within the valid range 0-255
            R = (R > 255) ? 255 : R;
            G = (G > 255) ? 255 : G;
            B = (B > 255) ? 255 : B;

            return Color.FromArgb(color.A, (int)R, (int)G, (int)B);
        }

        public static Color ChangeGreyScale(this Color color, double amount)
        {
            double avg = (color.R + color.G + color.B) / 3.0;
            double R = 0;
            double G = 0;
            double B = 0;
            if (avg == 0)
            {
                R = 255 * amount;
                G = R;
                B = R;
            }
            else
            {
                R = avg + (color.R - avg) * amount;
                G = avg + (color.G - avg) * amount;
                B = avg + (color.B - avg) * amount;
            }

            // Ensure they are within the valid range 0-255
            R = (R > 255) ? 255 : (R < 0) ? 0 : R;
            G = (G > 255) ? 255 : (G < 0) ? 0 : G;
            B = (B > 255) ? 255 : (B < 0) ? 0 : B;

            return Color.FromArgb(color.A, (int)R, (int)G, (int)B);
        }
    }
}