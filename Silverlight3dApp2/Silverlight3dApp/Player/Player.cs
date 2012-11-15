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
        public Player(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            texture = content.Load<Texture2D>("pac");
            font = content.Load<SpriteFont>("font");
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
            if (currentTile.coin != null)
            {
                currentTile.coin = null;
            }

            if (movingMode)
            {
                Move(ref horizontal, ref vertical, ref bounds.X, ref bounds.Y, ref direction.X, ref direction.Y, ref position.X, ref position.Y);
            }
            else
            {
                Move(ref vertical, ref horizontal, ref bounds.Y, ref bounds.X, ref direction.Y, ref direction.X, ref position.Y, ref position.X);
            }
        }

        
    }
}