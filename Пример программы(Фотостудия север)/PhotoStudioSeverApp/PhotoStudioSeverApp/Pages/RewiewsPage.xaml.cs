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
    /// Логика взаимодействия для RewiewsPage.xaml
    /// </summary>
    public partial class RewiewsPage : Page
    {
        int _itemcount = 0;
        public RewiewsPage()
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

                List<Rewiew> goods = PhotoStudioSeverBDEntities.GetContext().Rewiews.OrderBy(p => p.UserName).ToList();
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


        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного товара из таблицы
            //получаем все выделенные товары
            Rewiew selected = (sender as Button).DataContext as Rewiew;
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


                    PhotoStudioSeverBDEntities.GetContext().Rewiews.Remove(selected);
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
            var currentGoods = PhotoStudioSeverBDEntities.GetContext().Rewiews.OrderBy(p => p.UserName).ToList();
            // выбор только тех товаров, по определенному диапазону скидки

            currentGoods = currentGoods.Where(p => p.UserName.ToLower().Contains(TBoxSearch.Text.ToLower())
            || p.User.GetFIO.ToLower().Contains(TBoxSearch.Text.ToLower())
            || p.Room.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию цены
                if (ComboSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.Rate).ToList();
                // сортировка по убыванию цены
                if (ComboSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.Rate).ToList();
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

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            DialogHostMoreInformation.IsOpen = false;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //  если ни одного объекта не выделено, выходим
                if (DataGridGood.SelectedItem == null) return;
                // получаем выделенный объект
                Rewiew selected = DataGridGood.SelectedItem as Rewiew;


                //Trainer trainer = YogaFeatPilatesBDEntities.GetContext().Trainers.FirstOrDefault(p => p.Id == edu.TrainerId);
                //List<TimeTable> timeTables = YogaFeatPilatesBDEntities.GetContext().TimeTables.Where(p => p.CategoryTrainerId == edu.Id).ToList();
                //ListBoxTimeTable.ItemsSource = timeTables;
                //TbCategoryName.Text = edu.Category.Name;

                DialogHostMoreInformation.DataContext = selected;
                DialogHostMoreInformation.IsOpen = true;
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}