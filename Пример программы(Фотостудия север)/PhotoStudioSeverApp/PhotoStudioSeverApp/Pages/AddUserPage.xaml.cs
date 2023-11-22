using PhotoStudioSeverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public User _currentItem { get; private set; }
        List<User> users = new List<User>();
        bool isNew = false;
        bool badName = false;
        string msg = "Изменения сохранены";
        public AddUserPage(User user)
        {
            InitializeComponent();
            LoadAndInitData(user);
        }


        void LoadAndInitData(User selected)
        {     // если передано null, то мы добавляем новый товар

            users = PhotoStudioSeverBDEntities.GetContext().Users.OrderBy(p => p.UserName).ToList();

            ComboUserType.ItemsSource = PhotoStudioSeverBDEntities.GetContext().Roles.ToList();

            if (selected != null)
            {
                _currentItem = selected;
                TextBoxUserName.IsReadOnly = true;

            }
            else
            {
                _currentItem = new User();
                isNew = true;
                msg = "Запись добавлена";
            }
            // контекст данных текущий товар

            DataContext = _currentItem;

        }

        /// <summary>
        /// Проверка полей ввод на корректыне данные
        /// </summary>
        /// <returns>текст StringBuilder содержащий ошибки, если они есть</returns>
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();



            if (string.IsNullOrWhiteSpace(_currentItem.UserName))
                s.AppendLine("Задайте имя пользователя");
            if (badName)
                s.AppendLine("Выберите другое имя пользователя");
            if (string.IsNullOrWhiteSpace(_currentItem.Password))
                s.AppendLine("Задайте пароль");
            if (ComboUserType.SelectedIndex == -1)
                s.AppendLine("Укажите тип пользователя");
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
            // проверка полей прошла успешно
            // если товар новый, то его ID == 0
            if (isNew)
            {
                // добавление нового товара, 

                PhotoStudioSeverBDEntities.GetContext().Users.Add(_currentItem);
            }

            try
            {


                PhotoStudioSeverBDEntities.GetContext().SaveChanges();  // Сохраняем изменения в БД
                MessageBox.Show(msg, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();  // Возвращаемся на предыдущую форму
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string username = TextBoxUserName.Text.ToLower();
            User user = PhotoStudioSeverBDEntities.GetContext().Users.Where(p => p.UserName.ToLower() == username).FirstOrDefault();
            if (user == _currentItem)
                return;
            if (user != null)
            {
                TextBoxUserName.Foreground = new SolidColorBrush(Colors.Red);
                badName = true;
            }
            else
            {
                TextBoxUserName.Foreground = new SolidColorBrush(Colors.Green);
                badName = false;
            }
        }

        private void TextBoxUserName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z\s]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

