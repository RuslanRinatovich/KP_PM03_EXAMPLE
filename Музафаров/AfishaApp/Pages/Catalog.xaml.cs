using AfishaApp.Models;
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

namespace AfishaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Сatalog.xaml
    /// </summary>
    public partial class Сatalog : Page
    {
        int _itemcount = 0;
        public Сatalog()
        {
            InitializeComponent();
            // загрузка данных в combobox + добавление дополнительной строки
            var categories = AfishaBDEntities.GetContext().Categories.OrderBy(p => p.CategoryName).ToList();
            categories.Insert(0, new Category
            {
                CategoryName = "Все типы"
            }
            );
            ComboCategory.ItemsSource = categories;
            ComboCategory.SelectedIndex = 0;

            var statuses = AfishaBDEntities.GetContext().Status.OrderBy(p => p.StatusName).ToList();
            statuses.Insert(0, new Status
            {
                StatusName = "Все"
            }
            );
            ComboStatus.ItemsSource = statuses;
            ComboStatus.SelectedIndex = 0;
            // загрузка данных в listview сортируем по названию
            LViewGoods.ItemsSource = AfishaBDEntities.GetContext().EventTables.OrderBy(p => p.EventName).ToList();
            _itemcount = LViewGoods.Items.Count;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_itemcount}";
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //обновление данных после каждой активации окна
            if (Visibility == Visibility.Visible)
            {
                AfishaBDEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LViewGoods.ItemsSource = AfishaBDEntities.GetContext().EventTables.OrderBy(p => p.EventName).ToList();
            }
        }
        // Поиск товаров, которые содержат данную поисковую строку
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
            var currentGoods = AfishaBDEntities.GetContext().EventTables.OrderBy(p => p.EventName).ToList();
            // выбор только тех товаров, которые принадлежат данному производителю
            if (ComboCategory.SelectedIndex > 0)
                currentGoods = currentGoods.Where(p => p.CategoryId == (ComboCategory.SelectedItem as Category).CategoryId).ToList();
            if (ComboStatus.SelectedIndex > 0)
                currentGoods = currentGoods.Where(p => p.StatusId == (ComboStatus.SelectedItem as Status).StatusId).ToList();
            // выбор тех товаров, в названии которых есть поисковая строка
            currentGoods = currentGoods.Where(p => p.EventName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            // В качестве источника данных присваиваем список данных
            LViewGoods.ItemsSource = currentGoods;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentGoods.Count} записей из {_itemcount}";
        }
        // сортировка товаров 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

    }
}