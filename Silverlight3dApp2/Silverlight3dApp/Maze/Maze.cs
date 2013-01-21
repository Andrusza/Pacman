using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Ghosts;
using Silverlight3dApp.Pacman;

namespace Silverlight3dApp
{
    public class Maze
    {
        private static Tile[,] grid;

        public static Tile[,] Grid
        {
            get { return Maze.grid; }
            set { Maze.grid = value; }
        }

        public Tile GetTile(int x, int y)
        {
            return Grid[x, y];
        }

        public Vector2 GetSize()
        {
            return new Vector2(numX, numY);
        }

        private readonly ContentManager content;

        private Texture2D wall;
        private Texture2D space;
        private Texture2D coin;

        private int numX; //Number of tiles horizontaly
        private int numY; //Number of tiles verticaly
        private static int numCoins = 0;
        private Random rand;
        private static LevelDifficulty enemies;

        public static LevelDifficulty Enemies
        {
            get { return Maze.enemies; }
            set { Maze.enemies = value; }
        }

        private Difficulty gamedifficult;

        public static int NumCoins
        {
            get { return numCoins; }
            set { numCoins = value; }
        }

        public Maze(Difficulty enemies)
        {
            content = new ContentManager(null, "Content");

            wall = content.Load<Texture2D>("wall");
            space = content.Load<Texture2D>("space");
            coin = content.Load<Texture2D>("s");
            gamedifficult = enemies;
            rand = new Random();

            LoadFromFile("Levels/0.txt");
        }

        private void LoadFromFile(string path)
        {
            int width;
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            Grid = new Tile[width + 2, lines.Count + 2];

            numX = width;
            numY = lines.Count;

            Tile.Width /= numX; //Set width of tile based,based on size of the canvas divaded by number of tiles.
            Tile.Height /= numY;
            Tile.Size = new Vector2(Tile.Width, Tile.Height);

            // Loop over every tile position,
            for (int y = 0; y <= numY + 1; ++y)
            {
                for (int x = 0; x <= numX + 1; ++x)
                {
                    try
                    {
                        char type = lines[y][x];
                        Grid[x, y] = Load(type, x, y);
                    }
                    catch (Exception e)
                    {
                        Grid[x, y] = Load('1', x, y);
                    }
                }
            }

            Player.CreatInstance(this.GetTile(13, 11), content);

            Enemies = RandomPositionOfEnemies();
        }

        public LevelDifficulty RandomPositionOfEnemies()
        {
            Enemies = new LevelDifficulty(gamedifficult);
            for (int y = 0; y < numY; ++y)
            {
                for (int x = 0; x < numX; ++x)
                {
                    TileCollision type = grid[x, y].tileType;
                    if (type == TileCollision.Passable)
                    {
                        bool check = AddEnemy(x, y, content);
                        if (check == false) { x = numX; y = numY; }
                    }
                }
            }
            return Enemies;
        }

        private int trashold = NumCoins;

        private bool AddEnemy(int x, int y, ContentManager content)
        {
            int r = rand.Next(0, 100);
            if (r > trashold)
            {
                if (Enemies.numberOfDumpEnemies != 0)
                {
                    Enemies.listOfEnemies.Add(new DumpEnemy(this.GetTile(x, y), content));
                    Enemies.numberOfDumpEnemies--;
                    trashold = 120;
                    return true;
                }

                if (Enemies.numberOfSmartEnemies != 0)
                {
                    Enemies.listOfEnemies.Add(new SmartEnemy(this.GetTile(x, y), content));
                    Enemies.numberOfSmartEnemies--;
                    trashold = 120;
                    return true;
                }
                return false;
            }
            trashold--;
            return true;
        }

        private Tile Load(char type, int x, int y)
        {
            Vector2 gridPosition = new Vector2(x, y);
            Vector2 position = gridPosition * Tile.Size;
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)Tile.Size.X, (int)Tile.Size.Y);

            if (type == '0')
            {
                TileCollision tileType = TileCollision.Passable;
                Tile tile = new Tile(space, tileType, bounds, gridPosition);
                tile.coin = new Coin(coin, tile.position * Tile.Size, Tile.Size);
                numCoins++;

                return tile;
            }
            else
            {
                TileCollision tileType = TileCollision.Impassable;
                return new Tile(wall, tileType, bounds, gridPosition);
            }
        }

        public void Draw(DrawEventArgs drawEventArgs, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= numY + 1; i++)
            {
                for (int j = 0; j <= numX + 1; j++)
                {
                    Tile currentTile = Grid[j, i];

                    spriteBatch.Draw(currentTile.texture, currentTile.bounds, Color.White);
                    if (currentTile.coin != null)
                    {
                        Grid[j, i].coin.Draw(drawEventArgs, spriteBatch);
                    }
                }
            }
        }
    }
}