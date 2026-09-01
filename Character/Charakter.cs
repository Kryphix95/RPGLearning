using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using static GameAscendNamespace.Program;

namespace GameAscendNamespace;

public class Character
{
    public string Name { get; set; } = "Player"; // Default name value, Player can change it to whatever they want
    public int Level { get; set; } = 1; // Default level value, Max level is 99, goes up by 1 for each time EXP value gets increased
    public int MaxLevel { get; set; } = 99; // Default max level value, Max level is 99, goes up by 1 for each time EXP value gets increased
    public int Experience { get; set; } = 0; // Default experience value, goes up to 100, then Level + 1 and Experience resets to 0 and max Experience increases by 100 per level
    public int MaxExperience => 100 + (Level * 100 + 25); // Default max experience value, goes up by 100 per level, Max level is 99, so max Experience is 9900 at level 99
    public int GainExperience(int exp) // Gain experience and level up if max experience is reached, repeat this until max  Level is reached or until all experience is gained, then return the remaining experience
    {
        Experience += exp;
        while (Experience >= MaxExperience && Level < MaxLevel)
        {
            Experience -= MaxExperience;
            Level++;
            ResetResources();
        }
        if (Level == MaxLevel)
        {
            Experience = MaxExperience;
        }
        return Experience;
    }
    public int BaseHealth { get; set; } = 100; // Default base health value, goes up by 5 per Point of Vitality
    public int MaxHealth => BaseHealth + (Vitality * 5); // Default max health value, goes up by 5 per Point of Vitality
    public int Health { get; set; } // Default health value
    public int BaseMana { get; set; } = 100; // Default base mana value, goes up by 5 per Point of Intelligence
    public int MaxMana => BaseMana + (Intelligence * 5); // Default max mana value, goes up by 5 per Point of Intelligence
    public int Mana { get; set; }  // Default mana value
    public int Vitality { get; set; } = 0; // Default vitality value, Health + 5 per Point of Vitality
    public int Intelligence { get; set; } = 0; // Default intelligence value, Mana + 5 per Point of Intelligence
    public int Dexterity { get; set; } = 0;     // Default dexterity value, Chance to dodge + 0,25% per Point of Dexterity, + 0,5% Crit Chance per Point of Dexterity
    public int Strength { get; set; } = 0;  // Default strength value, Damage + 2 per Point of Strength
    public int Luck { get; set; } = 0; // Default luck value, Chance to find rare items + 0,5% per Point of Luck, + 0,25% Crit Chance per Point of Luck
    public int Defense { get; set; } = 0; // Default defense value, Damage reduction + 1 per Point of Defense, Chance to block + 0,25% per Point of Defense
    public string Weapon { get; set; } = ""; // Default weapon value, Player can change it via inventory or shop, Weapon can be a Sword, Axe, Bow, Staff, Dagger, etc.
    public int Armor { get; set; } = 0;
    public double BlockChance => Defense * 0.25; // Default block chance value, Block chance + 0,25% per Point of Defense
    public double DamageReduction => Math.Min((Defense * 0.1 + Armor * 0.5), 75); // Default damage reduction value, Damage reduction + 1% per 10 Points of Defense and + 0.5% per Point of Armor
    public double DodgeChance => Dexterity * 0.25; // Default dodge chance value, Dodge chance + 0,25% per Point of Dexterity
    public int WeaponDamage { get; set; } = 0;
    public int PhysicalDamage => Strength + WeaponDamage; // Default damage value, Strength + Weapon Damage
    public int MagicalDamage => Intelligence + WeaponDamage; // Default damage value, Intelligence + Weapon Damage
    public double BonusCritChance { get; set; } = 0; // Default Bonus Chance to Crits wich get Increased by Items Buffs etc 
    public double CritChance => (Dexterity * 0.5) + (Luck * 0.25) + BonusCritChance; // Default crit chance value, Crit chance + 0,5% per Point of Dexterity, + 0,25% per Point of Luck
    public int CritDMG { get; set; } = 0; // Default crit damage value, Crit damage + 1% per Point of CritDMG
    public double CritMultiplier => 1.5 + (CritDMG * 0.01); // Default crit multiplier value, Crit damage + 1% per Point of CritDMG
    public bool IsDead => Health <= 0; // Check if the character is dead, if Health is less than or equal to 0, then the character is dead
    private bool IsDodged => RandomNumberGenerator.GetInt32(0, 100) < DodgeChance; // Check if the character dodged the attack, if Random number between 0 and 100 is less than DodgeChance, then the character dodged the attack
    private bool IsBlocked => RandomNumberGenerator.GetInt32(0, 100) < BlockChance; // Check if the character blocked the attack, if Random number between 0 and 100 is less than BlockChance, then the character blocked the attack
    private bool IsCrit(double BonusCritChance = 0)
    { 
        return RandomNumberGenerator.GetInt32(0, 100) < CritChance + BonusCritChance;
    }// Check if the Character got a Crit or not 
    public int NormalAttack(double BonusCritChance = 0) // Calculate the Damage for a Normal Attack, if the character got a Crit, then the damage is multiplied by the CritMultiplier
    {
        int DealDamage;
        if (IsCrit(BonusCritChance))
        {
            DealDamage = (int)Math.Round(PhysicalDamage * CritMultiplier); // If the character got a Crit, then the damage is multiplied by the CritMultiplier
            return DealDamage;
        }
        DealDamage = PhysicalDamage; // If the character didn't get a Crit, then the damage is equal to the PhysicalDamage

        return DealDamage;

    } // Calculate the Damage for a Normal Attack
    public int DamageTaken(int incomingDamage) // Calculate damage taken after applying defense and damage reduction
    {
        if (IsDodged)
        {
            return Health; // If the character dodged the attack, then no damage is taken
        }

        if (IsBlocked)
        {
            incomingDamage = incomingDamage / 2; // If the character blocked the attack, then damage is reduced by 50%
        }
        double damageAfterReduction = incomingDamage * (1 - (DamageReduction / 100)); // Apply damage reduction based on defense and armor
        Health = Health - (int)Math.Round(damageAfterReduction);
        if (Health < 0)
        {
            Health = 0;
        }
        return Health;
    }
    protected void ResetResources() // Reset Health and Mana to Max values, can be called when the character levels up or when the character rests
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }
    public List<Ability> Abilities { get; set; } = new List<Ability>();

}