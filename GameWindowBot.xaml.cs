using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace crossesXnoughts
{
    public partial class GameWindowBot : Window
    {
        bool playerOne = true;
        Game game;
        Player player1;
        Bot bot;

        string CROSS_IMAGE = System.IO.Directory.GetCurrentDirectory() + @"\images\cross.png";
        string NOUGHT_IMAGE = System.IO.Directory.GetCurrentDirectory() +  @"\images\nought.png";

        public GameWindowBot(string nameOnePlayer, string nameTwoPlayer)
        {
            InitializeComponent();
            Prev.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\images\arrow.png"));
            game = new Game();
            player1 = new Player(nameOnePlayer);
            bot = new Bot(nameTwoPlayer);
            ChangeLabelWin(player1, bot);
            firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
        }

        private void ChangeLabelWin(Player player1, Bot bot)
        {
            labelFirstPlayer.Content = player1.Name;
            labelTwoPlayer.Content = bot.Name;
            ScoreLabel.Content = player1.CountWin.ToString() + ":" + bot.CountWin.ToString();
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            var button = (Button)sender as Button;
            var number = getNumberArray(button.Name);


            if (game.Field[number] == 0)
            {
                game.Field[number] = 1;
                var image = (Image)FindName("img" + button.Name);
                image.Source = new BitmapImage(new Uri(CROSS_IMAGE));
                Console.WriteLine(number.ToString() + " " + "img" + button.Name);
                Console.WriteLine("нажата кнопка");
                twoRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
                firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#883997"));

                if (!CheckGame())
                {

                    bool t = true;
                    while (t)
                    {
                        var item = bot.GetMotion(game.Field);
                        string name = "_" + (item / 3 + 1).ToString() + "x" + (item % 3 + 1).ToString();
                        if (game.Field[item] == 0)
                        {
                            BotPush(item, name);
                            t = false;
                        }

                    }
                }

                CheckGame();
                for (int i = 0; i < game.Field.Length; i++)
                {
                    if (i % 3 == 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(game.Field[i] + " ");
                }

                Console.WriteLine();

                playerOne = !playerOne;
            }
            else
            {
                Console.WriteLine("Ячейка занята");
            }
        }

        private bool CheckGame()
        {
            if (game.PlayerWin(1)[0] != -1)
            {
                player1.CountWin++;
                Console.WriteLine("Первый игрок выиграл");
                ClearCell();
                game.NewGame();
                ChangeLabelWin(player1, bot);
                return true;
            }

            else if (game.PlayerWin(2)[0] != -1)
            {
                bot.CountWin++;
                Console.WriteLine("Второй игрок выиграл");
                ClearCell();
                game.NewGame();
                ChangeLabelWin(player1, bot);
                return true;
            }

            else if (game.GameEnd())
            {
                Console.WriteLine("Ничья");

                Console.WriteLine("Игра окончена");
                ClearCell();
                game.NewGame();
                ChangeLabelWin(player1, bot);
                return true;
            }
            return false;
        }

        private void BotPush(int item, string name)
        {
            game.Field[item] = 2;
            Console.WriteLine(item.ToString() + " " + name);
            var image = (Image)FindName("img" + name);
            image.Source = new BitmapImage(new Uri(NOUGHT_IMAGE));
            Console.WriteLine("нажата кнопка");
            firstRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5c007a"));
            twoRect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#883997"));
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
            return (Convert.ToInt32(line[0]) - 1) * 3 + Convert.ToInt32(line[1]) - 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
