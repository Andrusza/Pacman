using System.Windows;
using System.Windows.Controls;

namespace Pacman
{
    public partial class Block : UserControl
    {
        private bool TOPWALL = true;
        private bool LEFTWALL = true;
        private bool RIGHTWALL = true;
        private bool BOTTOMWALL = true;
        public double centerX;
        public double centerY;

        public int idX;
        public int idY;

        public Block(bool top, bool bottom, bool left, bool right)
        {
            InitializeComponent();
            this.TOPWALL = top;
            this.LEFTWALL = left;
            this.RIGHTWALL = right;
            this.BOTTOMWALL = bottom;
            SetWalls();
        }

        private void SetWalls()
        {
            LeftWall();
            RightWall();
            TopWall();
            BottomWall();
        }

        public void LeftWall()
        {
            if (LEFTWALL == false)
            {
                LEFTWALL = true;
                LayoutRoot.ColumnDefinitions[0].Width = new GridLength(0.0, GridUnitType.Star);
            }
            else
            {
                LEFTWALL = false;
                LayoutRoot.ColumnDefinitions[0].Width = new GridLength(0.2, GridUnitType.Star);
            }
        }

        public void RightWall()
        {
            if (RIGHTWALL == false)
            {
                RIGHTWALL = true;
                LayoutRoot.ColumnDefinitions[2].Width = new GridLength(0.0, GridUnitType.Star);
            }
            else
            {
                RIGHTWALL = false;
                LayoutRoot.ColumnDefinitions[2].Width = new GridLength(0.2, GridUnitType.Star);
            }
        }

        public void TopWall()
        {
            if (TOPWALL == false)
            {
                TOPWALL=true;
                LayoutRoot.RowDefinitions[0].Height = new GridLength(0.0, GridUnitType.Star);
            }
            else
            {
                TOPWALL = false;
                LayoutRoot.RowDefinitions[0].Height = new GridLength(0.2, GridUnitType.Star);
            }
        }

        public void BottomWall()
        {
            if (BOTTOMWALL == false)
            {
                BOTTOMWALL = true;
                LayoutRoot.RowDefinitions[2].Height = new GridLength(0.0, GridUnitType.Star);
            }
            else
            {
                BOTTOMWALL = false;
                LayoutRoot.RowDefinitions[2].Height = new GridLength(0.2, GridUnitType.Star);
            }
        }

        private void TopWallClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TopWall();
        }

        private void BottomWallClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BottomWall();
        }

        private void LeftWallClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LeftWall();
        }

        private void RightWallClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RightWall();
        }
    }
}