using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public TextBlock textWritten;
        public TextBlock text;
        public TextBlock scoreLabel;
        public TextBlock failsLabel;


        private int score = 0;
        private int fails = 0;
        Random rand = new Random();

        public class KeyPop
        {
            public string Sign { get; set; }
            public int Width { get; set; }
            public KeyPop(string Sign, int Width)
            {
                this.Sign = Sign;
                this.Width = Width;
            }
        }

        static List<KeyPop> keyboard = new List<KeyPop>();
        public void KBInitialize()
        {
            keyboard.Add(new KeyPop("`", 2));
            keyboard.Add(new KeyPop("1", 2));
            keyboard.Add(new KeyPop("2", 2));
            keyboard.Add(new KeyPop("3", 2));
            keyboard.Add(new KeyPop("4", 2));
            keyboard.Add(new KeyPop("5", 2));
            keyboard.Add(new KeyPop("6", 2));
            keyboard.Add(new KeyPop("7", 2));
            keyboard.Add(new KeyPop("8", 2));
            keyboard.Add(new KeyPop("9", 2));
            keyboard.Add(new KeyPop("0", 2));
            keyboard.Add(new KeyPop("-", 2));
            keyboard.Add(new KeyPop("=", 2));
            keyboard.Add(new KeyPop("Backspace", 6));

            keyboard.Add(new KeyPop("Tab", 3));
            keyboard.Add(new KeyPop("q", 2));
            keyboard.Add(new KeyPop("w", 2));
            keyboard.Add(new KeyPop("e", 2));
            keyboard.Add(new KeyPop("r", 2));
            keyboard.Add(new KeyPop("t", 2));
            keyboard.Add(new KeyPop("y", 2));
            keyboard.Add(new KeyPop("u", 2));
            keyboard.Add(new KeyPop("i", 2));
            keyboard.Add(new KeyPop("o", 2));
            keyboard.Add(new KeyPop("p", 2));
            keyboard.Add(new KeyPop("[", 2));
            keyboard.Add(new KeyPop("]", 2));
            keyboard.Add(new KeyPop(@"\", 5));

            keyboard.Add(new KeyPop("Caps Lock", 4));
            keyboard.Add(new KeyPop("a", 2));
            keyboard.Add(new KeyPop("s", 2));
            keyboard.Add(new KeyPop("d", 2));
            keyboard.Add(new KeyPop("f", 2));
            keyboard.Add(new KeyPop("g", 2));
            keyboard.Add(new KeyPop("h", 2));
            keyboard.Add(new KeyPop("j", 2));
            keyboard.Add(new KeyPop("k", 2));
            keyboard.Add(new KeyPop("l", 2));
            keyboard.Add(new KeyPop(";", 2));
            keyboard.Add(new KeyPop("'", 2));
            keyboard.Add(new KeyPop("Enter", 6));

            keyboard.Add(new KeyPop("Shift", 5));
            keyboard.Add(new KeyPop("z", 2));
            keyboard.Add(new KeyPop("x", 2));
            keyboard.Add(new KeyPop("c", 2));
            keyboard.Add(new KeyPop("v", 2));
            keyboard.Add(new KeyPop("b", 2));
            keyboard.Add(new KeyPop("n", 2));
            keyboard.Add(new KeyPop("m", 2));
            keyboard.Add(new KeyPop(",", 2));
            keyboard.Add(new KeyPop(".", 2));
            keyboard.Add(new KeyPop("/", 2));
            keyboard.Add(new KeyPop("Shift", 7));

            keyboard.Add(new KeyPop("Ctrl", 3));
            keyboard.Add(new KeyPop("Win", 3));
            keyboard.Add(new KeyPop("Alt", 3));
            keyboard.Add(new KeyPop("Space", 14));
            keyboard.Add(new KeyPop("Alt", 3));
            keyboard.Add(new KeyPop("Win", 3));
            keyboard.Add(new KeyPop("Ctrl", 3));
        }


        public MainWindow()
        {
            InitializeComponent();
            KBInitialize();

            //количество строк и столбцов в сетке
            int col = 32;
            int row = 7;

            //добавление строк и столбцов. Сама сетка объявлена в xaml
            for (int i = 0; i < 32; i++)
                RootGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < row; i++)
                RootGrid.RowDefinitions.Add(new RowDefinition());

            //прорисовка уже набранной
            textWritten = new TextBlock();
            Grid.SetRow(textWritten, 1);
            Grid.SetColumn(textWritten, 0);
            Grid.SetColumnSpan(textWritten, 16);
            RootGrid.Children.Add(textWritten);
            textWritten.FontSize = 24;
            textWritten.Foreground = Brushes.LightGray;
            textWritten.HorizontalAlignment = HorizontalAlignment.Right;
            // textWritten.Text = "Text t";

            //прорисовка текстовой строки
            text = new TextBlock();
            Grid.SetRow(text, 1);
            Grid.SetColumn(text, col / 2);
            Grid.SetColumnSpan(text, 16);
            RootGrid.Children.Add(text);
            text.FontSize = 24;
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.Text = "any text to type";

            //прорисовка очков 
            scoreLabel = new TextBlock();
            Grid.SetRow(scoreLabel, 0);
            Grid.SetColumn(scoreLabel, 0);
            Grid.SetColumnSpan(scoreLabel, 4);
            RootGrid.Children.Add(scoreLabel);
            scoreLabel.HorizontalAlignment = HorizontalAlignment.Left;
            scoreLabel.Text = "Scores: 0";

            //прорисовка ошибок
            failsLabel = new TextBlock();
            Grid.SetRow(failsLabel, 0);
            Grid.SetColumn(failsLabel, 6);
            Grid.SetColumnSpan(failsLabel, 4);
            RootGrid.Children.Add(failsLabel);
            failsLabel.HorizontalAlignment = HorizontalAlignment.Left;
            failsLabel.Text = "Fails: 0";


            //прорисовка клавиатуры
            int length = keyboard.Count;
            int currentRow = 2;
            int currentCol = 0;

            for (int i = 0; i < length; i++)
            {
                Button button = new Button();
                button.Content = keyboard[i].Sign;

                //применение прописанного в XAML закруглённого стиля кнопок
                button.Style = (Style)FindResource("RoundedButtonStyle");

                //привязка кнопок к сетке
                Grid.SetRow(button, currentRow);
                Grid.SetColumn(button, currentCol);
                Grid.SetColumnSpan(button, keyboard[i].Width);
                RootGrid.Children.Add(button);

                //f button.Click += Button_Click;
                currentCol += keyboard[i].Width;
                if (currentCol >= col)
                {
                    currentRow++;
                    currentCol = 0;
                }
            }
        }

        //проба работы через нажатие клавиши
        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            string? temp = (sender as Button)?.Content.ToString();
            string key;
            switch (temp)
            {
                case "Space":
                    key = " ";
                    break;

                case "Backspace":
                case "Ctrl":
                case "Tab":
                case "Win":
                case "Alt":
                    key = "";
                    break;

                default:
                    key = temp;
                    break;
            }
            textWritten.Text += key;
        }*/

        //метод отвечает за изменения при верном/неверном наборе клавиши
        private void Scoring(bool isRight = true, string key = "")
        {

            if (text.Text == "")
            {
                MessageBox.Show($"Game is over.\nYour scores: {score}; fails: {fails}");
            }

            if (isRight && text.Text.Length > 0)
            {
                textWritten.Text += key;
                if (text.Text.Length == 1) text.Text = "";
                try
                {
                    text.Text = text.Text.Remove(0, 1);
                }
                catch { text.Text = ""; }

                scoreLabel.Text = "Scores: " + (++score).ToString();
            }

            else
            {
                failsLabel.Text = "Fails: " + (++fails).ToString();
            }
        }


        //анимация нажатия клавиши
        private async Task PressButton(KeyEventArgs e)
        {
            foreach (var it in RootGrid.Children)
            {
                if (it is Button)
                {
                    if (((Button)it).Content.ToString() == e.Key.ToString().ToLower())
                    {
                        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((Button)it, new object[] { true });
                        await Task.Delay(200); // Пауза на 2 секунды (время задержки в миллисекундах)
                        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((Button)it, new object[] { false });
                    }
                }
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Sound.Source = ""
            // string soundFile = @"Sound\KeyPop\1.wav";
            //  Sound.Source = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, soundFile));
            //  Sound.Stop();

            // Sound.Source = new Uri(@"C:\1.mp3");
            // Sound.Play();

            PressButton(e);
            string temp = e.Key.ToString();
            string key;
            bool isRight;
            switch (temp)
            {
                case "Space":
                    key = " ";
                    break;

                default:
                    if (temp.Length > 1) { isRight = false; Scoring(false); return; }
                    //ToLower - костыль,т.к. пока не реализована поддержка заглавных букв с помощью шифта.
                    key = temp.ToLower();
                    break;
            }
            if (text.Text.StartsWith(key) || text.Text.StartsWith(key.ToUpper())) isRight = true;
            else isRight = false;
            Scoring(isRight, key);
        }
    }
}
