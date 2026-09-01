using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;

public class Mage : Character // Mage class inherits from Character class, with Mage specific stats and abilities
{
    public Mage()
    {
        Vitality = -5;
        Intelligence = 20;
        Dexterity = 7;
        Strength = 3;
        Luck = 10;
        Defense = 2;
        CritDMG = 8;
        ResetResources();
        // Mage specific abilities can be added here // Maybe a Skilltree where you can choose between Fire, Ice or Lightning magic, each with their own unique abilities and effects
        // Fire = Damgage + DoT
        // Ice = Damage + Slow/Freeze
        // Lightning = Damage + Stun
    }
}