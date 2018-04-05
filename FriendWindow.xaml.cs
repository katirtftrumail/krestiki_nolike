using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace crossesXnoughts
{
    public partial class FriendWindow : Window
    {
        public FriendWindow()
        {
            InitializeComponent();
            Prev.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\images\arrow.png"));
        }

        /// <summary>
        /// Обработчик события на кнопке Начать игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Если оба поля заполнены, то открыть окно с игрой
            if (nameOnePlayer.Text != string.Empty && nameTwoPlayer.Text != string.Empty)
            {
                GameWindow gameWindow = new GameWindow(nameOnePlayer.Text, nameTwoPlayer.Text);
                gameWindow.Show();
                Close();
            }
            // Иначе поругаться
            else
            {
                MessageBox.Show("Введите имена обоих участников");
            }
        }

        // Пойти назад
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
