using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioSeverApp.Models
{
    public partial class Photo
    {
        public string GetPhoto
        {
            get
            {
                if (Photo1 is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\" + Photo1.Trim();
            }
        }
    }
}
