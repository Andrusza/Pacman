using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Base;

namespace Silverlight3dApp.Pacman
{
    public class Player : GhostBase
    {
        private static Player instance;
        private int coinsToCollect=288;

        public static Player GetInstance
        {
            get { return Player.instance; }
        }

        public static void CreatInstance(Tile curentTile, ContentManager content)
        {
            instance = new Player(curentTile, content);
        }

        private Player(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            htexture = content.Load<Texture2D>("pac");
            vtexture = content.Load<Texture2D>("pac2");

            directionTexture = htexture;
            font = content.Load<SpriteFont>("font");
            Maze.PositionInMaze(this);

            direction = new Vector2(1, -1);
            UpdateColisionTiles();
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            foreach (Key key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Key.A:
                        {
                            direction.X = -1;
                            UpdateColisionTiles();

                            break;
                        }

                    case Key.D:
                        {
                            direction.X = 1;
                            UpdateColisionTiles();

                            break;
                        }

                    case Key.W:
                        {
                            direction.Y = -1;
                            UpdateColisionTiles();
                            break;
                        }

                    case Key.S:
                        {
                            direction.Y = 1;
                            UpdateColisionTiles();
                            break;
                        }
                }
            }
        }

        private int score = 0;

        public void Update(KeyboardState keyboardState)
        {
            HandleInput(keyboardState);
            if (CurrentTile.coin != null)
            {
                score += CurrentTile.coin.pointValue;
                --coinsToCollect;
                CurrentTile.coin = null;
            }
            if (coinsToCollect == 0)
            {
                int lolx=0;
            }
            base.Update();
        }
    }
}