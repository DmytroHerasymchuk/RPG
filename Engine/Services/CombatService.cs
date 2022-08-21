using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

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
            int playerDexterity = player.Dexterity * player.Dexterity;
            int opponentDexterity = opponent.Dexterity * opponent.Dexterity;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            decimal totalOffset = dexterityOffset + randomOffset;
            return RandomNumberGenerator.NumberBetween(0,100) <= 50 + totalOffset 
                                                              ? Combatant.Player 
                                                              : Combatant.Opponent;
        }

        public static bool AttackSucceded(LivingEntity attacker, LivingEntity target)
        {
            int playerDexterity = attacker.Dexterity * attacker.Dexterity;
            int opponentDexterity = target.Dexterity * target.Dexterity;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            decimal totalOffset = dexterityOffset + randomOffset;
            return RandomNumberGenerator.NumberBetween(0, 100) <= 50 + totalOffset;
        }
    }
}
