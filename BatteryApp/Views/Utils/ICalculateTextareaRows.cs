namespace BatteryApp.Views.Utils
{
    public interface ICalculateTextareaRows
    {
        int CalculateRows(string value, int minRows = 3, int maxRows = 25);
    }
}