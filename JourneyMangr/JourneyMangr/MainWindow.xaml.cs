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

namespace JourneyMangr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBase database = DBase.GetInstance();
        public MainWindow()
        {
            InitializeComponent();
           
             foreach (var i in database.GetCarList())
             {
                 comboBox.Items.Add(i);
             }
          
        }

        

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.DataContext = database.GetCarData(comboBox.SelectedValue.ToString());
        }
    }
}
