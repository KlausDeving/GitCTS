using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

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
        var cards = (await _kanbanCardService.GetAsync()).ToList();
        var columns = (await _kanbanColumnService.GetAsync()).ToList();
        KanbanBoardViewModel.KanbanColumnViewModels=new ObservableCollection<KanbanColumnViewModel>(columns.Select(x => new KanbanColumnViewModel(x)));
    }
}

