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
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Disponibilite.xaml
    /// </summary>
    public partial class Disponibilite : UserControl
    {
        public Disponibilite()
        {
            InitializeComponent();
            this.DataContext = new DisponibiliteVM();

        }
    }
}
