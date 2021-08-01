using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zune_Sokoban
{
    interface IInputManager
    {
        event InputManagerHandler OnKeyPress;
        event InputManagerHandler OnKeyDown;
        event InputManagerHandler OnKeyRelease;
    }

    /// <summary>
    /// Delegate for this GameComponent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="keys"></param>
    public delegate void InputManagerHandler(InputManager sender, List<GameKeys> keys);

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class InputManager : GameComponent
    {
        /// <summary>
        /// Events
        /// </summary>
        public event InputManagerHandler OnKeyPress;
        public event InputManagerHandler OnKeyDown;
        public event InputManagerHandler OnKeyRelease;

        /// <summary>
        /// Private members 
        /// </summary>
        protected List<GameKeys> PreviousKeyMap;
        protected List<GameKeys> CurrentKeyMap;

        protected Dictionary<GamePadButtons, GameKeys> controllerDictionary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The main game component</param>
        public InputManager(Game game)
            : base(game)
        {
            // Init the lists
            this.PreviousKeyMap = new List<GameKeys>();
            this.CurrentKeyMap = new List<GameKeys>();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Create new lists to store currently pressed and released keys
            List<GameKeys> pressedKeys = new List<GameKeys>();
            List<GameKeys> releasedKeys = new List<GameKeys>();

            // Save the current key map in the previous map, so we can check it the next update
            this.PreviousKeyMap = this.CurrentKeyMap;
            this.CurrentKeyMap = new List<GameKeys>();

            // Retrieve the current keyboard key state
            GamePadState gs = GamePad.GetState(PlayerIndex.One);

            // Check for a connection
            if (gs.IsConnected)
            {
                if (gs.Buttons.Back == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Back);

                if (gs.Buttons.B == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Start);

                if (gs.DPad.Up == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Up);

                if (gs.DPad.Down == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Down);

                if (gs.DPad.Left == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Left);

                if (gs.DPad.Right == ButtonState.Pressed)
                    this.CurrentKeyMap.Add(GameKeys.Right);
            }

            // Add every key that wasn't previously pressed to the pressed key map
            foreach (GameKeys key in this.CurrentKeyMap)
            {
                if (!this.PreviousKeyMap.Contains(key))
                    pressedKeys.Add(key);
            }

            // Add every key that was previously pressed, but NOT currently to the released key map
            foreach (GameKeys key in this.PreviousKeyMap)
            {
                if (!this.CurrentKeyMap.Contains(key))
                    releasedKeys.Add(key);
            }

            // Invoke the OnKeyDown event
            if (this.CurrentKeyMap.Count > 0 && this.OnKeyDown != null)
                this.OnKeyDown(this, this.CurrentKeyMap);

            // Invoke the OnKeyPress event
            if (pressedKeys.Count > 0 && this.OnKeyPress != null)
                this.OnKeyPress(this, pressedKeys);

            // Invoke the OnKeyRelease event
            if (releasedKeys.Count > 0 && this.OnKeyRelease != null)
                this.OnKeyRelease(this, releasedKeys);

            // Call base
            base.Update(gameTime);
        }
    }

    /// <summary>
    /// Game key enumeration
    /// </summary>
    public enum GameKeys
    {
        Up, Down, Left, Right, Back, Start, Pad
    }
}