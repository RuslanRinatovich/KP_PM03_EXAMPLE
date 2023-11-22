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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoStudioSeverApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        int _itemcount = 0;
        public RoomsPage()
        {
            InitializeComponent();
            LoadComboBoxItems();
            LoadDataGrid();
        }

        void LoadDataGrid()
        {
            List<Room> goods = PhotoStudioSeverBDEntities.GetContext().Rooms.OrderBy(p => p.Title).ToList();
            DataGridGood.ItemsSource = goods;
            _itemcount = goods.Count;
            TextBlockCount.Text = $" Результат запроса: {goods.Count} записей из {goods.Count}";
        }

        void LoadComboBoxItems()
        {
            var categories = PhotoStudioSeverBDEntities.GetContext().Categories.OrderBy(p => p.Title).ToList();
            categories.Insert(0, new Category
            {
                Title = "Все виды"
            }
            );
            ComboCategory.ItemsSource = categories;
            ComboCategory.SelectedIndex = 0;

          
        }

        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CategoryPage());
        }

        private void BtnAges_Click(object sender, RoutedEventArgs e)
        {
          //  Manager.MainFrame.Navigate(new AgesPage());
        }

        private void BtnOrganizers_Click(object sender, RoutedEventArgs e)
        {
           // Manager.MainFrame.Navigate(new OrganizersPage());
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRoomPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRoomPage((sender as Button).DataContext as Room));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                PhotoStudioSeverBDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGridGood.ItemsSource = null;

                LoadDataGrid();
            }
        }


        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            //var currentGoods = DataBDEntities.GetContext().Abonements.OrderBy(p => p.CategoryTrainer.Trainer.LastName).ToList();

            var currentData = PhotoStudioSeverBDEntities.GetContext().Rooms.OrderBy(p => p.Title).ToList();
            // выбор только тех товаров, которые принадлежат данному производителю


            if (ComboCategory.SelectedIndex > 0)
            {
                int catId = Convert.ToInt32((ComboCategory.SelectedItem as Category).Id);
                List<Room> quests = new List<Room>();
                foreach (Room quest in currentData)
                {
                    List<RoomCategory> questCategories = quest.RoomCategories.ToList();

                    if (questCategories.Any(elem => elem.CategoryId == catId))
                        quests.Add(quest);
                }

                currentData = quests;
            }



            // выбор тех товаров, в названии которых есть поисковая строка
            currentData = currentData.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentData = currentData.OrderBy(p => p.Title).ToList();
                if (ComboSort.SelectedIndex == 1)
                    currentData = currentData.OrderByDescending(p => p.Title).ToList();
                if (ComboSort.SelectedIndex == 2)
                    currentData = currentData.OrderBy(p => p.GetRate).ToList();
                if (ComboSort.SelectedIndex == 3)
                    currentData = currentData.OrderByDescending(p => p.GetRate).ToList();
                // сортировка по убыванию цены
            }
            // В качестве источника данных присваиваем список данных
            DataGridGood.ItemsSource = currentData;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentData.Count} записей из {_itemcount}";
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboOrganizer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboAge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnTimeTable_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new TimeTablePage((sender as Button).DataContext as Room));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Room room = (sender as Button).DataContext as Room;
            room.SetPrevPhoto = 1;
            //MessageBox.Show(room.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(room.GetCurrentPhoto));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Room room = (sender as Button).DataContext as Room;
            room.SetNextPhoto = 1;
            // MessageBox.Show(room.GetCurrentPhoto);
            Image image = (sender as Button).Tag as Image;
            image.Source = new BitmapImage(new Uri(room.GetCurrentPhoto));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            Room selected = (sender as Button).DataContext as Room;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить информацию о зале, фотографии и ???",
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    //// проверка, есть ли у товара в таблице о продажах связанные записи
                    //// если да, то выбрасывается исключение и удаление прерывается
                    if (selected.Rewiews.Count > 0 || selected.TimeSheets.Count > 0)
                        throw new Exception("Есть зависимые записи о залах");

                    PhotoStudioSeverBDEntities.GetContext().RoomCategories.RemoveRange(selected.RoomCategories);
                    PhotoStudioSeverBDEntities.GetContext().Photos.RemoveRange(selected.Photos);
                    PhotoStudioSeverBDEntities.GetContext().Rooms.Remove(selected);
                    //сохраняем изменения
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    LoadDataGrid();
                    UpdateData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
