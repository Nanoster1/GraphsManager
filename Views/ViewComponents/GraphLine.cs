using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Input;

namespace GraphsManager.Views.ViewComponents;

public class GraphLine: Line
{
    private bool _activated;
    public GraphLine()
    {
        Stroke = GraphSettings.GraphLineColor;
        StrokeThickness = 1;
        PointerEnter += OnPointerEnter;
        PointerLeave += OnPointerLeave;
        ZIndex = 0;
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
}