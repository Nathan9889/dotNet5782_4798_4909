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
using Model;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DisplayPackage.xaml
    /// </summary>
    public partial class DisplayPackage : Page
    {
        private MainWindow mainWindow;
        private Model.PL pL;
        Package Package;

       

        public DisplayPackage(MainWindow mainWindow, Model.PL pL)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.pL = pL;
        }


        public DisplayPackage(MainWindow mainWindow, Model.PL pL, Model.Package package)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.pL = pL;
            this.Package = package;
            MainGrid.DataContext = package;
            Package_Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            Package_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void Delete_Package_Click(object sender, RoutedEventArgs e)
        {
            pL.DeletePackage(Package.package.ID);
            this.NavigationService.GoBack();
        }
    }
}
