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
    private readonly IKanbanCardService _kanbanCardService;
    [ObservableProperty]
    private KanbanBoardViewModel _kanbanBoardViewModel;

    public MainViewViewModel(IKanbanCardService kanbanCardService)
    {
        _kanbanCardService=kanbanCardService;
        KanbanBoardViewModel=new KanbanBoardViewModel(kanbanCardService);
        RetrieveKanban();
    }
    public MainViewViewModel()
    {

        _kanbanCardService=new KanbanCardService(new AppDbContext());
        KanbanBoardViewModel=new KanbanBoardViewModel(_kanbanCardService);
        RetrieveKanban();
    }

    private async Task RetrieveKanban()
    {
        var cards = await _kanbanCardService.GetAsync();
        KanbanBoardViewModel.KanbanColumnViewModels=new ObservableCollection<KanbanColumnViewModel>();
        var cardViewModels = new List<KanbanCardViewModel>();
        foreach(var card in cards)
        {
            cardViewModels.Add(new KanbanCardViewModel(card, _kanbanCardService));
        }
        var types = cards.Select(x => x.StatusType).Distinct();
        foreach(var type in types)
        {
            KanbanBoardViewModel.KanbanColumnViewModels.Add(new KanbanColumnViewModel(type, cardViewModels.Where(x => x.StatusType==type), _kanbanCardService));
        }
    }
}

