using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoStudioSeverApp.Models
{
    public partial class User
    {
        public string GetFIO
        {
            get
            {

                return $"{FirstName} {LastName}";
            }
        }
        public Visibility GetVisibility
        {
            get
            {
                if (RoleId == 3)
                    return Visibility.Visible;
                return Visibility.Hidden;
            }
        }
    }
}
