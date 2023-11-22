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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        public StatusPage()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            AfishaBDEntities.GetContext().Status.Load();
            DtStatus.ItemsSource = AfishaBDEntities.GetContext().Status.Local.ToBindingList();
        }

        void SaveData()
        {
            try
            {
                AfishaBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadData();
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = DtStatus.SelectedItems.Cast<Status>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить { selectedService.Count()} записей ??? ", "Удаление", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    AfishaBDEntities.GetContext().Status.RemoveRange(selectedService);
                    AfishaBDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    LoadData();
                }
            }
        }

        // отображение номеров строк в DataGrid
        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}