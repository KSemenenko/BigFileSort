using System.Windows;
using BigSort.ViewModel;

namespace BigSort.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new MainViewModel();
            this.DataContext = model;
        }
    }
}
