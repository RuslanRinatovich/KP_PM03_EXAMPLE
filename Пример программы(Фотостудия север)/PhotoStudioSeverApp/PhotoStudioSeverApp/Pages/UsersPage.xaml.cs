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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        int _itemcount = 0;
        List<User> data;
        public UsersPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // открытие редактирования товара
            // передача выбранного товара в AddGoodPage

        }

        void LoadData()
        {


            DataGridData.ItemsSource = null;
            //загрузка обновленных данных
            PhotoStudioSeverBDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            data = PhotoStudioSeverBDEntities.GetContext().Users.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
            DataGridData.ItemsSource = data;

            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_itemcount}";


            var roles = PhotoStudioSeverBDEntities.GetContext().Roles.OrderBy(p => p.Title).ToList();
            roles.Insert(0, new Role
            {
                Title = "Все типы"
            }
            );
            ComboUserType.ItemsSource = roles;
            ComboUserType.SelectedIndex = 0;
            _itemcount = data.Count;
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
            // открытие  AddGoodPage для добавления новой записи
            Manager.MainFrame.Navigate(new AddUserPage(null));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            var selected = (sender as Button).DataContext as User;

            int k = PhotoStudioSeverBDEntities.GetContext().Users.Count(p => p.RoleId == 1);
            if (selected.RoleId == 1 && k == 1)
            {
                MessageBox.Show("Ошибка удаления, невозможно удалить последнего администратора", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить  запись???",
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    // берем из списка удаляемых товаров один элемент

                    // проверка, есть ли у товара в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (selected.Bookings.Count > 0 || selected.Rewiews.Count > 0)
                        throw new Exception("Ошибка удаления, есть связанные записи");


                    PhotoStudioSeverBDEntities.GetContext().Users.Remove(selected);
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
        private void DataGridDataLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        // Поиск товаров конкретного производителя
        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            // получаем текущие данные из бд
            //var currentGoods = PhotoStudioSeverBDEntities.GetContext().Abonements.OrderBy(p => p.CategoryTrainer.Trainer.LastName).ToList();

            var currentData = PhotoStudioSeverBDEntities.GetContext().Users.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
            // выбор только тех товаров, которые принадлежат данному производителю
            if (ComboUserType.SelectedIndex > 0)
            {
                currentData = currentData.Where(p => p.RoleId == ((ComboUserType.SelectedItem) as Role).RoleId).ToList();
            }


            // выбор тех товаров, в названии которых есть поисковая строка
            currentData = currentData.Where(p => p.UserName.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
            (p.LastName != null && p.LastName.ToLower().Contains(TBoxSearch.Text.ToLower()))
            || (p.FirstName != null && p.FirstName.ToLower().Contains(TBoxSearch.Text.ToLower()))
            || (p.MiddleName != null && p.MiddleName.ToLower().Contains(TBoxSearch.Text.ToLower()))

            ).ToList();


            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentData = currentData.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ThenBy(p => p.MiddleName).ToList();
                if (ComboSort.SelectedIndex == 1)
                    currentData = currentData.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName).ThenByDescending(p => p.MiddleName).ToList();

            }
            // В качестве источника данных присваиваем список данных
            DataGridData.ItemsSource = currentData;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentData.Count} записей из {_itemcount}";
        }
        // сортировка товаров 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void DataGridData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddUserPage((sender as Button).DataContext as User));
        }






        private void ComboUserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnRewiews_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
