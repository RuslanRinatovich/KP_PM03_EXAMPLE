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
    /// Логика взаимодействия для MakeRewiewWindow.xaml
    /// </summary>
    public partial class MakeRewiewWindow : Window
    {
        public Rewiew currentRewiew { get; set; }
        Room currentRoom = new Room();
        public MakeRewiewWindow(Rewiew rewiew, Room room)
        {
            InitializeComponent();
            currentRewiew = rewiew;
            if (room != null)
            {
                currentRoom = room;
                TextBoxRoomTitle.Text = room.Title;
            }
            TbUserName.Text = Manager.CurrentUser.UserName;
            currentRewiew.UserName = Manager.CurrentUser.UserName;
            DataContext = currentRewiew;
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbTitle.Text))
                s.AppendLine("Отзыв пуст");

            //if (!CheckDateAndTime())
            //    s.AppendLine("Укажите корректные дату и время");
            return s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }


            currentRewiew.RoomId = currentRoom.Id;
            currentRewiew.Title = TbTitle.Text;
            currentRewiew.Rate = Convert.ToInt32(RatingBarRate.Value);
         
            currentRewiew.UserName = Manager.CurrentUser.UserName;

            try
            {
                if (currentRewiew.Id == 0)
                {
                    PhotoStudioSeverBDEntities.GetContext().Rewiews.Add(currentRewiew);
                }


                PhotoStudioSeverBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Отзыв сохранен");
                DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }


    }
}
