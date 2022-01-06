using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;

namespace GraphsManager.Views.ViewComponents;

public class GraphPoint : Ellipse
{
    public GraphPoint(string name)
    {
        Name = name;
        Width = 10;
        Height = 10;
        Fill = GraphSettings.GraphPointColor;
        PointerEnter += OnPointerEnter;
        PointerLeave += OnPointerLeave;
        ZIndex = 1;
    }

    public List<GraphLine> Lines { get; } = new();
    public Point Center { get; private set; }
    
    private void OnPointerLeave(object? sender, PointerEventArgs e)
    {
        RenderTransform = new ScaleTransform()
        {
            ScaleX = 1,
            ScaleY = 1
        };
        Fill = GraphSettings.GraphPointColor;
    }

    private void OnPointerEnter(object? sender, PointerEventArgs e)
    {
        RenderTransform = new ScaleTransform()
        {
            ScaleX = 2,
            ScaleY = 2
        };
        Fill = GraphSettings.GraphPointSelectedColor;
    }

    public void ChangePosition(Point changes)
    {
        var newPosition = Center - changes;
        SetPosition(newPosition);
    }

    public void SetPosition(Point position)
    {
        Canvas.SetTop(this, position.Y - 5);
        Canvas.SetLeft(this, position.X - 5);
        Center = position;
        
        Lines.ForEach(line =>
        {
            if (line.FirstPointName == Name) line.StartPoint = Center;
            else line.EndPoint = Center;
        });
    }
}