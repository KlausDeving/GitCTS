using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CTS.Kanban;

public partial class KanbanGenericViewModel<T> : ObservableObject where T : class, IKanbanDomainModel
{
    public IKanbanObjectService<T> KanbanObjectService;
    private T _domainObject;

    public KanbanGenericViewModel(T domainObject, IKanbanObjectService<T> kanbanObjectService)
    {
        KanbanObjectService=kanbanObjectService;
        _domainObject=domainObject;
    }

    public int Id { get => _domainObject.Id; }
}

public partial class KanbanCardViewModel : ObservableObject
{
    public KanbanCardViewModel(KanbanCard kanbanCard)
    {
        _kanbanCard=kanbanCard;
        _kanbanCardService=ServiceLocator.GetService<IKanbanObjectService<KanbanCard>>();
        Title=kanbanCard.Title;
        Description=kanbanCard.Description;
    }

    public int Id { get => _kanbanCard.Id; }
    public string StatusType
    {
        get => _kanbanCard.StatusType; set
        {
            _kanbanCard.StatusType=value;
            OnPropertyChanged();
            _kanbanCardService.UpsertAsync(_kanbanCard);
        }
    }

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;
    private readonly KanbanCard _kanbanCard;
    private readonly IKanbanObjectService<KanbanCard> _kanbanCardService;

    [RelayCommand]
    private void Edit()
    {
        CreateTaskModalWindow createTaskModalWindow = new CreateTaskModalWindow(_kanbanCardService.GetAsync().Result.Select(x => x.StatusType).ToList(), _kanbanCard);

        if(createTaskModalWindow.ShowDialog()==true)
        {
            _kanbanCard.Title=createTaskModalWindow.Result.Title;
            _kanbanCard.Description=createTaskModalWindow.Result.Description;
            _kanbanCard.StatusType=createTaskModalWindow.Result.StatusType;
            Title=_kanbanCard.Title;
            Description=_kanbanCard.Description;
            _kanbanCardService.UpsertAsync(_kanbanCard);
        }
    }
}