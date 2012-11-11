using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace SilverlightApplication3
{
    public partial class MainPage : UserControl
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
            // Let's go for another turn!
            e.InvalidateSurface();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if GPU is on
            if (GraphicsDeviceManager.Current.RenderMode != RenderMode.Hardware)
            {
                MessageBox.Show("Please activate enableGPUAcceleration=true on your Silverlight plugin page.", "Warning", MessageBoxButton.OK);
            }

            Tile.Width = (int)(myDrawingSurface.Width / Tile.XCount);
            Tile.Height = (int)(myDrawingSurface.Height / Tile.YCount);
            Tile.Size = new Vector2(Tile.Width, Tile.Height);

            game = new Game();
        }
    }
}