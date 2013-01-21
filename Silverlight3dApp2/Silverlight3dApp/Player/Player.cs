using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Base;
using Silverlight3dApp.Utility;

namespace Silverlight3dApp.Pacman
{
    public class Player : GhostBase
    {
        private static Player instance;
        private static int score = 0;
        private static int livesLeft = 3;

        public static int LivesLeft
        {
            get { return Player.livesLeft; }
            set { Player.livesLeft = value; }
        }

        public static int Score
        {
            get { return Player.score; }
            set { score = value; }
        }

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
            PlayerBorns(content);
            directionTexture = htexture;
            font = content.Load<SpriteFont>("font");
            this.CheckWay();

            direction = new Position2D(0, 0);
            UpdateColisionTiles();
        }

        public void PlayerBorns(ContentManager content)
        {
            htexture = content.Load<Texture2D>("pac");
            vtexture = content.Load<Texture2D>("pac2");
        }

        public void PlayerDies(ContentManager content)
        {
            htexture = content.Load<Texture2D>("pac5");
            vtexture = content.Load<Texture2D>("pac6");
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

        public void Update(KeyboardState keyboardState)
        {
            HandleInput(keyboardState);
            if (CurrentTile.coin != null)
            {
                score += CurrentTile.coin.pointValue;
                --Maze.NumCoins;
                CurrentTile.coin = null;
            }

            base.Update();
        }
    }
}