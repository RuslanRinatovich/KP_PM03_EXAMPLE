using PhotoStudioSeverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhotoStudioSeverApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для TimeTableWindow.xaml
    /// </summary>
    public partial class TimeTableWindow : Window
    {
        public TimeSheet currentItem { get; private set; }
        Room room;


        public TimeTableWindow(TimeSheet p, Room g)
        {
            InitializeComponent();


            currentItem = p;
            //  currentItem.TrainerId = g.Id;
            room = g;

            DataContext = currentItem;

        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();


            if (DatePickerDate.SelectedDate is null)
                s.AppendLine("Не выбрана дата");
            if (TimePickerTime.SelectedTime is null)
                s.AppendLine("Укажите время");

            return s;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            //currentItem.CategoryId = Convert.ToInt32(ComboCategory.SelectedValue);
            currentItem.Date = Convert.ToDateTime(DatePickerDate.SelectedDate).Date;
            currentItem.Time = Convert.ToDateTime(TimePickerTime.SelectedTime).TimeOfDay;
            currentItem.Price = Convert.ToDouble(DoubleUpDownPrice.Value);
            currentItem.RoomId = room.Id;
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            DoubleUpDownPrice.Value = currentItem.Price;
            if (currentItem.Id == 0)
            {
                DatePickerDate.SelectedDate = DateTime.Today;
                TimePickerTime.SelectedTime = DateTime.Today;
            }
            else
            {
                DatePickerDate.SelectedDate = Convert.ToDateTime(currentItem.Date);
                TimePickerTime.SelectedTime = Convert.ToDateTime(currentItem.Time.ToString());
            }

        }
    }
}