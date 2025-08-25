using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
   public class Admin :User
    {
        private string Password { get; set; }
        public Admin(int id, string username, string email, DateTime createdAt, string password) : base(id, username, email, createdAt)
        {
            Password = password;
        }
        public void AjouterPrestation(Prestation prestation)
        {
            // Code pour ajouter une prestation
        }
        public void AjouterCreneau(Creneau creneau)
        {
            // Code pour ajouter un créneau
        }

    }
    
}
