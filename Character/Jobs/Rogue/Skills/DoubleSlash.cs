using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;

public class DoubleSlash : Ability
{
    public DoubleSlash()
    {
        Name = "Double Slash";
        ManaCost = 10;
        Cooldown = 1;
        Description = "You attack the Target twice with Normal Attacks. This has a 25% to Critcal Hit, per Hit";
    }
    public override void Execute(Character User, List<Character> Targets)
    {
        Character Target = Targets[0];
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();

            DamageResult result1 = User.NormalAttack(25);
            result1 = Target.DamageTaken(result1);
            Console.WriteLine($"Damage: {result1.FinalDamage} | Crit: {result1.IsCrit} | Block: {result1.IsBlocked} | Dodge: {result1.IsDodged}");

            if (!Target.IsDead)
            {
                DamageResult result2 = User.NormalAttack(25);
                result2 = Target.DamageTaken(result2);
                Console.WriteLine($"Damage: {result2.FinalDamage} | Crit: {result2.IsCrit} | Block: {result2.IsBlocked} | Dodge: {result2.IsDodged}");
            }
        }
        else
        {
            Console.WriteLine("You dont have enough Mana to use this...");
        }

    }
}