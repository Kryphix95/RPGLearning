using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace GameAscendNamespace;



public class CombatSystem // Combat System class, handles the combat between the player and the enemy
{
    static List<Character> ChooseTargets(List<Character> validTargets, int targetCount)
    {
        List<Character> selectedTargets = new List<Character>();

        if (targetCount == 0)
        {
            return validTargets.ToList();
        }

        int targetsToChoose = Math.Min(targetCount, validTargets.Count);

        List<Character> availableTargets = validTargets.ToList();

        while (selectedTargets.Count < targetsToChoose)
        {
            Console.WriteLine("Choose your Target:");

            for (int i = 0; i < availableTargets.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. {availableTargets[i].Name} - HP: {availableTargets[i].Health}/{availableTargets[i].MaxHealth}");
            }

            string targetInput = Console.ReadLine();
            int targetNumber = int.Parse(targetInput);
            int targetIndex = targetNumber - 1;

            Character selectedTarget = availableTargets[targetIndex];

            selectedTargets.Add(selectedTarget);
            availableTargets.Remove(selectedTarget);
        }

        return selectedTargets;
    }

    static Character ChooseRandomTarget(List<Character> targets) // Choose Random Target method, handles the target selection for the enemy
    {
        int targetIndex = RandomNumberGenerator.GetInt32(0, targets.Count);
        return targets[targetIndex];
    }
    
