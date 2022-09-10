using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Shared;
using Core;

namespace Engine.Services
{
    public static class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

        public static Combatant FirstAttacker(Player player, Monster opponent)
        {
            // Formula: ((Dex(player)^2 - Dex(monster)^2)/10) + Random(-10/10)
            int playerDexterity = player.GetAttribute("DEX").Value * player.GetAttribute("DEX").Value;
            int opponentDexterity = opponent.GetAttribute("DEX").Value * opponent.GetAttribute("DEX").Value;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            decimal totalOffset = dexterityOffset + randomOffset;
            return RandomNumberGenerator.NumberBetween(0,100) <= 50 + totalOffset 
                                                              ? Combatant.Player 
                                                              : Combatant.Opponent;
        }

        public static bool AttackSucceded(LivingEntity attacker, LivingEntity target)
        {
            int playerDexterity = attacker.GetAttribute("DEX").Value * attacker.GetAttribute("DEX").Value;
            int opponentDexterity = target.GetAttribute("DEX").Value * target.GetAttribute("DEX").Value;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            decimal totalOffset = dexterityOffset + randomOffset;
            return RandomNumberGenerator.NumberBetween(0, 100) <= 50 + totalOffset;
        }
    }
}
