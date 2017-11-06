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
        public void Initialize()
        {
            foreach (var i in database.GetCarList())
            {
                listBox.Items.Add(i);
            }
        }
        DBase database = DBase.GetInstance();
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
            Initialize();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            database.DeleteCar(listBox.SelectedValue.ToString());
            Initialize();
        }
    }
}
