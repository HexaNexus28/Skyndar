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
using WPF.Services;

namespace WPF.ViewModels
{
    class PrestationVM
    {
        public ObservableCollection<Prestation> Prestations { get; set; }
        public int Id { get; set; } 
        public string Titre { get; set; }
        public int Duree { get; set; }
        public double Tarif { get; set; }
        public string Description { get; set; }
        public Prestation Select { get; set; }

        private readonly DatabaseService bdd = new();
        public PrestationVM()
        {
            bdd = new DatabaseService();
            Prestations = bdd.GetPrestations();
        }

        public void AjouterPrestation()
        {
            if (!string.IsNullOrWhiteSpace(Titre) &&
                Duree > 0 &&
                Tarif > 0 &&
                !string.IsNullOrWhiteSpace(Description))
            {
                Prestation prestation = new(0, Titre, Duree, Description, Tarif);

                bdd.AddPrestation(prestation);

                var nouvellePrestations = bdd.GetPrestations();
                var presta = nouvellePrestations.LastOrDefault();

                if(presta is not null)
                {
                    Prestations.Add(presta);
                }
            }
        }

        public void ModifierPrestation()
        {
            if (Select != null)
            {
                Select.Titre = Titre;
                Select.Duree = Duree;
                Select.Tarif = Tarif;
                Select.Description = Description;

                bdd.UpdatePrestation(Select);
            }
        }
        public void SupprimerPrestation()
        {
            if (Select != null)
            {
                bdd.DeletePrestation(Select.Id);
                Prestations.Remove(Select);
                Select = null;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
