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
using System.Windows.Shapes;

namespace JourneyMangr
{
    /// <summary>
    /// Interaction logic for carInput.xaml
    /// </summary>
    public partial class carInput : Window
    {
      
        DBase database = DBase.GetInstance();
        public void Initialize()
        {
            foreach (var i in database.GetCarList())
            {
                listBox.Items.Add(i);
            }

        }
        
        public carInput()
        {
            InitializeComponent();
            Initialize();
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            main.Show();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
                dataGrid_Copy.DataContext = database.GetCarData(listBox.SelectedValue.ToString());
         
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            database.AddCar(txtNev.Text,Convert.ToInt32(txtCcm.Text),txtFuelType.Text);
            listBox.Items.Clear();
            Initialize();
            
        }
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            database.ExportToExcel(database.GetCarData(listBox.SelectedItem.ToString()), "");

        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            database.DeleteCar(listBox.SelectedValue.ToString());
            listBox.SelectionChanged -= listBox_SelectionChanged;
            
            listBox.Items.Clear();
            Initialize();
            listBox.SelectionChanged += listBox_SelectionChanged;
            if (listBox.Items.Count>=1)
            {
                listBox.SelectedIndex = 0;
                dataGrid_Copy.DataContext = database.GetCarData(listBox.SelectedValue.ToString());

            }
        }
    }
}
