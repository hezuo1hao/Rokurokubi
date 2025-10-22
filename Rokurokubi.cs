using Microsoft.Xna.Framework;
using Rokurokubi.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rokurokubi
{
	public class Rokurokubi : Mod
	{
        public static int TransparentHead = -1;
        public static ModKeybind RokurokubiKeyBind { get; private set; }
        public static ModKeybind RidiculeKeyBind { get; private set; }
        public override void Load()
        {
            TransparentHead = EquipLoader.AddEquipTexture(this, "Rokurokubi/Projectiles/PlayerHead", EquipType.Head, name: "Rokurokubi.TransparentHead");
            RokurokubiKeyBind = KeybindLoader.RegisterKeybind(this, "RokurokubiKeyBind", "V");
            RidiculeKeyBind = KeybindLoader.RegisterKeybind(this, "RidiculeKeyBind", "F");
            On_Main.DrawPlayerChatBubbles += On_Main_DrawPlayerChatBubbles;
        }

        private void On_Main_DrawPlayerChatBubbles(On_Main.orig_DrawPlayerChatBubbles orig, Main self)
        {
            Vector2[] pos = new Vector2[256];
            foreach (Player player in Main.ActivePlayers)
            {
                pos[player.whoAmI] = player.position;

                if (player.ownedProjectileCounts[ModContent.ProjectileType<PlayerHead>()] == 0)
                    continue;

                foreach (Projectile proj in Main.ActiveProjectiles)
                    if (proj.owner == player.whoAmI && proj.type == ModContent.ProjectileType<PlayerHead>())
                        player.position = proj.position;
            }

            orig(self);

            for (int i = 0; i < 256; i++)
            {
                if (pos[i] != Vector2.Zero)
                    Main.player[i].position = pos[i];
            }
        }

        public override void PostSetupContent()
        {
            ArmorIDs.Head.Sets.DrawHead[TransparentHead] = false;
        }
    }
}
