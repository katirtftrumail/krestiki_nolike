using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace crossesXnoughts
{
    public partial class GameWindow : Window
    {
        bool playerOne = true;
        Game game;
        Player player1;
        Player player2;

        string CROSS_IMAGE = System.IO.Directory.GetCurrentDirectory() + @"\images\cross.png";
        string NOUGHT_IMAGE = System.IO.Directory.GetCurrentDirectory() + @"\images\nought.png";

        public GameWindow(string nameOnePlayer, string nameTwoPlayer)
        {
            InitializeComponent();
            Prev.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\images\arrow.png"));
            game = new Game();
            player1 = new Player(nameOnePlayer);
            player2 = new Player(nameTwoPlayer);
            ChangeLabelWin(player1, player2);
            firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
        }

        private void ChangeLabelWin(Player player1, Player player2)
        {
            labelFirstPlayer.Content = player1.Name;
            labelTwoPlayer.Content = player2.Name;
            ScoreLabel.Content = player1.CountWin.ToString() + ":" + player2.CountWin.ToString();
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            var button = (Button)sender as Button;
            var number = getNumberArray(button.Name);

            if (game.Field[number] == 0)
            {
                if (playerOne)
                {
                    game.Field[number] = 1;
                    var image = (Image)FindName("img" + button.Name);
                    image.Source = new BitmapImage(new Uri(CROSS_IMAGE));
                    Console.WriteLine("нажата кнопка");
                    twoRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
                    firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#883997"));
                }
                else
                {
                    game.Field[number] = 2;
                    var image = (Image)FindName("img" + button.Name);
                    image.Source = new BitmapImage(new Uri(NOUGHT_IMAGE));
                    Console.WriteLine("нажата кнопка");
                    firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
                    twoRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#883997"));
                }

                if (game.PlayerWin(1)[0] != -1)
                {
                    player1.CountWin++;
                    Console.WriteLine("Первый игрок выиграл");
                    ClearCell();
                    game.NewGame();
                    ChangeLabelWin(player1, player2);
                }

                if (game.PlayerWin(2)[0] != -1)
                {
                    player2.CountWin++;
                    Console.WriteLine("Второй игрок выиграл");
                    ClearCell();
                    game.NewGame();
                    ChangeLabelWin(player1, player2);
                }
                
                if (game.GameEnd())
                {
                    Console.WriteLine("Ничья");

                    Console.WriteLine("Игра окончена");
                    ClearCell();
                    game.NewGame();
                    ChangeLabelWin(player1, player2);
                }
                playerOne = !playerOne;
            }
            else
            {
                Console.WriteLine("Ячейка занята");
            }
        }

        private void ClearCell()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    var image = (Image)FindName("img_" + i.ToString() + "x" + j.ToString());
                    image.Source = null;
                }
            }
        }

        private int getNumberArray(string buttonName)
        {
            var line = buttonName.Remove(0, 1).Split('x');
            return (Convert.ToInt32(line[0])-1)*3 + Convert.ToInt32(line[1])-1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int CountWin { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }

    public class Game
    {
        private int[] field = new int[9];
        public static int numberGame;

        public int[] Field
        {
            get { return field; }
            set { field = value; }
        }

        public Game()
        {
            numberGame++;
        }

        public void NewGame()
        {
            numberGame++;
            Array.Clear(field, 0, field.Length);
        }

        public bool GameEnd()
        {
            return field.All(x => x == 1 || x == 2);
        }

        public int[] PlayerWin(int x)
        {
            if (field[0] == x && field[1] == x && field[2] == x) return new int[3] { 0, 1, 2 };
            if (field[3] == x && field[4] == x && field[5] == x) return new int[3] { 3, 4, 5 };
            if (field[6] == x && field[7] == x && field[8] == x) return new int[3] { 6, 7, 8 };
            if (field[0] == x && field[3] == x && field[6] == x) return new int[3] { 0, 3, 6 };
            if (field[1] == x && field[4] == x && field[7] == x) return new int[3] { 1, 4, 7 };
            if (field[2] == x && field[5] == x && field[8] == x) return new int[3] { 2, 5, 8 };
            if (field[0] == x && field[4] == x && field[8] == x) return new int[3] { 0, 4, 8 };
            if (field[2] == x && field[4] == x && field[6] == x) return new int[3] { 2, 4, 6 };
            return new int[1] { -1 };
        }
    }
}
