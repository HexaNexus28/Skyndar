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
using WPF.ViewModels; // Assuming ViewModels namespace contains HistoriqueVM
namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Historique.xaml
    /// </summary>
    public partial class Historique : UserControl
    {
        public Historique()
        {
            InitializeComponent();
            this.DataContext = new HistoriqueVM();
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is HistoriqueVM vm)
            {
                vm.SupprimerRendezVous();
            }
        }
    }
}
