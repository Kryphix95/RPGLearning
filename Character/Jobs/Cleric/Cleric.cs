using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;
 public class Cleric : Character // Cleric class inherits from Character class, with Cleric specific stats and abilities
 {
    public Cleric()
    {
            Vitality = 10;
            Intelligence = 7;
            Dexterity = 3;
            Strength = 5;
            Speed = 4;
            Luck = 5;
            Defense = 8;
            CritDMG = 2;
            ResetResources();

            // Cleric specific abilities can be added here // Maybe a Skilltree where you can choose between Holy, Dark or Physical magic, each with their own unique abilities and effects
            Abilities.Add(new SmallRegeneration());
            Abilities.Add(new MinorHeal());
            Abilities.Add(new Haste());
            Abilities.Add(new MinorSmite());

        // Holy = Damage + Heal
        // Dark = Damage + Drain
        // Physical = Damage + Stun
        // Cleric can also have a passive ability that increases the healing done by the player and their allies, and a passive ability that increases the damage done by the player and their allies
        // Cleric has Abilities that can block or reduce damage taken by the player and their allies, and a passive ability that increases the defense of the player and their allies
    }
 }