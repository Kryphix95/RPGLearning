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
        Dexterity = 10;
        Strength = 7;
        Speed = 10;
        Luck = 10;
        Defense = 5;
        CritDMG = 10;

        // Rogue specific abilities can be added here 
        Abilities.Add(new DoubleSlash());
        Abilities.Add(new PoisonStrike());
        Abilities.Add(new RendingSlash());
        Abilities.Add(new Rupture());
        
        
        // Maybe a Skilltree where you can choose between 2 Dagger Weapons , each with their own unique abilities and effects

        // 2 Daggers = Damage + Bleed + Poison = High chance to apply Bleed and Poison effects on enemies, but lower damage output than 2 Swords + more Crit Chance
        ResetResources();
    }
}