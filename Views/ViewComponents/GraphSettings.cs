using Avalonia.Media;

namespace GraphsManager.Views.ViewComponents;

public static class GraphSettings
{
    public static IBrush GraphPointColor { get; set; } = Brushes.Black;
    public static IBrush GraphPointSelectedColor { get; set; } = Brushes.Red;
    public static IBrush GraphLineColor { get; set; } = Brushes.Black;
    public static IBrush GraphLineSelectedColor { get; set; } = Brushes.Yellow;
}