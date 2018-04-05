using System.Windows;

namespace crossesXnoughts
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события на изменения значения элемента управления поля со списком (ComboBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Если пользователь выбирает играть с другом, то открываем окно игры с друзьями
            if (comboBox.SelectedIndex == 0)
            {
                FriendWindow friendWindow = new FriendWindow();
                friendWindow.Show();
                Close();
            }
            // Иначе открываем окно игры в компьютером
            else if (comboBox.SelectedIndex == 1)
            {
                GameWindowBot window = new GameWindowBot("Вы", "Компьютер");
                window.Show();
                Close();
            }
        }
    }
}
