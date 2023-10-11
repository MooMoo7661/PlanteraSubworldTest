using SubworldLibrary;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;

namespace SubworldTesting
{
	public class SubworldTestingModSystem : ModSystem
	{
        public override void PreUpdateWorld()
        {
            if (SubworldSystem.IsActive<PlanteraSubworld>())
            {
                // Update mechanisms
                Wiring.UpdateMech();

                // Update tile entities
                TileEntity.UpdateStart();
                foreach (TileEntity te in TileEntity.ByID.Values)
                {
                    te.Update();
                }
                TileEntity.UpdateEnd();

                // Update liquid
                if (++Liquid.skipCount > 1)
                {
                    Liquid.UpdateLiquid();
                    Liquid.skipCount = 0;
                }
            }
        }
    }
}