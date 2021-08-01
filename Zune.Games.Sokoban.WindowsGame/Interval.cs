using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zune.Games.Sokoban.WindowsGame
{
    public class GameTimeEventArgs : EventArgs
    {
        public GameTime GameTime { get; set; }

    }
    class Interval
    {
        private TimeSpan startTime = TimeSpan.Zero;

        /// <summary>
        /// Events
        /// </summary>
        public event EventHandler<GameTimeEventArgs> Tick;

        /// <summary>
        /// Properties
        /// </summary>
        public TimeSpan Time { get; set; }
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="time"></param>
        public Interval(TimeSpan time)
        {
            this.Time = time;
        }

        /// <summary>
        /// Start the interval 
        /// </summary>
        /// <param name="time"></param>
        public void Start(GameTime gameTime)
        {
            // Set start time
            this.startTime = gameTime.TotalGameTime;

            // Enable the timer
            this.Enabled = true;
        }

        /// <summary>
        /// Updates the interval and calls the event listeners
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // 
            if (!this.Enabled)
                return;

            // Check for update
            if (gameTime.TotalGameTime.Ticks - this.startTime.Ticks > this.Time.Ticks)
            {
                // Reset the start time
                this.startTime = gameTime.TotalGameTime;

                // Handle event
                if (this.Tick != null)
                    this.Tick(this, new GameTimeEventArgs() { GameTime = gameTime });

            }
        }

    }
}
