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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            try
            {  //загрузка всех пользователей из БД в список
                List<User> users = PhotoStudioSeverBDEntities.GetContext().Users.ToList();
                //попытка найти пользователя с указанным паролем и логином
                //если такого пользователя не будет обнаружено то переменная u будет равна null
                User u = users.FirstOrDefault(p => p.Password == TbPass.Password && p.UserName == TbLogin.Text);

                if (u != null)
                {
                    // логин и пароль корректные запускаем главную форму приложения
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Owner = this;
                    //this.Hide();
                    //mainWindow.Show();
                    Manager.CurrentUser = u;
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //код кнопки Cancel
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if ((tbRegLogin.Text == "") || (psbPassword1.Password == "") || (psbPassword2.Password == ""))
            {
                MessageBox.Show("Поля пустые", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<User> users = PhotoStudioSeverBDEntities.GetContext().Users.ToList();
            //попытка найти пользователя с указанным паролем и логином
            //если такого пользователя не будет обнаружено то переменная u будет равна null
            User u = users.FirstOrDefault(p => p.UserName == tbRegLogin.Text);
            if (u != null)
            {
                MessageBox.Show("Данный логин занят, выберите другой логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (psbPassword1.Password != psbPassword2.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User user = new User();
            user.UserName = tbRegLogin.Text;
            user.Password = psbPassword1.Password;
            user.RoleId = 3;
            user.FirstName = "";
            user.LastName = "";
            user.MiddleName = "";

            user.Phone = "";
            user.Email = "";
            PhotoStudioSeverBDEntities.GetContext().Users.Add(user);
            PhotoStudioSeverBDEntities.GetContext().SaveChanges();

            MessageBox.Show("Регистраця прошла успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogHost.IsOpen = false;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }
    }
}

