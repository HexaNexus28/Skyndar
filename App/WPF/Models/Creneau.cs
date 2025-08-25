using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF.Models
{
    public class Creneau
    {
        public int Id { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public bool Cabinet { get; set; }
        public int DayId { get; set; }
        public int PrestationId { get; set; }

        public Creneau(int id, int dayId, int prestationId, TimeSpan heureDebut, TimeSpan heureFin, bool cabinet)
        {
            Id = id;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
            Cabinet = cabinet;
            DayId = dayId;
            PrestationId = prestationId;
        }
    }
}
