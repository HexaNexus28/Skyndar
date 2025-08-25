using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.Windows.Input;
using WPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using WPF.Services;

namespace WPF.ViewModels
{
    public class HistoriqueVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<RendezVous> Historique { get; set; } = [];

        private readonly DatabaseService bdd = new();
        private RendezVous _selectedRendezVous;
        public RendezVous SelectedRendezVous
        {
            get => _selectedRendezVous;
            set
            {
                _selectedRendezVous = value;
                OnPropertyChanged();
            }
        }

        public HistoriqueVM()
        {
            Historique = bdd.GetRendezVous();
        }

        public void SupprimerRendezVous()
        {
            if (SelectedRendezVous != null)
            {
                bdd.DeleteRendezVous(SelectedRendezVous.Id);
                Historique.Remove(SelectedRendezVous);
                SelectedRendezVous = null;
            }
        }
        public void Rafraichir()
        {
            Historique.Clear();
            foreach (var RendezVous in bdd.GetRendezVous())
            {
                Historique.Add(RendezVous);
            }
        }
    }
}
