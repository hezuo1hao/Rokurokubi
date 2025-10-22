using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Rokurokubi.Projectiles;

public class Saliva : ModProjectile
{
    public override string Texture => "Rokurokubi/Projectiles/PlayerHead";
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WaterGun);
        Projectile.aiStyle = -1;
    }

    public override void AI()
    {
        if (Projectile.localAI[0] == 0)
        {
            SoundEngine.PlaySound(new SoundStyle("Rokurokubi/Sound/Mob.llama.spit1"), Projectile.Center);
            Projectile.localAI[0]++;
        }

        Projectile.scale -= 0.015f;
        if (Projectile.scale <= 0f)
        {
            Projectile.velocity *= 5f;
            Projectile.oldVelocity = Projectile.velocity;
            Projectile.Kill();
        }

        if (Projectile.owner == Main.myPlayer)
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (!npc.dontTakeDamage && npc.lifeMax > 1)
                {
                    if (Projectile.getRect().Intersects(npc.getRect()))
                    {
                        npc.AddBuff(BuffID.Wet, 300);
                        npc.AddBuff(BuffID.Stinky, 300);
                        Projectile.Kill();
                    }
                }
            }

            foreach (Player player in Main.ActivePlayers)
            {
                if (player != Main.player[Projectile.owner] && !player.dead)
                {
                    if (Projectile.getRect().Intersects(player.getRect()))
                    {
                        player.AddBuff(BuffID.Wet, 300);
                        player.AddBuff(BuffID.Stinky, 300);
                        Projectile.Kill();
                    }
                }
            }
        }

        Projectile.ai[0] += Projectile.ai[1];
        if (Projectile.ai[0] > 30f)
            Projectile.velocity.Y += 0.1f;

        int dustOffset = 6;
        for (int i = 0; i < 6; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * dustOffset, Projectile.width - dustOffset * 2, Projectile.height - dustOffset * 2, DustID.Wet, 0f, 0f, 75, default, 1.2f);
            
            for (int j = 0; j < 3; j++)
            if (Main.rand.NextBool(2))
                dust.alpha += 25;

            dust.noGravity = true;
            dust.velocity *= 0.3f;
            dust.velocity += Projectile.velocity * 0.5f;
            dust.position = Projectile.Center - Projectile.velocity / 6f * i;
            dust.velocity *= 0.2f;
        }

        if (Main.rand.NextBool(4))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * dustOffset, Projectile.width - dustOffset * 2, Projectile.height - dustOffset * 2, DustID.Wet, 0f, 0f, 75, default, 0.65f);
            dust.velocity *= 0.5f;
            dust.velocity += Projectile.velocity * 0.5f;
        }

        Color newColor2 = new(0, 80, 255, 100);
        for (int i = 0; i < 6; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * dustOffset, Projectile.width - dustOffset * 2, Projectile.height - dustOffset * 2, DustID.TintableDust, 0f, 0f, 175, newColor2, 1.2f);

            for (int j = 0; j < 3; j++)
                if (Main.rand.NextBool(2))
                    dust.alpha += 25;

            dust.noGravity = true;
            dust.velocity *= 0.3f;
            dust.velocity += Projectile.velocity * 0.5f;
            dust.position = Projectile.Center - Projectile.velocity / 6f * i;
            dust.velocity *= 0.2f;
        }

        if (Main.rand.NextBool(4))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * dustOffset, Projectile.width - dustOffset * 2, Projectile.height - dustOffset * 2, DustID.TintableDust, 0f, 0f, 175, newColor2, 1.2f);
            dust.velocity *= 0.5f;
            dust.velocity += Projectile.velocity * 0.5f;
        }
    }
    public override void OnKill(int timeLeft)
    {
        Projectile.velocity = Projectile.oldVelocity * 0.2f;
        for (int i = 0; i < 100; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.Wet, 0f, 0f, 75, default, 1.2f);

            for (int j = 0; j < 3; j++)
                if (Main.rand.NextBool(2))
                    dust.alpha += 25;

            if (Main.rand.NextBool(2))
                dust.scale = 0.6f;
            else
                dust.noGravity = true;

            dust.velocity *= 0.3f;
            dust.velocity += Projectile.velocity;
            dust.velocity *= 1f + Main.rand.Next(-100, 101) * 0.01f;
            dust.velocity.X += Main.rand.Next(-50, 51) * 0.015f;
            dust.velocity.Y += Main.rand.Next(-50, 51) * 0.015f;
            dust.position = Projectile.Center;
        }
    }
}