using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
   public class RendezVous
    {
        public int Id {get; set;}
        public User User { get; set; }
        public Prestation Prestation { get; set; }
        public Creneau Creneau { get; set; }
        public CalendarDay CalendarDay { get; set; }
        public RendezVous (User user, Creneau creneau, CalendarDay calendarDay, Prestation prestation)
        {
            this.User = user;
            this.Creneau = creneau;
            this.CalendarDay = calendarDay;
            this.Prestation = prestation;
        }
    }
}