    static List<Character> GetValidTargets(Character participant,TargetType targetType, List<Character> participants) // Get Valid Targets method, handles the target selection for the player and enemy
    {
            switch (targetType)
            {
                case TargetType.Any:
                    return participants.Where(t => !t.IsDead).ToList();

                case TargetType.Ally:
                    return participants.Where(t => !t.IsDead && (t is Enemy) == (participant is Enemy)).ToList();

                case TargetType.DeadAlly:
                    return participants.Where(t => t.IsDead &&(t is Enemy) == (participant is Enemy)).ToList();

                case TargetType.Self:
                    return new List<Character> { participant };

                case TargetType.Enemy:
                    return participants.Where(t => !t.IsDead &&(t is Enemy) != (participant is Enemy)).ToList();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        
    }
    
    static void PlayerTurn(Character player, List<Character> participants) // Player Turn method, handles the player's turn in combat
    {
        while (true)
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
                    List<Character> attackTargets = GetValidTargets(player, TargetType.Any, participants); // Get a list of valid targets for the player to attack

                    List<Character> targets = ChooseTargets(attackTargets, 1); // Choose targets from the list of valid targets
                    Character target = targets[0];

                    DamageResult result = player.NormalAttack();
                    result = target.DamageTaken(result);

                    if (result.IsCrit)
                    {
                        Console.WriteLine("Critical Hit!");
                    }

                    if (result.IsDodged)
                    {
                        Console.WriteLine(target.Name + " dodged the attack!");
                    }

                    if (result.IsBlocked)
                    {
                        Console.WriteLine(target.Name + " blocked the attack!");
                    }


                    Console.WriteLine(player.Name + " attacked " + target.Name + " for " + result.FinalDamage + " damage!");
                    return;


                case "2":

                    if (player.Abilities.Count == 0)
                    {
                        Console.WriteLine("You have no abilities to use!");
                        continue;
                    }

                    for (int i = 0; i < player.Abilities.Count; i++)
                    {
                        Console.WriteLine(
                            $"{i + 1}. {player.Abilities[i].Name} - Mana: {player.Abilities[i].ManaCost} - Cooldown: {player.Abilities[i].RemainingCooldown}"
                        );
                    }

                    string abilityInput = Console.ReadLine();
                    int abilityIndex = int.Parse(abilityInput) - 1;

                    Ability selectedAbility = player.Abilities[abilityIndex];
                    if (selectedAbility.RemainingCooldown > 0)
                    {
                        Console.WriteLine("That ability is on cooldown!");
                        continue;
                    }
                    if (player.Mana < selectedAbility.ManaCost)
                    {
                        Console.WriteLine("You do not have enough mana to use that ability!");
                        continue;
                    }

                    List<Character> abilityTargets = GetValidTargets(player, selectedAbility.TargetType, participants);

                    List<Character> selectedAbilityTargets = ChooseTargets(abilityTargets, selectedAbility.TargetCount);

                    selectedAbility.Execute(player, selectedAbilityTargets);

                    return;

                case "3":
                    // Item logic goes here
                    break;

                case "4":
                    // Flee logic goes here
                    break;
            }
        }
    }
    static void EnemyTurn(Enemy enemy, Character target) // Enemy Turn method, handles the enemy's turn in combat
    {
        DamageResult result = enemy.NormalAttack();
        result = target.DamageTaken(result);
        if (result.IsCrit)
        {
            Console.WriteLine("Critical Hit!");
        }

        if (result.IsDodged)
        {
            Console.WriteLine(target.Name + " dodged the attack!");
        }

        if (result.IsBlocked)
        {
            Console.WriteLine(target.Name + " blocked the attack!");
        }

        Console.WriteLine(enemy.Name + " attacked " + target.Name + " for " + result.FinalDamage + " damage!");
    }

    private List<Character> Participants = new List<Character>();
    private List<Character> Players;
    private List<Enemy> Enemies;

    public CombatSystem(List<Character> players, List <Enemy> enemies) // Constructor for the CombatSystem class, takes a player and a list of enemies as parameters
    {
        Players = players;
        Enemies = enemies;
        Participants.AddRange(players);
        Participants.AddRange(enemies);
    }
    public void StartCombat() // Starts the combat between the player and the enemy, and handles the combat logic
    {
        while (Players.Any(p => !p.IsDead) && Enemies.Any(e => !e.IsDead))
        {
            // Snapshot of the participants at the start of the round
            List<Character> roundParticipants = Participants.Where(p => !p.IsDead).ToList();

            // List to keep track of participants who have already acted in this round
            List<Character> actedParticipants = new List<Character>();

            while (true)
            {
                Character? participant = roundParticipants.Where(p => !p.IsDead && !actedParticipants.Contains(p)).OrderByDescending(p => p.Speed).FirstOrDefault(); // Get the next participant who has not acted yet and is not dead, ordered by speed

                // If there are no more participants who have not acted yet, then break out of the loop and start a new round
                if (participant == null)
                {
                    break; // All participants have acted, break out of the loop
                }

                // If either the player or the enemy is dead, then break out of the loop and end the combat
                if (!Players.Any(p => !p.IsDead) || !Enemies.Any(e => !e.IsDead))
                {
                    break;
                }

                actedParticipants.Add(participant); // Add the participant to the list of participants who have acted

                participant.ProcessCooldowns(); // Process the cooldowns of each character

                List<StatusEffect> effectsAtTurnStart = participant.ActiveEffects.ToList(); // Snapshot of all status effects that were already active at the start of this turn

                participant.ProcessTurnStartEffects(); // Process the effects that do something on turn start of each character

                // Checking if anything hinders the Character to play out their turn
                bool actionPrevented = participant.ActiveEffects.Any(effect => effect.PreventsAction); 
                if (!actionPrevented)
                {
                    if (Players.Contains(participant))
                    {
                        PlayerTurn(participant, Participants);
                    }
                    else if (participant is Enemy enemy)
                    {
                        List<Character> validTargets = Players.Where(p => !p.IsDead).ToList(); // Get a list of valid targets (not dead)
                        Character target = ChooseRandomTarget(validTargets);
                        EnemyTurn(enemy, target);

                    }
                }

                participant.ProcessTurnEndEffects(effectsAtTurnStart); // process the effects at the end of the turn of each character
            }
        }

    if (Enemies.All(e => e.IsDead)) // Check if all enemies are dead
        {
            Console.WriteLine("Victory! All enemies have been defeated!");
        }
        else if (Players.All(p => p.IsDead)) // Check if all players are dead
        {
            Console.WriteLine("Defeat! All players have been defeated!");
        }
    }
}