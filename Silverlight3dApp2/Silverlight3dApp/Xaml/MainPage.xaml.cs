using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Silverlight3dApp
{
    public partial class MainPage
    {
        private Game game;

        public MainPage()
        {
            InitializeComponent();
            Mouse.RootControl = this;
            Keyboard.RootControl = this;
        }

        private void myDrawingSurface_Draw(object sender, DrawEventArgs e)
        {
            game.Update(e); // We make a copy of keys to prevent external updates of the collection
            game.Draw(e);

            e.InvalidateSurface();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if GPU is on
            if (GraphicsDeviceManager.Current.RenderMode != RenderMode.Hardware)
            {
                MessageBox.Show("Please activate enableGPUAcceleration=true on your Silverlight plugin page.", "Warning", MessageBoxButton.OK);
            }

            Tile.Width = (int)myDrawingSurface.Width;
            Tile.Height = (int)myDrawingSurface.Height;

            game = new Game();
        }
    }
}