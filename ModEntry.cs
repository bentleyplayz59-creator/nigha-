using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace BentleyMakeover
{
    /// <summary>The mod entry point recognized by SMAPI.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.ConsoleCommands.Add(
                "bentley_makeover",
                "Applies the Bentley look to the current farmer (Hairstyle 15, blonde hair, suit, relaxed pants, and top hat).",
                this.OnBentleyMakeoverExecuted
            );
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised when the user runs the 'bentley_makeover' command in the SMAPI console.</summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="args">The arguments passed to the command.</param>
        private void OnBentleyMakeoverExecuted(string command, string[] args)
        {
            // 1. Verify game state and context
            if (!Context.IsWorldReady || Game1.player == null)
            {
                this.Monitor.Log("You must load a save file before running this command.", LogLevel.Error);
                return;
            }

            try
            {
                Farmer player = Game1.player;
                this.Monitor.Log($"Applying Bentley Makeover to {player.Name}...", LogLevel.Info);

                // 2. Apply Hairstyle and Hair Color
                player.hair.Value = 15;
                player.hairColor.Value = new Color(255, 220, 130);

                // 3. Equipping Clothes and Hat using Stardew Valley 1.6 unified APIs
                // Shirt: Shirt And Tie (S)1123
                if (!player.IsWearingItem("(S)1123"))
                {
                    Item shirt = ItemRegistry.Create("(S)1123");
                    player.shirtItem.Set(shirt as StardewValley.Objects.Clothing);
                }

                // Pants: Relaxed Fit Pants (P)12
                if (!player.IsWearingItem("(P)12"))
                {
                    Item pants = ItemRegistry.Create("(P)12");
                    player.pantsItem.Set(pants as StardewValley.Objects.Clothing);
                }

                // Hat: Top Hat (H)10
                if (player.hat.Value == null || player.hat.Value.ItemId != "10")
                {
                    Item hat = ItemRegistry.Create("(H)10");
                    player.hat.Value = hat as StardewValley.Objects.Hat;
                }

                // 4. Force a visual appearance refresh
                player.UpdateClothing();

                this.Monitor.Log("Bentley Makeover applied successfully!", LogLevel.Green);
            }
            catch (Exception ex)
            {
                this.Monitor.Log($"An error occurred while applying the makeover: {ex.Message}", LogLevel.Error);
                this.Monitor.Log(ex.ToString(), LogLevel.Trace);
            }
        }
    }
}