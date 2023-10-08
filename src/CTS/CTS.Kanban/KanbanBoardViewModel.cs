using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CTS.Kanban;
public partial class KanbanBoardViewModel : ObservableObject
{
    public KanbanBoardViewModel(IKanbanCardService kanbanCardService)
    {
        _kanbanCardService=kanbanCardService;
    }

    [ObservableProperty]
    private ObservableCollection<KanbanColumnViewModel> _kanbanColumnViewModels = new();

    [ObservableProperty]
    private Visibility _inputVisibility = Visibility.Hidden;


    private string _input = string.Empty;

    public string Input
    {
        get => _input;
        set
        {
            _input=value;
            OnPropertyChanged();
            KanbanColumnViewModels.Add(new KanbanColumnViewModel(Input, new List<KanbanCardViewModel>(), _kanbanCardService));
            InputVisibility=Visibility.Hidden;
        }
    }



    private readonly IKanbanCardService _kanbanCardService;

    [RelayCommand]
    private void AddColumn()
    {
        InputVisibility=Visibility.Visible;
    }
}
