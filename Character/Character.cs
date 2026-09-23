using System;
using System.Collections.Generic;
using System.Linq;

namespace GameAscendNamespace;

public class Character
{
    public string Name { get; set; } = "Player"; // Default name value, Player can change it to whatever they want
    public int Level { get; set; } = 1; // Default level value, goes up by 1 for each time EXP value gets increased to the threshold
    public int MaxLevel { get; set; } = 99; // Maximum Level value the Characters can reach
    public int Experience { get; set; } = 0; // Current experience points, remaining experience is carried over after a level up
    public int MaxExperience => 100 + (Level * 100 + 25); // Experience threshold for the current level, starts at 225 at level 1 and increases by 100 per level
    public int GainExperience(int exp) // Adds experience, processes multiple level ups if enough EXP is gained, keeps remaining EXP and stops at MaxLevel
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
    public int BaseHealth { get; set; } = 100; // Default base health value, before applying Vitality multipliers 
    public int MaxHealth => BaseHealth + (Vitality * 5); // Default max health value, goes up by 5 per Point of Vitality
    public int Health { get; set; } // Default health value
    public int BaseMana { get; set; } = 100; // Default base mana value, before applying Intelligence multipliers
    public int MaxMana => BaseMana + (Intelligence * 5); // Default max mana value, goes up by 5 per Point of Intelligence
    public int Mana { get; set; }  // Default mana value
    public int Vitality { get; set; } = 0; // Default vitality value, Health + 5 per Point of Vitality
    public int Intelligence { get; set; } = 0; // Default intelligence value, Mana + 5 per Point of Intelligence
    public int Dexterity { get; set; } = 0; // Increases DodgeChance by 0.25% and PhysicalCritChance by 0.15% per point.
    public int Speed { get; set; } = 0; // Default speed value, determines turn order in combat. Can be increased by Buffs, Effects, and Abilities.
    public int Strength { get; set; } = 0;  // Contributes 1 point of PhysicalDamage per point of Strength
    public int Luck { get; set; } = 0; // Increases Physical and Magical CritChance by 0.5% per point. Additionally it should increase loot drop chance later on
    public int Defense { get; set; } = 0; // Increases DamageReduction by 0.1% and BlockChance by 0.25% per point
    

    public int Armor { get; set; } = 0;
    public double BlockChance => Defense * 0.25; // Default block chance value, Block chance + 0,25% per Point of Defense
    public double DamageReduction => Math.Min((Defense * 0.1 + Armor * 0.5), 75); // Default damage reduction value, Damage reduction + 1% per 10 Points of Defense and + 0.5% per Point of Armor 
    public double DodgeChance => Dexterity * 0.25; // Default dodge chance value, Dodge chance + 0,25% per Point of Dexterity
    public Dictionary<DamageType, double> Resistances { get; set; } = new Dictionary<DamageType, double>();
    public double GetResistance(DamageType damageType) // Get the resistance value for a specific damage type, returns 0 if the damage type is not found in the Resistances dictionary
    {
        if (Resistances.TryGetValue(damageType, out double resistance))
        {
            return resistance;
        }
        return 0;
    }


    public int WeaponDamage { get; set; } = 0;
    public int PhysicalDamage => Strength + WeaponDamage; // Default damage value, Strength + Weapon Damage
    public int MagicalDamage => Intelligence + WeaponDamage; // Default damage value, Intelligence + Weapon Damage
    
    public double BonusCritChance { get; set; } = 0; // Default Bonus Chance to Crits wich get Increased by Items Buffs etc 
    public double PhysicalCritChance => (Luck * 0.5 + Dexterity * 0.15) + BonusCritChance; // Default physical crit chance value, Crit chance + 0.5% per Point of Luck and + 0.15% per Point of Dexterity, + BonusCritChance
    public double MagicalCritChance => (Luck * 0.5) + BonusCritChance; // Default magical crit chance value, Crit chance + 0.5% per Point of Luck + BonusCritChance
    
    private bool IsCrit(double baseCritChance, double additionalCritChance = 0)
    {
        return Random.Shared.NextDouble() * 100
            < Math.Clamp(baseCritChance + additionalCritChance, 0, 100);
    }
    public int CritDMG { get; set; } = 0; // Default crit damage value, Crit damage + 1% per Point of CritDMG
    public double CritMultiplier => 1.5 + (CritDMG * 0.01); // Default crit multiplier value, Crit damage + 1% per Point of CritDMG
    
    
    public bool IsDead => Health <= 0; // Check if the character is dead, if Health is less than or equal to 0, then the character is dead
    private bool IsDodged => Random.Shared.NextDouble() * 100 < Math.Clamp(DodgeChance, 0, 100); // Check if the character dodged the attack, if Random number between 0 and 100 is less than DodgeChance, then the character dodged the attack
    private bool IsBlocked => Random.Shared.NextDouble() * 100 < Math.Clamp(BlockChance, 0, 100); // Check if the character blocked the attack, if Random number between 0 and 100 is less than BlockChance, then the character blocked the attack



