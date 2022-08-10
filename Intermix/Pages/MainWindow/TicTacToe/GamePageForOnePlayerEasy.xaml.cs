using Intermix.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intermix.Pages.MainWindow.TicTacToe
{
    /// <summary>
    /// Interaction logic for GamePageForOnePlayerEasy.xaml
    /// </summary>
    public partial class GamePageForOnePlayerEasy : Page
    {

        #region Private Variables
        public static ICommand AnyButtonClick { get; set; }
        private static MarkTypeTTT[] _results;
        private static bool _isPlayerOneTurn;
        private List<int> _freeFields;
        private static bool _hasGameEnded;

        double Y1;
        double Y2;
        double X1;
        double X2;
        #endregion

        #region Constructor
        public GamePageForOnePlayerEasy()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion
        private void NewGame()
        {
            _results = new MarkTypeTTT[9];
            _freeFields = new List<int>();


            for (int i = 0; i < _results.Length; i++)
            {
                _results[i] = MarkTypeTTT.Blank;
            }

            _isPlayerOneTurn = true;

            //deleting line
            int intTotalChildren = ggg.Children.Count - 1;
            for (int intCounter = intTotalChildren; intCounter > 0; intCounter--)
            {
                if (ggg.Children[intCounter].GetType() == typeof(Line))
                {
                    Line ucCurrentChild = (Line)ggg.Children[intCounter];
                    ggg.Children.Remove(ucCurrentChild);
                }
            }

            GameGrid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
            });

            Winner.Text = String.Empty;

            _hasGameEnded = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_hasGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            Random randomBlankSpace = new();

            if (_results[index] != MarkTypeTTT.Blank)
            {
                return;
            }

            if (_isPlayerOneTurn)
            {
                _results[index] = MarkTypeTTT.Cross;

                button.Content = "X";
                button.Foreground = Brushes.Red;

            }

            Random rnd = new();
            List<Button> allButtons = GameGrid.Children.OfType<Button>().ToList();

            int randomButton = 0;
            bool isRandomInArray = false;

            while (!isRandomInArray)
            {
                randomButton = rnd.Next(0, allButtons.Count);

                if (!_results.Contains(MarkTypeTTT.Blank))
                {
                    isRandomInArray = true;

                }

                if (_results.ElementAt(randomButton).Equals(MarkTypeTTT.Blank))
                {

                    isRandomInArray = true;
                }
            }

            CheckIfWinner();

            if (!_hasGameEnded)
            {
                allButtons.ElementAt(randomButton).Content = "O";
                allButtons.ElementAt(randomButton).Foreground = Brushes.Blue;
                _results[randomButton] = MarkTypeTTT.Circle;
                CheckIfWinner();
            }


        }

        private void CheckIfWinner()
        {
            #region Draw|No Winners

            if (!_results.Any(x => x == MarkTypeTTT.Blank))
            {
                _hasGameEnded = true;
                GameGrid.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Foreground = Brushes.OrangeRed;
                    Winner.Text = "Draw";
                });
            }

            #endregion

            #region HorizontalWins

            double ParentGridHeight = ggg.ActualHeight;
            double ParentGridWidth = ggg.ActualWidth;
            double GridHeight = GameGrid.ActualHeight;
            double GridWidth = GameGrid.ActualWidth;
            double buttonHeight = x1y1.ActualHeight;
            double buttonWidth = x1y1.ActualWidth;

            double różnica = (ParentGridHeight - GridHeight) / 2;

            //1 row
            if (_results[0] != MarkTypeTTT.Blank && _results[1] == _results[0] && _results[2] == _results[0])
            {
                _hasGameEnded = true;
                x1y1.Foreground = x1y2.Foreground = x1y3.Foreground = Brushes.Gold;

                Y2 = różnica + (buttonHeight / 2) + 10;
                Y1 = Y2;
                X2 = GridWidth + 14;
                X1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y1.Content.ToString());

            }
            //2 row
            if (_results[3] != MarkTypeTTT.Blank && _results[4] == _results[3] && _results[5] == _results[3])
            {
                _hasGameEnded = true;
                x2y1.Foreground = x2y2.Foreground = x2y3.Foreground = Brushes.Gold;

                Y2 = ParentGridHeight / 2;
                Y1 = Y2;
                X2 = GridWidth + 14;
                X1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x2y1.Content.ToString());

            }
            //3 row
            if (_results[6] != MarkTypeTTT.Blank && _results[7] == _results[6] && _results[8] == _results[6])
            {
                _hasGameEnded = true;
                x3y1.Foreground = x3y2.Foreground = x3y3.Foreground = Brushes.Gold;

                Y2 = różnica + 12 + 2.5 * buttonHeight;
                Y1 = Y2;
                X2 = GridWidth + 14;
                X1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x3y1.Content.ToString());

            }
            #endregion

            #region VerticalWins
            //column 1
            if (_results[0] != MarkTypeTTT.Blank && _results[3] == _results[0] && _results[6] == _results[0])
            {
                _hasGameEnded = true;
                x1y1.Foreground = x2y1.Foreground = x3y1.Foreground = Brushes.Gold;

                X2 = (buttonWidth / 2) + 20;
                X1 = X2;
                Y2 = GridHeight + 20;
                Y1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y1.Content.ToString());

            }
            //column 2
            if (_results[1] != MarkTypeTTT.Blank && _results[4] == _results[1] && _results[7] == _results[1])
            {
                _hasGameEnded = true;
                x1y2.Foreground = x2y2.Foreground = x3y2.Foreground = Brushes.Gold;

                X2 = ParentGridWidth / 2 + 10;
                X1 = X2;
                Y2 = GridHeight + 20;
                Y1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y2.Content.ToString());

            }
            //column 3
            if (_results[2] != MarkTypeTTT.Blank && _results[5] == _results[2] && _results[8] == _results[2])
            {
                _hasGameEnded = true;
                x1y3.Foreground = x2y3.Foreground = x3y3.Foreground = Brushes.Gold;

                X2 = 2.5 * buttonWidth + 12 + 20;
                X1 = X2;
                Y2 = GridHeight + 20;
                Y1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y3.Content.ToString());

            }
            #endregion

            #region DiagonalWins
            //Diagonal 0 to 9
            if (_results[0] != MarkTypeTTT.Blank && _results[4] == _results[0] && _results[8] == _results[0])
            {
                _hasGameEnded = true;
                x1y1.Foreground = x2y2.Foreground = x3y3.Foreground = Brushes.Gold;

                X2 = GridWidth + 12;
                X1 = 20;
                Y2 = GridHeight + 12;
                Y1 = 20;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y1.Content.ToString());

            }
            //Diagonal 2 to 6
            if (_results[2] != MarkTypeTTT.Blank && _results[4] == _results[2] && _results[6] == _results[2])
            {
                _hasGameEnded = true;
                x1y3.Foreground = x2y2.Foreground = x3y1.Foreground = Brushes.Gold;

                X2 = GridWidth + 12;
                X1 = 20;
                Y2 = 20;
                Y1 = GridHeight + 12;
                AnimationStart(Y1, Y2, X1, X2);
                CommunicateWinner(x1y3.Content.ToString());

            }

            #endregion

        }

        private void AnimationStart(double Y1, double Y2, double X1, double X2)
        {
            Line line = new();
            ggg.Children.Add(line);
            line.Stroke = Brushes.Gold;
            line.StrokeThickness = 12;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;

            line.X1 = X1;
            line.Y1 = Y1;
            line.X2 = X2;
            line.Y2 = Y2;

            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(line.Y1, line.Y2, new Duration(TimeSpan.FromSeconds(0.2)));
            DoubleAnimation da1 = new DoubleAnimation(line.X1, line.X2, new Duration(TimeSpan.FromSeconds(0.2)));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Line.Y2)"));
            Storyboard.SetTargetProperty(da1, new PropertyPath("(Line.X2)"));
            sb.Children.Add(da);
            sb.Children.Add(da1);

            line.BeginStoryboard(sb);

        }

        private void CommunicateWinner(string winner)
        {
            Winner.Text = $"Player {winner} won this one";
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

    }
}
