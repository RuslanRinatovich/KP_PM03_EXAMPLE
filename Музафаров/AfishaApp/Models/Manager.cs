using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AfishaApp.Models
{
    public class Manager
    {
        public static Frame MainFrame { get; set; }
    }
}

//private static AfishaBDEntities _context;

//public static AfishaBDEntities GetContext()
//{
//    if (_context == null)
//    {
//        _context = new AfishaBDEntities();
//    }
//    return _context;
//}

//public string GetPhoto
//{
//    get
//    {
//        if (Photo is null)
//            return null;
//        return Directory.GetCurrentDirectory() + @"\Images\" + Photo.Trim();
//    }
//}
///// <summary>
///// Задает цвет фона товара
///// </summary>
//public string GetColor
//{
//    get
//    {
//        return Status.Color;
//    }
//}
///// <summary>
///// Текстовое представление активности товара
///// </summary>
//public string GetStatus
//{
//    get
//    {
//        return $"Состояние: {Status.StatusName}";
//    }
//}
