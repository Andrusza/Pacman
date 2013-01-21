using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Pacman;
using Silverlight3dApp.Utility;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Silverlight3dApp
{
    public partial class MainPage
    {
        private Game game;
        private Difficulty difficulty;
        private Hscore h;

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

            //if (Player.LivesLeft != 0 && Maze.NumCoins != 0)
            //{
            //    e.InvalidateSurface();
            //}
            //else
            //{
            //    h.WriteToIsolatedStorage(Player.Score);
            //}
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
            Player.LivesLeft = 3;

            if (radioButton1.IsChecked == true)
            {
                difficulty = Difficulty.Easy;
            }

            else if (radioButton1.IsChecked == true)
            {
                difficulty = Difficulty.Normal;
            }

            else
            {
                difficulty = Difficulty.Hard;
            }

            Tile.Width = (int)myDrawingSurface.ActualWidth;
            Tile.Height = (int)myDrawingSurface.ActualHeight;

            game = new Game(difficulty);
            Hscore.Highscore=new List<int>();
            Hscore.ReadFromIsolatedStorage();
            
            textBlock1.Text = "";
            textBlock1.Inlines.Add("**HIGHSCORE**");
            textBlock1.Inlines.Add(new LineBreak());
            int num = 0;
            foreach (int x in Hscore.Highscore)
            {
                textBlock1.Inlines.Add(num.ToString()+": "+x.ToString());
                textBlock1.Inlines.Add(new LineBreak());
                num++;
            }

            myDrawingSurface.Draw += new EventHandler<DrawEventArgs>(myDrawingSurface_Draw);
        }
    }
}