using System.Collections.Generic;
using System.Windows;

namespace CTS.Kanban;
/// <summary>
/// Interaction logic for CreateTaskModalWindow.xaml
/// </summary>
public partial class CreateTaskModalWindow : Window
{
    private readonly KanbanColumn _kanbanColumn;

    public CreateTaskModalWindow(KanbanColumn kanbanColumn, List<string> statuses, KanbanCard? kanbanCard = null)
    {
        InitializeComponent();
        TitleBox.Focus();
        StatusBox.ItemsSource=statuses;
        StatusBox.SelectedIndex=0;
        if(kanbanCard is not null)
        {
            TitleBox.Text=kanbanCard.Title;
            DescriptionBox.Text=kanbanCard.Description;
            StatusBox.SelectedItem=kanbanCard.StatusType;
        }
        _kanbanColumn=kanbanColumn;
    }
    public KanbanCard? Result { get; set; }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Result=new KanbanCard()
        {
            Title=this.TitleBox.Text,
            Description=this.DescriptionBox.Text,
            StatusType=(StatusBox.SelectedItem??string.Empty)?.ToString(),
            KanbanColumn=_kanbanColumn
        };
        this.DialogResult=true;
        Close();
    }
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        this.DialogResult=false;
    }

    private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        (sender as Window).DragMove();
    }
}
