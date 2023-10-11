 using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SubworldTesting
{
	public class SubworldPlayer : ModPlayer
	{
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            SubworldSystem.Enter<PlanteraSubworld>();
        }

        public override void UpdateEquips()
        {
            Main.NewText(NPC.downedPlantBoss);
        }
    }
}