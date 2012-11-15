using System;
using System.Collections.Generic;

namespace Silverlight3dApp.Pathfinding
{
    public class Astar
    {
        public List<Tile> open;
        private Maze maze;
        private Tile start;
        private Tile stop;
        private Tile currentTile;

        public Astar(Maze maze, Tile start, Tile stop)
        {
            open = new List<Tile>();
            this.maze = maze;

            this.start = start;
            this.stop = stop;
            Search(start, stop);
        }

        public int ComputeDistance(Tile current)
        {
            int x1 = (int)current.position.X;
            int y1 = (int)current.position.Y;
            int x2 = (int)stop.position.X;
            int y2 = (int)stop.position.Y;

            return (Math.Abs(x2 - x1) * 10 + Math.Abs(y2 - y1) * 10);
        }

        public void CheckXY(int x, int y)
        {
            if (maze.grid[x, y].tileType == TileCollision.Passable)
            {
                if (maze.grid[x, y].pathfindingParm.closed == false)
                {
                    if (maze.grid[x, y].pathfindingParm.open == false)
                    {
                        int tempG = 0;
                        if (currentTile.pathfindingParm.parent != null)
                        {
                            tempG = currentTile.pathfindingParm.parent.pathfindingParm.g;
                        }
                        tempG += +10;
                        int tempH = ComputeDistance(maze.grid[x, y]);

                        maze.grid[x, y].pathfindingParm.f = tempG + tempH;
                        maze.grid[x, y].pathfindingParm.open = true;
                        maze.grid[x, y].pathfindingParm.parent = currentTile;
                        open.Add(maze.grid[x, y]);
                    }
                    else
                    {
                        int tempG = currentTile.pathfindingParm.parent.pathfindingParm.g + 10;
                        int tempH = ComputeDistance(maze.grid[x, y]);
                        int tempF = tempG + tempH;
                        if (currentTile.pathfindingParm.f > tempF)
                        {
                            maze.grid[x, y].pathfindingParm.f = tempG + tempH;
                            maze.grid[x, y].pathfindingParm.parent = currentTile;
                        }
                    }
                }
            }
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

        public void WritePath(Tile parent)
        {
            if (parent != null)
            {
                WritePath(parent.pathfindingParm.parent);
                parent.pathfindingParm = new AstarParam();
            }
        }

        public void Search(Tile start, Tile stop)
        {
            open.Add(start);
            while (open.Count != 0)
            {
                open.Sort((a, b) => a.pathfindingParm.f.CompareTo(b.pathfindingParm.f));
                currentTile = open[0];
                open.Remove(currentTile);
                currentTile.pathfindingParm.closed = true;
                currentTile.pathfindingParm.open = false;

                CheckNeighborhood();
                if (currentTile.position == stop.position)
                {
                    break;
                }
            }
            WritePath(stop);
        }
    }
}