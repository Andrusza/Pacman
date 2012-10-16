﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pacman.Game
{
   public class GameSurface
    {
        private DispatcherTimer _gameLoop;
        private DateTime _lastTick = DateTime.Now;

        #region Singleton Pattern

        // Static members are lazily initialized.
        // .NET guarantees thread safety for static initialization.
        private static readonly GameSurface _instance = new GameSurface();

        // Make the constructor private to hide it. 
        // This class adheres to the singleton pattern.
        private GameSurface()
        {
            this._gameLoop = new DispatcherTimer { Interval = TimeSpan.Zero };
            this._gameLoop.Tick += GameLoopTick;
        }

        /// <summary>
        /// Return the single instance of the GameSurface type.
        /// </summary>
        /// <returns>GameSurface</returns> 
        public static GameSurface GetInstance()
        {
            return _instance;
        }

        #endregion

        private void GameLoopTick(object sender, EventArgs e)
        {
            RenderFrameEventArgs eventArgs = new RenderFrameEventArgs 
                { ElapsedTime = DateTime.Now - this._lastTick };
            this._lastTick = DateTime.Now;
            if (this.RenderFrame != null)
            {
                this.RenderFrame(this, eventArgs);
            }
        }

        public void StartGame()
        {
            this._gameLoop.Start();
        }

        public void StopGame()
        {
            this._gameLoop.Stop();
        }

        public event EventHandler<RenderFrameEventArgs> RenderFrame;
    }
}
