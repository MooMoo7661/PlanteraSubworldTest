using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace SubworldTesting
{
	public class SubworldSceneEffects : ModSceneEffect
	{
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/UndergroundJungle");

        public override bool IsSceneEffectActive(Player player)
        {
            if (SubworldSystem.Current == ModContent.GetInstance<PlanteraSubworld>())
            {
                return true;                
            }

            return false;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
}