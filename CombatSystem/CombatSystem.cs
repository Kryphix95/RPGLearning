using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;

public class CombatSystem // Combat System class, handles the combat between the player and the enemy
{
    static void PlayerTurn(Character player, Enemy enemy) // Player Turn method, handles the player's turn in combat
    {
        Console.WriteLine("It's " + player.Name + "'s turn!");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Attack");
        Console.WriteLine("2. Use Ability");
        Console.WriteLine("3. Use Item");
        Console.WriteLine("4. Flee");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                int damage = player.NormalAttack(player.PhysicalDamage);
                enemy.DamageTaken(damage);
                Console.WriteLine(player.Name + " attacked " + enemy.Name + " for " + damage + " damage!");
                break;

            case "2":
                // Ability logic goes here
                for (int i = 0; i < player.Abilities.Count; i++) // Sorts Abilities of the Charakter
                {
                    Console.WriteLine($"{i + 1}. {player.Abilities[i].Name} - Mana: {player.Abilities[i].ManaCost}"); // Shows Charakter Abilities wich are available
                }
                    string input2 = Console.ReadLine();
                    switch (input2) // input to choose between Abilities
                    {
                        case "1": player.Abilities[0].Execute(player, enemy); // Ability 1
                        break;
                    }
                break;

            case "3":
                // Item logic goes here
                break;

            case "4":
                // Flee logic goes here
                break;
        }
    }

    private List<Character> Participants = new List<Character>();
    private Character Player;
    private Enemy Enemy;
    private void SortParticipantsBySpeed() // Sorts the participants by their dexterity, so that the participant with the highest dexterity goes first
    {
        Participants = Participants.OrderByDescending(p => p.Dexterity).ToList();
    }
    public CombatSystem(Character player, Enemy enemy) // Constructor for the CombatSystem class, takes in a player and an enemy as parameters
    {
        Player = player;
        Enemy = enemy;
        Participants.Add(player);
        Participants.Add(enemy);
    }
    public void StartCombat() // Starts the combat between the player and the enemy, and handles the combat logic
    {
        while (!Player.IsDead && !Enemy.IsDead)
        {

            Console.WriteLine("Player Name: " + Player.Name + " | Player Level: " + Player.Level);
            Console.WriteLine("Player Health: " + Player.Health + "/" + Player.MaxHealth + " | Mana: " + Player.Mana + "/" + Player.MaxMana);
            Console.WriteLine();
            Console.WriteLine("Enemy Name: " + Enemy.Name + " | Enemy Level: " + Enemy.Level);
            Console.WriteLine("Enemy Health: " + Enemy.Health + "/" + Enemy.MaxHealth);
            Console.WriteLine();

            SortParticipantsBySpeed();

            foreach (Character participant in Participants) // Loop through the participants and handle their turns
            {
                if (participant.IsDead) // If the participant is dead, then skip their turn and continue to the next participant
                {
                    continue;
                }

                if (participant == Player) //   If the participant is the player, then call the PlayerTurn method and pass in the player and the enemy as parameters
                {
                    PlayerTurn(Player, Enemy);
                }
                else if (participant == Enemy) // If the participant is the enemy, then call the EnemyTurn method and pass in the enemy and the player as parameters
                {
                    int damage = Enemy.NormalAttack(Enemy.PhysicalDamage);
                    Player.DamageTaken(damage);
                    Console.WriteLine(Enemy.Name + " attacked " + Player.Name + " for " + damage + " damage!");
                    System.Threading.Thread.Sleep(1000); // Pause for 1 second
                    Console.Clear();
                }

                if (Enemy.IsDead || Player.IsDead) // If either the player or the enemy is dead, then break out of the loop and end the combat
                {
                    if (Enemy.IsDead)
                    {
                        Console.WriteLine("" + Player.Name + " has defeated " + Enemy.Name + " and gained " + Enemy.ExperienceReward + " experience!");
                        Player.GainExperience(Enemy.ExperienceReward);
                        break; // Break out of the loop if either the player or the enemy is dead
                    }
                    if (Player.IsDead)
                    {
                        Console.WriteLine("" + Player.Name + " has been defeated by " + Enemy.Name + "!");
                        Console.WriteLine("Game Over!");
                        Console.WriteLine("You return to the Village to rest and recover, but you have lost some of your experience and gold.");
                        break; // Break out of the loop if either the player or the enemy is dead
                    }
                }
            }
        }
        // Combat logic goes here
    }
}