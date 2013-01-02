using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Pacman;

namespace Silverlight3dApp
{
    public partial class MainPage
    {
        private Game game;
        private LevelDifficulty difficulty;

        public MainPage()
        {
            App.Current.MainWindow.WindowState = WindowState.Maximized;
            InitializeComponent();

            Mouse.RootControl = this;
            Keyboard.RootControl = this;
        }

        private void myDrawingSurface_Draw(object sender, DrawEventArgs e)
        {
            game.state.Update(e);
            game.state.Draw(e);

            ThreadPool.QueueUserWorkItem(o =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    lScore.Content = Player.Score;
                }));
            });

            e.InvalidateSurface();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if GPU is on
            if (GraphicsDeviceManager.Current.RenderMode != RenderMode.Hardware)
            {
                MessageBox.Show("Please activate enableGPUAcceleration=true on your Silverlight plugin page.", "Warning", MessageBoxButton.OK);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            myDrawingSurface.Draw -= new EventHandler<DrawEventArgs>(myDrawingSurface_Draw);
            Player.Score = 0;

            if (radioButton1.IsChecked == true)
            {
                difficulty = new LevelDifficulty(Difficulty.Easy);
            }

            else if (radioButton1.IsChecked == true)
            {
                difficulty = new LevelDifficulty(Difficulty.Normal);
            }

            else
            {
                difficulty = new LevelDifficulty(Difficulty.Hard);
            }

            Tile.Width = (int)myDrawingSurface.ActualWidth;
            Tile.Height = (int)myDrawingSurface.ActualHeight;

            game = new Game(difficulty);
            myDrawingSurface.Draw += new EventHandler<DrawEventArgs>(myDrawingSurface_Draw);
        }

       
    }
}