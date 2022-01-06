using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GraphsManager.Views.ViewComponents;
using SpiroNet;
using SpiroNet.Editor;

namespace GraphsManager.Views;

public partial class MainWindow : Window
{
    private GraphEditor _editor;
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        _editor = this.FindControl<GraphEditor>("Editor");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _editor.State = (GraphState)e.AddedItems[0]!;
    }
}