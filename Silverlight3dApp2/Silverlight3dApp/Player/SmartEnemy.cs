using System;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Base;
using Silverlight3dApp.Pacman;
using Silverlight3dApp.Pathfinding;

namespace Silverlight3dApp.Ghosts
{
    public class SmartEnemy : GhostBase
    {
        private Astar pathfinding;
        private DispatcherTimer timer;
        private static System.Random rand = new System.Random();

        public SmartEnemy(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 10, 0);
            timer.Tick += new EventHandler(HuntPlayer);
            timer.Start();

            htexture = content.Load<Texture2D>("pac3");
            vtexture = content.Load<Texture2D>("pac4");
            font = content.Load<SpriteFont>("font");
            directionTexture = htexture;
            HuntPlayer(null, null);
        }

        public void RandomDir()
        {
            if (this.direction.X == 0)
            {
                this.direction.X = rand.Next(-1, 2);
            }
            if (this.direction.Y == 0)
            {
                this.direction.Y = rand.Next(-1, 2);
            }
            UpdateColisionTiles();
        }

        public override void CheckWay()
        {
            int x = (int)this.position.X;
            int y = (int)this.position.Y;
            this.CurrentTile = Maze.Grid[x, y];
            this.bounds = CurrentTile.bounds;

            neighborhood.Left = Maze.Grid[x - 1, y];
            neighborhood.Right = Maze.Grid[x + 1, y];
            neighborhood.Up = Maze.Grid[x, y - 1];
            neighborhood.Bottom = Maze.Grid[x, y + 1];

            if (pathfinding.path.Count != 0)
            {
                this.direction = pathfinding.path.Dequeue();
                UpdateColisionTiles();
            }
            
        }

        public void HuntPlayer(object o, EventArgs sender)
        {
            pathfinding = new Astar(Player.GetInstance.CurrentTile, this.CurrentTile);
            direction = pathfinding.path.Peek();
            this.CheckWay();
        }

        public override void Update()
        {
            if (pathfinding.path.Count == 0)
            {
                RandomDir();
                UpdateColisionTiles();
            }

            base.Update();
        }
    }
}