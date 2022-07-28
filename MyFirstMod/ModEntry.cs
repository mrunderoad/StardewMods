using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using Microsoft.Xna.Framework.Input;

namespace MyFirstMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SButtonState state = this.Helper.Input.GetState(SButton.G);
            SButtonState state2 = this.Helper.Input.GetState(SButton.H);
            bool isDown = (state == SButtonState.Pressed || state == SButtonState.Held);
            bool HomeBtn = (state2 == SButtonState.Pressed || state2 == SButtonState.Held);

            //FOR CONTROLLER
            SButton button = Buttons.LeftTrigger.ToSButton();
            bool controller = this.Helper.Input.IsDown(button);


            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
            return;

            //Checks for button press or Controller press
            if (isDown == true)
            {
                this.Monitor.Log($"A random $2000 appeared in your bank!", LogLevel.Debug);
                Game1.player.Money -= 2000;
            }

            if (HomeBtn == true)
            {
                if (Game1.player.Money <= 499)
                {
                    this.Monitor.Log($"You dont have enough money to teleport right now. You need $500 and you only have {Game1.player.Money}.");
                }
                else
                {
                    this.Monitor.Log($"You teleported home!", LogLevel.Debug);
                    Game1.warpHome();
                    Game1.player.Money -= 500;
                }
            }

            if (controller == true)
            {
                Game1.player.Speed += 3;
            }
            // print button presses to the console window
            //this.Monitor.Log($"{Game1.player.Name} of {Game1.player.farmName} pressed {e.Button}.", LogLevel.Debug);

           
        }
    }
}
