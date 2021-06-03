using System.Drawing;

namespace BatteryApp.Views.Utils
{
    public interface IColorChanger
    {
        string DarkenBy(string colorHex, int percent);
        string LightenBy(string colorHex, int percent);
    }
}