using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class CalendarDay(int id, DateTime date, int daynumber, bool isvalid = true)
    {
        public int Id { get; set; } = id;
        public DateTime Date { get; set; } = date;
        public int DayNumber { get; set; } = daynumber; 
        public bool IsValid { get; set; } = isvalid;
        public ObservableCollection<Creneau> DayCreneaux { get; set; } = [];

    }
}
