using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CTS.Kanban;
public partial class KanbanCardViewModel : ObservableObject
{
    public KanbanCardViewModel(KanbanCard kanbanCard, IKanbanCardService kanbanService)
    {
        _kanbanCard=kanbanCard;
        _kanbanService=kanbanService;
        Title=kanbanCard.Title;
        Description=kanbanCard.Description;
    }

    public int Id { get => _kanbanCard.Id; }
    public string StatusType { get => _kanbanCard.StatusType; }

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;
    private readonly KanbanCard _kanbanCard;
    private readonly IKanbanCardService _kanbanService;

    [RelayCommand]
    private void Edit()
    {
        CreateTaskModalWindow createTaskModalWindow = new CreateTaskModalWindow(_kanbanService.GetAsync().Result.Select(x => x.StatusType).ToList(), _kanbanCard);

        if(createTaskModalWindow.ShowDialog()==true)
        {
            _kanbanCard.Title=createTaskModalWindow.Result.Title;
            _kanbanCard.Description=createTaskModalWindow.Result.Description;
            _kanbanCard.StatusType=createTaskModalWindow.Result.StatusType;
            Title=_kanbanCard.Title;
            Description=_kanbanCard.Description;
            _kanbanService.UpsertAsync(_kanbanCard);
        }
    }
}