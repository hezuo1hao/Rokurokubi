using Microsoft.Xna.Framework;
using Rokurokubi.Projectiles;
using Terraria;
using Terraria.Chat;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Rokurokubi
{
	public class RokurokubiMP : ModPlayer
	{
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Rokurokubi.RokurokubiKeyBind.JustPressed)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<PlayerHead>()] > 0)
                {
                    foreach (Projectile proj in Main.ActiveProjectiles)
                        if (proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<PlayerHead>())
                        {
                            (proj.ModProjectile as PlayerHead).homing++;
                            proj.netUpdate = true;
                        }
                }
                else
                {
                    Vector2 pos = Player.Center - Vector2.UnitY * 12;
                    Projectile.NewProjectile(Player.GetSource_Misc("Rokurokubi"), pos, Vector2.Normalize(Main.MouseWorld - pos) * 3, ModContent.ProjectileType<PlayerHead>(), 0, 0, Player.whoAmI, ai2: -2);
                }
            }

            if (Rokurokubi.RidiculeKeyBind.JustPressed)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<PlayerHead>()] > 0)
                {
                    foreach (Projectile proj in Main.ActiveProjectiles)
                        if (proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<PlayerHead>())
                        {
                            Projectile.NewProjectile(Player.GetSource_Misc("Ridicule"), proj.Center, Vector2.Normalize(Main.MouseWorld - proj.Center) * 12, ModContent.ProjectileType<Saliva>(), 0, 0, Player.whoAmI, ai1: Main.rand.Next(10, 31) * 0.1f);
                        }
                }
                else
                {
                    Vector2 pos = Player.Center - Vector2.UnitY * 12;
                    Projectile.NewProjectile(Player.GetSource_Misc("Rokurokubi"), pos, Vector2.Normalize(Main.MouseWorld - pos) * 12, ModContent.ProjectileType<Saliva>(), 0, 0, Player.whoAmI, ai1: Main.rand.Next(10, 31) * 0.1f);
                }
            }
        }
    }
}