    // Methods for Combat, including Normal Attack and Damage Taken, which calculate the damage dealt and taken by the character, taking into account the character's stats and abilities

    public DamageResult NormalAttack(double additionalCritChance = 0) // Normal Attack method, calculates the damage for a normal attack, takes into account the character's strength, weapon damage, and crit chance
    {
        bool isCrit = IsCrit(PhysicalCritChance, additionalCritChance);
        int rawDamage;

        if(isCrit)
        {
            rawDamage = (int)Math.Round(PhysicalDamage * CritMultiplier); // If the character got a Crit, then the damage is multiplied by the CritMultiplier

        }
        else
        {
            rawDamage = PhysicalDamage; // If the character didn't get a Crit, then the damage is equal to the PhysicalDamage
        }

        return new DamageResult(rawDamage, isCrit, DamageType.Physical);
    }
    public DamageResult MagicAttack(DamageType damageType, double damageMultiplier = 1.0, double additionalCritChance = 0)
    {
        bool isCrit = IsCrit(MagicalCritChance, additionalCritChance);
        double rawDamage = MagicalDamage * damageMultiplier;
        if(isCrit)
        {
            rawDamage *= CritMultiplier; // If the character got a Crit, then the damage is multiplied by the CritMultiplier
        }

        int finalRawDamage = Math.Max(1, (int)Math.Round(rawDamage));

        return new DamageResult(finalRawDamage, isCrit, damageType);
    }


    // Calculates incoming damage based on Dodge, Block, Physical mitigation
    // and the Resistance or Weakness of the incoming DamageType.
    public DamageResult DamageTaken(DamageResult result) 
    {
        int incomingDamage = result.RawDamage;

        if(IsDodged)
        {
            result.IsDodged = true;
            result.IsCrit = false;
            result.FinalDamage = 0;

            return result;
        }
        if(IsBlocked)
        {
            result.IsBlocked = true;
            incomingDamage /= 2;
        }

        double damageAfterReduction = incomingDamage;

        // Defense and Armor reduce only Physical damage
        if(result.DamageType == DamageType.Physical)
        {
            damageAfterReduction *= (1 - DamageReduction / 100);
        }

        // Active Status Effects can modify incoming damage
        foreach (StatusEffect effect in ActiveEffects)
        {
            damageAfterReduction = effect.ModifyIncomingDamage(damageAfterReduction, result.DamageType);
        }

        // Apply Resistance or Weakness for the incoming DamageType.
        // Negative values increase damage taken. Values are clamped between -100 and 100.
        double resistance = Math.Clamp(GetResistance(result.DamageType), -100, 100);
        damageAfterReduction *= (1 - resistance / 100);

        int finalDamage = Math.Max( 0, (int)Math.Round(damageAfterReduction));

        Health -= finalDamage;
        if(Health < 0)
        {
            Health = 0;
        }

        result.FinalDamage = finalDamage;
        result.TargetDied = IsDead;

        return result;
        
    }
    
   
    protected void ResetResources() // Reset Health and Mana to Max values, can be called when the character levels up or when the character rests
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }


    // Handles abilities, cooldowns and active status effects

    public List<Ability> Abilities { get; set; } = new List<Ability>(); // List of abilities the character has
    public void ProcessCooldowns() // Process the cooldowns of the character's abilities, calls the ReduceCooldown method of each ability
    {
        foreach(Ability ability in Abilities)
        {
            ability.ReduceCooldown();
        }
    }

    public List<StatusEffect> ActiveEffects { get; set; } = new List<StatusEffect>(); // List of status effects the character has
    public void ApplyStatusEffect(StatusEffect effect) // // Applies a new status effect or refreshes the duration of an existing effect of the same type
    {
        StatusEffect? existingEffect = ActiveEffects.FirstOrDefault(e => e.GetType() == effect.GetType()); // Check if the character already has the same type of status effect applied
        if(existingEffect != null)
        {
            existingEffect.Duration = Math.Max(existingEffect.Duration, effect.Duration); // If the character already has the same type of status effect applied, then the duration of the existing effect is set to the maximum of the existing effect's duration and the new effect's duration
            return;
        }
        ActiveEffects.Add(effect);
        effect.OnApply(this);
    }
    public void ProcessTurnStartEffects() // Process the start of the character's turn, calls the OnTurnStart method of each active status effect
    {
        foreach(StatusEffect effect in ActiveEffects.ToList()) // temporary list to avoid modification during iteration
        { 
            effect.OnTurnStart(this);
        }
    }
    public void ProcessTurnEndEffects(List<StatusEffect> effectsAtTurnStart) // Processes status effects that were already active at the start of the turn
    {
        foreach(StatusEffect effect in effectsAtTurnStart) // Process only effects that were active at the start of the turn
        {
            effect.OnTurnEnd(this);
            effect.Duration--;

            if (effect.Duration <= 0)
            {
                effect.OnRemove(this);
                ActiveEffects.Remove(effect);
            }
        }
    }
}