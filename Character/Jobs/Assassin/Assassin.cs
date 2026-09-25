using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;
public class Assassin : Character
{
    public Assassin()
    {

        Vitality = 10;
        Intelligence = 5;
        Dexterity = 20;
        Strength = 15;
        Speed = 25;
        Luck = 22;
        Defense = 5;
        CritDMG = 15;

        Abilities.Add(new Rupture());


        ResetResources();
    }
}