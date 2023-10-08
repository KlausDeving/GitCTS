using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GongSolutions.Wpf.DragDrop;

namespace CTS.Kanban;
public partial class KanbanColumnViewModel : ObservableObject, IDropTarget
{
    public KanbanColumnViewModel(string statusType, IEnumerable<KanbanCardViewModel> items, IKanbanCardService kanbanCardService)
    {
        Title=statusType;
        _kanbanCardService=kanbanCardService;
        Items=new ObservableCollection<KanbanCardViewModel>(items);
    }

    [ObservableProperty]
    private ObservableCollection<KanbanCardViewModel> _items = new();

    [ObservableProperty]
    private string _title = string.Empty;

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
        CreateTaskModalWindow createTaskModalWindow = new CreateTaskModalWindow(statuses);
        if(createTaskModalWindow.ShowDialog()==true)
        {
            Items.Add(new KanbanCardViewModel(_kanbanCardService.Create(createTaskModalWindow.Result), _kanbanCardService));
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
        var sourceItem = dropInfo.Data as KanbanCardViewModel;
        if(sourceItem!=null)
        {
            var kanbanItem = await _kanbanCardService.GetAsync(sourceItem.Id);
            // Remove the item from the source collection
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(sourceItem);


            kanbanItem.StatusType=Title;


            // Add the item to the target collection
            Items.Add(new KanbanCardViewModel(await _kanbanCardService.UpsertAsync(kanbanItem), _kanbanCardService));
        }
    }

    private readonly IKanbanCardService _kanbanCardService;
}
