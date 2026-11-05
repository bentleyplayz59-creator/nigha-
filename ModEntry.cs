using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;

namespace BentleyMakeover
{
    /// <summary>Adds the <c>bentley_makeover</c> console command, which gives the current farmer Bentley's new look.</summary>
    public class ModEntry : Mod
    {
        private const string CommandName = "bentley_makeover";

        private const int HairStyleId = 15;
        private static readonly Color HairColor = new Color(255, 220, 130);

        private const string ShirtId = "(S)1123"; // Shirt And Tie
        private const string PantsId = "(P)12";   // Relaxed Fit Pants
        private const string HatId = "(H)2";      // Top Hat

        public override void Entry(IModHelper helper)
        {
            helper.ConsoleCommands.Add(
                CommandName,
                "Gives the current farmer Bentley's new look: hairstyle 15, hair color RGB(255, 220, 130), Shirt And Tie, Relaxed Fit Pants and a Top Hat.\n\nUsage: bentley_makeover",
                this.OnCommand
            );
        }

        private void OnCommand(string command, string[] args)
        {
            try
            {
                this.ApplyMakeover();
            }
            catch (Exception ex)
            {
                this.Monitor.Log($"The makeover failed unexpectedly. Details:\n{ex}", LogLevel.Error);
            }
        }

        private void ApplyMakeover()
        {
            if (!Context.IsWorldReady)
            {
                this.Monitor.Log("You need to load a save first.", LogLevel.Error);
                return;
            }

            Farmer farmer = Game1.player;
            if (farmer == null)
            {
                this.Monitor.Log("Couldn't find the current farmer.", LogLevel.Error);
                return;
            }

            var missing = new List<string>();
            foreach (string id in new[] { ShirtId, PantsId, HatId })
            {
                if (!ItemRegistry.Exists(id))
                    missing.Add(id);
            }
            if (missing.Count > 0)
            {
                this.Monitor.Log($"These item IDs don't exist in this game version, so nothing was changed: {string.Join(", ", missing)}.", LogLevel.Error);
                return;
            }

            Clothing shirt = ItemRegistry.Create<Clothing>(ShirtId);
            Clothing pants = ItemRegistry.Create<Clothing>(PantsId);
            Hat hat = ItemRegistry.Create<Hat>(HatId);
            if (shirt == null || pants == null || hat == null)
            {
                this.Monitor.Log("Couldn't create one or more of the clothing items, so nothing was changed.", LogLevel.Error);
                return;
            }

            Item[] replaced = { farmer.shirtItem.Value, farmer.pantsItem.Value, farmer.hat.Value };
            foreach (Item old in replaced)
            {
                if (old != null)
                    Utility.CollectOrDrop(old);
            }

            farmer.hair.Value = HairStyleId;
            farmer.hairstyleColor.Value = HairColor;
            farmer.shirtItem.Value = shirt;
            farmer.pantsItem.Value = pants;
            farmer.hat.Value = hat;

            farmer.FarmerRenderer?.MarkSpriteDirty();

            this.Monitor.Log("Makeover complete: hairstyle 15, blond hair, Shirt And Tie, Relaxed Fit Pants and Top Hat. Any previously worn items were moved to your inventory (or dropped if it was full).", LogLevel.Info);
        }
    }
}
