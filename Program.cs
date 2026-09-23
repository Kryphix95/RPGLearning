using System.Collections.Generic;

namespace GameAscendNamespace;

class Program
{
    static void Main()
    {
            List<Character> players = new List<Character>
        {
            new Rogue{Name = "Rogue" },
            new Cleric{Name = "Cleric" },
            new Mage{Name = "Mage" },
            new Warrior{Name = "Warrior" }

        };

        List<Enemy> enemies = new List<Enemy>
        {
            new Goblin{Name = "Goblin 1"},
            new Slime{Name = "Slime 1"},
            new Goblin{Name = "Goblin 2"},
            new Slime {Name = "Slime 2"},
            new Goblin{Name = "Goblin 3"},
        };

        CombatSystem combat = new CombatSystem(players, enemies);
        combat.StartCombat();
    }
}

