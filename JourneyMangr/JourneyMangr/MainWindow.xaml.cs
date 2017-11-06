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
        public void Initialization()
        {
            foreach (var i in database.GetCarList())
            {
                comboBox.Items.Add(i);
            }
           
        }
        DBase database = DBase.GetInstance();
        public MainWindow()
        {
            InitializeComponent();

            Initialization();
         
        }

        

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.DataContext = database.GetCarData(comboBox.SelectedValue.ToString());
            List<CarData> d = database.GetCarDataList(comboBox.SelectedValue.ToString());
            futottkm_text.Text = d[d.Count-1].futottkm.ToString();
            kmallas_text.Text = d[d.Count-1].kmallas.ToString();
            fogyasztas_text.Text = d[d.Count-1].fogyasztas.ToString();
            szerviz_text.Text = d[d.Count-1].szerviz.ToString();
            ar_text.Text = d[d.Count-1].ar.ToString();
        }
    }
}
