using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioSeverApp.Models
{
    public partial class TimeSheet
    {
        public string GetFullDateTime
        {
            get
            {


                return $"{Date.Day:00} {Date.ToString("MMMM")} {Time.Hours:00}:{Time.Minutes:00}";
            }
        }
    }
}
