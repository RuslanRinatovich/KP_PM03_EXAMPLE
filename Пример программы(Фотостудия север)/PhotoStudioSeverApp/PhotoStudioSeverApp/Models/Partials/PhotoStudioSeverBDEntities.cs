using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioSeverApp.Models
{
    public partial class PhotoStudioSeverBDEntities: DbContext
    {
        private static PhotoStudioSeverBDEntities _context;
        public static PhotoStudioSeverBDEntities GetContext()
        {
            if (_context == null)
            {
                _context = new PhotoStudioSeverBDEntities();
            }
            return _context;
        }
    }
}
