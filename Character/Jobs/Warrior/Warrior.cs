using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;

public class Warrior : Character // Warrior class inherits from Character class, with Warrior specific stats and abilities
{
    public Warrior()
    {
        Vitality = 15;
        Intelligence = -5;
        Dexterity = 5;
        Strength = 10;
        Speed = 4;
        Luck = 2;
        Defense = 10;
        CritDMG = 2;
        ResetResources();
        // Warrior specific abilities can be added here // Maybe a Skilltree where you can choose between GreatSword, GreatAxe, each with their own unique abilities and effects
        // GreatSword = Damage + Stun + Selfhealth  and has Higher Defense Paramters = Percentage of Damage dealt is converted to Health, GreatSword has a higher percentage of Damage dealt converted to Health than GreatAxe
        // GreatAxe = Damage + Stun + Selfhealth = Percentage of Damage dealt is converted to Health, GreatSword has a higher percentage of Damage dealt converted to Health than GreatAxe
        // Selfhealth = Damage + Heal = Percentage of Damage dealt is converted to Health, GreatSword has a higher percentage of Damage dealt converted to Health than GreatAxe
        // Both Share similiar abilities, but have different effects and playstyles, GreatSword is more of a tanky playstyle, while GreatAxe is more of a damage dealer playstyle 
    }
}