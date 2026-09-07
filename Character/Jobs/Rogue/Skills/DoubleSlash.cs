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
    public override void Execute(Character User, Enemy Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;                
            int hit1 = User.NormalAttack(25);
            int actualdamage1 = Target.DamageTaken(hit1);
            Console.WriteLine($"Double Slash hits {Target.Name} for {actualdamage1} Damage.");
            System.Threading.Thread.Sleep(1000);
            if (!Target.IsDead)
            {
                int hit2 = User.NormalAttack(25);
                int actualdamage2 = Target.DamageTaken(hit2);
                Console.WriteLine($"Double Slash hits {Target.Name} for {actualdamage2} Damage.");
                System.Threading.Thread.Sleep(1000);
            }
        }
        else
        {
            Console.WriteLine("You dont have enough Mana to use this...");
        }

    }
}

