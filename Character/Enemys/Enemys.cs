using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;


public class Enemy : Character // Enemy class inherits from Character class, with Enemy specific stats and abilities
{
    public int ExperienceReward { get; set; } = 0; //  Experience awarded for defeating this enemy, defined individually by each enemy type
}

// ENEMY TYPES
public class Slime : Enemy
{
    public Slime()
    {
        Name = "Slime";
        BaseHealth = 10;
        Vitality = 3;
        Strength = 2;
        Dexterity = 1;
        Luck = 0;
        Defense = 1;
        CritDMG = 1;
        ExperienceReward = 10;
        ResetResources();
        // Slime Specific Abilities can be added here 
        // Loottable has to be added here, with a chance to drop items, gold and a chance to drop nothing at all
    }
}
public class Goblin : Enemy
{
    public Goblin()
    {
        Name = "Goblin";
        BaseHealth = 15;
        Vitality = 4;
        Strength = 3;
        Dexterity = 2;
        Luck = 1;
        Defense = 2;
        CritDMG = 2;
        ExperienceReward = 15;
        ResetResources();
        // Goblin Specific Abilities can be added here 
        // Loottable has to be added here, with a chance to drop items, gold and a chance to drop nothing at all
    }
}