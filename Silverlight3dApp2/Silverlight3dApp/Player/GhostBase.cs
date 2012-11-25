using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Pacman;

namespace Silverlight3dApp.Base
{
    public class Neighborhood
    {
        public Tile Left;
        public Tile Right;
        public Tile Up;
        public Tile Bottom;
    }

    public abstract class GhostBase
    {
        protected Rectangle bounds;
        protected Texture2D htexture;
        protected Texture2D vtexture;
        protected Texture2D directionTexture;
        protected SpriteEffects orientation = SpriteEffects.None;
        
        protected Vector2 direction;
        public Vector2 position;
        private Tile currentTile;

        public Tile CurrentTile
        {
            get { return currentTile; }
            set { currentTile = value; }
        }

        public bool movingMode;

        protected Tile horizontal;
        protected Tile vertical;

        protected Neighborhood neighborhood;

        protected SpriteFont font;

        public GhostBase(Tile curentTile, ContentManager content)
        {
            this.bounds = curentTile.bounds;
            this.currentTile = curentTile;
            this.position = currentTile.position;
            
            this.neighborhood = new Neighborhood();
            movingMode = true;
            direction = new Vector2(0, 0);
        }

        public virtual void CheckWay(Tile[,] grid)
        {
            int x = (int)this.position.X;
            int y = (int)this.position.Y;
            this.CurrentTile = grid[x, y];
            this.bounds = CurrentTile.bounds;

            neighborhood.Left = grid[x - 1, y];
            neighborhood.Right = grid[x + 1, y];
            neighborhood.Up = grid[x, y - 1];
            neighborhood.Bottom = grid[x, y + 1];
        }

        public bool CheckPlayerMazeCollision(Tile toCheck)
        {
            int a_x1 = bounds.Left;
            int a_x2 = bounds.Right;
            int a_y1 = bounds.Top;
            int a_y2 = bounds.Bottom;

            int b_x1 = toCheck.bounds.Left;
            int b_x2 = toCheck.bounds.Right;
            int b_y1 = toCheck.bounds.Top;
            int b_y2 = toCheck.bounds.Bottom;

            if (a_x1 < b_x2 && a_x2 > b_x1 && a_y1 < b_y2 && a_y2 > b_y1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool Move(ref Tile from, ref Tile to, ref int fromAxis, ref int toAxis, ref float fromAmount, ref float toAmout, ref float fromPosition, ref float toPosition)
        {
            if (from != null)
            {
                fromAxis += (int)fromAmount * 2;
                if (CheckPlayerMazeCollision(from))
                {
                    if (from.tileType == TileCollision.Passable)
                    {
                        if (!CheckPlayerMazeCollision(CurrentTile))
                        {
                            fromPosition += fromAmount;
                            Maze.PositionInMaze(this);
                            UpdateColisionTiles();
                            if (to != null)
                            {
                                toAxis += (int)toAmout * 2;
                                if (CheckPlayerMazeCollision(to))
                                {
                                    if (to.tileType == TileCollision.Passable)
                                    {
                                        fromAmount = 0;
                                        movingMode = !movingMode;
                                    }
                                    else
                                    {
                                        movingMode = !movingMode;
                                        toAxis -= (int)toAmout * 2;
                                    }
                                }
                            }
                        }
                        return true;
                        
                    }
                    else
                    {
                        movingMode = !movingMode;
                        fromAxis -= (int)fromAmount * 2;
                        if (this.GetType()!=typeof(Player))
                        {
                            fromAmount = 0;
                        }
                        return false;
                    }
                }
               
            }
            else
            {
                movingMode = !movingMode;
            }
            return true;
        }

        protected void UpdateColisionTiles()
        {
            switch ((int)direction.X)
            {
                case -1:
                    {
                        horizontal = neighborhood.Left;
                        break;
                    }

                case 1:
                    {
                        horizontal = neighborhood.Right;
                        break;
                    }
                default:
                    {
                        horizontal = null;
                        break;
                    }
            }

            switch ((int)direction.Y)
            {
                case -1:
                    {
                        vertical = neighborhood.Up;
                        break;
                    }

                case 1:
                    {
                        vertical = neighborhood.Bottom;
                        break;
                    }
                default:
                    {
                        vertical = null;
                        break;
                    }
            }
        }

        public virtual void Update() 
        {
            if (movingMode)
            {
                bool turn = Move(ref horizontal, ref vertical, ref bounds.X, ref bounds.Y, ref direction.X, ref direction.Y, ref position.X, ref position.Y);
                if (turn)
                {
                    directionTexture = htexture;
                    if (direction.X == 1)
                    {
                        orientation = SpriteEffects.None;
                    }
                    else
                    {
                        orientation = SpriteEffects.FlipHorizontally;
                    }
                }
            }
            else
            {
                bool turn = Move(ref vertical, ref horizontal, ref bounds.Y, ref bounds.X, ref direction.Y, ref direction.X, ref position.Y, ref position.X);
                if (turn)
                {
                    directionTexture = vtexture;
                    if (direction.Y == -1)
                    {
                        orientation = SpriteEffects.None;
                    }
                    else
                    {
                        orientation = SpriteEffects.FlipVertically;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.directionTexture, this.bounds, null, Color.White, 0, Vector2.Zero, orientation, 1f);
            //spriteBatch.DrawString(font, this.direction.X + " " + this.direction.Y, new Vector2(100, 170), Color.Black);
        }
    }
}