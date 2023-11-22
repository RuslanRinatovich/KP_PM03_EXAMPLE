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
    /// Логика взаимодействия для MyAccountWindow.xaml
    /// </summary>
    public partial class MyAccountWindow : Window
    {
        public User currentItem { get; private set; }
        public MyAccountWindow()
        {
            InitializeComponent();
            currentItem = Manager.CurrentUser;
            this.DataContext = Manager.CurrentUser;
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentItem.UserName))
                s.AppendLine("Задайте имя пользователя");
            if (string.IsNullOrWhiteSpace(currentItem.FirstName))
                s.AppendLine("Поле имя пустое");

            if (string.IsNullOrWhiteSpace(currentItem.LastName))
                s.AppendLine("Поле фамилия пустое");
            if (string.IsNullOrWhiteSpace(currentItem.Phone))
                s.AppendLine("Поле телефон пустое");
            if (string.IsNullOrWhiteSpace(currentItem.Email))
                s.AppendLine("Поле email пустое");



            if (CheckBoxChangePassword.IsChecked == true)
            {
                User user = PhotoStudioSeverBDEntities.GetContext().Users.Find(currentItem.UserName);
                if ((PasswordBoxNewPassword1.Password != PasswordBoxNewPassword2.Password) || (PasswordBoxOldPassword.Password != user.Password))
                {
                    s.AppendLine("Пароли не совпадают");
                }
                else
                {
                    currentItem.Password = PasswordBoxNewPassword1.Password;
                }
            }
            return s;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }


            try
            {
                PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись изменена");
                this.DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Ошибка");
                this.DialogResult = false;
            }


        }
    }
}