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
using AfishaApp.Pages;
using System.IO;
using Microsoft.Win32;

namespace AfishaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        //текущий товар
        private EventTable _currentExhibit = new EventTable();
        // путь к файлу
        private string _filePath = null;
        // название текущей главной фотографии
        private string _photoName = null;
        // текущая папка приложения
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";


        // передача в AddExhibitPage товара 
        public AddEventPage(EventTable selectedExhibit)
        {
            InitializeComponent();
            // если передано null, то мы добавляем новый товар

            if (selectedExhibit != null)
            {
                _currentExhibit = selectedExhibit;
                TextBoxExhibitId.Visibility = Visibility.Hidden;
                TextBlockExhibitId.Visibility = Visibility.Hidden;
                int x = selectedExhibit.EventId;
                List<EventTable> goods = new List<EventTable>();
                _filePath = _currentDirectory + _currentExhibit.Photo;
            }
            DataContext = _currentExhibit;
            _photoName = _currentExhibit.Photo;
            //загрузка производителей
            ComboCategory.ItemsSource = AfishaBDEntities.GetContext().Categories.ToList();
            ComboStatus.ItemsSource = AfishaBDEntities.GetContext().Status.ToList();
        }
        // проверка полей
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentExhibit.EventName))
                s.AppendLine("Поле название пустое");

            if (_currentExhibit.Category == null)
                s.AppendLine("Выберите производителя");


            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");
            return s;
        }

        // сохранение 
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


            // проверка полей прошла успешно
            if (_currentExhibit.EventId == 0)
            {
                // добавление нового товара
                // формируем новое название файла картинки,
                // так как в папке может быть файл с тем же именем
                string photo = ChangePhotoName();
                // путь куда нужно скопировать файл
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentExhibit.Photo = photo;
                // добавляем товар в БД
                AfishaBDEntities.GetContext().EventTables.Add(_currentExhibit);
            }
            try
            {
                if (_filePath != null)
                {

                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentExhibit.Photo = photo;
                }
                // Сохраняем изменения в БД
                AfishaBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                // Возвращаемся на предыдущую форму
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // загрузка фото 
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
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        // размер файла меньше 2Мб. Поэтому выбрасывается новое исключение
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    ImagePhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }
        //подбор имени файла
        string ChangePhotoName()
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
    }
}

