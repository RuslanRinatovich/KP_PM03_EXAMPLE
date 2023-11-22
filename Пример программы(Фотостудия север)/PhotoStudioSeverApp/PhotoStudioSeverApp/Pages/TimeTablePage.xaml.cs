using PhotoStudioSeverApp.Models;
using PhotoStudioSeverApp.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoStudioSeverApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {
        List<TimeSheet> timeSheets = new List<TimeSheet>();
        public TimeTablePage(Room room)
        {
            InitializeComponent();
            LoadData(room);

        }
        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        // загрузка данных в DataGrid и ComboBox
        void LoadData(Room room)
        {
            timeSheets = PhotoStudioSeverBDEntities.GetContext().TimeSheets.Where(p => p.RoomId == room.Id).OrderBy(p => p.Date).ThenBy(p => p.Time).ToList();
            DtData.ItemsSource = timeSheets;
            ComboRoom.ItemsSource = PhotoStudioSeverBDEntities.GetContext().Rooms.OrderBy(p => p.Title).ToList(); ;
            ComboRoom.SelectedIndex = 0;
            ComboRoom.SelectedValue = room.Id;
            CardRoom.DataContext = room;
        }
        // фильтрация продаж по товару
        private void ComboGoodsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboRoom.SelectedIndex >= 0)
            {
                int roomId = Convert.ToInt32(ComboRoom.SelectedValue);
                timeSheets = PhotoStudioSeverBDEntities.GetContext().TimeSheets.Where(p => p.RoomId == roomId).OrderBy(p => p.Date).ThenBy(p => p.Time).ToList();
                DtData.ItemsSource = timeSheets;
                CardRoom.DataContext = ComboRoom.SelectedItem;
                Room room = (ComboRoom.SelectedItem) as Room;
                ImagePhoto.Source = new BitmapImage(new Uri(room.GetCurrentPhoto));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Room g = ComboRoom.SelectedItem as Room;
                TimeTableWindow window = new TimeTableWindow(new TimeSheet(), g);
                if (window.ShowDialog() == true)
                {
                    PhotoStudioSeverBDEntities.GetContext().TimeSheets.Add(window.currentItem);
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();

                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData(g);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }




        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSheet selected = (sender as Button).DataContext as TimeSheet;
                // Room g = ComboRoom.SelectedItem as Room;
                Room g = ComboRoom.SelectedItem as Room;
                // если ни одного объекта не выделено, выходим
                //if (DtData.SelectedItem == null) return;
                //// получаем выделенный объект
                //TimeSheet selected = DtData.SelectedItem as TimeSheet;

                //    double k = selected.Count;

                TimeTableWindow window = new TimeTableWindow(selected, g);

                if (window.ShowDialog() == true)
                {
                    selected = PhotoStudioSeverBDEntities.GetContext().TimeSheets.Find(window.currentItem.Id);
                    // получаем измененный объект
                    if (selected != null)
                    {

                        PhotoStudioSeverBDEntities.GetContext().Entry(selected).State = EntityState.Modified;
                        PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                        // LoadData(g);

                        MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(g);
                        //ComboGoods.SelectedIndex = -1;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Room room = (sender as Button).DataContext as Room;
            room.SetPrevPhoto = 1;
           
            ImagePhoto.Source = new BitmapImage(new Uri(room.GetCurrentPhoto));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Room room = (sender as Button).DataContext as Room;
            room.SetNextPhoto = 1;
         
            ImagePhoto.Source = new BitmapImage(new Uri(room.GetCurrentPhoto));
        }

    



        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // удаление выбранного товара из таблицы
                //получаем все выделенные товары
                TimeSheet selected = (sender as Button).DataContext as TimeSheet;
                // Room g = ComboRoom.SelectedItem as Room;
                Room g = ComboRoom.SelectedItem as Room;

                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    

                    if (selected.Bookings.Count > 0)
                    {
                        MessageBox.Show("Ошибка удаления, есть связанные записи", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    PhotoStudioSeverBDEntities.GetContext().TimeSheets.Remove(selected);
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();


                    LoadData(g);
                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}

