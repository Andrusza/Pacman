using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Graphics;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Base;
using Silverlight3dApp.Pacman;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Silverlight3dApp
{
    public abstract class State
    {
        public Game game;

        public abstract void Update(DrawEventArgs drawEventArgs);

        public virtual void Draw(DrawEventArgs drawEventArgs)
        {
            GraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.White);
            this.game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            this.game.maze.Draw(drawEventArgs, this.game.spriteBatch);
            Player.GetInstance.Draw(this.game.spriteBatch);

            foreach (GhostBase x in this.game.difficulty.listOfEnemies)
            {
                x.Draw(this.game.spriteBatch);
            }

            this.game.spriteBatch.End();
        }
    }

    public class StartState : State
    {
        private SpriteFont font;
        private Vector2 position;
        private int time = 5;
        private DispatcherTimer timer;

        public override void Update(DrawEventArgs drawEventArgs)
        {
        }

        public override void Draw(DrawEventArgs drawEventArgs)
        {
            GraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.White);
            this.game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            this.game.maze.Draw(drawEventArgs, this.game.spriteBatch);
            Player.GetInstance.Draw(this.game.spriteBatch);

            foreach (GhostBase x in this.game.difficulty.listOfEnemies)
            {
                x.Draw(this.game.spriteBatch);
            }
            this.game.spriteBatch.DrawString(font, time.ToString(), position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            this.game.spriteBatch.End();
        }

        public StartState(Game game)
        {
            this.game = game;
            font = this.game.contentManager.Load<SpriteFont>("font");
            position = game.maze.GetSize();
            position = position / 2;
            position *= Tile.Size;
            Player.GetInstance.PlayerBorns(game.contentManager);

            ThreadPool.QueueUserWorkItem(o =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                    timer.Tick += new EventHandler(CountDown);
                    timer.Start();
                });
            });
        }

        private void CountDown(object o, EventArgs sender)
        {
            if (time == 0)
            {
                this.game.state = new PlayState(this.game);
                timer.Stop();
            }
            else
            {
                time--;
            }
        }
    }

    public class WinState : State
    {
        public override void Update(DrawEventArgs drawEventArgs)
        {
            int lol = 1;
        }

        public WinState(Game game)
        {
            this.game = game;
        }
    }

    

    public class PlayState : State
    {
        public override void Update(DrawEventArgs drawEventArgs)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Player.GetInstance.Update(keyboardState);
            foreach (GhostBase x in game.difficulty.listOfEnemies)
            {
                x.Update();
                if (Player.GetInstance.CheckGhostsCollision(x))
                {
                    this.game.state = new LoseState(this.game);
                }
            }
            if (Maze.NumCoins == 0) this.game.state = new WinState(this.game);
        }

        public PlayState(Game game)
        {
            this.game = game;
        }
    }

    public class LoseState : State
    {
        private DispatcherTimer timer;

        public override void Update(DrawEventArgs drawEventArgs)
        {
        }

        public LoseState(Game game)
        {
            this.game = game;
            Player.GetInstance.PlayerDies(this.game.contentManager);
            Player.GetInstance.Update();

            this.game.difficulty=this.game.maze.RandomPositionOfEnemies();

            ThreadPool.QueueUserWorkItem(o =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                    timer.Tick += new EventHandler(CountDown);
                    timer.Start();
                });
            });

        }

        private void CountDown(object o, EventArgs sender)
        {
            this.game.state = new StartState(this.game);
            timer.Stop();
        }
    }

    public class Game
    {
        public readonly SpriteBatch spriteBatch;
        public readonly ContentManager contentManager;
        public Maze maze;
        public LevelDifficulty difficulty;
        public State state;

        public Game(LevelDifficulty difficulty)
        {
            contentManager = new ContentManager(null, "Content");
            spriteBatch = new SpriteBatch(GraphicsDeviceManager.Current.GraphicsDevice);
            this.difficulty = difficulty;

            maze = new Maze(difficulty);
            state = new StartState(this);
        }
    }
}