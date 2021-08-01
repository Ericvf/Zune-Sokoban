using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zune.Games.Sokoban.WindowsGame.Transitions
{
    class Tile
    {
        /// <summary>
        /// Properties
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Tile(int x, int y, int size)
        {
            // 
            this.X = x;
            this.Y = y;
            this.Size = size;
        }

        /// <summary>
        /// Draws the tile
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tile"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tile, int alpha)
        {
            // Calc positions
            int xPos = this.X * this.Size - this.Size;
            int yPos = this.Y * this.Size - this.Size;

            // Draw sprite
            spriteBatch.Draw(tile, new Rectangle(xPos, yPos, this.Size, this.Size), new Color(0, 0, 0, (byte)alpha));
        }
    }

    class TileStrip
    {

        /// <summary>
        /// Properties
        /// </summary>
        public TimeSpan TransitionStartTime { get; set; }
        public TimeSpan TransitionTime { get; set; }
        public float TransitionPosition { get; set; }
        public bool Enabled { get; set; }
        // 
        public Tile[] Tiles { get; set; }
        //

        /// <summary>
        /// Constructor
        /// </summary>
        public TileStrip(TimeSpan transitionTime)
        {
            // The time it takes for each strip to transition
            this.TransitionStartTime = TimeSpan.Zero;
            this.TransitionTime = transitionTime;
            this.TransitionPosition = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="direction"></param>
        public void Update(GameTime gameTime, int direction)
        {
            if (this.TransitionStartTime == TimeSpan.Zero)
                return;

            // How much should we move by?
            float transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / this.TransitionTime.TotalMilliseconds);

            // Update the transition position.
            TransitionPosition += transitionDelta * direction;

            if (TransitionPosition > 1 || TransitionPosition < 0)
                TransitionPosition = MathHelper.Clamp(TransitionPosition, 0, 1);

            if (TransitionPosition == 1 || TransitionPosition == 0)
                this.TransitionStartTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Draws the entire strip at once
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tile"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tileTexture)
        {
            int alpha = (int)(this.TransitionPosition * 255);

            // Draw every tile
            foreach (Tile tile in this.Tiles)
                tile.Draw(gameTime, spriteBatch, tileTexture, alpha);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Start(GameTime gameTime, int direction)
        {
            this.TransitionStartTime = gameTime.TotalGameTime;
        }
    }

    public class TileTransition : Transition
    {
        /// <summary>
        /// Properties
        /// </summary>
        public TimeSpan TransitionStripTime { get; set; }
        public TimeSpan TransitionTileTime { get; set; }
        public int TransitionDirection { get; set; }
        //
        public bool Enabled { get; protected set; }


        /// <summary>
        /// Events
        /// </summary>
        public event EventHandler<GameTimeEventArgs> TransitionStop;

        /// <summary>
        /// Private fields
        /// </summary>
        private TimeSpan TransitionStartTime;
        private TimeSpan TransitionEndTime;
        private TileStrip[] TileStrips;
        //
        private int tileStripPointer = 0;
        private Interval interval;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tileSize"></param>
        /// <param name="horizontalTileCount"></param>
        /// <param name="verticalTileCount"></param>
        /// <param name="tileStripInterval"></param>
        public TileTransition(int tileSize, int horizontalTilesCount, int verticalTilesCount, TimeSpan transitionStripTime, TimeSpan transitionTileTime)
        {
            // Store the transition times
            this.TransitionStripTime = transitionStripTime;
            this.TransitionTileTime = transitionTileTime;

            // Set the direction
            this.TransitionDirection = 1;

            // Create all the tiles for this transition effect
            this.CreateTileStrips(tileSize, horizontalTilesCount, verticalTilesCount);

            // Create an interval that incrementally updates the tileStrips
            this.CreateInterval(transitionStripTime);
        }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="tileSize"></param>
        /// <param name="horizontalTilesCount"></param>
        /// <param name="verticalTilesCount"></param>
        private void CreateTileStrips(int tileSize, int horizontalTilesCount, int verticalTilesCount)
        {
            // Temp local vars
            int cX = 1; // horizontal tile pointer
            int cY = 1; // vertical tile pointer
            int tX = 1; // horizontal step

            // Create the tilestrips array
            this.TileStrips = new TileStrip[horizontalTilesCount + verticalTilesCount - 1];
            int tileStripPointer = 0;

            // Create a loop that breaks when c greater than total amount of tiles to be created
            for (int c = 0; c < horizontalTilesCount * verticalTilesCount; /**/)
            {
                // Create a new tile strip and add it to the collection
                TileStrip currentTileStrip = this.TileStrips[tileStripPointer++] = new TileStrip(this.TransitionTileTime);

                // Create temp list of tiles
                List<Tile> tiles = new List<Tile>();

                // Create tile equal to amount of horizontal steps taken
                while (cX > 0)
                {
                    // Check if the tile fits within the bounderies of the grid
                    if (cX <= horizontalTilesCount && cY <= verticalTilesCount)
                    {
                        // Add the tile
                        tiles.Add(new Tile(cX, cY, tileSize));

                        // Increment the counter
                        c++;
                    }

                    // Next tile goes to the lower-left
                    cY++;
                    cX--;
                }

                // Increment the horizontal step and move tile there, top is reset
                cX = ++tX;
                cY = 1;

                // Add the current tiles to the strip
                currentTileStrip.Tiles = tiles.ToArray();
            }
        }

        /// <summary>
        /// Creates a new interval
        /// </summary>
        /// <param name="tileStripInterval"></param>
        private void CreateInterval(TimeSpan intervalTime)
        {
            // Create a new interval
            this.interval = new Interval(intervalTime);
            this.interval.Tick += new EventHandler<GameTimeEventArgs>(interval_Tick);
        }

        /// <summary>
        /// Updates the transition elements
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Check
            if (!this.Enabled)
                return;

            // Update all the strips
            foreach (TileStrip tileStrip in this.TileStrips)
                tileStrip.Update(gameTime, this.TransitionDirection);

            // Update the interval 
            this.interval.Update(gameTime);

            // Disable the transition if transition time had passed
            if (this.TransitionEndTime != TimeSpan.Zero && gameTime.TotalGameTime - this.TransitionEndTime > TimeSpan.Zero)
                this.Stop(gameTime);
        }


        /// <summary>
        /// Draws the transition
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tile"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tile)
        {
            // Check
            if (!this.Enabled)
                return;

            // Draw every strip
            foreach (TileStrip tileStrip in this.TileStrips)
                tileStrip.Draw(gameTime, spriteBatch, tile);
        }

        /// <summary>
        /// Interval tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void interval_Tick(object sender, GameTimeEventArgs e)
        {
            // Start the strip
            this.TileStrips[this.tileStripPointer++].Start(e.GameTime, this.TransitionDirection);

            // Check if we have started all the tilestrips
            if (this.tileStripPointer >= this.TileStrips.Length)
            {
                // Set the end time
                this.TransitionEndTime = e.GameTime.TotalGameTime + this.TransitionTileTime;

                // Reset the pointer
                this.tileStripPointer = 0;

                // Stop the interval
                this.interval.Enabled = false;
            }
        }

        /// <summary>
        /// Starts the transition
        /// </summary>
        /// <param name="gameTime"></param>
        public void Start(GameTime gameTime)
        {
            // Store the start time of the transition
            this.TransitionStartTime = gameTime.TotalGameTime;

            // Start the interval
            this.interval.Start(gameTime);

            // Enable the update and draw methods
            this.Enabled = true;
        }

        /// <summary>
        /// Stops the transition
        /// </summary>
        private void Stop(GameTime gameTime)
        {
            //
            this.TransitionEndTime = TimeSpan.Zero;

            // Check for event listeners
            if (this.TransitionStop != null)
                this.TransitionStop(this, new GameTimeEventArgs() { GameTime = gameTime });
        }
    }
}
