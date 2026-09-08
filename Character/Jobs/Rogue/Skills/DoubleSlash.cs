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
        Description = "You attack the Target twice with Normal Attacks. This has a 25% to Critcal Hit, per Hit";
    }
    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;                
            DamageResult result1 = User.NormalAttack(25);
            result1 = Target.DamageTaken(result1);
            Console.WriteLine($"Double Slash hits {Target.Name} for {result1.FinalDamage} Damage.");
            System.Threading.Thread.Sleep(1000);
            if (!Target.IsDead)
            {
                DamageResult result2 = User.NormalAttack(25);
                result2 = Target.DamageTaken(result2);
                Console.WriteLine($"Double Slash hits {Target.Name} for {result2.FinalDamage} Damage.");
                System.Threading.Thread.Sleep(1000);
            }
        }
        else
        {
            Console.WriteLine("You dont have enough Mana to use this...");
        }

    }
}