using System.Collections.Specialized;
using System.Linq;
using Avalonia.Input;
using DynamicData;

namespace GraphsManager.Views.ViewComponents;

public partial class GraphEditor
{
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (State is GraphState.Point)
        {
            var point = new GraphPoint(_pointNamesCounter++.ToString());
            point.PointerPressed += PointOnPointerPressed;
            point.PointerReleased += PointOnPointerReleased;
            point.SetPosition(e.GetPosition(this));
            Children.Add(point);
        }
        else if (State is GraphState.MovingElement && e.GetCurrentPoint(this).Properties.IsRightButtonPressed &&
                 _selectedElement is GraphLine line)
        {
            if (string.IsNullOrEmpty(line.SecondPointName))
            {
                _points.First(x => x.Name == line.FirstPointName).Lines.Remove(line);
                Children.Remove(line);
            }

            _selectedElement = null!;
            State = GraphState.Line;
        }
    }
    
    private void PointOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = (GraphPoint)sender!;
        if (State is GraphState.Line)
        {
            var line = new GraphLine()
            {
                StartPoint = point.Center,
                EndPoint = e.GetPosition(this),
                FirstPointName = point.Name
            };
            point.Lines.Add(line);
            Children.Add(line);
            _selectedElement = line;
            State = GraphState.MovingElement;
        }
        else if (State is GraphState.MovingElement && _selectedElement is GraphLine line && line.FirstPointName != point.Name)
        {
            line.EndPoint = point.Center;
            line.SecondPointName = point.Name;
            line.PointerPressed += LineOnPointerPressed;
            line.Activate();
            point.Lines.Add(line);
            _selectedElement = null!;
            State = GraphState.Line;
        }
        else if (State is GraphState.Hand)
        {
            _selectedElement = point;
            State = GraphState.MovingElement;
        }
    }

    private void LineOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var line = (GraphLine) sender!;
        line.Deactivate();
        _selectedElement = line;
        State = GraphState.MovingElement;
    }

    private void PointOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (State is not GraphState.MovingElement || _selectedElement is not GraphPoint) return;
        State = GraphState.Hand;
        _selectedElement = null;
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var position = e.GetPosition(this);
        if (State is GraphState.MovingElement)
        {
            if (_selectedElement is GraphPoint point) point.SetPosition(position);
            else if (_selectedElement is GraphLine line) line.EndPoint = position; 
        }
        else if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            foreach (var graphPoint in _points)
            {
                graphPoint.ChangePosition(_currentPointerPoint - position);
            }
        }

        _currentPointerPoint = position;
    }
    
    private void ChildrenOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems?.Count > 0)
        {
            if (e.NewItems[0] is GraphLine) _lines.AddRange(e.NewItems.Cast<GraphLine>());
            else if (e.NewItems[0] is GraphPoint) _points.AddRange(e.NewItems.Cast<GraphPoint>());
        }

        if (e.OldItems?.Count > 0)
        {
            if (e.OldItems[0] is GraphLine) _lines.RemoveMany(e.OldItems.Cast<GraphLine>());
            else if (e.OldItems[0] is GraphPoint) _points.RemoveMany(e.OldItems.Cast<GraphPoint>());
        }
    }
}