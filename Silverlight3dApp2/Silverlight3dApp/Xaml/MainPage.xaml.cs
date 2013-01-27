using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using Silverlight3dApp.Pacman;
using Silverlight3dApp.Utility;
using Silverlight3dApp.Xaml;

namespace Silverlight3dApp
{
    public partial class MainPage
    {
        private Game game;
        private Difficulty difficulty;
        private Hscore h;

        public MainPage(Difficulty difficulty)
        {
            App.Current.MainWindow.WindowState = WindowState.Maximized;
            this.difficulty = difficulty;
            InitializeComponent();
            LayoutRoot.SizeChanged += LayoutRoot_SizeChanged;
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitGame();
        }

        public void End()
        {
            this.Content = new GameOver();
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

            ThreadPool.QueueUserWorkItem(o =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    label1.Content = Player.LivesLeft;
                }));
            });

            if (Player.LivesLeft == 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Content = new GameOver();
                }));
            }
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

        private void InitGame()
        {
            myDrawingSurface.Draw -= new EventHandler<DrawEventArgs>(myDrawingSurface_Draw);
            Player.Score = 0;
            Player.LivesLeft = 3;

            Tile.Width = (int)myDrawingSurface.ActualWidth;
            Tile.Height = (int)myDrawingSurface.ActualHeight;

            game = new Game(difficulty);

            myDrawingSurface.Draw += new EventHandler<DrawEventArgs>(myDrawingSurface_Draw);
        }
    }
}