using PhotoStudioSeverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        int _itemcount = 0;
        public BookingPage()
        {
            InitializeComponent();
        }



        void LoadData()
        {
            try
            {
                DataGridGood.ItemsSource = null;
                //загрузка обновленных данных
                PhotoStudioSeverBDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<Booking> goods = PhotoStudioSeverBDEntities.GetContext().Bookings.OrderBy(p => p.TimeSheet.Date).ThenBy(p => p.TimeSheet.Time).ToList();
                DataGridGood.ItemsSource = goods;
                _itemcount = goods.Count;
                TextBlockCount.Text = $" Результат запроса: {goods.Count} записей из {goods.Count}";
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }


        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            //try
            //{


            //    OrganizerWindow window = new OrganizerWindow(new Organizer());
            //    if (window.ShowDialog() == true)
            //    {
            //        PhotoStudioSeverBDEntities.GetContext().Organizers.Add(window.currentItem);
            //        PhotoStudioSeverBDEntities.GetContext().SaveChanges();
            //        LoadData();
            //        MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка");
            //}
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            Booking selected = (sender as Button).DataContext as Booking;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись???",
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    // проверка, есть ли у товара в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается


                    PhotoStudioSeverBDEntities.GetContext().Bookings.Remove(selected);
                    //сохраняем изменения
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // отображение номеров строк в DataGrid
        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        // Поиск товаров конкретного производителя
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            var currentGoods = PhotoStudioSeverBDEntities.GetContext().Bookings.OrderBy(p => p.TimeSheet.Date).ThenBy(p => p.TimeSheet.Time).ToList();
            // выбор только тех товаров, по определенному диапазону скидки

            currentGoods = currentGoods.Where(p => p.UserInfo.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
            p.TimeSheet.Room.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.TimeSheet.Date).ThenBy(p => p.TimeSheet.Time).ToList();
                // сортировка по убыванию цены
                if (ComboSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.TimeSheet.Date).ThenByDescending(p => p.TimeSheet.Time).ToList();
            }
            // В качестве источника данных присваиваем список данных
            DataGridGood.ItemsSource = currentGoods;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentGoods.Count} записей из {_itemcount}";
        }
        // сортировка товаров 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    //  если ни одного объекта не выделено, выходим
            //    if (DataGridGood.SelectedItem == null) return;
            //    // получаем выделенный объект
            //    Organizer selected = DataGridGood.SelectedItem as Organizer;

            //    OrganizerWindow window = new OrganizerWindow(selected);


            //    if (window.ShowDialog() == true)
            //    {
            //        if (window.currentItem != null)
            //        {
            //            PhotoStudioSeverBDEntities.GetContext().Entry(window.currentItem).State = EntityState.Modified;
            //            PhotoStudioSeverBDEntities.GetContext().SaveChanges();
            //            LoadData();
            //            MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            //        }
            //    }
            //    else
            //    {
            //        PhotoStudioSeverBDEntities.GetContext().Entry(window.currentItem).Reload();
            //        LoadData();
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка");
            //}
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Booking selected = (sender as ToggleButton).DataContext as Booking;

            if ((sender as ToggleButton).IsChecked == true)
            {
                selected.Payed = true;
                PhotoStudioSeverBDEntities.GetContext().SaveChanges();

            }
            else
            {
                selected.Payed = false;
                PhotoStudioSeverBDEntities.GetContext().SaveChanges();
            }

        }
    }
}