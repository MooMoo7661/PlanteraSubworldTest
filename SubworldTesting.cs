using SubworldTesting.Tiles;
using System;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static SubworldTesting.Tiles.PlanteraAltar;

namespace SubworldTesting
{
	public class SubworldTesting : Mod
	{
        // Thank fucking god SpritMod was open source.
        // This code was "borrowed" from => https://github.com/GabeHasWon/SpiritMod/blob/367e1da73022ec8741673b4bfbc629c3798a04e4/SpiritMultiplayer.cs

        public static SubworldTesting Instance;

        public SubworldTesting()
        {
            Instance = this;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            var id = (PacketID)reader.ReadByte();
            byte player;
            switch(id)
            {
                case PacketID.SpawnPlantera:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        player = reader.ReadByte();
                        int bossType = reader.ReadInt32();
                        int spawnX = reader.ReadInt32();
                        int spawnY = reader.ReadInt32();

                        if (NPC.AnyNPCs(bossType))
                            return;

                        int npcID = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), spawnX, spawnY, bossType);
                        Main.npc[npcID].netUpdate2 = true;
                    }
                    break;
            }
        }

        public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
        {
            packet.Write(msg);

            for (int m = 0; m < param.Length; m++)
            {
                object obj = param[m];
                if (obj is bool) packet.Write((bool)obj);
                else if (obj is byte) packet.Write((byte)obj);
                else if (obj is int) packet.Write((int)obj);
                else if (obj is float) packet.Write((float)obj);
                else if (obj is double) packet.Write((double)obj);
                else if (obj is short) packet.Write((short)obj);
                else if (obj is ushort) packet.Write((ushort)obj);
                else if (obj is sbyte) packet.Write((sbyte)obj);
                else if (obj is uint) packet.Write((uint)obj);
                else if (obj is decimal) packet.Write((decimal)obj);
                else if (obj is long) packet.Write((long)obj);
                else if (obj is string) packet.Write((string)obj);
            }
            return packet;
        }   

        public static void SpawnBossFromClient(byte whoAmI, int type, int x, int y) => WriteToPacket(Instance.GetPacket(), (byte)PacketID.SpawnPlantera, whoAmI, type, x, y).Send();
    }
}