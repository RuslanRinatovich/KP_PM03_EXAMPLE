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

namespace AfishaApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Сatalog());
            Manager.MainFrame = MainFrame;
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            // показать владельца формы
            this.Close();
            //Owner.Show();
            // или если надо, то можно закрыть приложение  командой
            // App.Current.Shutdown();

        }
        //событие попытки закрытия окна,
        // если пользователь выберет Cancel, то форму не закроем
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?",
                "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
        // Кнопка назад
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        // Кнопка навигации
        private void BtnEditGoodsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EventsPage());
        }
        // Событие отрисовки страницы
        // Скрываем или показываем кнопку Назад 
        // Скрываем или показываем кнопки Для перехода к остальным страницам
        private void MainFrameContentRendered(object sender, EventArgs e)
        {

            if (MainFrame.CanGoBack)
            {
                // показываем кнопку назад
                BtnBack.Visibility = Visibility.Visible;
                // скрываем кнопку товары 
                BtnEditGoods.Visibility = Visibility.Collapsed;
            }
            else
            {
                // скрываем кнопку назад
                BtnBack.Visibility = Visibility.Collapsed;
                // показываем кнопку товары 
                BtnEditGoods.Visibility = Visibility.Visible;


            }
        }


    }
}
