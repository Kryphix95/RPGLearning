using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;

class Program
{
    //CHECKLIST of features to implement in the game:

    /* EQUIPMENT
        // Weapons
            // Weapon Types
                //Dagger
                // Sword
                // Greatsword
                // Greataxe
                // Staff
                // Hammer
                // Bow
                // Gloves
                // Relic
        // Armor
            // Armor Types 
                // Cloth
                // Leather
                // Chainmail
                // Plate
                    // Helmet
                    // Chestplate
                    // Leggings
                    // Boots
                    // Shield
        // Accessories
            // Accessory Types
                // Ring
                // Necklace
                // Bracelet
                // Earring
    */
    // INVENTORY SYSTEM
    // CONSUMABLE SYSTEM
    // SHOP SYSTEM

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

