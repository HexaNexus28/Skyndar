using Microsoft.VisualBasic; 
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using WPF.Models;
using WPF.Services;


namespace WPF.ViewModels
{
    public class DisponibiliteVM : MainVM
    {

        private DateTime currentMonth = DateTime.Today;
        public string CurrentMonth => currentMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));

      
        private bool iscabinetchecked;
        public bool IsCabinetChecked
        {
            get { return iscabinetchecked; }
            set
            {
                iscabinetchecked = value;
                OnPropertyChanged(nameof(IsCabinetChecked));
                FiltrerCreneaux();
                OnPropertyChanged(nameof(DayCreneaux));
            }
        }
        private bool isvisiochecked;
        public bool IsVisioChecked
        {
            get { return isvisiochecked; }
            set
            {
                isvisiochecked = value;
                OnPropertyChanged(nameof(IsVisioChecked));
                FiltrerCreneaux();
                OnPropertyChanged(nameof(DayCreneaux)); 
            }
        }

        private int? selectedPrestationId { get; set; }
        public int SelectedPrestationId
        {
            get { return selectedPrestationId ??-1; }
            set
            {
                selectedPrestationId = value;
                OnPropertyChanged(nameof(SelectedPrestationId));
                DateTime Date = new (currentMonth.Year, currentMonth.Month, 1);
                CalendarDay DayInWeek = new (1, Date, Date.Day, true);

                if (selectedPrestationId == -1)
                {
                    GenererCreneaux(Prestations[0], DayInWeek);

                }
                else
                {
                    AfficherCreneaux(DayInWeek);
                }
            }



        }

        public ObservableCollection<CalendarDay> Days { get; set; }
        public ObservableCollection<CalendarDay> DaysInWeeks { get; set; }
        
        public ObservableCollection<Creneau> DayCreneaux { get; set; }
        public ObservableCollection<Prestation> Prestations { get; set; }
        public ICommand PreviousMonthCommand { get; set; }
        public ICommand NextMonthCommand { get; set; }
        public ICommand SelectDayCommand { get; set; }
        public ICommand AjouterCreneauCommand { get; set; }
        public ICommand SupprimerCreneauCommand { get; set; }

        readonly DatabaseService BDD = new();
        public int GetPrestationDureeById(int Id)
        {
            return Prestations.FirstOrDefault(p => p.Id == Id)?.Duree ?? 0;
        }
     
        public void FiltrerCreneaux()
        {
            

            if (IsCabinetChecked && !IsVisioChecked)
            {
                DayCreneaux.Clear();
                foreach (CalendarDay day in DaysInWeeks)
                {
                    List<Creneau> filtered = [.. day.DayCreneaux.Where(c => c.Cabinet == false)];
                    foreach (Creneau creneau in filtered)
                    {
                        day.DayCreneaux.Remove(creneau);
                    }
                   
                }
            }
            else if (!IsCabinetChecked && IsVisioChecked)
            {
                
                foreach (CalendarDay day in DaysInWeeks.ToList())
                {
                    List<Creneau> filtered = [.. day.DayCreneaux.Where(c => c.Cabinet == true)];
                    foreach (Creneau creneau in filtered)
                    {
                        day.DayCreneaux.Remove(creneau);
                    }
                   

                }
            }

                
        }

        public void PreviousMonthAction()
        {
            
            currentMonth = currentMonth.AddMonths(-1);

            DateTime Date = new(currentMonth.Year, currentMonth.Month, 1);
            CalendarDay DayInWeek = new(1, Date, Date.Day, true);
            OnPropertyChanged(nameof(CurrentMonth));

            DaysExistForCurrentMonth();
            LoadCalendar();
            DateTime firstOfMonth = new(currentMonth.Year, currentMonth.Month, 1);
            int dayId = BDD.GetOrInsertId(DayInWeek);
            CalendarDay dummyDay = new (dayId, firstOfMonth, 1, true);
            AfficherCreneaux(dummyDay); ;
        }
        
        public void NextMonthAction()
        {


            currentMonth = currentMonth.AddMonths(1);
            DateTime Date = new(currentMonth.Year, currentMonth.Month, 1);
            CalendarDay DayInWeek = new(1, Date, Date.Day, true);
            OnPropertyChanged(nameof(CurrentMonth));
            
            DaysExistForCurrentMonth();
            LoadCalendar();
            
            DateTime firstOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);

            int dayId = BDD.GetOrInsertId(DayInWeek);
            CalendarDay dummyDay = new (dayId, firstOfMonth, 1, true);
            AfficherCreneaux(dummyDay);
        }

        public void AjouterCreneauAction(CalendarDay day)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Voulez-vous ajouter un créneau ?", "Confirmation", buttons, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            string startTimeInput = Interaction.InputBox("Heure de début (HH:mm)", "Créneau", "14:00", -1, -1);

            if (!TimeSpan.TryParse(startTimeInput, out TimeSpan startTime))
            {
                MessageBox.Show("Format d'heure invalide. Utilisez HH:mm.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
                if (startTime < day.DayCreneaux.Last().HeureFin.Add(TimeSpan.FromMinutes(15)))
                {
                    MessageBox.Show("Ajouter 15 min au minumun au dernier créneau ");
                    return;
                }
            

           
            MessageBoxButton actions = MessageBoxButton.YesNo;
            MessageBoxResult resultat = MessageBox.Show("Est-ce en  Cabinet ? ", "Confirmation", actions, MessageBoxImage.Question);
            bool cabinet = (resultat == MessageBoxResult.Yes);

            int duree = GetPrestationDureeById(SelectedPrestationId);
            TimeSpan endTime = startTime.Add(TimeSpan.FromMinutes(duree));

            int id = day.DayCreneaux.Count > 0 ? day.DayCreneaux.Max(c => c.Id) + 1 : 1; 
            Creneau creneau = new(id, day.Id, SelectedPrestationId,startTime, endTime, cabinet);
            int newId = BDD.AddCreneau(creneau);
            creneau.Id = newId; 

            day.DayCreneaux.Add(creneau);

            MessageBox.Show($"Créneau ajouté : {startTime} - {endTime} ({(cabinet ? " Cabinet " : " Visio ")})");


        }
        public void SupprimerCreneauAction(Creneau creneau)
        {

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Voulez-vous supprimer ce créneau ?", "Confirmation", buttons, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            CalendarDay day = BDD.GetDayFromCreneauId(creneau.Id);
            ObservableCollection<Creneau> existingcreneaux = BDD.GetCreneauxForPrestation(SelectedPrestationId,day.Date,day.Date);
            if (existingcreneaux.Any())
            {
                foreach (Creneau cr in existingcreneaux)
                {
                    day.DayCreneaux.Add(cr);

                }

            }
           

            Creneau creneauToDelete =   day.DayCreneaux.FirstOrDefault(c=>c.Id == creneau.Id);
            if (creneauToDelete != null)
            {
                BDD.DeleteCreneau(creneauToDelete.Id);
                day.DayCreneaux.Remove(creneauToDelete);
                OnPropertyChanged(nameof(DaysInWeeks));
                MessageBox.Show($"Créneau supprimé : {creneauToDelete.HeureDebut} - {creneauToDelete.HeureFin} ({(creneauToDelete.Cabinet ? " Cabinet " : " Visio ")})");
            }
        }

        public void DaysExistForCurrentMonth()
        {

            DateTime firstOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            DateTime lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);

            for (DateTime date = firstOfMonth; date <= lastOfMonth; date = date.AddDays(1))
            {

                CalendarDay tempday = new (0, date, date.Day, true);
                int _= BDD.GetOrInsertId(tempday);
            }
           
        }
        public void LoadCalendar()
        {
            Days.Clear();

            DateTime FirstDay = new (currentMonth.Year, currentMonth.Month,1); 
            int OffsetDay = ((int)FirstDay.DayOfWeek - (int)DayOfWeek.Monday+7)%7;

            int DaysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);


            for (int i = 1; i < OffsetDay; i++)
            {
                DateTime PreMonthdate = new DateTime(currentMonth.Year, currentMonth.Month, 1).AddDays(-OffsetDay +i );

                CalendarDay PreMonthDay = new (i, PreMonthdate, PreMonthdate.Day, false);
                Days.Add(PreMonthDay);
                
            }

            for (int i = 1; i <= DaysInMonth; i++)
            {
                DateTime date = new (currentMonth.Year, currentMonth.Month, i );

                CalendarDay Day = new (i, date, i, true);
                Days.Add(Day);
              
            }


            int totalCells = 42;
            int remainingDays = totalCells - Days.Count;
            DateTime nextMonthDate = FirstDay.AddMonths(1);
            for (int i = 0; i < remainingDays; i++)
            {
                DateTime date = nextMonthDate.AddDays(i);
                
                CalendarDay NextMonthDay = new (i, date, date.Day, false);
                Days.Add(NextMonthDay);
             
            }

        }
        public void GenererCreneaux(Prestation prestation, CalendarDay DayInWeek)
        {
            DateTime Tdate = (DayInWeek != null) ? DayInWeek.Date : new DateTime(currentMonth.Year, currentMonth.Month, 1);

          
            int offset = ((int)Tdate.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime firstMonday = Tdate.AddDays(-offset);
            DateTime lastSunday = firstMonday.AddDays(6);
            ObservableCollection<Creneau> existingcreneaux = BDD.GetCreneauxForPrestation(prestation.Id, firstMonday,lastSunday);
            ObservableCollection<CalendarDay> existingdayweeks = BDD.GetDayInWeeks(DayInWeek.Date);
            DaysInWeeks.Clear();

            for (int j = 0; j < 7; j++)
            {
                DateTime jour = firstMonday.AddDays(j);
                CalendarDay jourInDB = existingdayweeks.FirstOrDefault(d => d.Date.Date == jour.Date);
                CalendarDay dayVM;

                if (jourInDB != null)
                {
                    dayVM = jourInDB;
                }
                else
                {
                    dayVM = new CalendarDay(0, jour, jour.Day, true);
                    int newId = BDD.GetOrInsertId(dayVM);
                    dayVM.Id = newId;
                }
                DaysInWeeks.Add(dayVM);
            }


            if (existingcreneaux.Any())
            {
                foreach (Creneau creneau in existingcreneaux)
                {
                    CalendarDay day = DaysInWeeks.FirstOrDefault(d => d.Id == creneau.DayId);

                    day?.DayCreneaux.Add(creneau);

                }

            }
            else
            {
                for (int i = 0; i < DaysInWeeks.Count; i++)
                {
                    CalendarDay day = DaysInWeeks[i];

                    if (!day.IsValid) continue;

                    TimeSpan startTime = new(8, 0, 0);

                    for (int j = 0; j < 3; j++)
                    {
                        _ = day.Date;

                        TimeSpan endTime = startTime.Add(TimeSpan.FromMinutes(prestation.Duree));

                        Creneau creneau = new(j, day.Id, prestation.Id, startTime, endTime, true);
                        int newCreId = BDD.AddCreneau(creneau); 

                        creneau.Id = newCreId; 
                        day.DayCreneaux.Add(creneau);

                        startTime = endTime.Add(TimeSpan.FromMinutes(15));

                    }
                }

            }

               
            }
        

        public void SelectDayAction(CalendarDay day)

        {
            AfficherCreneaux(day);
          
        }


        public void AfficherCreneaux(CalendarDay DayInWeek)

        {
            foreach (Prestation prestation in Prestations)
            {
                if (prestation.Id == SelectedPrestationId)
                {
                    GenererCreneaux(prestation, DayInWeek);
                    break;
                }
            }
        }


        public DisponibiliteVM()
        {
            if(Days == null)
            {
                Days = [];
            }
            if (DaysInWeeks == null)
            {
                DaysInWeeks = [];
            }
            if (DayCreneaux == null)
            {
                DayCreneaux = [];
            }

                
                LoadCalendar();
            Prestations = BDD.GetPrestations();
            
            PreviousMonthCommand = new RelayCommand(PreviousMonthAction);

            NextMonthCommand = new RelayCommand(NextMonthAction);

            SelectDayCommand = new RelayCommand((param) => {
                    if (param is CalendarDay day) SelectDayAction(day); 
                });


            AjouterCreneauCommand = new RelayCommand((param) => {

                if (param is CalendarDay day) AjouterCreneauAction(day);
            });

            SupprimerCreneauCommand = new RelayCommand((param) => {

                if (param is Creneau creneau) SupprimerCreneauAction(creneau);
            });


            DateTime DayInWeek = new(currentMonth.Year, currentMonth.Month, 1);

            
        }

    }
}
