using Ap_project.MyModels;
using Ap_project.Myviewmodels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ap_project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public barnamerizi wop { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        wop = new barnamerizi();
        DataContext = wop;
    }

    public void Add_roeedad(object sender, RoutedEventArgs e)
    {
        wop.Additem();
    }

    public void delete_roeedad(object sender, RoutedEventArgs e)
    {
        wop.Deleteitem();
    }

    public void Edit_roeedad(object sender, RoutedEventArgs e)
    {
        wop.Edititem();
    }

    public void last_week_btn(object sender, RoutedEventArgs e)
    {
        wop.lastweek();
    }

    public void next_week_btn(object sender, RoutedEventArgs e)
    {
        wop.nextweek();
    }

    public void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListView currentListView)
        {
            wop.selecting = currentListView.SelectedItem as roeedad;



            foreach (var list in new[] { SundayListView, MondayListView, TuesdayListView,
                                   WednesdayListView, ThursdayListView, FridayListView,
                                   SaturdayListView })
            {
                if (list != currentListView)
                    list.SelectedItem = null;
            }
        }
    }
}