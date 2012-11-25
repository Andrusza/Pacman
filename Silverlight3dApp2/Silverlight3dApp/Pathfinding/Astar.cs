using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Silverlight3dApp.Pathfinding
{
    public class Astar
    {
        public List<Tile> closed;
        public List<Tile> open;
        public Queue<Vector2> path;
        private Tile currentTile;
        private Tile start;
        private Tile stop;

        public Astar(Tile stop, Tile start)
        {
            open = new List<Tile>();
            closed = new List<Tile>();
            path = new Queue<Vector2>();

            this.start = start;
            this.stop = stop;
            Search(start, stop);
        }

        public void CheckNeighborhood()
        {
            int x = (int)currentTile.position.X;
            int y = (int)currentTile.position.Y;

            CheckXY(x + 1, y);
            CheckXY(x - 1, y);
            CheckXY(x, y + 1);
            CheckXY(x, y - 1);
        }

        public void CheckXY(int x, int y)
        {
            if (Maze.Grid[x, y].tileType == TileCollision.Passable)
            {
                if (Maze.Grid[x, y].pathfindingParm.closed == false)
                {
                    if (Maze.Grid[x, y].pathfindingParm.open == false)
                    {
                        int tempG = 0;
                        if (currentTile.pathfindingParm.parent != null)
                        {
                            tempG = currentTile.pathfindingParm.g;
                        }
                        tempG += +10;
                        int tempH = ComputeDistance(Maze.Grid[x, y]);

                        Maze.Grid[x, y].pathfindingParm.g = tempG;
                        Maze.Grid[x, y].pathfindingParm.f = tempG + tempH;
                        Maze.Grid[x, y].pathfindingParm.open = true;
                        Maze.Grid[x, y].pathfindingParm.parent = currentTile;
                        open.Add(Maze.Grid[x, y]);
                    }
                    else
                    {
                        int tempG = currentTile.pathfindingParm.parent.pathfindingParm.g + 10;
                        int tempH = ComputeDistance(Maze.Grid[x, y]);
                        int tempF = tempG + tempH;
                        if (currentTile.pathfindingParm.f > tempF)
                        {
                            Maze.Grid[x, y].pathfindingParm.f = tempG + tempH;
                            Maze.Grid[x, y].pathfindingParm.parent = currentTile;
                        }
                    }
                }
            }
        }

        public void ComputeDirection(Tile children, Tile parent)
        {
            if (parent != null)
            {
                int x = (int)parent.position.X - (int)children.position.X;
                int y = (int)parent.position.Y - (int)children.position.Y;
                path.Enqueue(new Vector2(-x, -y));
            }
        }

        public int ComputeDistance(Tile current)
        {
            int x1 = (int)current.position.X;
            int y1 = (int)current.position.Y;
            int x2 = (int)stop.position.X;
            int y2 = (int)stop.position.Y;

            return (Math.Abs(x2 - x1) * 10 + Math.Abs(y2 - y1) * 10);
        }

        public void Search(Tile start, Tile stop)
        {
            open.Add(start);
            while (open.Count != 0)
            {
                open.Sort((a, b) => a.pathfindingParm.f.CompareTo(b.pathfindingParm.f));
                currentTile = open[0];
                open.Remove(currentTile);
                closed.Add(currentTile);
                currentTile.pathfindingParm.closed = true;
                currentTile.pathfindingParm.open = false;

                CheckNeighborhood();
                if (currentTile.position == stop.position)
                {
                    break;
                }
            }
            WritePath(stop);
            foreach (Tile x in open)
            {
                x.pathfindingParm = new AstarParam();
            }

            foreach (Tile x in closed)
            {
                x.pathfindingParm = new AstarParam();
            }
        }

        public void WritePath(Tile children)
        {
            if (children != null)
            {
                WritePath(children.pathfindingParm.parent);
                ComputeDirection(children, children.pathfindingParm.parent);
            }
        }
    }
}