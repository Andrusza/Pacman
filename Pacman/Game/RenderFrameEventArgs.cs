using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Game
{
    public class RenderFrameEventArgs : EventArgs
    {
        public TimeSpan ElapsedTime { get; set; }
    }
}
