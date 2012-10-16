using System;
using System.Windows;
using System.Windows.Controls;
using Pacman;
using Pacman.Game;

namespace PacMan
{
    public partial class MainPage : UserControl
    {
        private double height;
        private double width;

        private GameSurface _gameSurface = GameSurface.GetInstance();
        private int _fps;
        private DateTime _lastFpsReport;

        public MainPage()
        {
            InitializeComponent();
            App.Current.Host.Content.Resized += new EventHandler(Content_Resized);

            this._gameSurface.RenderFrame += RenderFrame;
            this._gameSurface.StartGame();
        }

        private void Content_Resized(object sender, EventArgs e)
        {
            height = App.Current.Host.Content.ActualHeight;
            width = App.Current.Host.Content.ActualWidth;
            Window.Height = height;
            Window.Width = width;

            Level.Children.Clear();

            double _width = (width * 0.800) / 10.00;
            double _height = height / 10.00;

            double centerX = _width / 2;
            double centerY = _height / 2;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    Pacman.Block b = new Pacman.Block(false, true, true, true);
                    b.Width = _width;
                    b.Height = _height;

                    b.centerX = j * centerX;
                    b.centerY = i * centerY;

                    b.idX = j;
                    b.idY = i;

                    Level.Children.Add(b);
                }
            }  
        }

        private void RenderFrame(object sender, RenderFrameEventArgs e)
        {
            int x = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
        }
    }
}