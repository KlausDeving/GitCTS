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
    public KanbanBoardViewModel(IKanbanObjectService<KanbanCard> kanbanCardService, IKanbanObjectService<KanbanColumn> kanbanColumnService)
    {
        _kanbanCardService=kanbanCardService;
        _kanbanColumnService=kanbanColumnService;
        _kanbanColumnViewModels.CollectionChanged+=KanbanColumnViewModels_CollectionChanged;
    }

    private void KanbanColumnViewModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch(e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                {
                    _kanbanColumnService.UpsertAsync(new KanbanColumn { StatusType=e.NewItems[0] as string });
                }
                break;
            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                {
                    _kanbanColumnService.DeleteAsync((e.OldItems[0] as KanbanColumnViewModel).Id);
                }
                break;
        }
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
            KanbanColumn kanbanColumn = new KanbanColumn()
            {
                StatusType=value
            };
            KanbanColumnViewModels.Add(new KanbanColumnViewModel(kanbanColumn, Input, new List<KanbanCardViewModel>(), _kanbanCardService));
            InputVisibility=Visibility.Hidden;
        }
    }



    private readonly IKanbanObjectService<KanbanCard> _kanbanCardService;
    private readonly IKanbanObjectService<KanbanColumn> _kanbanColumnService;

    [RelayCommand]
    private void AddColumn()
    {
        InputVisibility=Visibility.Visible;
    }
}
