using Microsoft.Win32;
using PhotoStudioSeverApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddRoomPage.xaml
    /// </summary>
    public partial class AddRoomPage : Page
    {
        // текущий товар
        private Room _currentItem = new Room();
        // путь к файлу
       
        // текущая папка приложения
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";

        List<RoomCategory> currentItems = new List<RoomCategory>();
        class ComboBoxTypeItem
        {
            public int Id { get; set; }

            public int RoomCategoryId { get; set; }
            public string Title { get; set; }
            public bool Selected { get; set; }
        }

        List<ComboBoxTypeItem> categoriesItems = new List<ComboBoxTypeItem>();
        private string GetComboBoxItemContent(List<ComboBoxTypeItem> items)
        {
            List<string> s = new List<string>();

            foreach (ComboBoxTypeItem item in items)
            {
                if (item.Selected == true)
                {
                    s.Add(item.Title);

                }
            }
            return String.Join(", ", s);
        }
        // загрузка 

        private void LoadRooms()
        {
            categoriesItems.Clear();
            List<RoomCategory> xlist = PhotoStudioSeverBDEntities.GetContext().RoomCategories.Where(i => i.QuestId == _currentItem.Id).ToList();
            List<Category> items = PhotoStudioSeverBDEntities.GetContext().Categories.ToList();

            List<int> datas = new List<int>();
            foreach (RoomCategory c in xlist)
            {
                datas.Add(c.CategoryId);
                currentItems.Add(c);
            }
            //  MessageBox.Show(currentItems.Count.ToString());


            foreach (Category item in items)
            {
                if (datas.Contains(item.Id))
                {
                    categoriesItems.Add(new ComboBoxTypeItem
                    {
                        Id = item.Id,

                        Title = item.Title,
                        Selected = true
                    });

                }
                else
                    categoriesItems.Add(new ComboBoxTypeItem
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Selected = false
                    });
            }
            ComboCategories.ItemsSource = null;
            ComboCategories.ItemsSource = categoriesItems;
       
            // производители товаров

        }
        //сохраytybt
        void SaveRooms()
        {
            List<RoomCategory> saveItems = new List<RoomCategory>();

            foreach (ComboBoxTypeItem item in categoriesItems)
            {
                if (item.Selected == true)
                {
                    saveItems.Add(new RoomCategory
                    {
                        QuestId = _currentItem.Id,
                        CategoryId = item.Id,
                    }); ;
                }
            }
            // MessageBox.Show(currentItems.Count.ToString());
            // MessageBox.Show(saveItems.Count.ToString());
            List<RoomCategory> delItems = new List<RoomCategory>();
            List<RoomCategory> addItems = new List<RoomCategory>();

            foreach (RoomCategory x in currentItems)
            {
                bool b = false;
                foreach (RoomCategory y in saveItems)
                {
                    if ((y.CategoryId == x.CategoryId) && (y.QuestId == x.QuestId))
                    {
                        b = true;
                        break;
                    }

                }
                if (!b)
                    delItems.Add(x);
            }

            foreach (RoomCategory x in saveItems)
            {
                bool b = false;
                foreach (RoomCategory y in currentItems)
                {
                    if ((y.CategoryId == x.CategoryId) && (y.QuestId == x.QuestId))
                    {
                        b = true;
                        break;
                    }

                }
                if (!b)
                    addItems.Add(x);
            }

            PhotoStudioSeverBDEntities.GetContext().RoomCategories.RemoveRange(delItems);
            PhotoStudioSeverBDEntities.GetContext().RoomCategories.AddRange(addItems);
            PhotoStudioSeverBDEntities.GetContext().SaveChanges();
        }



        public AddRoomPage(Room selected)
        {

            InitializeComponent();
            LoadAndInitData(selected);
        }

        void LoadAndInitData(Room selected)
        {
            BtnLoad.Visibility = Visibility.Collapsed;

            if (selected != null)
            {
                _currentItem = selected;
                BtnLoad.Visibility = Visibility.Visible;
                if (selected.Photos.Count > 0)
                    {
                    ImageCurrentPhoto.Source = new BitmapImage(new Uri(selected.Photos.ToList()[0].GetPhoto)); }
                else
                {
                    ImageCurrentPhoto.Source = null;
                }


            }
            DataContext = _currentItem;
            LoadRooms();
        

        }

        /// <summary>
        /// Проверка полей ввод на корректыне данные
        /// </summary>
        /// <returns>текст StringBuilder содержащий ошибки, если они есть</returns>
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            // проверка полей на содержимое
            if (string.IsNullOrWhiteSpace(_currentItem.Title))
                s.AppendLine("Поле название пустое");
            if (string.IsNullOrWhiteSpace(_currentItem.Description))
                s.AppendLine("Поле описание пустое");

            return s;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            try { 
            // проверка полей прошла успешно
            // если товар новый, то его ID == 0
          
            if (_currentItem.Id == 0)
            {
              

                PhotoStudioSeverBDEntities.GetContext().Rooms.Add(_currentItem);
            }
           
            PhotoStudioSeverBDEntities.GetContext().SaveChanges();  // Сохраняем изменения в БД
            SaveRooms();
            MessageBox.Show("Запись Изменена");
                BtnLoad.Visibility = Visibility.Visible;
                // Manager.MainFrame.GoBack();  // Возвращаемся на предыдущую форму
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
}    // загрузка фото 
        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    FileInfo fileInfo = new FileInfo(op.FileName);

                    string photo = ChangePhotoName(op.SafeFileName);
                    // путь куда нужно скопировать файл
                    string dest = _currentDirectory + photo;
                    File.Copy(op.FileName, dest);
                    Photo newPhoto = new Photo();
                    newPhoto.RoomId = _currentItem.Id;
                    newPhoto.Photo1 = photo;
                    PhotoStudioSeverBDEntities.GetContext().Photos.Add(newPhoto);
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                    _currentItem.ReloadPhotos = 1;
                    //PhotoStudioSeverBDEntities.GetContext().Entry(_currentItem).Reload();
                   // ListBoxPhotos.ItemsSource = null;
                    ListBoxPhotos.Items.Refresh();



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        //подбор имени файла
        string ChangePhotoName(string _photoName)
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)


        {
            ListBoxItem selectedItem = (ListBoxItem)ListBoxPhotos.ItemContainerGenerator.
                              ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;

            Photo deletedPhoto = (sender as Button).DataContext as Photo;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить изображение???",
               "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    // проверка, есть ли у товара в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается

                    int k = ListBoxPhotos.SelectedIndex;
                    PhotoStudioSeverBDEntities.GetContext().Photos.Remove(deletedPhoto);
                    //сохраняем изменения
                    PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Изображение удалено");
                    _currentItem.ReloadPhotos = 1;
                    //PhotoStudioSeverBDEntities.GetContext().Entry(_currentItem).Reload();
                    // ListBoxPhotos.ItemsSource = null;
                    ListBoxPhotos.Items.Refresh();
                    if (k > 0) k--;

                    ListBoxPhotos.SelectedIndex = k;
                    if (k == 0)
                        ImageCurrentPhoto.Source = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ListBoxPhotos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxPhotos.SelectedItems.Count == 0)
                return;

            ImageCurrentPhoto.Source = new BitmapImage(new Uri((ListBoxPhotos.SelectedItem as Photo).GetPhoto));
        
    }
    }
}


