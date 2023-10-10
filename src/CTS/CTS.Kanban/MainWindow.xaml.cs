using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CTS.Kanban;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        AppDbContext appDbContext = new AppDbContext();
        var kanbanCardService = new KanbanService<KanbanCard>(appDbContext);
        var kanbanColumnService = new KanbanService<KanbanColumn>(appDbContext);
        ServiceLocator.Register<IKanbanObjectService<KanbanCard>>(kanbanCardService);
        ServiceLocator.Register<IKanbanObjectService<KanbanColumn>>(kanbanColumnService);
        DataContext=new MainViewViewModel();
        InitializeComponent();
    }
}
