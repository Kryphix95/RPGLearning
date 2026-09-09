using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;
public class Rogue : Character // Rogue class inherits from Character class, with Rogue specific stats and abilities
{
    public Rogue()
    {
        Vitality = 0;
        Intelligence = 5;
        Dexterity = 15;
        Strength = 8;
        Luck = 7;
        Defense = 5;
        CritDMG = 10;

        // Rogue specific abilities can be added here 
        Abilities.Add(new DoubleSlash());
        Abilities.Add(new PoisonStrike());
        
        
        // Maybe a Skilltre9e where you can choose between 2 Daggers,2 Swords or Fist like Weapons , each with their own unique abilities and effects


        // 2 Daggers = Damage + Bleed + Poison = High chance to apply Bleed and Poison effects on enemies, but lower damage output than 2 Swords + more Crit Chance
        // 2 Swords = Damage + Stun + Bleed = High chance to apply Stun and Bleed effects on enemies, but lower Crit Chance than 2 Daggers + more Damage output // Highest Raw Damage output of the 3 Weapon types but slowest
        // Fist like Weapons = Damage + Stun + Slow = High chance to apply Stun and Slow effects on enemies, but lower Crit Chance than 2 Daggers + more Damage output and higher chance to Stun than 2 Swords
        ResetResources();
    }
}