using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Input;

namespace GraphsManager.Views.ViewComponents;

public class GraphLine: Polyline
{
    private bool _activated;
    public GraphLine()
    {
        Stroke = GraphSettings.GraphLineColor;
        StrokeThickness = 1;
        PointerEnter += OnPointerEnter;
        PointerLeave += OnPointerLeave;
        ZIndex = 0;
        Points = new List<Point>(3);
    }
    
    public void Activate()
    {
        if (string.IsNullOrWhiteSpace(FirstPointName) || string.IsNullOrWhiteSpace(SecondPointName))
            throw new ValidationException();
        _activated = true;
        IsVisible = true;
    }

    public void Deactivate()
    {
        _activated = false;
    }

    private void OnPointerLeave(object? sender, PointerEventArgs e)
    {
        if (!_activated) return;
        StrokeThickness = 1;
        Stroke = GraphSettings.GraphLineColor;
    }

    private void OnPointerEnter(object? sender, PointerEventArgs e)
    {
        if (!_activated) return;
        StrokeThickness = 1.5;
        Stroke = GraphSettings.GraphLineSelectedColor;
    }
    

    public new string Name { get; set; }
    public string FirstPointName { get; set; }
    public string SecondPointName { get; set; }
    public Point StartPoint { get => Points[0]; set => Points[0] = value; }
    public Point EndPoint { get => Points[^1]; set => Points[^1] = value; }
}