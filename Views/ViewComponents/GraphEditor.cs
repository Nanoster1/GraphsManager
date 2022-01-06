using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using DynamicData;

namespace GraphsManager.Views.ViewComponents;

public enum GraphState
{
    Hand, Point, Line, MovingElement
}

public enum GraphTheme
{
    Light
}
    
public partial class GraphEditor: Canvas, IDisposable
{
    private Collection<GraphLine> _lines = new();
    private Collection<GraphPoint> _points = new();
    private Point _currentPointerPoint;
    private char _pointNamesCounter = 'A';
    private IControl? _selectedElement;
    private IDisposable _disposable = Disposable.Empty;
    private GraphState _state;
    
    
        
    public GraphEditor()
    {
        Children.CollectionChanged += ChildrenOnCollectionChanged;
        PointerMoved += OnPointerMoved;
        PointerPressed += OnPointerPressed;
        ClipToBounds = true;
    }

    private void SetTheme(GraphTheme theme)
    {
        switch (theme)
        {
            case GraphTheme.Light:
                Background = Brushes.White;
                break;
        }
    }

    public IReadOnlyCollection<GraphPoint> Points { get => _points; }
    public IReadOnlyCollection<GraphLine> Lines { get => _lines; }
    public GraphTheme Theme { set => SetTheme(value); }
    public GraphState State
    {
        get => _state;
        set
        {
            _state = value;
            if (value is not (GraphState.Hand or GraphState.Line or GraphState.Point)) return;
            if (_selectedElement is GraphLine line)
            {
                Children.Remove(line);
                _selectedElement = null;   
            }
        }
    }

    public void Dispose()
    {
    }
}