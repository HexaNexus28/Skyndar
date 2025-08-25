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
using MySql.Data.MySqlClient;
using WPF.ViewModels;
namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Prestation.xaml
    /// </summary>
    public partial class Prestation : UserControl
    {
        
        public Prestation()
        {
            InitializeComponent();           
            this.DataContext = new PrestationVM();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PrestationVM vm)
            {
                vm.AjouterPrestation();
            }
        }
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PrestationVM vm)
            {
                vm.SupprimerPrestation();
            }
        }
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PrestationVM vm)
            {
                vm.ModifierPrestation();
            }
        }
    }
}
