using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GongSolutions.Wpf.DragDrop;

namespace CTS.Kanban;
public partial class KanbanColumnViewModel : ObservableObject, IDropTarget
{
    public KanbanColumnViewModel(
        KanbanColumn kanbanColumn)
    {
        _kanbanColumn=kanbanColumn;
        _kanbanCardService=ServiceLocator.GetService<IKanbanObjectService<KanbanCard>>();
        _kanbanColumnService=ServiceLocator.GetService<IKanbanObjectService<KanbanColumn>>();
        Title=_kanbanColumn.Title;
        Items=new ObservableCollection<KanbanCardViewModel>(kanbanColumn.KanbanCards.Select(x => new KanbanCardViewModel(x)));
    }

    public int Id
    {
        get => _kanbanColumn.Id;
    }


    [ObservableProperty]
    private ObservableCollection<KanbanCardViewModel> _items = new();

    public string Title
    {
        get => _kanbanColumn.Title;
        set
        {
            if(!string.IsNullOrEmpty(value)&&_kanbanColumn.Title!=value)
            {
                _kanbanColumn.Title=value;
                _kanbanColumnService.UpsertAsync(_kanbanColumn);
            }
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private KanbanCardViewModel? _selectedCard;


    [RelayCommand]
    private async Task RemoveTask()
    {
        if(SelectedCard is null)
        {
            return;
        }
        await _kanbanCardService.DeleteAsync(SelectedCard.Id);
        Items.Remove(SelectedCard);
    }

    [RelayCommand]
    private async void CreateTask()
    {
        var statuses = _kanbanCardService.GetAsync().Result.Select(x => x.StatusType).ToList();
        if(!statuses.Contains(Title))
            statuses.Add(Title);
        CreateTaskModalWindow createTaskModalWindow = new CreateTaskModalWindow(_kanbanColumn, statuses);
        if(createTaskModalWindow.ShowDialog()==true)
        {
            Items.Add(new KanbanCardViewModel(_kanbanCardService.Create(createTaskModalWindow.Result)));
        }
    }

    public void DragOver(IDropInfo dropInfo)
    {
        if(dropInfo.Data is KanbanCardViewModel sourceItem)
        {
            if(sourceItem!=null)
            {
                dropInfo.DropTargetAdorner=DropTargetAdorners.Highlight;
                dropInfo.Effects=DragDropEffects.Move;
            }
        }
    }

    public async void Drop(IDropInfo dropInfo)
    {
        if(dropInfo.Data is KanbanCardViewModel sourceItem)
        {
            var kanbanItem = await _kanbanCardService.GetAsync(sourceItem.Id);
            // Remove the item from the source collection
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(sourceItem);

            kanbanItem.StatusType=Title;

            // Add the item to the target collection
            Items.Add(sourceItem);
            _kanbanColumn.KanbanCards.Add(kanbanItem);
            await _kanbanColumnService.UpsertAsync(_kanbanColumn);
        }
    }

    private readonly KanbanColumn _kanbanColumn;
    private readonly IKanbanObjectService<KanbanCard> _kanbanCardService;
    private readonly IKanbanObjectService<KanbanColumn> _kanbanColumnService;
}
