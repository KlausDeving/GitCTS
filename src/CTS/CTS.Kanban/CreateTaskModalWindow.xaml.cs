using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace CTS.Kanban;
/// <summary>
/// Interaction logic for CreateTaskModalWindow.xaml
/// </summary>
public partial class CreateTaskModalWindow : Window
{
    public CreateTaskModalWindow(List<string> statuses, KanbanCard? kanbanCard = null)
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
    }
    public KanbanCard? Result { get; set; }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Result=new KanbanCard()
        {
            Title=this.TitleBox.Text,
            Description=this.DescriptionBox.Text,
            StatusType=(StatusBox.SelectedItem??string.Empty)?.ToString()
        };
        this.DialogResult=true;
        Close();
    }
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        this.DialogResult=false;
    }
}
