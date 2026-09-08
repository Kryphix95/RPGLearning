using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;

class Program
{

    /* EQUIPMENT
        // Weapons
            // Weapon Types
                //Dagger
                // Sword
                // Greatsword
                // Greataxe
                // Staff
                // Hammer
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
    // EQUIPMENT SYSTEM
    // CONSUMABLE SYSTEM
    // SHOP SYSTEM


    static void Main()
        {
            List<Character> players = new List<Character>
        {
            new Rogue{Name = "Rogue" },
        };

            List<Enemy> enemies = new List<Enemy>
        {
            new Goblin(),
            new Slime(),
            new Goblin()
        };

        CombatSystem combat = new CombatSystem(players, enemies);
        combat.StartCombat();
        }
    }

