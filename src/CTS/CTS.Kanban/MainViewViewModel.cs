using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CTS.Kanban;
public partial class MainViewViewModel : ObservableObject
{
    private readonly IKanbanObjectService<KanbanCard> _kanbanCardService;
    private readonly IKanbanObjectService<KanbanColumn> _kanbanColumnService;
    [ObservableProperty]
    private KanbanBoardViewModel _kanbanBoardViewModel;

    public MainViewViewModel()
    {
        _kanbanCardService=ServiceLocator.GetService<IKanbanObjectService<KanbanCard>>();
        _kanbanColumnService=ServiceLocator.GetService<IKanbanObjectService<KanbanColumn>>();
        KanbanBoardViewModel=new KanbanBoardViewModel();
        RetrieveKanban();
    }


    private async Task RetrieveKanban()
    {
        var cards = await _kanbanCardService.GetAsync();
        var columns = await _kanbanColumnService.GetAsync();
        KanbanBoardViewModel.KanbanColumnViewModels=new ObservableCollection<KanbanColumnViewModel>(columns.Select(x => new KanbanColumnViewModel(x)));
    }
}

