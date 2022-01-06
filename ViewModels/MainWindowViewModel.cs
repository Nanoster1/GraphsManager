using System;
using System.Collections.Generic;
using System.Text;
using GraphsManager.Views.ViewComponents;

namespace GraphsManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public List<GraphState> States { get; } = new() {GraphState.Hand, GraphState.Point, GraphState.Line};
}