using Desiccation.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Desiccation.Items.Miner
{
    public class OverbrightTorch : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Light up whever your mouse goes.");
        }

        public override void SetDefaults()
        {
            item.holdStyle = 4;
            item.width = 40;
            item.height = 40;
            item.value = 10000;
            item.rare = 9;
            item.noWet = true;
        }

        public override void HoldStyle(Player player)
        {
            Vector2 position = GetLightPosition(player);
            if ((position.Y >= player.Center.Y) == (player.gravDir == 1))
            {
                player.itemLocation.X = player.Center.X + 6f * player.direction;
                player.itemLocation.Y = player.position.Y + 21f + 23f * player.gravDir + player.mount.PlayerOffsetHitbox;
            }
            else
            {
                player.itemLocation.X = player.Center.X;
                player.itemLocation.Y = player.position.Y + 21f - 3f * player.gravDir + player.mount.PlayerOffsetHitbox;
            }
            player.itemRotation = 0f;
        }

        public override bool HoldItemFrame(Player player)
        {
            Vector2 position = GetLightPosition(player);
            if ((position.Y >= player.Center.Y) == (player.gravDir == 1))
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
            }
            else
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 2;
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            Vector2 position = GetLightPosition(player) - new Vector2(20f, 20f);
            if (Main.rand.Next(10) == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, DustType<Flame>());
            }
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(position, 40, 40, DustType<Flame>());
            }
        }

        public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
        {
            dryTorch = true;
        }

        private Vector2 GetLightPosition(Player player)
        {
            Vector2 position = Main.screenPosition;
            position.X += Main.mouseX;
            position.Y += player.gravDir == 1 ? Main.mouseY : Main.screenHeight - Main.mouseY;
            return position;
        }
    }
}